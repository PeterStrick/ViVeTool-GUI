'ViVeTool-GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2022  Peter Strick / Peters Software Solutions
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.
'
'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
Option Strict On

''' <summary>
''' ViVeTool GUI - Feature Scanner
''' </summary>
Public Class ScannerUI
    Private WithEvents Proc As Process
    Private Delegate Sub AppendStdOutDelegate(text As String)
    Private Delegate Sub AppendStdErrDelegate(text As String)
    Public BuildNumber As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber", Nothing).ToString

    ''' <summary>
    ''' File extensions to skip during scanning (noisy/non-symbol files).
    ''' These files do not contain symbol information and should be excluded
    ''' to improve scan performance.
    ''' </summary>
    Private Shared ReadOnly NoisyFileExtensions As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
        ".log", ".log1", ".log2", ".etl", ".dat", ".hve", ".tmp",
        ".txt", ".xml", ".json", ".ini", ".config", ".manifest",
        ".nls", ".mum", ".cat", ".msi", ".msp", ".cab", ".ttf", ".ttc",
        ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".ico",
        ".html", ".htm", ".css", ".js"
    }

    ' Shared ToolTip for controls
    Private ReadOnly _toolTip As New ToolTip With {
        .AutoPopDelay = 15000,
        .InitialDelay = 300,
        .ReshowDelay = 100,
        .ShowAlways = True
    }

    ''' <summary>
    ''' Debugging Tools/symchk.exe Path Browse Button
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_DbgPath_Browse_Click(sender As Object, e As EventArgs) Handles RB_DbgPath_Browse.Click
        Dim OFD As New OpenFileDialog With {
            .InitialDirectory = "C:\",
            .Title = "Path to symchk.exe from the Windows Debugging Tools",
            .Filter = "Symbol Checker|symchk.exe"
        }

        If OFD.ShowDialog() = DialogResult.OK Then
            RTB_DbgPath.Text = OFD.FileName
        End If
    End Sub

    ''' <summary>
    ''' Symbol Path Browse Button
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_SymbolPath_Browse_Click(sender As Object, e As EventArgs) Handles RB_SymbolPath_Browse.Click
        Dim FBD As New FolderBrowserDialog With {
            .ShowNewFolderButton = True,
            .Description = "Select a Folder to store the downloaded Debug Symbols into. The downloaded .pdb Files usually take up to 5~8GB of Space."
        }

        If FBD.ShowDialog() = DialogResult.OK Then
            RTB_SymbolPath.Text = FBD.SelectedPath
        End If
    End Sub

    ''' <summary>
    ''' Continue Button. Checks if the Requirements are met by calling CheckPreReq()
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_Continue_Click(sender As Object, e As EventArgs) Handles RB_Continue.Click
        Dim BT As New Threading.Thread(AddressOf CheckPreReq) With {
            .IsBackground = True
        }
        BT.SetApartmentState(Threading.ApartmentState.STA)
        BT.Start()
    End Sub

    ''' <summary>
    ''' Checks if the Requirements are met by checking if the path in RTB_DbgPath is valid and that the path in RTB_SymbolPath is writable
    ''' </summary>
    Private Sub CheckPreReq()
        'First disable the Buttons
        Invoke(Sub()
                   RPBE_StatusProgressBar.Value = 10
                   RB_Continue.Enabled = False
                   RB_DbgPath_Browse.Enabled = False
                   RB_SymbolPath_Browse.Enabled = False
               End Sub)

#Region "1. Check RTB_DbgPath"
        'Check if the Path to symchk.exe is correct and if symchk.exe exists
        If RTB_DbgPath.Text.EndsWith("\symchk.exe") AndAlso IO.File.Exists(RTB_DbgPath.Text) Then
            Invoke(Sub() RPBE_StatusProgressBar.Value = 50)
        Else
            DialogHelper.ShowErrorDialog(" An Error occurred", "An Error occurred while checking if the specified Path to symchk.exe is valid." & vbNewLine & vbNewLine & "Please be sure to enter a valid path to symchk.exe." & vbNewLine & "If you can not find symchk.exe, it is usually located at the Installation Directory of the Windows SDK\10\Debuggers\x64")

            Invoke(Sub()
                       RPBE_StatusProgressBar.Value = 0
                       RTB_DbgPath.Text = Nothing
                       RB_Continue.Enabled = True
                       RB_DbgPath_Browse.Enabled = True
                       RB_SymbolPath_Browse.Enabled = True
                   End Sub)
        End If
#End Region

#Region "2. Check RTB_SymbolPath"
        'Check if the Application has Write Access to the specified symbol path
        Invoke(Sub() RPBE_StatusProgressBar.Value = 80)

        'If the Path in RTB_SymbolPath exists, and try to write a Test File to it
        If IO.Directory.Exists(RTB_SymbolPath.Text) Then
            Try
                Dim WT = IO.File.CreateText(RTB_SymbolPath.Text & "\Test.txt")
                WT.WriteLine("Test File")
                WT.Close()

                'Check if the Test File contains "Test File". If it does contain "Test File" then delete it and continue.
                If IO.File.ReadAllText(RTB_SymbolPath.Text & "\Test.txt").Contains("Test File") Then
                    IO.File.Delete(RTB_SymbolPath.Text & "\Test.txt")
                Else
                    DialogHelper.ShowErrorDialog(" An Error occurred", "An Error occurred while trying to write a test file to " & RTB_SymbolPath.Text & vbNewLine & vbNewLine & "Please make sure that the application has write access to the folder, and that the folder isn't write protected.")

                    Invoke(Sub()
                               RPBE_StatusProgressBar.Value = 0
                               RTB_SymbolPath.Text = Nothing
                               RB_Continue.Enabled = True
                               RB_DbgPath_Browse.Enabled = True
                               RB_SymbolPath_Browse.Enabled = True
                           End Sub)
                End If
            Catch ex As Exception
                DialogHelper.ShowExceptionDialog(" An Exception occurred", "An Exception occurred", ex.Message, "An Exception occurred while trying to write a test file to " & RTB_SymbolPath.Text & vbNewLine & vbNewLine & "Please make sure that the application has write access to the folder, and that the folder isn't write protected.")

                Invoke(Sub()
                           RPBE_StatusProgressBar.Value = 0
                           RTB_SymbolPath.Text = Nothing
                           RB_Continue.Enabled = True
                           RB_DbgPath_Browse.Enabled = True
                           RB_SymbolPath_Browse.Enabled = True
                       End Sub)
            End Try
        Else
            DialogHelper.ShowErrorDialog(" An Error occurred", "An Error occurred while trying to write a test file to the symbol folder." & vbNewLine & vbNewLine & "A symbol folder must be specified to download Program Debug Database files into.")

            Invoke(Sub()
                       RPBE_StatusProgressBar.Value = 0
                       RTB_SymbolPath.Text = Nothing
                       RB_Continue.Enabled = True
                       RB_DbgPath_Browse.Enabled = True
                       RB_SymbolPath_Browse.Enabled = True
                   End Sub)
        End If
#End Region

        'Now if both Text Boxes aren't empty, enable the Download PDB Tab
        If RTB_SymbolPath.Text = Nothing OrElse RTB_DbgPath.Text = Nothing Then
            Invoke(Sub() RPBE_StatusProgressBar.Value = 0)
        Else
            'Disable the current Tab and move to the Download PDB Tab
            Invoke(Sub()
                       RPBE_StatusProgressBar.Value = 100
                       TabPage_DownloadPDB.Enabled = True
                       TabControl_Main.SelectedTab = TabPage_DownloadPDB
                       TabPage_Setup.Enabled = False
                   End Sub)

            'Save the Paths to My.Settings
            My.Settings.DebuggerPath = RTB_DbgPath.Text
            My.Settings.SymbolPath = RTB_SymbolPath.Text
            My.Settings.Save()

            'Start the PDB Download automatically
            DownloadPDBFiles()
        End If
    End Sub

    ''' <summary>
    ''' Downloads all the .pdb files of C:\Windows\*.* to the path specified in My.Settings.SymbolPath.
    ''' Default fast scanning: only scans C:\Windows and skips noisy file types for better performance.
    ''' </summary>
    Private Sub DownloadPDBFiles()
        'Set up the File System Watcher
        FSW_SymbolPath.SynchronizingObject = Me
        FSW_SymbolPath.Path = My.Settings.SymbolPath

        'Create a Process with Process StartInfo
        Proc = New Process
        With Proc.StartInfo
            .FileName = My.Settings.DebuggerPath 'Path to symchk.exe
            .UseShellExecute = False 'Required for Output/Error Redirection to work
            .CreateNoWindow = True 'Required for Output/Error Redirection to work
            .RedirectStandardError = True 'Enables Redirection of Error Output
            .RedirectStandardOutput = True 'Enables Redirection of Standard Output
        End With

        ' Default fast scanning: Only scan C:\Windows (skip Program Files directories for performance)
        Dim windowsDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows)
        If String.IsNullOrEmpty(windowsDir) Then
            windowsDir = "C:\Windows"
        End If

        ' Create a temporary file list with filtered files (skip noisy extensions)
        Dim filteredFilesPath = IO.Path.Combine(IO.Path.GetTempPath(), "symchk_files_" & Guid.NewGuid().ToString("N") & ".txt")

        Try
            ' Write filtered file list
            WriteFilteredFileList(windowsDir, filteredFilesPath)

            ' Check if filtered file list has any files
            If IO.File.Exists(filteredFilesPath) AndAlso New IO.FileInfo(filteredFilesPath).Length > 0 Then
                ' Use /if flag to read files from the specified input file
                ' Uses Microsoft symbol server with cache + /oc to output to symbol path
                Proc.StartInfo.Arguments = "/if """ & filteredFilesPath & """ /s srv*""" & My.Settings.SymbolPath & """*https://msdl.microsoft.com/download/symbols /oc """ & My.Settings.SymbolPath & """ /cn"
                Proc.Start()
                Proc.BeginErrorReadLine()
                Proc.BeginOutputReadLine()
                Proc.WaitForExit()
                Proc.CancelOutputRead()
                Proc.CancelErrorRead()
            Else
                AppendStdOut("No scannable files found in " & windowsDir & " after filtering noisy extensions." & Environment.NewLine)
            End If
        Catch ex As Exception
            DialogHelper.ShowErrorDialog(" An Error occurred", "An Error occurred while downloading the symbol files." & vbNewLine & vbNewLine & "Check if you have access to symchk.exe and that your Antivirus isn't blocking it.")
        Finally
            ' Clean up the temporary file list
            Try
                If IO.File.Exists(filteredFilesPath) Then
                    IO.File.Delete(filteredFilesPath)
                End If
            Catch
                ' Ignore cleanup errors
            End Try
        End Try

        'Disable the current tab and move to the Scan PDB Tab
        Invoke(Sub()
                   TabPage_ScanPDB.Enabled = True
                   TabControl_Main.SelectedTab = TabPage_ScanPDB
                   TabPage_DownloadPDB.Enabled = False
               End Sub)
        ScanPDBFiles()
    End Sub

    ''' <summary>
    ''' Writes a filtered list of files to scan, excluding noisy file extensions.
    ''' </summary>
    ''' <param name="directory">The directory to enumerate files from.</param>
    ''' <param name="outputPath">The path to write the filtered file list.</param>
    Private Sub WriteFilteredFileList(directory As String, outputPath As String)
        Using writer As New IO.StreamWriter(outputPath, False, System.Text.Encoding.UTF8)
            Try
                ' Enumerate files recursively and filter by extension
                For Each filePath In EnumerateFilesRecursive(directory)
                    Dim extension = IO.Path.GetExtension(filePath)
                    If Not NoisyFileExtensions.Contains(extension) Then
                        writer.WriteLine(filePath)
                    End If
                Next
            Catch ex As Exception
                ' Log enumeration errors but don't fail the entire operation
                Diagnostics.Debug.WriteLine("Error enumerating files in " & directory & ": " & ex.Message)
            End Try
        End Using
    End Sub

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
        Catch ex As IO.DirectoryNotFoundException
            ' Skip directories that don't exist
        Catch ex As IO.IOException
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
        Catch ex As IO.DirectoryNotFoundException
            ' Skip directories that don't exist
        Catch ex As IO.IOException
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
    ''' If the Process encounters an Error, send the Error Output to AppendStdErr
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">DataReceived EventArgs</param>
    Private Sub MyProcess_ErrorDataReceived(sender As Object, e As DataReceivedEventArgs) Handles Proc.ErrorDataReceived
        AppendStdErr(e.Data & Environment.NewLine)
    End Sub

    ''' <summary>
    ''' Send the Standard Output to AppendStdOut
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">DataReceived EventArgs</param>
    Private Sub MyProcess_OutputDataReceived(sender As Object, e As DataReceivedEventArgs) Handles Proc.OutputDataReceived
        AppendStdOut(e.Data & Environment.NewLine)
    End Sub

    ''' <summary>
    ''' Append Standard Process Output to RTB_PDBDownloadStatus
    ''' </summary>
    ''' <param name="text">Text to append</param>
    Private Sub AppendStdOut(text As String)
        If RTB_PDBDownloadStatus.InvokeRequired Then
            Dim myDelegate As New AppendStdOutDelegate(AddressOf AppendStdOut)
            Invoke(myDelegate, text)
        Else
            RTB_PDBDownloadStatus.AppendText(text)
        End If
    End Sub

    ''' <summary>
    ''' Append Error Process Output to RTB_PDBDownloadStatus
    ''' </summary>
    ''' <param name="text">Text to append</param>
    Private Sub AppendStdErr(text As String)
        If RTB_PDBDownloadStatus.InvokeRequired Then
            Dim myDelegate As New AppendStdErrDelegate(AddressOf AppendStdErr)
            Invoke(myDelegate, text)
        Else
            RTB_PDBDownloadStatus.AppendText(text)
        End If
    End Sub

    ''' <summary>
    ''' When a PDB File is downloaded, display the File Name in the Text Box
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">IO.FileSystem EventArgs</param>
    Private Sub FSW_SymbolPath_Created(sender As Object, e As IO.FileSystemEventArgs) Handles FSW_SymbolPath.Created
        RTB_PDBDownloadStatus.AppendText("[" & Date.Now.TimeOfDay.Hours & ":" & Date.Now.TimeOfDay.Minutes & "] Symbol " & e.Name & " downloaded." & vbNewLine)
    End Sub

    ''' <summary>
    ''' Scan the PDB Files. Will also create and start a new Thread that calls ScanPDBFiles_Calculation
    ''' </summary>
    Private Sub ScanPDBFiles()
        'Start the calculation of Files/Folders/Folder Size of the Symbol Folder
        Dim ScanPDBFiles_Calculation_Thread As New Threading.Thread(AddressOf ScanPDBFiles_Calculation) With {
            .IsBackground = True
        }
        ScanPDBFiles_Calculation_Thread.SetApartmentState(Threading.ApartmentState.MTA)
        ScanPDBFiles_Calculation_Thread.Start()

        'Scan the .pdb files
        With Proc.StartInfo
            .FileName = Application.StartupPath & "\mach2.exe" 'Path to mach2.exe
            .Arguments = "scan """ & My.Settings.SymbolPath & """ -i """ & My.Settings.SymbolPath & """ -o """ & My.Settings.SymbolPath & "\" & BuildNumber & ".txt"" -u -s"
            .WorkingDirectory = Application.StartupPath 'Set the Working Directory to the path of mach2
            .UseShellExecute = True 'mach2 will crash without this
            .CreateNoWindow = False 'Create a Window
            .WindowStyle = ProcessWindowStyle.Minimized 'Minimize the Window
            .RedirectStandardError = False 'mach2 will crash without this
            .RedirectStandardOutput = False 'mach2 will crash without this
        End With

        'Rescan until the Exitcode is 0
        Dim mach2_ExitCode As Integer = 1
        Do Until mach2_ExitCode = 0
            Proc.Start()
            Proc.WaitForExit()

            If Proc.ExitCode >= 1 Then
                Invoke(Sub()
                           DialogHelper.ShowErrorDialog(" An Error occurred", "An Error occurred while scanning the symbol files." & vbNewLine & vbNewLine & "The application will attempt to rescan the symbol folder.")
                       End Sub)
            Else
                mach2_ExitCode = 0
            End If
        Loop

        'Disable the current tab and move to the Done Tab
        Invoke(Sub()
                   TabPage_Done.Enabled = True
                   TabControl_Main.SelectedTab = TabPage_Done
                   TabPage_ScanPDB.Enabled = False
               End Sub)
        Done()
    End Sub

    ''' <summary>
    ''' Calculates the File/Folder Size and the Folder Amount of the Symbol Folder, while the application is scanning the PDB Files
    ''' </summary>
    Private Sub ScanPDBFiles_Calculation()
        'Set Labels
        Invoke(Sub()
                   RL_SymbolSize.Text = "Current Size of " & My.Settings.SymbolPath & ": " & "Calculating..."
                   RL_SymbolFiles.Text = "Total Files in " & My.Settings.SymbolPath & ": " & "Calculating..."
                   RL_SymbolFolders.Text = "Total Folders in " & My.Settings.SymbolPath & ": " & "Calculating..."
               End Sub)

        'Calculate Size of the Symbol Folder
        Try
            Dim SymbolFolderSize As Long = GetDirSize(My.Settings.SymbolPath)
            Invoke(Sub() RL_SymbolSize.Text = "Current Size of " & My.Settings.SymbolPath & ": " & FormatNumber(SymbolFolderSize / 1024 / 1024 / 1024, 1) & " GB")
        Catch ex As Exception
            Invoke(Sub() RL_SymbolSize.Text = "Current Size of " & My.Settings.SymbolPath & ": IO Error")
        End Try

        'Calculate amount of Total Files in the Symbol Folder
        Try
            'Use EnumerateFiles for better memory efficiency - doesn't load all paths into memory at once
            Dim TotalFiles As Integer = IO.Directory.EnumerateFiles(My.Settings.SymbolPath, "*.*").Count
            Invoke(Sub() RL_SymbolFiles.Text = "Total Files in " & My.Settings.SymbolPath & ": " & TotalFiles.ToString)
        Catch ex As Exception
            Invoke(Sub() RL_SymbolFiles.Text = "Total Files in " & My.Settings.SymbolPath & ": IO Error")
        End Try

        'Calculate amount of Total Folders in the Symbol Folder
        Try
            'Use EnumerateDirectories for better memory efficiency - doesn't load all paths into memory at once
            Dim TotalFolders As Integer = IO.Directory.EnumerateDirectories(My.Settings.SymbolPath).Count
            Invoke(Sub() RL_SymbolFolders.Text = "Total Folders in " & My.Settings.SymbolPath & ": " & TotalFolders.ToString)
        Catch ex As Exception
            Invoke(Sub() RL_SymbolFolders.Text = "Total Folders in " & My.Settings.SymbolPath & ": IO Error")
        End Try

    End Sub

    ''' <summary>
    ''' Functions that get's the total Size of a Folder
    ''' </summary>
    ''' <param name="RootFolder">Folder to get the total Size from</param>
    ''' <returns>Total Folder Size of RootFolder as Long</returns>
    Public Function GetDirSize(RootFolder As String) As Long
        'Use EnumerateFiles with SearchOption.AllDirectories for better performance
        'This avoids the overhead of recursive function calls and the shared TotalSize variable bug
        Dim totalSize As Long = 0
        Try
            For Each file In IO.Directory.EnumerateFiles(RootFolder, "*.*", IO.SearchOption.AllDirectories)
                Try
                    Dim fileInfo = New IO.FileInfo(file)
                    totalSize += fileInfo.Length
                Catch ex As Exception
                    'Skip files that can't be accessed (e.g., permission denied, file in use)
                    'This is expected behavior for system/protected files during directory scanning
                    Diagnostics.Debug.WriteLine("Skipped file during size calculation: " & file & " - " & ex.Message)
                End Try
            Next
        Catch ex As Exception
            'Handle access denied or other directory enumeration errors
            Diagnostics.Debug.WriteLine("Error enumerating directory: " & RootFolder & " - " & ex.Message)
        End Try
        Return totalSize
    End Function

    ''' <summary>
    ''' Last things to do in the Done Tab.
    ''' </summary>
    Private Sub Done()
        'Replace Labels
        Invoke(Sub()
                   RL_OutputFile.Text = "Output File: " & My.Settings.SymbolPath & "\" & BuildNumber & ".txt"
                   RB_OA_DeleteSymbolPath.Text = "Delete " & My.Settings.SymbolPath
                   RL_Done.Text.Replace("Features.txt", BuildNumber & ".txt")
                   RB_OA_CopyFeaturesTXT.Text.Replace("Features.txt", BuildNumber & ".txt")
               End Sub)

        'Show Notification
        Invoke(Sub()
                   Try
                       'Use a simple notification icon instead of RadDesktopAlert
                       MsgBox("The Debug Symbol Scan is complete. Return to the ViVeTool GUI Feature Scanner to find out more.", vbInformation, "Debug Symbol Scan complete")
                   Catch ex As Exception
                       'Sometimes the message box may fail, so we catch any exception
                       MsgBox("The Debug Symbol Scan is complete. Return to the ViVeTool GUI Feature Scanner to find out more.", vbInformation, "Debug Symbol Scan complete")
                   End Try
               End Sub)
    End Sub

    ''' <summary>
    ''' Copy the Features.txt File to the Desktop
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_OA_CopyFeaturesTXT_Click(sender As Object, e As EventArgs) Handles RB_OA_CopyFeaturesTXT.Click
        Try
            IO.File.Copy(My.Settings.SymbolPath & "\" & BuildNumber & ".txt", My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & BuildNumber & ".txt")
            DialogHelper.ShowSuccessDialog(" File Copy successful", BuildNumber & ".txt was successfully copied to your desktop.")
        Catch ex As Exception
            DialogHelper.ShowExceptionDialog(" An Exception occurred", "An Exception occurred", ex.Message, "An Exception occurred while trying to copy " & BuildNumber & ".txt to your desktop.")
        End Try
    End Sub

    ''' <summary>
    ''' Delete the Symbol Folder
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_OA_DeleteSymbolPath_Click(sender As Object, e As EventArgs) Handles RB_OA_DeleteSymbolPath.Click
        Try
            IO.Directory.Delete(My.Settings.SymbolPath, True)
            DialogHelper.ShowSuccessDialog(" Symbol Folder deleted successfully", My.Settings.SymbolPath & "was successfully deleted.")
        Catch ex As Exception
            DialogHelper.ShowExceptionDialog(" An Exception occurred", "An Exception occurred", ex.Message, "An Exception occurred while trying to delete " & My.Settings.SymbolPath)
        End Try
    End Sub

    ''' <summary>
    ''' Form Load Event. Loads the labels and configures CrashReporter.Net
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub ScannerUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Listen to Application Crashes and show CrashReporter.Net if one occurs.
        AddHandler Application.ThreadException, AddressOf CrashReporter.ApplicationThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CrashReporter.CurrentDomainOnUnhandledException

        ' Set up TabControl for proper dark mode rendering (handles DrawItem and Paint events)
        ThemeHelper.SetupTabControlForDarkMode(TabControl_Main)

        'Load About Labels
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        RL_ProductName.Text = My.Application.Info.ProductName
        RL_Version.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        RL_License.Text = My.Application.Info.Copyright
        RL_Description.Text = My.Application.Info.Description

        ' Initialize tooltips for text boxes
        _toolTip.SetToolTip(RTB_DbgPath, "Example Path: C:\Program Files\Windows Kits\10\Debuggers\x64\symchk.exe")
        _toolTip.SetToolTip(RTB_SymbolPath, "The downloaded Debug Symbols can be up to 5~8GB in size.")

        ' Set up the Windows SDK link in the introduction text
        Dim linkText As String = "Windows SDK"
        Dim linkStart As Integer = LL_Introduction.Text.IndexOf(linkText)
        If linkStart >= 0 Then
            LL_Introduction.Links.Clear()
            LL_Introduction.Links.Add(linkStart, linkText.Length, "https://go.microsoft.com/fwlink/?linkid=2342616")
        End If

        ' Apply saved theme settings
        If My.Settings.UseSystemTheme Then
            CB_UseSystemTheme.Checked = True
            ThemeHelper.ApplySystemTheme(CB_ThemeToggle, My.Resources.icons8_moon_and_stars_24, My.Resources.icons8_sun_24)
        ElseIf My.Settings.DarkMode Then
            CB_ThemeToggle.Checked = True
        End If

        ' Apply the theme to the form
        ThemeHelper.ApplyThemeToForm(Me, CB_ThemeToggle.Checked)
    End Sub

    ''' <summary>
    ''' Opens the Windows SDK download page in the default browser
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">LinkLabelLinkClicked EventArgs</param>
    Private Sub LL_Introduction_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LL_Introduction.LinkClicked
        Process.Start(New ProcessStartInfo(e.Link.LinkData.ToString()) With {.UseShellExecute = True})
    End Sub

    ''' <summary>
    ''' Changes the Application theme depending on the CheckState
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub CB_ThemeToggle_CheckedChanged(sender As Object, e As EventArgs) Handles CB_ThemeToggle.CheckedChanged
        If CB_ThemeToggle.Checked Then
            ThemeHelper.ApplyDarkTheme(CB_ThemeToggle, My.Resources.icons8_moon_and_stars_24)
        Else
            ThemeHelper.ApplyLightTheme(CB_ThemeToggle, My.Resources.icons8_sun_24)
        End If
        ThemeHelper.ApplyThemeToForm(Me, CB_ThemeToggle.Checked)
    End Sub

    ''' <summary>
    ''' Changes the Application theme, using the System Theme depending on the CheckState
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub CB_UseSystemTheme_CheckedChanged(sender As Object, e As EventArgs) Handles CB_UseSystemTheme.CheckedChanged
        If CB_UseSystemTheme.Checked Then
            ThemeHelper.ApplySystemTheme(CB_ThemeToggle, My.Resources.icons8_moon_and_stars_24, My.Resources.icons8_sun_24)
            ThemeHelper.ApplyThemeToForm(Me, CB_ThemeToggle.Checked)
        Else
            ThemeHelper.DisableSystemTheme()
        End If
    End Sub
End Class