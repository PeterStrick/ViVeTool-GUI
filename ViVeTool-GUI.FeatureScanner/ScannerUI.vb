'ViVeTool GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2023 Peter Strick
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
Imports Telerik.WinControls, Telerik.RadToastNotificationManager, System.IO

''' <summary>
''' ViVeTool GUI - Feature Scanner
''' </summary>
Public Class ScannerUI
    Private WithEvents Proc As Process
    Private Delegate Sub AppendStdOutDelegate(text As String)
    Private Delegate Sub AppendStdErrDelegate(text As String)
    Public BuildNumber As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber", Nothing).ToString

    Friend WithEvents RTNM As New RadToastNotificationManager()
    ReadOnly RTN_FeatureScanComplete As New RadToastNotification(RadToastTemplateType.ToastGeneric, "ToastTest",
        String.Format("<toast><visual><binding template=""ToastGeneric""><text>{0}</text><text>{1}</text></binding></visual></toast>",
        My.Resources.Done_Alert_CaptionText, My.Resources.Done_Alert_ContentText))

#Region "Debug Subs"
    Private Sub __DBG_ScanSymbols_Click(sender As Object, e As EventArgs) Handles __DBG_ScanSymbols.Click
        My.Settings.DebuggerPath = RTB_DbgPath.Text
        My.Settings.SymbolPath = RTB_SymbolPath.Text
        My.Settings.Save()

        Proc = New Process
        With Proc.StartInfo
            .FileName = My.Settings.DebuggerPath 'Path to symchk.exe
            .UseShellExecute = False 'Required for Output/Error Redirection to work
            .CreateNoWindow = True 'Required for Output/Error Redirection to work
            .RedirectStandardError = True 'Enables Redirection of Error Output
            .RedirectStandardOutput = True 'Enables Redirection of Standard Output
        End With

        ' Create Junctions
        Junction.FeatureScanner_CreateJunctions()

        RPVP_ScanPDB.Enabled = True
        RPV_Main.SelectedPage = RPVP_ScanPDB
        ScanPDBFiles()
    End Sub

    Private Sub __DBG_RemoveJunctions_Click(sender As Object, e As EventArgs) Handles __DBG_RemoveJunctions.Click
        Junction.FeatureScanner_DeleteJunctions()
        MsgBox("Junctions deleted")
    End Sub

    Private Sub __DBG_TestSymDownloadedText_Click(sender As Object, e As EventArgs) Handles __DBG_TestSymDownloadedText.Click
        RPVP_DownloadPDB.Enabled = True
        RPV_Main.SelectedPage = RPVP_DownloadPDB

        Dim SymbolString = String.Format(My.Resources.SymbolDownloaded, "TESTSYMBOL1.PDB")
        Dim Time As Date = Date.Now
        RTB_PDBDownloadStatus.AppendText(Time.ToString("[HH:mm] ") & SymbolString & vbNewLine)
    End Sub

    Private Sub __DBG_UnlockAllTabs_Click(sender As Object, e As EventArgs) Handles __DBG_UnlockAllTabs.Click
        RPVP_AboutAndSettings.Enabled = True
        RPVP_Done.Enabled = True
        RPVP_DownloadPDB.Enabled = True
        RPVP_ScanPDB.Enabled = True
        RPVP_Setup.Enabled = True
    End Sub

    Private Sub __DBG_AddJunctions_Click(sender As Object, e As EventArgs) Handles __DBG_AddJunctions.Click
        Junction.FeatureScanner_CreateJunctions()
        MsgBox("Junctions created")
    End Sub

    Private Sub __DBG_SendToastNotification_Click(sender As Object, e As EventArgs) Handles __DBG_SendToastNotification.Click
        RTNM.ShowNotification(0)
    End Sub
#End Region

    ''' <summary>
    ''' Debugging Tools/symchk.exe Path Browse Button
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_DbgPath_Browse_Click(sender As Object, e As EventArgs) Handles RB_DbgPath_Browse.Click
        Dim OFD As New OpenFileDialog With {
            .InitialDirectory = "C:\",
            .Title = My.Resources.Browse_PathToDebuggingTools,
            .Filter = "Symbol Checker|symchk.exe"
        }

        If OFD.ShowDialog() = DialogResult.OK Then RTB_DbgPath.Text = OFD.FileName
    End Sub

    ''' <summary>
    ''' Symbol Path Browse Button
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_SymbolPath_Browse_Click(sender As Object, e As EventArgs) Handles RB_SymbolPath_Browse.Click
        Dim FBD As New FolderBrowserDialog With {
            .ShowNewFolderButton = True,
            .Description = My.Resources.Browse_SymbolPath_Description
        }

        If FBD.ShowDialog() = DialogResult.OK Then RTB_SymbolPath.Text = FBD.SelectedPath
    End Sub

    ''' <summary>
    ''' Show a ToolTip for 15 Seconds when hovering over RTB_SymbolPath
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">ToolTipTextNeeded EventArgs</param>
    Private Sub RTB_DbgPath_ToolTipTextNeeded(sender As Object, e As ToolTipTextNeededEventArgs) Handles RTB_DbgPath.ToolTipTextNeeded
        e.ToolTip.AutoPopDelay = 15000
        e.ToolTipText = My.Resources.ToolTip_RTB_DbgPath
    End Sub

    ''' <summary>
    ''' Show a ToolTip for 15 Seconds when hovering over RTB_SymbolPath
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">ToolTipTextNeeded EventArgs</param>
    Private Sub RTB_SymbolPath_ToolTipTextNeeded(sender As Object, e As ToolTipTextNeededEventArgs) Handles RTB_SymbolPath.ToolTipTextNeeded
        e.ToolTip.AutoPopDelay = 15000
        e.ToolTipText = My.Resources.ToolTip_RTB_SymbolPath
    End Sub

    ''' <summary>
    ''' Continue Button. Checks if the Requirements are met by calling CheckPreReq()
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_Continue_Click(sender As Object, e As EventArgs) Handles RB_Continue.Click
        Dim BT As New Threading.Thread(AddressOf CheckPreReq) With {.IsBackground = True}
        BT.SetApartmentState(Threading.ApartmentState.STA)
        BT.Start()
    End Sub

    ''' <summary>
    ''' Checks if the Requirements are met by checking if the path in RTB_DbgPath is valid and that the path in RTB_SymbolPath is writable
    ''' </summary>
    Private Sub CheckPreReq()
        ' First disable the Buttons
        Invoke(Sub()
                   RPBE_StatusProgressBar.Value1 = 10
                   RB_Continue.Enabled = False
                   RB_DbgPath_Browse.Enabled = False
                   RB_SymbolPath_Browse.Enabled = False
               End Sub)

#Region "1. Check RTB_DbgPath"
        ' Check if the Path to symchk.exe is correct and if symchk.exe exists
        If RTB_DbgPath.Text.EndsWith("\symchk.exe") AndAlso IO.File.Exists(RTB_DbgPath.Text) Then
            Invoke(Sub() RPBE_StatusProgressBar.Value1 = 50)
        Else
            ' Show the Message Box
            RadTD.ShowDialog($" {My.Resources.Error_AnErrorOccurred}", My.Resources.Error_AnErrorOccurred,
            My.Resources.Error_SymchkPath_N, RadTaskDialogIcon.ShieldErrorRedBar)

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
        ' Check if the Application has Write Access to the specified symbol path
        Invoke(Sub() RPBE_StatusProgressBar.Value1 = 80)

        ' If the Path in RTB_SymbolPath exists, and try to write a Test File to it
        If IO.Directory.Exists(RTB_SymbolPath.Text) Then
            Try
                Dim WT = IO.File.CreateText(RTB_SymbolPath.Text & "\Test.txt")
                WT.WriteLine("Test File")
                WT.Close()

                ' Check if the Test File contains "Test File". If it does contain "Test File" then delete it and continue.
                If IO.File.ReadAllText(RTB_SymbolPath.Text & "\Test.txt").Contains("Test File") Then
                    IO.File.Delete(RTB_SymbolPath.Text & "\Test.txt")
                Else
                    ' Show the Message Box
                    RadTD.ShowDialog($" {My.Resources.Error_AnErrorOccurred}", My.Resources.Error_AnErrorOccurred,
                    String.Format(My.Resources.Error_SymbolPath_NewN, RTB_SymbolPath.Text),
                    RadTaskDialogIcon.ShieldErrorRedBar)

                    Invoke(Sub()
                               RPBE_StatusProgressBar.Value1 = 0
                               RTB_SymbolPath.Text = Nothing
                               RB_Continue.Enabled = True
                               RB_DbgPath_Browse.Enabled = True
                               RB_SymbolPath_Browse.Enabled = True
                           End Sub)
                End If
            Catch ex As Exception
                ' Show an Error Dialog
                RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnExceptionOccurred,
                    String.Format(My.Resources.Error_SymbolPath_NewN, RTB_SymbolPath.Text),
                    RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.Message)

                Invoke(Sub()
                           RPBE_StatusProgressBar.Value1 = 0
                           RTB_SymbolPath.Text = Nothing
                           RB_Continue.Enabled = True
                           RB_DbgPath_Browse.Enabled = True
                           RB_SymbolPath_Browse.Enabled = True
                       End Sub)
            End Try
        Else
            'Show an Error Dialog
            RadTD.ShowDialog($" {My.Resources.Error_AnErrorOccurred}", My.Resources.Error_AnErrorOccurred,
                    My.Resources.Error_SymbolFolderTestFile_N, RadTaskDialogIcon.ShieldErrorRedBar)

            Invoke(Sub()
                       RPBE_StatusProgressBar.Value1 = 0
                       RTB_SymbolPath.Text = Nothing
                       RB_Continue.Enabled = True
                       RB_DbgPath_Browse.Enabled = True
                       RB_SymbolPath_Browse.Enabled = True
                   End Sub)
        End If
#End Region

        ' Now if both Text Boxes aren't empty, enable the Download PDB Tab
        If RTB_SymbolPath.Text = Nothing OrElse RTB_DbgPath.Text = Nothing Then
            Invoke(Sub() RPBE_StatusProgressBar.Value1 = 0)
        Else
            ' Disable the current Tab and move to the Download PDB Tab
            Invoke(Sub()
                       RPBE_StatusProgressBar.Value1 = 100
                       RPVP_DownloadPDB.Enabled = True
                       RPV_Main.SelectedPage = RPVP_DownloadPDB
                       RPVP_Setup.Enabled = False
                   End Sub)

            ' Save the Paths to My.Settings
            My.Settings.DebuggerPath = RTB_DbgPath.Text
            My.Settings.SymbolPath = RTB_SymbolPath.Text
            My.Settings.Save()

            ' Start the PDB Download automatically
            DownloadPDBFiles()
        End If
    End Sub

    ''' <summary>
    ''' Downloads all the .pdb files of every Junction in C:\FeatureScanner recursively to the path specified in My.Settings.SymbolPath
    ''' </summary>
    Private Sub DownloadPDBFiles()
        ' Create Junctions
        Junction.FeatureScanner_CreateJunctions()

        ' Set up the File System Watcher
        FSW_SymbolPath.SynchronizingObject = Me
        FSW_SymbolPath.Path = My.Settings.SymbolPath

        ' Create a Process with Process StartInfo
        Proc = New Process
        With Proc.StartInfo
            .FileName = My.Settings.DebuggerPath 'Path to symchk.exe
            .UseShellExecute = False 'Required for Output/Error Redirection to work
            .CreateNoWindow = True 'Required for Output/Error Redirection to work
            .RedirectStandardError = True 'Enables Redirection of Error Output
            .RedirectStandardOutput = True 'Enables Redirection of Standard Output
        End With

        ' Get the .pdb files of C:\FeatureScanner\*.* - Recursively. Includes C:\Windows, and some Folders from Program Files x64/x86
        Try
            Proc.StartInfo.Arguments = "/r ""C:\FeatureScanner"" /oc """ & My.Settings.SymbolPath & """ /cn"
            Proc.Start()
            Proc.BeginErrorReadLine()
            Proc.BeginOutputReadLine()
            Proc.WaitForExit()
            Proc.CancelOutputRead()
            Proc.CancelErrorRead()
        Catch ex As Exception
            ' Show an Error Dialog
            RadTD.ShowDialog($" {My.Resources.Error_AnErrorOccurred}", My.Resources.Error_AnErrorOccurred,
                       My.Resources.Error_SymbolDownload_N, RadTaskDialogIcon.ShieldErrorRedBar)
        End Try

        ' Disable the current tab and move to the Scan PDB Tab
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
        Else RTB_PDBDownloadStatus.AppendText(text)
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
        Else RTB_PDBDownloadStatus.AppendText(text)
        End If
    End Sub

    ''' <summary>
    ''' When a PDB File is downloaded, display the File Name in the Text Box
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">IO.FileSystem EventArgs</param>
    Private Sub FSW_SymbolPath_Created(sender As Object, e As IO.FileSystemEventArgs) Handles FSW_SymbolPath.Created
        Dim SymbolString = String.Format(My.Resources.SymbolDownloaded, e.Name)
        Dim Time As Date = Date.Now
        RTB_PDBDownloadStatus.AppendText(Time.ToString("[HH:mm] ") & SymbolString & vbNewLine)
    End Sub

    ''' <summary>
    ''' Scan the PDB Files. Will also create and start a new Thread that calls ScanPDBFiles_Calculation
    ''' </summary>
    Private Sub ScanPDBFiles()
        ' Start the calculation of Files/Folders/Folder Size of the Symbol Folder
        Dim ScanPDBFiles_Calculation_Thread As New Threading.Thread(AddressOf ScanPDBFiles_Calculation) With {.IsBackground = True}
        ScanPDBFiles_Calculation_Thread.SetApartmentState(Threading.ApartmentState.MTA)
        ScanPDBFiles_Calculation_Thread.Start()

        ' Scan the .pdb files
        With Proc.StartInfo
            .FileName = Application.StartupPath & "\mach2\mach2.exe" ' Path to mach2.exe
            .Arguments = "scan """ & My.Settings.SymbolPath & """ -i ""C:\FeatureScanner"" -o """ & My.Settings.SymbolPath & "\" & BuildNumber & ".txt"" -u -s"
            .WorkingDirectory = Application.StartupPath ' Set the Working Directory to the path of mach2
            .UseShellExecute = True ' mach2 will crash without this
            .CreateNoWindow = False ' Create a Window
            .WindowStyle = ProcessWindowStyle.Minimized ' Minimize the Window
            .RedirectStandardError = False ' mach2 will crash without this
            .RedirectStandardOutput = False ' mach2 will crash without this
        End With

        ' Rescan until the Exit-code is 0
        Dim mach2_ExitCode As Integer = 1
        Do Until mach2_ExitCode = 0
            Proc.Start()
            Proc.WaitForExit()

            If Proc.ExitCode >= 1 Then
                Invoke(Sub() RadTD.ShowDialog($" {My.Resources.Error_AnErrorOccurred}", My.Resources.Error_AnErrorOccurred,
                            My.Resources.Error_mach2Scan_N, RadTaskDialogIcon.ShieldErrorRedBar))
            Else mach2_ExitCode = 0
            End If
        Loop

        ' Disable the current tab and move to the Done Tab
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
        ' Set Labels
        Invoke(Sub()
                   RL_SymbolSize.Text = String.Format(My.Resources.Calculation_CurrentSizeOf_N, My.Settings.SymbolPath, My.Resources.Calculation_Calculating)
                   RL_SymbolFiles.Text = String.Format(My.Resources.Calculation_TotalFilesIn_N, My.Settings.SymbolPath, My.Resources.Calculation_Calculating)
                   RL_SymbolFolders.Text = String.Format(My.Resources.Calculation_TotalFoldersIn_N, My.Settings.SymbolPath, My.Resources.Calculation_Calculating)
               End Sub)

        ' Calculate Size of the Symbol Folder
        Try
            Dim SymbolFolderSize As Long = GetDirSize(My.Settings.SymbolPath)
            Invoke(Sub() RL_SymbolSize.Text = String.Format(
                       My.Resources.Calculation_CurrentSizeOf_N,
                       My.Settings.SymbolPath, FormatNumber(SymbolFolderSize / 1024 / 1024 / 1024, 1) &
                       " " & My.Resources.Generic_FileSize_Gigabyte))
        Catch ex As Exception
            Invoke(Sub() RL_SymbolSize.Text = String.Format(
                       My.Resources.Calculation_CurrentSizeOf_N,
                       My.Settings.SymbolPath, My.Resources.Error_IOError))
        End Try

        ' Calculate amount of Total Files in the Symbol Folder
        Try
            Dim TotalFiles As Integer = Directory.GetFiles(My.Settings.SymbolPath, "*.*").Count
            Invoke(Sub() RL_SymbolFiles.Text = String.Format(
                       My.Resources.Calculation_TotalFilesIn_N,
                       My.Settings.SymbolPath, TotalFiles.ToString))
        Catch ex As Exception
            Invoke(Sub() RL_SymbolFiles.Text = String.Format(
                       My.Resources.Calculation_TotalFilesIn_N,
                       My.Settings.SymbolPath, My.Resources.Error_IOError))
        End Try

        ' Calculate amount of Total Folders in the Symbol Folder
        Try
            Dim TotalFolders As Integer = Directory.GetDirectories(My.Settings.SymbolPath).Count
            Invoke(Sub() RL_SymbolFolders.Text = String.Format(
                       My.Resources.Calculation_TotalFoldersIn_N,
                       My.Settings.SymbolPath, TotalFolders.ToString))
        Catch ex As Exception
            Invoke(Sub() RL_SymbolFolders.Text = String.Format(
                       My.Resources.Calculation_TotalFoldersIn_N,
                       My.Settings.SymbolPath, My.Resources.Error_IOError))
        End Try
    End Sub

    ''' <summary>
    ''' Variable that stores the Total File Size of the Symbol Folder
    ''' </summary>
    Dim TotalSize As Long

    ''' <summary>
    ''' Functions that get's the total Size of a Folder
    ''' </summary>
    ''' <param name="RootFolder">Folder to get the total Size from</param>
    ''' <returns>Total Folder Size of RootFolder as Long</returns>
    Public Function GetDirSize(RootFolder As String) As Long
        Dim FolderInfo = New DirectoryInfo(RootFolder)
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
        ' Delete Junctions
        Junction.FeatureScanner_DeleteJunctions()

        ' Replace Labels
        Invoke(Sub()
                   RL_OutputFile.Text = String.Format(My.Resources.Done_OutputFile_N, Path.Combine(My.Settings.SymbolPath, BuildNumber & ".txt"))
                   RB_OA_DeleteSymbolPath.Text = String.Format(My.Resources.Done_SymbolDelete, My.Settings.SymbolPath)
                   RL_Done.Text.Replace("Features.txt", BuildNumber & ".txt")
                   RB_OA_CopyFeaturesTXT.Text.Replace("Features.txt", BuildNumber & ".txt")
               End Sub)

        ' Show Notification
        Invoke(Sub() RTNM.ShowNotification(0))
    End Sub

    ''' <summary>
    ''' Copy the Features.txt File to the Desktop
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_OA_CopyFeaturesTXT_Click(sender As Object, e As EventArgs) Handles RB_OA_CopyFeaturesTXT.Click, __DBG_CopyFeatureList.Click
        Try
            Dim RSFD As New RadSaveFileDialog With {.DefaultExt = "txt", .Filter = "Text Files (*.txt)|*.txt"}
            If RSFD.ShowDialog = DialogResult.OK Then
                File.Copy(Path.Combine(My.Settings.SymbolPath, BuildNumber & ".txt"), RSFD.FileName, True)
            End If
        Catch ex As Exception
            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnExceptionOccurred,
            String.Format(My.Resources.Error_CopyException, BuildNumber & ".txt"),
            RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.Message, ex.ToString)
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

            ' Show the Task Dialog
            RadTD.ShowDialog(My.Resources.Done_SymbolFolderDeleted_Caption,
                       String.Format(My.Resources.Done_SymbolFolderDeleted_Heading_N, My.Settings.SymbolPath),
                       Nothing, RadTaskDialogIcon.ShieldSuccessGreenBar)
        Catch ex As Exception
            ' Show the Error Dialog

            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnExceptionOccurred,
            String.Format(My.Resources.Error_SymbolFolderDeleted_N, My.Settings.SymbolPath),
            RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.Message, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Form Load Event. Loads the labels and configures CrashReporter.Net
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub ScannerUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
#If DEBUG Then
        If Debugger.IsAttached Then
            __DBG_OPTIONS.Enabled = True
            __DBG_OPTIONS.Visible = True
            MsgBox("Debugger detected. The Debug Menu is enabled and visible.")
        End If
#End If

        ' Add Toast to RadToastNotificationManager
        RTNM.ToastNotifications.Add(RTN_FeatureScanComplete)

        ' Localize the Introduction Text
        Functions.SetWBDocumentText(WB_Introduction, My.Resources.WB_HTML_Introduction)

        ' Listen to Application Crashes and show CrashReporter.Net if one occurs.
        AddHandler Application.ThreadException, AddressOf CrashReporter.ApplicationThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CrashReporter.CurrentDomainOnUnhandledException

        ' Load About Labels
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If

        RL_ProductName.Text = My.Application.Info.ProductName
        RL_Version.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        RL_License.Text = My.Application.Info.Copyright
        RL_Description.Text = My.Application.Info.Description

        ' Set the Language Drop Down Button Text to the current Language
        LocalizeLanguageDropDown()
    End Sub

    ''' <summary>
    ''' Set's the Language Drop Down Button Text to the current Language
    ''' </summary>
    Private Sub LocalizeLanguageDropDown()
        Select Case My.Settings.TwoCharLanguageCode
            Case "en"
                RDDB_Language.Text = RMI_L_English.Text
            Case "de"
                RDDB_Language.Text = RMI_L_German.Text
            Case "zh"
                RDDB_Language.Text = RMI_L_Chinese.Text
            Case "pl"
                RDDB_Language.Text = RMI_L_Polish.Text
            Case "id"
                RDDB_Language.Text = RMI_L_Indonesian.Text
            Case "it"
                RDDB_Language.Text = RMI_L_Italian.Text
            Case "jp"
                RDDB_Language.Text = RMI_L_Japanese.Text
        End Select
    End Sub

    ''' <summary>
    ''' Changes the Application theme depending on the ToggleState
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="args">StateChanging EventArgs</param>
    Private Sub RTB_ThemeToggle_ToggleStateChanging(sender As Object, args As StateChangingEventArgs) Handles RTB_ThemeToggle.ToggleStateChanging
        If args.NewValue = Telerik.WinControls.Enumerations.ToggleState.On Then
            ThemeResolutionService.ApplicationThemeName = "FluentDark"
            RTB_ThemeToggle.Text = My.Resources.Generic_DarkTheme
            RTB_ThemeToggle.Image = My.Resources.icons8_moon_and_stars_24
            My.Settings.DarkMode = True
        Else
            ThemeResolutionService.ApplicationThemeName = "Fluent"
            RTB_ThemeToggle.Text = My.Resources.Generic_LightTheme
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

    ''' <summary>
    ''' Changes the Window State to Normal and Brings the Form to the Front, if a Toast Notification is clicked
    ''' </summary>
    ''' <param name="e">RadToastOnActivated EventArgs</param>
    Private Sub RTNM_RadToastOnActivated(e As RadToastOnActivatedEventArgs) Handles RTNM.RadToastOnActivated
        Invoke(Sub()
                   WindowState = FormWindowState.Normal
                   Activate()
               End Sub)
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to English
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_English_Click(sender As Object, e As EventArgs) Handles RMI_L_English.Click
        Functions.ChangeLanguage("en")
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to German
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_German_Click(sender As Object, e As EventArgs) Handles RMI_L_German.Click
        Functions.ChangeLanguage("de")
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to Chinese
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_Chinese_Click(sender As Object, e As EventArgs) Handles RMI_L_Chinese.Click
        Functions.ChangeLanguage("zh")
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to Polish
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_Polish_Click(sender As Object, e As EventArgs) Handles RMI_L_Polish.Click
        Functions.ChangeLanguage("pl")
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to Indonesian
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_Indonesian_Click(sender As Object, e As EventArgs) Handles RMI_L_Indonesian.Click
        Functions.ChangeLanguage("id")
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to Italian
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_Italian_Click(sender As Object, e As EventArgs) Handles RMI_L_Italian.Click
        Functions.ChangeLanguage("it")
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to Japanese
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_Japanese_Click(sender As Object, e As EventArgs) Handles RMI_L_Japanese.Click
        Functions.ChangeLanguage("jp")
    End Sub
End Class