' ViVeTool-GUI - Windows Feature Control GUI for ViVeTool
' Copyright (C) 2022  Peter Strick / Peters Software Solutions
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <https://www.gnu.org/licenses/>.

Option Explicit On
Option Strict On

Imports System.Diagnostics
Imports System.IO
Imports System.Text
Imports System.Threading
Imports ViVeTool_GUI.Wpf.Models

Namespace Services
    ''' <summary>
    ''' Service for scanning Windows features using the mach2 tool.
    ''' This integrates the mach2-based scanning flow from the legacy Feature Scanner.
    ''' </summary>
    Public Class FeatureScannerService
        Implements IDisposable

        Private Const Mach2Executable As String = "mach2.exe"
        Private Const Msdia140Dll As String = "msdia140.dll"
        Private Const SymchkExecutable As String = "symchk.exe"

        ''' <summary>
        ''' File extensions to skip during scanning (noisy/non-symbol files).
        ''' These files do not contain symbol information and should be excluded
        ''' to improve scan performance.
        ''' </summary>
        Private Shared ReadOnly NoisyFileExtensions As HashSet(Of String) = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
            ".log", ".log1", ".log2", ".etl", ".dat", ".hve", ".tmp",
            ".txt", ".xml", ".json", ".ini", ".config", ".manifest",
            ".nls", ".mum", ".cat", ".msi", ".msp", ".cab", ".ttf", ".ttc",
            ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".ico",
            ".html", ".htm", ".css", ".js"
        }

        Private _currentProcess As Process
        Private _disposed As Boolean = False
        Private _cancellationRequested As Boolean = False

        ''' <summary>
        ''' Event raised when progress is reported during scanning.
        ''' </summary>
        Public Event ProgressChanged As EventHandler(Of ScanProgressEventArgs)

        ''' <summary>
        ''' Event raised when output is received from the scanning process.
        ''' </summary>
        Public Event OutputReceived As EventHandler(Of String)

        ''' <summary>
        ''' Gets the path to the mach2 executable.
        ''' </summary>
        Public ReadOnly Property Mach2Path As String
            Get
                Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Mach2Executable)
            End Get
        End Property

        ''' <summary>
        ''' Validates that all prerequisites are available for scanning.
        ''' </summary>
        ''' <param name="symbolPath">The path where symbols will be stored.</param>
        ''' <param name="debuggerPath">The path to symchk.exe from Windows Debugging Tools.</param>
        ''' <returns>A validation result with success status and error message if failed.</returns>
        Public Function ValidatePrerequisites(symbolPath As String, debuggerPath As String) As ScannerValidationResult
            ' Check mach2.exe exists
            If Not File.Exists(Mach2Path) Then
                Return New ScannerValidationResult(False, $"mach2.exe not found at {Mach2Path}. Please ensure the scanner backend is included in the application.")
            End If

            ' Check msdia140.dll exists
            Dim msdiaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Msdia140Dll)
            If Not File.Exists(msdiaPath) Then
                Return New ScannerValidationResult(False, $"msdia140.dll not found at {msdiaPath}. This DLL is required for mach2 to function.")
            End If

            ' Validate debugger path
            If String.IsNullOrWhiteSpace(debuggerPath) Then
                Return New ScannerValidationResult(False, "Debugger path (symchk.exe) is required.")
            End If

            If Not debuggerPath.EndsWith(SymchkExecutable, StringComparison.OrdinalIgnoreCase) Then
                Return New ScannerValidationResult(False, "The debugger path must point to symchk.exe from the Windows Debugging Tools.")
            End If

            If Not File.Exists(debuggerPath) Then
                Return New ScannerValidationResult(False, $"symchk.exe not found at {debuggerPath}. Please install Windows SDK or Debugging Tools.")
            End If

            ' Validate symbol path
            If String.IsNullOrWhiteSpace(symbolPath) Then
                Return New ScannerValidationResult(False, "Symbol path is required to store downloaded PDB files.")
            End If

            ' Check if symbol path exists or can be created
            Try
                If Not Directory.Exists(symbolPath) Then
                    Directory.CreateDirectory(symbolPath)
                End If

                ' Test write access
                Dim testFile = Path.Combine(symbolPath, "test_write.tmp")
                File.WriteAllText(testFile, "test")
                File.Delete(testFile)
            Catch ex As Exception
                Return New ScannerValidationResult(False, $"Cannot write to symbol path: {ex.Message}")
            End Try

            Return New ScannerValidationResult(True, String.Empty)
        End Function

        ''' <summary>
        ''' Downloads PDB files using symchk.exe.
        ''' Default fast scanning scans only C:\Windows and skips noisy file types.
        ''' </summary>
        ''' <param name="debuggerPath">Path to symchk.exe.</param>
        ''' <param name="symbolPath">Path to store downloaded symbols.</param>
        ''' <param name="cancellationToken">Cancellation token to stop the operation.</param>
        ''' <returns>True if successful, false otherwise.</returns>
        Public Async Function DownloadSymbolsAsync(debuggerPath As String, symbolPath As String, cancellationToken As CancellationToken) As Task(Of ScannerResult)
            _cancellationRequested = False

            ' Default fast scanning: Only scan C:\Windows (skip Program Files directories for performance)
            Dim systemRoot = Environment.GetFolderPath(Environment.SpecialFolder.Windows)

            ' Build list of directories to scan (only Windows root for fast scanning)
            Dim directories = New List(Of String)()
            If Not String.IsNullOrEmpty(systemRoot) Then directories.Add(systemRoot)

            If directories.Count = 0 Then
                Return ScannerResult.CreateFailure("Unable to determine Windows directory path.")
            End If

            Dim totalDirectories = directories.Count
            Dim currentIndex = 0

            For Each directory In directories
                If cancellationToken.IsCancellationRequested OrElse _cancellationRequested Then
                    Return ScannerResult.CreateCancelled()
                End If

                currentIndex += 1
                Dim progress = CInt((currentIndex / CDbl(totalDirectories)) * 50) ' First 50% is download
                RaiseEvent ProgressChanged(Me, New ScanProgressEventArgs(progress, $"Downloading symbols from {directory}..."))

                ' Create a temporary file list with filtered files (skip noisy extensions)
                Dim filteredFilesPath = Path.Combine(Path.GetTempPath(), $"symchk_files_{Guid.NewGuid():N}.txt")
                Try
                    Await WriteFilteredFileListAsync(directory, filteredFilesPath, cancellationToken)

                    If cancellationToken.IsCancellationRequested OrElse _cancellationRequested Then
                        Return ScannerResult.CreateCancelled()
                    End If

                    ' Check if the filtered file list has any files
                    If Not File.Exists(filteredFilesPath) OrElse New FileInfo(filteredFilesPath).Length = 0 Then
                        RaiseEvent OutputReceived(Me, $"No scannable files found in {directory} after filtering noisy extensions.")
                        Continue For
                    End If

                    ' Build the command arguments using Microsoft symbol server with file list
                    ' The /if flag reads files from the specified input file
                    Dim arguments = $"/if ""{filteredFilesPath}"" /s srv*""{symbolPath}""*https://msdl.microsoft.com/download/symbols /oc ""{symbolPath}"" /cn"

                    ' Log the exact symchk command being executed (verbose output like WinForms version)
                    RaiseEvent OutputReceived(Me, $"Executing: {debuggerPath} {arguments}")

                    Dim result = Await RunProcessAsync(debuggerPath, arguments, AppDomain.CurrentDomain.BaseDirectory, cancellationToken)
                    If Not result.Success AndAlso Not cancellationToken.IsCancellationRequested Then
                        RaiseEvent OutputReceived(Me, $"Warning: Error downloading symbols from {directory}: {result.ErrorMessage}")
                    End If
                Catch ex As OperationCanceledException
                    Return ScannerResult.CreateCancelled()
                Catch ex As Exception
                    RaiseEvent OutputReceived(Me, $"Warning: Exception downloading symbols from {directory}: {ex.Message}")
                Finally
                    ' Clean up the temporary file list
                    Try
                        If File.Exists(filteredFilesPath) Then
                            File.Delete(filteredFilesPath)
                        End If
                    Catch
                        ' Ignore cleanup errors
                    End Try
                End Try
            Next

            Return ScannerResult.CreateSuccess(String.Empty)
        End Function

        ''' <summary>
        ''' Writes a filtered list of files to scan, excluding noisy file extensions.
        ''' </summary>
        ''' <param name="directory">The directory to enumerate files from.</param>
        ''' <param name="outputPath">The path to write the filtered file list.</param>
        ''' <param name="cancellationToken">Cancellation token.</param>
        Private Async Function WriteFilteredFileListAsync(directory As String, outputPath As String, cancellationToken As CancellationToken) As Task
            Await Task.Run(
                Sub()
                    Using writer As New StreamWriter(outputPath, False, Encoding.UTF8)
                        Try
                            ' Enumerate files recursively and filter by extension
                            For Each filePath In EnumerateFilesRecursive(directory)
                                If cancellationToken.IsCancellationRequested OrElse _cancellationRequested Then
                                    Exit For
                                End If

                                Dim extension = Path.GetExtension(filePath)
                                If Not NoisyFileExtensions.Contains(extension) Then
                                    writer.WriteLine(filePath)
                                End If
                            Next
                        Catch ex As Exception
                            ' Log enumeration errors but don't fail the entire operation
                            Debug.WriteLine($"Error enumerating files in {directory}: {ex.Message}")
                        End Try
                    End Using
                End Sub, cancellationToken)
        End Function

        ''' <summary>
        ''' Recursively enumerates files in a directory, handling access denied errors gracefully.
        ''' </summary>
        ''' <param name="directory">The directory to enumerate.</param>
        ''' <returns>An enumerable of file paths.</returns>
        Private Iterator Function EnumerateFilesRecursive(directory As String) As IEnumerable(Of String)
            ' First, yield files in the current directory
            Dim files As IEnumerable(Of String) = Nothing
            Try
                files = IO.Directory.EnumerateFiles(directory)
            Catch ex As UnauthorizedAccessException
                ' Skip directories we can't access
            Catch ex As DirectoryNotFoundException
                ' Skip directories that don't exist
            Catch ex As IOException
                ' Skip directories with I/O errors
            End Try

            If files IsNot Nothing Then
                For Each file In files
                    Yield file
                Next
            End If

            ' Then recursively enumerate subdirectories
            Dim subdirectories As IEnumerable(Of String) = Nothing
            Try
                subdirectories = IO.Directory.EnumerateDirectories(directory)
            Catch ex As UnauthorizedAccessException
                ' Skip directories we can't access
            Catch ex As DirectoryNotFoundException
                ' Skip directories that don't exist
            Catch ex As IOException
                ' Skip directories with I/O errors
            End Try

            If subdirectories IsNot Nothing Then
                For Each subdir In subdirectories
                    For Each file In EnumerateFilesRecursive(subdir)
                        Yield file
                    Next
                Next
            End If
        End Function

        ''' <summary>
        ''' Scans PDB files using mach2.exe to extract feature information.
        ''' </summary>
        ''' <param name="symbolPath">Path containing the PDB files to scan.</param>
        ''' <param name="buildNumber">The Windows build number for the output file name.</param>
        ''' <param name="cancellationToken">Cancellation token to stop the operation.</param>
        ''' <returns>A scanner result with the path to the generated feature list.</returns>
        Public Async Function ScanSymbolsAsync(symbolPath As String, buildNumber As String, cancellationToken As Threading.CancellationToken) As Task(Of ScannerResult)
            _cancellationRequested = False

            Dim outputFile = Path.Combine(symbolPath, $"{buildNumber}.txt")
            Dim arguments = $"scan ""{symbolPath}"" -i ""{symbolPath}"" -o ""{outputFile}"" -u -s"

            RaiseEvent ProgressChanged(Me, New ScanProgressEventArgs(60, "Scanning symbol files with mach2..."))
            RaiseEvent OutputReceived(Me, $"Running: mach2.exe {arguments}")

            Dim retryCount = 0
            Const MaxRetries = 3
            ' Exit codes that may indicate transient failures and are worth retrying
            ' mach2 exit code 1 typically indicates a recoverable error
            Dim retryableExitCodes = New Integer() {1}

            While retryCount < MaxRetries
                If cancellationToken.IsCancellationRequested OrElse _cancellationRequested Then
                    Return ScannerResult.CreateCancelled()
                End If

                Try
                    Dim result = Await RunMach2Async(arguments, cancellationToken)

                    If result.Success Then
                        RaiseEvent ProgressChanged(Me, New ScanProgressEventArgs(100, "Scan complete!"))

                        If File.Exists(outputFile) Then
                            Return ScannerResult.CreateSuccess(outputFile)
                        Else
                            Return ScannerResult.CreateFailure($"Scan completed but output file not found: {outputFile}")
                        End If
                    ElseIf retryableExitCodes.Contains(result.ExitCode) Then
                        ' Retry only for known transient exit codes
                        retryCount += 1
                        RaiseEvent OutputReceived(Me, $"mach2 returned exit code {result.ExitCode}. Retry {retryCount}/{MaxRetries}...")
                        If retryCount < MaxRetries Then
                            Await Task.Delay(1000, cancellationToken)
                        End If
                    Else
                        ' Non-retryable exit code - return failure immediately
                        Return ScannerResult.CreateFailure($"mach2 failed with exit code {result.ExitCode}. {result.ErrorMessage}")
                    End If
                Catch ex As OperationCanceledException
                    Return ScannerResult.CreateCancelled()
                Catch ex As Exception
                    Return ScannerResult.CreateFailure($"Exception during scan: {ex.Message}")
                End Try
            End While

            Return ScannerResult.CreateFailure($"Scan failed after {MaxRetries} attempts.")
        End Function

        ''' <summary>
        ''' Runs the complete scan process: validate, download symbols, and scan.
        ''' </summary>
        ''' <param name="debuggerPath">Path to symchk.exe.</param>
        ''' <param name="symbolPath">Path to store symbols and output.</param>
        ''' <param name="buildNumber">The Windows build number.</param>
        ''' <param name="cancellationToken">Cancellation token.</param>
        ''' <returns>A scanner result with the output file path on success.</returns>
        Public Async Function RunFullScanAsync(debuggerPath As String, symbolPath As String, buildNumber As String, cancellationToken As Threading.CancellationToken) As Task(Of ScannerResult)
            ' Validate prerequisites
            Dim validation = ValidatePrerequisites(symbolPath, debuggerPath)
            If Not validation.IsValid Then
                Return ScannerResult.CreateFailure(validation.ErrorMessage)
            End If

            RaiseEvent ProgressChanged(Me, New ScanProgressEventArgs(5, "Prerequisites validated. Starting symbol download..."))

            ' Download symbols
            Dim downloadResult = Await DownloadSymbolsAsync(debuggerPath, symbolPath, cancellationToken)
            If Not downloadResult.Success Then
                Return downloadResult
            End If

            RaiseEvent ProgressChanged(Me, New ScanProgressEventArgs(55, "Symbol download complete. Starting scan..."))

            ' Scan symbols
            Return Await ScanSymbolsAsync(symbolPath, buildNumber, cancellationToken)
        End Function

        ''' <summary>
        ''' Runs mach2.exe with the specified arguments.
        ''' Note: mach2 requires UseShellExecute=True and cannot redirect output.
        ''' </summary>
        Private Async Function RunMach2Async(arguments As String, cancellationToken As Threading.CancellationToken) As Task(Of ScannerResult)
            Return Await Task.Run(Function()
                                      Try
                                          Using proc As New Process()
                                              proc.StartInfo = New ProcessStartInfo() With {
                                                .FileName = Mach2Path,
                                                .Arguments = arguments,
                                                .WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                                                .UseShellExecute = True,
                                                .CreateNoWindow = False,
                                                .WindowStyle = ProcessWindowStyle.Minimized,
                                                .RedirectStandardError = False,
                                                .RedirectStandardOutput = False
                                            }

                                              _currentProcess = proc
                                              proc.Start()

                                              ' Wait for exit or cancellation
                                              While Not proc.HasExited
                                                  If cancellationToken.IsCancellationRequested OrElse _cancellationRequested Then
                                                      Try
                                                          proc.Kill()
                                                      Catch
                                                          ' Ignore kill errors
                                                      End Try
                                                      Return ScannerResult.CreateCancelled()
                                                  End If
                                                  Threading.Thread.Sleep(100)
                                              End While

                                              _currentProcess = Nothing

                                              If proc.ExitCode = 0 Then
                                                  Return ScannerResult.CreateSuccess(String.Empty)
                                              Else
                                                  Return New ScannerResult() With {
                                                    .Success = False,
                                                    .ExitCode = proc.ExitCode,
                                                    .ErrorMessage = $"mach2 exited with code {proc.ExitCode}"
                                                }
                                              End If
                                          End Using
                                      Catch ex As Exception
                                          Return ScannerResult.CreateFailure(ex.Message)
                                      End Try
                                  End Function, cancellationToken)
        End Function

        ''' <summary>
        ''' Runs a process and captures its output.
        ''' </summary>
        Private Async Function RunProcessAsync(fileName As String, arguments As String, workingDirectory As String, cancellationToken As Threading.CancellationToken) As Task(Of ScannerResult)
            Return Await Task.Run(Function()
                                      Try
                                          Using proc As New Process()
                                              Dim outputBuilder As New StringBuilder()
                                              Dim errorBuilder As New StringBuilder()

                                              proc.StartInfo = New ProcessStartInfo() With {
                                                .FileName = fileName,
                                                .Arguments = arguments,
                                                .WorkingDirectory = workingDirectory,
                                                .UseShellExecute = False,
                                                .CreateNoWindow = True,
                                                .RedirectStandardOutput = True,
                                                .RedirectStandardError = True
                                            }

                                              AddHandler proc.OutputDataReceived, Sub(s, e)
                                                                                       If e.Data IsNot Nothing Then
                                                                                           outputBuilder.AppendLine(e.Data)
                                                                                           RaiseEvent OutputReceived(Me, e.Data)
                                                                                       End If
                                                                                   End Sub

                                              AddHandler proc.ErrorDataReceived, Sub(s, e)
                                                                                      If e.Data IsNot Nothing Then
                                                                                          errorBuilder.AppendLine(e.Data)
                                                                                          RaiseEvent OutputReceived(Me, $"[Error] {e.Data}")
                                                                                      End If
                                                                                  End Sub

                                              _currentProcess = proc
                                              proc.Start()
                                              proc.BeginOutputReadLine()
                                              proc.BeginErrorReadLine()

                                              ' Wait for exit or cancellation
                                              While Not proc.HasExited
                                                  If cancellationToken.IsCancellationRequested OrElse _cancellationRequested Then
                                                      Try
                                                          proc.Kill()
                                                      Catch
                                                          ' Ignore kill errors
                                                      End Try
                                                      Return ScannerResult.CreateCancelled()
                                                  End If
                                                  Threading.Thread.Sleep(100)
                                              End While

                                              proc.WaitForExit()
                                              _currentProcess = Nothing

                                              If proc.ExitCode = 0 Then
                                                  Return ScannerResult.CreateSuccess(outputBuilder.ToString())
                                              Else
                                                  Dim errorMsg = If(errorBuilder.Length > 0, errorBuilder.ToString(), $"Process exited with code {proc.ExitCode}")
                                                  Return New ScannerResult() With {
                                                    .Success = False,
                                                    .ExitCode = proc.ExitCode,
                                                    .ErrorMessage = errorMsg,
                                                    .Output = outputBuilder.ToString()
                                                }
                                              End If
                                          End Using
                                      Catch ex As Exception
                                          Return ScannerResult.CreateFailure(ex.Message)
                                      End Try
                                  End Function, cancellationToken)
        End Function

        ''' <summary>
        ''' Requests cancellation of the current scanning operation.
        ''' </summary>
        Public Sub RequestCancellation()
            _cancellationRequested = True
            If _currentProcess IsNot Nothing AndAlso Not _currentProcess.HasExited Then
                Try
                    _currentProcess.Kill()
                Catch
                    ' Ignore errors when killing process
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Gets the current Windows build number from the registry.
        ''' </summary>
        Public Shared Function GetCurrentBuildNumber() As String
            Try
                Dim buildNumber = Microsoft.Win32.Registry.GetValue(
                    "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion",
                    "CurrentBuildNumber",
                    Nothing)
                Return If(buildNumber?.ToString(), "Unknown")
            Catch
                Return "Unknown"
            End Try
        End Function

        ''' <summary>
        ''' Disposes resources used by the service.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Disposes resources.
        ''' </summary>
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    RequestCancellation()
                    _currentProcess?.Dispose()
                End If
                _disposed = True
            End If
        End Sub
    End Class
End Namespace
