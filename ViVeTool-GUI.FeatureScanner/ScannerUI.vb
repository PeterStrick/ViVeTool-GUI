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
Imports Telerik.WinControls, Telerik.WinControls.UI

''' <summary>
''' ViVeTool GUI - Feature Scanner
''' </summary>
Public Class ScannerUI
    Private WithEvents Proc As Process
    Private Delegate Sub AppendStdOutDelegate(text As String)
    Private Delegate Sub AppendStdErrDelegate(text As String)
    Public BuildNumber As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber", Nothing).ToString

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
    ''' Show a ToolTip for 15 Seconds when hovering over RTB_SymbolPath
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">ToolTipTextNeeded EventArgs</param>
    Private Sub RTB_DbgPath_ToolTipTextNeeded(sender As Object, e As ToolTipTextNeededEventArgs) Handles RTB_DbgPath.ToolTipTextNeeded
        e.ToolTip.AutoPopDelay = 15000
        e.ToolTipText = "Example Path: C:\Program Files\Windows Kits\10\Debuggers\x64\symchk.exe"
    End Sub

    ''' <summary>
    ''' Show a ToolTip for 15 Seconds when hovering over RTB_SymbolPath
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">ToolTipTextNeeded EventArgs</param>
    Private Sub RTB_SymbolPath_ToolTipTextNeeded(sender As Object, e As ToolTipTextNeededEventArgs) Handles RTB_SymbolPath.ToolTipTextNeeded
        e.ToolTip.AutoPopDelay = 15000
        e.ToolTipText = "The Downloaded Debug Symbols can be up to 5~8GB in size."
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
                   RPBE_StatusProgressBar.Value1 = 10
                   RB_Continue.Enabled = False
                   RB_DbgPath_Browse.Enabled = False
                   RB_SymbolPath_Browse.Enabled = False
               End Sub)

#Region "1. Check RTB_DbgPath"
        'Check if the Path to symchk.exe is correct and if symchk.exe exists
        If RTB_DbgPath.Text.EndsWith("\symchk.exe") AndAlso IO.File.Exists(RTB_DbgPath.Text) Then
            Invoke(Sub() RPBE_StatusProgressBar.Value1 = 50)
        Else
            Dim RTD As New RadTaskDialogPage With {
                    .Caption = " An Error occurred",
                    .Heading = "An Error occurred",
                    .Text = "An Error occurred while checking if the specified Path to symchk.exe is valid." & vbNewLine & vbNewLine & "Please be sure to enter a valid path to symchk.exe." & vbNewLine & "If you can not find symchk.exe, it is usually located at the Installation Directory of the Windows SDK\10\Debuggers\x64",
                    .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                }

            'Show the Message Box
            RadTaskDialog.ShowDialog(RTD)

            Invoke(Sub()
                       RPBE_StatusProgressBar.Value1 = 0
                       RTB_DbgPath.Text = Nothing
                       RB_Continue.Enabled = True
                       RB_DbgPath_Browse.Enabled = True
                       RB_SymbolPath_Browse.Enabled = True
                   End Sub)
        End If
#End Region

#Region "2. Check RTB_SymbolPath"
        'Check if the Application has Write Access to the specified symbol path
        Invoke(Sub() RPBE_StatusProgressBar.Value1 = 80)

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
                    Dim RTD As New RadTaskDialogPage With {
                        .Caption = " An Error occurred",
                        .Heading = "An Error occurred",
                        .Text = "An Error occurred while trying to write a test file to " & RTB_SymbolPath.Text & vbNewLine & vbNewLine & "Please make sure that the application has write access to the folder, and that the folder isn't write protected.",
                        .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                    }
                    'Show the Message Box
                    RadTaskDialog.ShowDialog(RTD)

                    Invoke(Sub()
                               RPBE_StatusProgressBar.Value1 = 0
                               RTB_SymbolPath.Text = Nothing
                               RB_Continue.Enabled = True
                               RB_DbgPath_Browse.Enabled = True
                               RB_SymbolPath_Browse.Enabled = True
                           End Sub)
                End If

            Catch ex As Exception
                'Create a Button that on Click, copies the Exception Text
                Dim CopyExAndClose As New RadTaskDialogButton With {
                    .Text = "Copy Exception and Close"
                }
                AddHandler CopyExAndClose.Click, New EventHandler(Sub() My.Computer.Clipboard.SetText(ex.ToString))

                Dim RTD As New RadTaskDialogPage With {
                        .Caption = " An Exception occurred",
                        .Heading = "An Exception occurred",
                        .Text = "An Exception occurred while trying to write a test file to " & RTB_SymbolPath.Text & vbNewLine & vbNewLine & "Please make sure that the application has write access to the folder, and that the folder isn't write protected.",
                        .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                    }

                'Add the Exception Text to the Expander
                RTD.Expander.Text = ex.Message

                'Set the Text for the "Collapse Info" and "More Info" Buttons
                RTD.Expander.ExpandedButtonText = "Collapse Exception"
                RTD.Expander.CollapsedButtonText = "Show Exception"

                'Add the Button to the Message Box
                RTD.CommandAreaButtons.Add(CopyExAndClose)

                'Show the Message Box
                RadTaskDialog.ShowDialog(RTD)

                Invoke(Sub()
                           RPBE_StatusProgressBar.Value1 = 0
                           RTB_SymbolPath.Text = Nothing
                           RB_Continue.Enabled = True
                           RB_DbgPath_Browse.Enabled = True
                           RB_SymbolPath_Browse.Enabled = True
                       End Sub)
            End Try
        Else
            Dim RTD As New RadTaskDialogPage With {
                        .Caption = " An Error occurred",
                        .Heading = "An Error occurred",
                        .Text = "An Error occurred while trying to write a test file to the symbol folder." & vbNewLine & vbNewLine & "A symbol folder must be specified to download Program Debug Database files into.",
                        .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                    }
            'Show the Message Box
            RadTaskDialog.ShowDialog(RTD)

            Invoke(Sub()
                       RPBE_StatusProgressBar.Value1 = 0
                       RTB_SymbolPath.Text = Nothing
                       RB_Continue.Enabled = True
                       RB_DbgPath_Browse.Enabled = True
                       RB_SymbolPath_Browse.Enabled = True
                   End Sub)
        End If
#End Region

        'Now if both Text Boxes aren't empty, enable the Download PDB Tab
        If RTB_SymbolPath.Text = Nothing OrElse RTB_DbgPath.Text = Nothing Then
            Invoke(Sub() RPBE_StatusProgressBar.Value1 = 0)
        Else
            'Disable the current Tab and move to the Download PDB Tab
            Invoke(Sub()
                       RPBE_StatusProgressBar.Value1 = 100
                       RPVP_DownloadPDB.Enabled = True
                       RPV_Main.SelectedPage = RPVP_DownloadPDB
                       RPVP_Setup.Enabled = False
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
    ''' Downloads all the .pdb files of C:\Windows\*.*, C:\Program Files\*.*, C:\Program Files (x86)\*.* to the path specified in My.Settings.SymbolPath
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

        'Get the .pdb files of C:\Windows\*.* - Recursively
        Proc.StartInfo.Arguments = "C:\Windows\*.* /oc " & My.Settings.SymbolPath & " /cn /r"
        Proc.Start()
        Proc.BeginErrorReadLine()
        Proc.BeginOutputReadLine()
        Proc.WaitForExit()
        Proc.CancelOutputRead()
        Proc.CancelErrorRead()

        'Get the .pdb files of C:\Program Files\*.* - Recursively
        Proc.StartInfo.Arguments = "C:\Program Files\*.* /oc " & My.Settings.SymbolPath & " /cn /r"
        Proc.Start()
        Proc.BeginErrorReadLine()
        Proc.BeginOutputReadLine()
        Proc.WaitForExit()
        Proc.CancelOutputRead()
        Proc.CancelErrorRead()

        'Get the .pdb files of C:\Program Files (x86)\*.* - Recursively
        Proc.StartInfo.Arguments = "C:\Program Files (x86)\*.* /oc " & My.Settings.SymbolPath & " /cn /r"
        Proc.Start()
        Proc.BeginErrorReadLine()
        Proc.BeginOutputReadLine()
        Proc.WaitForExit()
        Proc.CancelOutputRead()
        Proc.CancelErrorRead()

        'Disable the current tab and move to the Scan PDB Tab
        Invoke(Sub()
                   RPVP_ScanPDB.Enabled = True
                   RPV_Main.SelectedPage = RPVP_ScanPDB
                   RPVP_DownloadPDB.Enabled = False
               End Sub)
        ScanPDBFiles()
    End Sub

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
            .Arguments = "scan " & My.Settings.SymbolPath & " -i " & My.Settings.SymbolPath & " -o " & My.Settings.SymbolPath & "\" & BuildNumber & ".txt -u -s"
            .WorkingDirectory = Application.StartupPath 'Set the Working Directory to the path of mach2
            .UseShellExecute = True 'mach2 will crash without this
            .CreateNoWindow = False 'Create a Window
            .WindowStyle = ProcessWindowStyle.Minimized 'Minimize the Window
            .RedirectStandardError = False 'mach2 will crash without this
            .RedirectStandardOutput = False 'mach2 will crash without this
        End With

        'Rescan until the Exitcode is 0
        Do Until Proc.ExitCode = 0
            Proc.Start()
            Proc.WaitForExit()

            If Proc.ExitCode >= 1 Then
                Invoke(Sub()
                           Dim RTD As New RadTaskDialogPage With {
                                .Caption = " An Error occurred",
                                .Heading = "An Error occurred",
                                .Text = "An Error occurred while scanning the symbol files." & vbNewLine & vbNewLine & "The application will attempt to rescan the symbol folder.",
                                .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                            }
                           'Show the Message Box
                           RadTaskDialog.ShowDialog(RTD)
                       End Sub)
            End If
        Loop

        'Disable the current tab and move to the Done Tab
        Invoke(Sub()
                   RPVP_Done.Enabled = True
                   RPV_Main.SelectedPage = RPVP_Done
                   RPVP_ScanPDB.Enabled = False
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
        Dim SymbolFolderSize As Long = GetDirSize(My.Settings.SymbolPath)
        Invoke(Sub() RL_SymbolSize.Text = "Current Size of " & My.Settings.SymbolPath & ": " & FormatNumber(SymbolFolderSize / 1024 / 1024 / 1024, 1) & " GB")

        'Calculate amount of Total Files in the Symbol Folder
        Dim TotalFiles As Integer = IO.Directory.GetFiles(My.Settings.SymbolPath, "*.*").Count
        Invoke(Sub() RL_SymbolFiles.Text = "Total Files in " & My.Settings.SymbolPath & ": " & TotalFiles.ToString)

        'Calculate amount of Total Folders in the Symbol Folder
        Dim TotalFolders As Integer = IO.Directory.GetDirectories(My.Settings.SymbolPath).Count
        Invoke(Sub() RL_SymbolFolders.Text = "Total Folders in " & My.Settings.SymbolPath & ": " & TotalFolders.ToString)
    End Sub

    ''' <summary>
    ''' Variable that stores the Total File Size of the Symbol Folder
    ''' </summary>
    Dim TotalSize As Long = 0

    ''' <summary>
    ''' Functions that get's the total Size of a Folder
    ''' </summary>
    ''' <param name="RootFolder">Folder to get the total Size from</param>
    ''' <returns>Total Folder Size of RootFolder as Long</returns>
    Public Function GetDirSize(RootFolder As String) As Long
        Dim FolderInfo = New IO.DirectoryInfo(RootFolder)
        For Each File In FolderInfo.GetFiles : TotalSize += File.Length
        Next
        For Each SubFolderInfo In FolderInfo.GetDirectories : GetDirSize(SubFolderInfo.FullName)
        Next
        Return TotalSize
    End Function

    ''' <summary>
    ''' Last things to do in the Done Tab.
    ''' </summary>
    Private Sub Done()
        'Replace Labels
        Invoke(Sub()
                   RL_OutputFile.Text = "Output File: " & My.Settings.SymbolPath & "\" & BuildNumber & ".txt"
                   RB_OA_DeleteSymbolPath.Text = "Delete " & My.Settings.SymbolPath
                   RL_Done.Text.Replace("Features", BuildNumber)
                   RB_OA_CopyFeaturesTXT.Text.Replace("Features", BuildNumber)
               End Sub)

        'Show Notification
        Invoke(Sub()
                   Try
                       Dim RDA_Done As New RadDesktopAlert With {
                        .CaptionText = "Debug Symbol Scan complete",
                        .ContentText = "The Debug Symbol Scan is complete. Return to the ViVeTool GUI Feature Scanner to find out more.",
                        .Opacity = 1,
                        .ShowCloseButton = True,
                        .ShowPinButton = False,
                        .ShowOptionsButton = False,
                        .AutoClose = False,
                        .AutoSize = True,
                        .CanMove = False,
                        .FadeAnimationType = FadeAnimationType.FadeOut,
                        .IsPinned = True,
                        .PopupAnimationDirection = RadDirection.Up
                    }
                       RDA_Done.Show()
                   Catch ex As Exception
                       'Sometimes, again rarely the RadDesktopAlert will fail so we fall back to a god ol' message box
                       MsgBox("The Debug Symbol Scan is complete. Return to the ViVeTool GUI Feature Scanner to find out more.", vbInformation, "Debug Symbol Scan complete")
                   End Try
               End Sub)
    End Sub

    ''' <summary>
    ''' Copy Features.TXT File to the Desktop
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_OA_CopyFeaturesTXT_Click(sender As Object, e As EventArgs) Handles RB_OA_CopyFeaturesTXT.Click
        Try
            IO.File.Copy(My.Settings.SymbolPath & "\" & BuildNumber & ".txt", My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & BuildNumber & ".txt")
            Dim RTD As New RadTaskDialogPage With {
                       .Caption = " File Copy successful",
                       .Heading = BuildNumber & ".txt was successfully copied to your desktop.",
                       .Icon = RadTaskDialogIcon.ShieldSuccessGreenBar
                   }
            'Show the Message Box
            RadTaskDialog.ShowDialog(RTD)
        Catch ex As Exception
            'Create a Button that on Click, copies the Exception Text
            Dim CopyExAndClose As New RadTaskDialogButton With {
                    .Text = "Copy Exception and Close"
                }
            AddHandler CopyExAndClose.Click, New EventHandler(Sub() My.Computer.Clipboard.SetText(ex.ToString))

            Dim RTD As New RadTaskDialogPage With {
                        .Caption = " An Exception occurred",
                        .Heading = "An Exception occurred",
                        .Text = "An Exception occurred while trying to copy " & BuildNumber & ".txt to your desktop.",
                        .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                    }

            'Add the Exception Text to the Expander
            RTD.Expander.Text = ex.Message

            'Set the Text for the "Collapse Info" and "More Info" Buttons
            RTD.Expander.ExpandedButtonText = "Collapse Exception"
            RTD.Expander.CollapsedButtonText = "Show Exception"

            'Add the Button to the Message Box
            RTD.CommandAreaButtons.Add(CopyExAndClose)

            'Show the Message Box
            RadTaskDialog.ShowDialog(RTD)
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
            Dim RTD As New RadTaskDialogPage With {
                       .Caption = " Symbol Folder deleted successfully",
                       .Heading = My.Settings.SymbolPath & "was successfully deleted.",
                       .Icon = RadTaskDialogIcon.ShieldSuccessGreenBar
                   }
            'Show the Message Box
            RadTaskDialog.ShowDialog(RTD)
        Catch ex As Exception
            'Create a Button that on Click, copies the Exception Text
            Dim CopyExAndClose As New RadTaskDialogButton With {
                    .Text = "Copy Exception and Close"
                }
            AddHandler CopyExAndClose.Click, New EventHandler(Sub() My.Computer.Clipboard.SetText(ex.ToString))

            Dim RTD As New RadTaskDialogPage With {
                        .Caption = " An Exception occurred",
                        .Heading = "An Exception occurred",
                        .Text = "An Exception occurred while trying to delete " & My.Settings.SymbolPath,
                        .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                    }

            'Add the Exception Text to the Expander
            RTD.Expander.Text = ex.Message

            'Set the Text for the "Collapse Info" and "More Info" Buttons
            RTD.Expander.ExpandedButtonText = "Collapse Exception"
            RTD.Expander.CollapsedButtonText = "Show Exception"

            'Add the Button to the Message Box
            RTD.CommandAreaButtons.Add(CopyExAndClose)

            'Show the Message Box
            RadTaskDialog.ShowDialog(RTD)
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

        'Load About Labels
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.RL_ProductName.Text = My.Application.Info.ProductName
        Me.RL_Version.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.RL_License.Text = My.Application.Info.Copyright
        Me.RL_Description.Text = My.Application.Info.Description
    End Sub

    ''' <summary>
    ''' Changes the Application theme depending on the ToggleState
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="args">StateChanging EventArgs</param>
    Private Sub RTB_ThemeToggle_ToggleStateChanging(sender As Object, args As StateChangingEventArgs) Handles RTB_ThemeToggle.ToggleStateChanging
        If args.NewValue = Telerik.WinControls.Enumerations.ToggleState.On Then
            ThemeResolutionService.ApplicationThemeName = "FluentDark"
            RTB_ThemeToggle.Text = "Dark Theme"
            RTB_ThemeToggle.Image = My.Resources.icons8_moon_and_stars_24
            My.Settings.DarkMode = True
        Else
            ThemeResolutionService.ApplicationThemeName = "Fluent"
            RTB_ThemeToggle.Text = "Light Theme"
            RTB_ThemeToggle.Image = My.Resources.icons8_sun_24
            My.Settings.DarkMode = False
        End If
    End Sub

    ''' <summary>
    ''' Changes the Application theme, using the System Theme depending on the ToggleState
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="args">StateChanged EventArgs</param>
    Private Sub RTB_UseSystemTheme_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles RTB_UseSystemTheme.ToggleStateChanged
        If args.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On Then
            My.Settings.UseSystemTheme = True
            Dim AppsUseLightTheme_CurrentUserDwordKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
            Dim AppsUseLightTheme_CurrentUserDwordValue As Object = AppsUseLightTheme_CurrentUserDwordKey.GetValue("SystemUsesLightTheme")
            If CInt(AppsUseLightTheme_CurrentUserDwordValue) = 0 Then
                RTB_ThemeToggle.ToggleState = Enumerations.ToggleState.On
                RTB_ThemeToggle.Image = My.Resources.icons8_moon_and_stars_24
            Else
                RTB_ThemeToggle.ToggleState = Enumerations.ToggleState.Off
                RTB_ThemeToggle.Image = My.Resources.icons8_sun_24
            End If
        Else
            My.Settings.UseSystemTheme = False
        End If
    End Sub
End Class