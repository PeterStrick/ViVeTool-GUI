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

Imports System.Text
Imports System.Threading
Imports CommunityToolkit.Mvvm.ComponentModel
Imports CommunityToolkit.Mvvm.Input
Imports ViVeTool_GUI.Wpf.Models
Imports ViVeTool_GUI.Wpf.Services

Namespace ViewModels
    ''' <summary>
    ''' ViewModel for the Feature Scanner dialog.
    ''' </summary>
    Partial Public Class FeatureScannerViewModel
        Inherits ObservableObject
        Implements IDisposable

        Private ReadOnly _scannerService As FeatureScannerService
        Private _cancellationTokenSource As CancellationTokenSource
        Private _disposed As Boolean = False

        ' Properties
        Private _symbolPath As String = String.Empty
        Private _debuggerPath As String = String.Empty
        Private _buildNumber As String = String.Empty
        Private _currentState As ScannerState = ScannerState.Idle
        Private _progressPercentage As Integer = 0
        Private _statusMessage As String = "Ready to scan"
        Private _outputLog As String = String.Empty
        Private _errorMessage As String = String.Empty
        Private _outputFilePath As String = String.Empty
        Private _isScanning As Boolean = False

        ''' <summary>
        ''' Event raised when the scan completes successfully and the dialog should close.
        ''' </summary>
        Public Event ScanCompleted As EventHandler(Of ScannerResult)

        ''' <summary>
        ''' Event raised to request dialog close.
        ''' </summary>
        Public Event RequestClose As EventHandler

#Region "Properties"

        ''' <summary>
        ''' Gets or sets the symbol path where PDB files will be stored.
        ''' </summary>
        Public Property SymbolPath As String
            Get
                Return _symbolPath
            End Get
            Set(value As String)
                If SetProperty(_symbolPath, value) Then
                    RunScanCommand.NotifyCanExecuteChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the path to symchk.exe from Windows Debugging Tools.
        ''' </summary>
        Public Property DebuggerPath As String
            Get
                Return _debuggerPath
            End Get
            Set(value As String)
                If SetProperty(_debuggerPath, value) Then
                    RunScanCommand.NotifyCanExecuteChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the Windows build number.
        ''' </summary>
        Public Property BuildNumber As String
            Get
                Return _buildNumber
            End Get
            Set(value As String)
                If SetProperty(_buildNumber, value) Then
                    RunScanCommand.NotifyCanExecuteChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the current scanner state.
        ''' </summary>
        Public Property CurrentState As ScannerState
            Get
                Return _currentState
            End Get
            Set(value As ScannerState)
                If SetProperty(_currentState, value) Then
                    OnPropertyChanged(NameOf(IsIdle))
                    OnPropertyChanged(NameOf(IsCompleted))
                    OnPropertyChanged(NameOf(HasError))
                    OnPropertyChanged(NameOf(CanClose))
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the progress percentage (0-100).
        ''' </summary>
        Public Property ProgressPercentage As Integer
            Get
                Return _progressPercentage
            End Get
            Set(value As Integer)
                SetProperty(_progressPercentage, Math.Max(0, Math.Min(100, value)))
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the current status message.
        ''' </summary>
        Public Property StatusMessage As String
            Get
                Return _statusMessage
            End Get
            Set(value As String)
                SetProperty(_statusMessage, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the output log from the scanning process.
        ''' </summary>
        Public Property OutputLog As String
            Get
                Return _outputLog
            End Get
            Set(value As String)
                SetProperty(_outputLog, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the error message if scanning failed.
        ''' </summary>
        Public Property ErrorMessage As String
            Get
                Return _errorMessage
            End Get
            Set(value As String)
                SetProperty(_errorMessage, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the path to the generated feature list file.
        ''' </summary>
        Public Property OutputFilePath As String
            Get
                Return _outputFilePath
            End Get
            Set(value As String)
                SetProperty(_outputFilePath, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether a scan is in progress.
        ''' </summary>
        Public Property IsScanning As Boolean
            Get
                Return _isScanning
            End Get
            Set(value As Boolean)
                If SetProperty(_isScanning, value) Then
                    RunScanCommand.NotifyCanExecuteChanged()
                    StopScanCommand.NotifyCanExecuteChanged()
                    BrowseSymbolPathCommand.NotifyCanExecuteChanged()
                    BrowseDebuggerPathCommand.NotifyCanExecuteChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets whether the scanner is in idle state.
        ''' </summary>
        Public ReadOnly Property IsIdle As Boolean
            Get
                Return CurrentState = ScannerState.Idle
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the scan completed successfully.
        ''' </summary>
        Public ReadOnly Property IsCompleted As Boolean
            Get
                Return CurrentState = ScannerState.Completed
            End Get
        End Property

        ''' <summary>
        ''' Gets whether there is an error to display.
        ''' </summary>
        Public ReadOnly Property HasError As Boolean
            Get
                Return CurrentState = ScannerState.Failed AndAlso Not String.IsNullOrEmpty(ErrorMessage)
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the dialog can be closed.
        ''' </summary>
        Public ReadOnly Property CanClose As Boolean
            Get
                Return Not IsScanning
            End Get
        End Property

#End Region

#Region "Commands"

        ''' <summary>
        ''' Command to browse for symbol path.
        ''' </summary>
        Public ReadOnly Property BrowseSymbolPathCommand As IRelayCommand

        ''' <summary>
        ''' Command to browse for debugger path.
        ''' </summary>
        Public ReadOnly Property BrowseDebuggerPathCommand As IRelayCommand

        ''' <summary>
        ''' Command to run the scan.
        ''' </summary>
        Public ReadOnly Property RunScanCommand As IAsyncRelayCommand

        ''' <summary>
        ''' Command to stop the scan.
        ''' </summary>
        Public ReadOnly Property StopScanCommand As IRelayCommand

        ''' <summary>
        ''' Command to close the dialog.
        ''' </summary>
        Public ReadOnly Property CloseCommand As IRelayCommand

        ''' <summary>
        ''' Command to use the result (close and populate publish panel).
        ''' </summary>
        Public ReadOnly Property UseResultCommand As IRelayCommand

#End Region

        ''' <summary>
        ''' Creates a new instance of FeatureScannerViewModel.
        ''' </summary>
        Public Sub New()
            _scannerService = New FeatureScannerService()

            ' Initialize build number from system
            _buildNumber = FeatureScannerService.GetCurrentBuildNumber()

            ' Wire up service events
            AddHandler _scannerService.ProgressChanged, AddressOf OnProgressChanged
            AddHandler _scannerService.OutputReceived, AddressOf OnOutputReceived

            ' Initialize commands
            BrowseSymbolPathCommand = New RelayCommand(AddressOf ExecuteBrowseSymbolPath, AddressOf CanExecuteBrowse)
            BrowseDebuggerPathCommand = New RelayCommand(AddressOf ExecuteBrowseDebuggerPath, AddressOf CanExecuteBrowse)
            RunScanCommand = New AsyncRelayCommand(AddressOf ExecuteRunScanAsync, AddressOf CanExecuteRunScan)
            StopScanCommand = New RelayCommand(AddressOf ExecuteStopScan, AddressOf CanExecuteStopScan)
            CloseCommand = New RelayCommand(AddressOf ExecuteClose, AddressOf CanExecuteClose)
            UseResultCommand = New RelayCommand(AddressOf ExecuteUseResult, AddressOf CanExecuteUseResult)

            ' Auto-detect debugger path
            AutoDetectDebuggerPath()

            ' Initialize default symbol path to C:\Symbols if empty
            InitializeDefaultSymbolPath()
        End Sub

        ''' <summary>
        ''' Initializes the default symbol path to C:\Symbols if empty.
        ''' Attempts to create the folder safely without crashing on failure.
        ''' </summary>
        Private Sub InitializeDefaultSymbolPath()
            If String.IsNullOrWhiteSpace(SymbolPath) Then
                Const DefaultSymbolPath As String = "C:\Symbols"
                SymbolPath = DefaultSymbolPath

                ' Attempt to create the folder safely - no crash on failure
                Try
                    If Not System.IO.Directory.Exists(DefaultSymbolPath) Then
                        System.IO.Directory.CreateDirectory(DefaultSymbolPath)
                    End If
                Catch
                    ' Ignore errors when creating the default symbol path folder
                    ' The user can still browse to a different location
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Attempts to auto-detect symchk.exe in common Windows Kits Debuggers locations.
        ''' Sets DebuggerPath if found.
        ''' </summary>
        Private Sub AutoDetectDebuggerPath()
            Dim programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            Dim programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)

            ' Common paths to check for symchk.exe
            Dim pathsToCheck As New List(Of String)()

            ' Add Program Files paths (x64 and x86 debuggers)
            If Not String.IsNullOrEmpty(programFiles) Then
                pathsToCheck.Add(System.IO.Path.Combine(programFiles, "Windows Kits", "10", "Debuggers", "x64", "symchk.exe"))
                pathsToCheck.Add(System.IO.Path.Combine(programFiles, "Windows Kits", "10", "Debuggers", "x86", "symchk.exe"))
            End If

            ' Add Program Files (x86) paths (for 32-bit SDK installations)
            If Not String.IsNullOrEmpty(programFilesX86) AndAlso programFilesX86 <> programFiles Then
                pathsToCheck.Add(System.IO.Path.Combine(programFilesX86, "Windows Kits", "10", "Debuggers", "x64", "symchk.exe"))
                pathsToCheck.Add(System.IO.Path.Combine(programFilesX86, "Windows Kits", "10", "Debuggers", "x86", "symchk.exe"))
            End If

            ' Check each path and set DebuggerPath if found
            For Each path In pathsToCheck
                If System.IO.File.Exists(path) Then
                    DebuggerPath = path
                    Exit For
                End If
            Next
        End Sub

#Region "Command Implementations"

        Private Function CanExecuteBrowse() As Boolean
            Return Not IsScanning
        End Function

        Private Sub ExecuteBrowseSymbolPath()
            ' Use FolderBrowserDialog via Windows Forms interop
            Dim folderDialog As New System.Windows.Forms.FolderBrowserDialog() With {
                .Description = "Select a folder to store downloaded debug symbols (5-8GB space required)",
                .ShowNewFolderButton = True
            }

            If folderDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                SymbolPath = folderDialog.SelectedPath
            End If
        End Sub

        Private Sub ExecuteBrowseDebuggerPath()
            Dim dialog = New Microsoft.Win32.OpenFileDialog() With {
                .Title = "Select symchk.exe from Windows Debugging Tools",
                .Filter = "Symbol Checker|symchk.exe",
                .CheckFileExists = True
            }

            ' Try common installation paths
            If System.IO.Directory.Exists("C:\Program Files\Windows Kits\10\Debuggers\x64") Then
                dialog.InitialDirectory = "C:\Program Files\Windows Kits\10\Debuggers\x64"
            ElseIf System.IO.Directory.Exists("C:\Program Files (x86)\Windows Kits\10\Debuggers\x64") Then
                dialog.InitialDirectory = "C:\Program Files (x86)\Windows Kits\10\Debuggers\x64"
            End If

            If dialog.ShowDialog() = True Then
                DebuggerPath = dialog.FileName
            End If
        End Sub

        Private Function CanExecuteRunScan() As Boolean
            Return Not IsScanning AndAlso
                   Not String.IsNullOrWhiteSpace(SymbolPath) AndAlso
                   Not String.IsNullOrWhiteSpace(DebuggerPath) AndAlso
                   Not String.IsNullOrWhiteSpace(BuildNumber)
        End Function

        Private Async Function ExecuteRunScanAsync() As Task
            _cancellationTokenSource = New CancellationTokenSource()

            Try
                IsScanning = True
                CurrentState = ScannerState.Validating
                ErrorMessage = String.Empty
                OutputLog = String.Empty
                OutputFilePath = String.Empty
                ProgressPercentage = 0
                StatusMessage = "Starting scan..."

                AppendToLog("=== Feature Scanner Started ===")
                AppendToLog($"Build Number: {BuildNumber}")
                AppendToLog($"Symbol Path: {SymbolPath}")
                AppendToLog($"Debugger Path: {DebuggerPath}")
                AppendToLog("")

                Dim result = Await _scannerService.RunFullScanAsync(
                    DebuggerPath,
                    SymbolPath,
                    BuildNumber,
                    _cancellationTokenSource.Token)

                If result.Success Then
                    CurrentState = ScannerState.Completed
                    OutputFilePath = result.OutputFilePath
                    StatusMessage = "Scan completed successfully!"
                    AppendToLog("")
                    AppendToLog($"=== Scan Complete ===")
                    AppendToLog($"Output file: {OutputFilePath}")

                    ' Raise completion event
                    RaiseEvent ScanCompleted(Me, result)
                ElseIf result.IsCancelled Then
                    CurrentState = ScannerState.Cancelled
                    StatusMessage = "Scan was cancelled."
                    AppendToLog("")
                    AppendToLog("=== Scan Cancelled ===")
                Else
                    CurrentState = ScannerState.Failed
                    ErrorMessage = result.ErrorMessage
                    StatusMessage = "Scan failed."
                    AppendToLog("")
                    AppendToLog($"=== Scan Failed ===")
                    AppendToLog($"Error: {result.ErrorMessage}")
                End If

            Catch ex As OperationCanceledException
                CurrentState = ScannerState.Cancelled
                StatusMessage = "Scan was cancelled."
                AppendToLog("")
                AppendToLog("=== Scan Cancelled ===")
            Catch ex As Exception
                CurrentState = ScannerState.Failed
                ErrorMessage = ex.Message
                StatusMessage = "Scan failed with an exception."
                AppendToLog("")
                AppendToLog($"=== Exception ===")
                AppendToLog(ex.Message)
            Finally
                IsScanning = False
                _cancellationTokenSource?.Dispose()
                _cancellationTokenSource = Nothing
            End Try
        End Function

        Private Function CanExecuteStopScan() As Boolean
            Return IsScanning
        End Function

        Private Sub ExecuteStopScan()
            StatusMessage = "Stopping scan..."
            AppendToLog("")
            AppendToLog("Cancellation requested...")
            _cancellationTokenSource?.Cancel()
            _scannerService.RequestCancellation()
        End Sub

        Private Function CanExecuteClose() As Boolean
            Return Not IsScanning
        End Function

        Private Sub ExecuteClose()
            RaiseEvent RequestClose(Me, EventArgs.Empty)
        End Sub

        Private Function CanExecuteUseResult() As Boolean
            Return IsCompleted AndAlso Not String.IsNullOrEmpty(OutputFilePath)
        End Function

        Private Sub ExecuteUseResult()
            ' The result will be passed via ScanCompleted event
            RaiseEvent ScanCompleted(Me, ScannerResult.CreateSuccess(OutputFilePath))
            RaiseEvent RequestClose(Me, EventArgs.Empty)
        End Sub

#End Region

#Region "Event Handlers"

        Private Sub OnProgressChanged(sender As Object, e As ScanProgressEventArgs)
            ' Marshal to UI thread
            System.Windows.Application.Current?.Dispatcher?.Invoke(Sub()
                                                                        ProgressPercentage = e.ProgressPercentage
                                                                        StatusMessage = e.StatusMessage

                                                                        ' Update state based on progress
                                                                        If e.ProgressPercentage < 50 Then
                                                                            CurrentState = ScannerState.DownloadingSymbols
                                                                        ElseIf e.ProgressPercentage < 100 Then
                                                                            CurrentState = ScannerState.Scanning
                                                                        End If
                                                                    End Sub)
        End Sub

        Private Sub OnOutputReceived(sender As Object, e As String)
            ' Marshal to UI thread
            System.Windows.Application.Current?.Dispatcher?.Invoke(Sub()
                                                                        AppendToLog(e)
                                                                    End Sub)
        End Sub

        Private Sub AppendToLog(message As String)
            Dim timestamp = DateTime.Now.ToString("HH:mm:ss")
            OutputLog = OutputLog & $"[{timestamp}] {message}" & Environment.NewLine
        End Sub

#End Region

#Region "Public Methods"

        ''' <summary>
        ''' Validates the current configuration without running a scan.
        ''' </summary>
        Public Function ValidateConfiguration() As ScannerValidationResult
            Return _scannerService.ValidatePrerequisites(SymbolPath, DebuggerPath)
        End Function

        ''' <summary>
        ''' Resets the scanner to idle state.
        ''' </summary>
        Public Sub Reset()
            CurrentState = ScannerState.Idle
            ProgressPercentage = 0
            StatusMessage = "Ready to scan"
            ErrorMessage = String.Empty
            OutputLog = String.Empty
            OutputFilePath = String.Empty
        End Sub

#End Region

#Region "IDisposable"

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    RemoveHandler _scannerService.ProgressChanged, AddressOf OnProgressChanged
                    RemoveHandler _scannerService.OutputReceived, AddressOf OnOutputReceived
                    _cancellationTokenSource?.Dispose()
                    _scannerService?.Dispose()
                End If
                _disposed = True
            End If
        End Sub

#End Region

    End Class
End Namespace
