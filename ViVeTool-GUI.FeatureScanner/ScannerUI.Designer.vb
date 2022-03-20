<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ScannerUI
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScannerUI))
        Me.RPV_Main = New Telerik.WinControls.UI.RadPageView()
        Me.RPVP_Setup = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RL_Introduction = New Telerik.WinControls.UI.RadLabel()
        Me.RL_SymbolPath = New Telerik.WinControls.UI.RadLabel()
        Me.RL_DbgPath = New Telerik.WinControls.UI.RadLabel()
        Me.RB_Continue = New Telerik.WinControls.UI.RadButton()
        Me.RSS_Setup = New Telerik.WinControls.UI.RadStatusStrip()
        Me.RPBE_StatusProgressBar = New Telerik.WinControls.UI.RadProgressBarElement()
        Me.RLE_StatusAndInfoLabel = New Telerik.WinControls.UI.RadLabelElement()
        Me.RB_SymbolPath_Browse = New Telerik.WinControls.UI.RadButton()
        Me.RB_DbgPath_Browse = New Telerik.WinControls.UI.RadButton()
        Me.RTB_SymbolPath = New Telerik.WinControls.UI.RadTextBox()
        Me.RTB_DbgPath = New Telerik.WinControls.UI.RadTextBox()
        Me.RPVP_DownloadPDB = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RTB_PDBDownloadStatus = New Telerik.WinControls.UI.RadTextBox()
        Me.RL_DownloadIntroduction = New Telerik.WinControls.UI.RadLabel()
        Me.RPVP_ScanPDB = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RL_SymbolFolders = New Telerik.WinControls.UI.RadLabel()
        Me.RL_SymbolFiles = New Telerik.WinControls.UI.RadLabel()
        Me.RL_SymbolSize = New Telerik.WinControls.UI.RadLabel()
        Me.RL_InfoScan = New Telerik.WinControls.UI.RadLabel()
        Me.RPVP_Done = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RL_OA = New Telerik.WinControls.UI.RadLabel()
        Me.RB_OA_DeleteSymbolPath = New Telerik.WinControls.UI.RadButton()
        Me.RB_OA_CopyFeaturesTXT = New Telerik.WinControls.UI.RadButton()
        Me.RL_Done = New Telerik.WinControls.UI.RadLabel()
        Me.RL_OutputFile = New Telerik.WinControls.UI.RadLabel()
        Me.RPVP_About = New Telerik.WinControls.UI.RadPageViewPage()
        Me.PB_AppImage = New System.Windows.Forms.PictureBox()
        Me.RL_Comments = New Telerik.WinControls.UI.RadLabel()
        Me.RL_ProductName = New Telerik.WinControls.UI.RadLabel()
        Me.RL_Description = New Telerik.WinControls.UI.RadLabel()
        Me.RL_Version = New Telerik.WinControls.UI.RadLabel()
        Me.RL_License = New Telerik.WinControls.UI.RadLabel()
        Me.FSW_SymbolPath = New System.IO.FileSystemWatcher()
        Me.RDA_DoneNotification = New Telerik.WinControls.UI.RadDesktopAlert(Me.components)
        CType(Me.RPV_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RPV_Main.SuspendLayout()
        Me.RPVP_Setup.SuspendLayout()
        CType(Me.RL_Introduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_SymbolPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_DbgPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_Continue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RSS_Setup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_SymbolPath_Browse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_DbgPath_Browse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RTB_SymbolPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RTB_DbgPath, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RPVP_DownloadPDB.SuspendLayout()
        CType(Me.RTB_PDBDownloadStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_DownloadIntroduction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RPVP_ScanPDB.SuspendLayout()
        CType(Me.RL_SymbolFolders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_SymbolFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_SymbolSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_InfoScan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RPVP_Done.SuspendLayout()
        CType(Me.RL_OA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_OA_DeleteSymbolPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_OA_CopyFeaturesTXT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Done, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_OutputFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RPVP_About.SuspendLayout()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Comments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_ProductName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Description, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Version, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_License, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FSW_SymbolPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RPV_Main
        '
        Me.RPV_Main.Controls.Add(Me.RPVP_Setup)
        Me.RPV_Main.Controls.Add(Me.RPVP_DownloadPDB)
        Me.RPV_Main.Controls.Add(Me.RPVP_ScanPDB)
        Me.RPV_Main.Controls.Add(Me.RPVP_Done)
        Me.RPV_Main.Controls.Add(Me.RPVP_About)
        Me.RPV_Main.DefaultPage = Me.RPVP_Setup
        Me.RPV_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RPV_Main.Location = New System.Drawing.Point(0, 0)
        Me.RPV_Main.Name = "RPV_Main"
        Me.RPV_Main.SelectedPage = Me.RPVP_Setup
        Me.RPV_Main.Size = New System.Drawing.Size(832, 516)
        Me.RPV_Main.TabIndex = 0
        CType(Me.RPV_Main.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RPV_Main.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ShowItemCloseButton = False
        '
        'RPVP_Setup
        '
        Me.RPVP_Setup.Controls.Add(Me.RL_Introduction)
        Me.RPVP_Setup.Controls.Add(Me.RL_SymbolPath)
        Me.RPVP_Setup.Controls.Add(Me.RL_DbgPath)
        Me.RPVP_Setup.Controls.Add(Me.RB_Continue)
        Me.RPVP_Setup.Controls.Add(Me.RSS_Setup)
        Me.RPVP_Setup.Controls.Add(Me.RB_SymbolPath_Browse)
        Me.RPVP_Setup.Controls.Add(Me.RB_DbgPath_Browse)
        Me.RPVP_Setup.Controls.Add(Me.RTB_SymbolPath)
        Me.RPVP_Setup.Controls.Add(Me.RTB_DbgPath)
        Me.RPVP_Setup.ItemSize = New System.Drawing.SizeF(141.0!, 29.0!)
        Me.RPVP_Setup.Location = New System.Drawing.Point(6, 35)
        Me.RPVP_Setup.Name = "RPVP_Setup"
        Me.RPVP_Setup.Size = New System.Drawing.Size(820, 475)
        Me.RPVP_Setup.Text = "Introduction and Setup"
        '
        'RL_Introduction
        '
        Me.RL_Introduction.AutoSize = False
        Me.RL_Introduction.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_Introduction.Location = New System.Drawing.Point(0, 3)
        Me.RL_Introduction.Name = "RL_Introduction"
        Me.RL_Introduction.Size = New System.Drawing.Size(820, 293)
        Me.RL_Introduction.TabIndex = 6
        Me.RL_Introduction.Text = resources.GetString("RL_Introduction.Text")
        Me.RL_Introduction.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'RL_SymbolPath
        '
        Me.RL_SymbolPath.Location = New System.Drawing.Point(6, 399)
        Me.RL_SymbolPath.Name = "RL_SymbolPath"
        Me.RL_SymbolPath.Size = New System.Drawing.Size(119, 18)
        Me.RL_SymbolPath.TabIndex = 1
        Me.RL_SymbolPath.Text = "Path to store PDB Files"
        '
        'RL_DbgPath
        '
        Me.RL_DbgPath.Location = New System.Drawing.Point(6, 356)
        Me.RL_DbgPath.Name = "RL_DbgPath"
        Me.RL_DbgPath.Size = New System.Drawing.Size(102, 18)
        Me.RL_DbgPath.TabIndex = 0
        Me.RL_DbgPath.Text = "Path to symchk.exe"
        '
        'RB_Continue
        '
        Me.RB_Continue.Location = New System.Drawing.Point(342, 302)
        Me.RB_Continue.Name = "RB_Continue"
        Me.RB_Continue.Size = New System.Drawing.Size(137, 30)
        Me.RB_Continue.TabIndex = 5
        Me.RB_Continue.Text = "Continue"
        '
        'RSS_Setup
        '
        Me.RSS_Setup.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RPBE_StatusProgressBar, Me.RLE_StatusAndInfoLabel})
        Me.RSS_Setup.Location = New System.Drawing.Point(0, 451)
        Me.RSS_Setup.Name = "RSS_Setup"
        Me.RSS_Setup.Size = New System.Drawing.Size(820, 24)
        Me.RSS_Setup.TabIndex = 4
        '
        'RPBE_StatusProgressBar
        '
        Me.RPBE_StatusProgressBar.Hatch = False
        Me.RPBE_StatusProgressBar.IntegralDash = True
        Me.RPBE_StatusProgressBar.Name = "RPBE_StatusProgressBar"
        Me.RPBE_StatusProgressBar.SeparatorColor1 = System.Drawing.Color.White
        Me.RPBE_StatusProgressBar.SeparatorColor2 = System.Drawing.Color.White
        Me.RPBE_StatusProgressBar.SeparatorColor3 = System.Drawing.Color.White
        Me.RPBE_StatusProgressBar.SeparatorColor4 = System.Drawing.Color.White
        Me.RPBE_StatusProgressBar.SeparatorGradientAngle = 0
        Me.RPBE_StatusProgressBar.SeparatorGradientPercentage1 = 0.4!
        Me.RPBE_StatusProgressBar.SeparatorGradientPercentage2 = 0.6!
        Me.RPBE_StatusProgressBar.SeparatorNumberOfColors = 2
        Me.RSS_Setup.SetSpring(Me.RPBE_StatusProgressBar, False)
        Me.RPBE_StatusProgressBar.StepWidth = 14
        Me.RPBE_StatusProgressBar.SweepAngle = 90
        Me.RPBE_StatusProgressBar.Text = ""
        Me.RPBE_StatusProgressBar.Value1 = 0
        Me.RPBE_StatusProgressBar.Value2 = 0
        '
        'RLE_StatusAndInfoLabel
        '
        Me.RLE_StatusAndInfoLabel.Name = "RLE_StatusAndInfoLabel"
        Me.RSS_Setup.SetSpring(Me.RLE_StatusAndInfoLabel, False)
        Me.RLE_StatusAndInfoLabel.Text = ""
        Me.RLE_StatusAndInfoLabel.TextWrap = True
        '
        'RB_SymbolPath_Browse
        '
        Me.RB_SymbolPath_Browse.Location = New System.Drawing.Point(718, 394)
        Me.RB_SymbolPath_Browse.Name = "RB_SymbolPath_Browse"
        Me.RB_SymbolPath_Browse.Size = New System.Drawing.Size(96, 28)
        Me.RB_SymbolPath_Browse.TabIndex = 3
        Me.RB_SymbolPath_Browse.Text = "Browse"
        '
        'RB_DbgPath_Browse
        '
        Me.RB_DbgPath_Browse.Location = New System.Drawing.Point(718, 351)
        Me.RB_DbgPath_Browse.Name = "RB_DbgPath_Browse"
        Me.RB_DbgPath_Browse.Size = New System.Drawing.Size(96, 28)
        Me.RB_DbgPath_Browse.TabIndex = 2
        Me.RB_DbgPath_Browse.Text = "Browse"
        '
        'RTB_SymbolPath
        '
        Me.RTB_SymbolPath.Location = New System.Drawing.Point(131, 394)
        Me.RTB_SymbolPath.Name = "RTB_SymbolPath"
        Me.RTB_SymbolPath.NullText = "Path to store the downloaded Debugging Symbols to"
        Me.RTB_SymbolPath.ReadOnly = True
        Me.RTB_SymbolPath.ShowNullText = True
        Me.RTB_SymbolPath.Size = New System.Drawing.Size(581, 28)
        Me.RTB_SymbolPath.TabIndex = 1
        '
        'RTB_DbgPath
        '
        Me.RTB_DbgPath.Location = New System.Drawing.Point(131, 351)
        Me.RTB_DbgPath.Name = "RTB_DbgPath"
        Me.RTB_DbgPath.NullText = "Path to symchk.exe from the Windows 10/11 SDK"
        Me.RTB_DbgPath.ReadOnly = True
        Me.RTB_DbgPath.ShowNullText = True
        Me.RTB_DbgPath.Size = New System.Drawing.Size(581, 28)
        Me.RTB_DbgPath.TabIndex = 0
        '
        'RPVP_DownloadPDB
        '
        Me.RPVP_DownloadPDB.Controls.Add(Me.RTB_PDBDownloadStatus)
        Me.RPVP_DownloadPDB.Controls.Add(Me.RL_DownloadIntroduction)
        Me.RPVP_DownloadPDB.Enabled = False
        Me.RPVP_DownloadPDB.ItemSize = New System.Drawing.SizeF(160.0!, 29.0!)
        Me.RPVP_DownloadPDB.Location = New System.Drawing.Point(6, 35)
        Me.RPVP_DownloadPDB.Name = "RPVP_DownloadPDB"
        Me.RPVP_DownloadPDB.Size = New System.Drawing.Size(820, 475)
        Me.RPVP_DownloadPDB.Text = "Download Debug Symbols"
        '
        'RTB_PDBDownloadStatus
        '
        Me.RTB_PDBDownloadStatus.Location = New System.Drawing.Point(6, 184)
        Me.RTB_PDBDownloadStatus.Multiline = True
        Me.RTB_PDBDownloadStatus.Name = "RTB_PDBDownloadStatus"
        Me.RTB_PDBDownloadStatus.ReadOnly = True
        '
        '
        '
        Me.RTB_PDBDownloadStatus.RootElement.StretchVertically = True
        Me.RTB_PDBDownloadStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.RTB_PDBDownloadStatus.Size = New System.Drawing.Size(808, 285)
        Me.RTB_PDBDownloadStatus.TabIndex = 0
        '
        'RL_DownloadIntroduction
        '
        Me.RL_DownloadIntroduction.AutoSize = False
        Me.RL_DownloadIntroduction.Location = New System.Drawing.Point(6, 3)
        Me.RL_DownloadIntroduction.Name = "RL_DownloadIntroduction"
        Me.RL_DownloadIntroduction.Size = New System.Drawing.Size(808, 175)
        Me.RL_DownloadIntroduction.TabIndex = 0
        Me.RL_DownloadIntroduction.Text = resources.GetString("RL_DownloadIntroduction.Text")
        '
        'RPVP_ScanPDB
        '
        Me.RPVP_ScanPDB.Controls.Add(Me.RL_SymbolFolders)
        Me.RPVP_ScanPDB.Controls.Add(Me.RL_SymbolFiles)
        Me.RPVP_ScanPDB.Controls.Add(Me.RL_SymbolSize)
        Me.RPVP_ScanPDB.Controls.Add(Me.RL_InfoScan)
        Me.RPVP_ScanPDB.Enabled = False
        Me.RPVP_ScanPDB.ItemSize = New System.Drawing.SizeF(213.0!, 29.0!)
        Me.RPVP_ScanPDB.Location = New System.Drawing.Point(6, 35)
        Me.RPVP_ScanPDB.Name = "RPVP_ScanPDB"
        Me.RPVP_ScanPDB.Size = New System.Drawing.Size(820, 475)
        Me.RPVP_ScanPDB.Text = "Scan Debug Symbols for Feature IDs"
        '
        'RL_SymbolFolders
        '
        Me.RL_SymbolFolders.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_SymbolFolders.Location = New System.Drawing.Point(6, 241)
        Me.RL_SymbolFolders.Name = "RL_SymbolFolders"
        Me.RL_SymbolFolders.Size = New System.Drawing.Size(287, 23)
        Me.RL_SymbolFolders.TabIndex = 3
        Me.RL_SymbolFolders.Text = "Total Folders in {My.Settings.SymbolPath}:"
        '
        'RL_SymbolFiles
        '
        Me.RL_SymbolFiles.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_SymbolFiles.Location = New System.Drawing.Point(6, 197)
        Me.RL_SymbolFiles.Name = "RL_SymbolFiles"
        Me.RL_SymbolFiles.Size = New System.Drawing.Size(267, 23)
        Me.RL_SymbolFiles.TabIndex = 2
        Me.RL_SymbolFiles.Text = "Total Files in {My.Settings.SymbolPath}:"
        '
        'RL_SymbolSize
        '
        Me.RL_SymbolSize.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_SymbolSize.Location = New System.Drawing.Point(6, 153)
        Me.RL_SymbolSize.Name = "RL_SymbolSize"
        Me.RL_SymbolSize.Size = New System.Drawing.Size(281, 23)
        Me.RL_SymbolSize.TabIndex = 1
        Me.RL_SymbolSize.Text = "Current size of {My.Settings.SymbolPath}:"
        '
        'RL_InfoScan
        '
        Me.RL_InfoScan.Location = New System.Drawing.Point(6, 3)
        Me.RL_InfoScan.Name = "RL_InfoScan"
        Me.RL_InfoScan.Size = New System.Drawing.Size(709, 105)
        Me.RL_InfoScan.TabIndex = 0
        Me.RL_InfoScan.Text = resources.GetString("RL_InfoScan.Text")
        '
        'RPVP_Done
        '
        Me.RPVP_Done.Controls.Add(Me.RL_OA)
        Me.RPVP_Done.Controls.Add(Me.RB_OA_DeleteSymbolPath)
        Me.RPVP_Done.Controls.Add(Me.RB_OA_CopyFeaturesTXT)
        Me.RPVP_Done.Controls.Add(Me.RL_Done)
        Me.RPVP_Done.Controls.Add(Me.RL_OutputFile)
        Me.RPVP_Done.Enabled = False
        Me.RPVP_Done.ItemSize = New System.Drawing.SizeF(44.0!, 29.0!)
        Me.RPVP_Done.Location = New System.Drawing.Point(6, 35)
        Me.RPVP_Done.Name = "RPVP_Done"
        Me.RPVP_Done.Size = New System.Drawing.Size(820, 475)
        Me.RPVP_Done.Text = "Done"
        '
        'RL_OA
        '
        Me.RL_OA.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_OA.Location = New System.Drawing.Point(6, 287)
        Me.RL_OA.Name = "RL_OA"
        Me.RL_OA.Size = New System.Drawing.Size(122, 23)
        Me.RL_OA.TabIndex = 9
        Me.RL_OA.Text = "Optional Actions:"
        '
        'RB_OA_DeleteSymbolPath
        '
        Me.RB_OA_DeleteSymbolPath.Location = New System.Drawing.Point(275, 316)
        Me.RB_OA_DeleteSymbolPath.Name = "RB_OA_DeleteSymbolPath"
        Me.RB_OA_DeleteSymbolPath.Size = New System.Drawing.Size(263, 39)
        Me.RB_OA_DeleteSymbolPath.TabIndex = 8
        Me.RB_OA_DeleteSymbolPath.Text = "Delete {My.Settings.SymbolPath}"
        '
        'RB_OA_CopyFeaturesTXT
        '
        Me.RB_OA_CopyFeaturesTXT.Location = New System.Drawing.Point(6, 316)
        Me.RB_OA_CopyFeaturesTXT.Name = "RB_OA_CopyFeaturesTXT"
        Me.RB_OA_CopyFeaturesTXT.Size = New System.Drawing.Size(263, 39)
        Me.RB_OA_CopyFeaturesTXT.TabIndex = 7
        Me.RB_OA_CopyFeaturesTXT.Text = "Copy the Features.txt File to your Desktop"
        '
        'RL_Done
        '
        Me.RL_Done.AutoSize = False
        Me.RL_Done.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_Done.Location = New System.Drawing.Point(6, 3)
        Me.RL_Done.Name = "RL_Done"
        Me.RL_Done.Size = New System.Drawing.Size(814, 186)
        Me.RL_Done.TabIndex = 6
        Me.RL_Done.Text = resources.GetString("RL_Done.Text")
        '
        'RL_OutputFile
        '
        Me.RL_OutputFile.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_OutputFile.Location = New System.Drawing.Point(6, 227)
        Me.RL_OutputFile.Name = "RL_OutputFile"
        Me.RL_OutputFile.Size = New System.Drawing.Size(89, 23)
        Me.RL_OutputFile.TabIndex = 5
        Me.RL_OutputFile.Text = "Output File: "
        '
        'RPVP_About
        '
        Me.RPVP_About.Controls.Add(Me.PB_AppImage)
        Me.RPVP_About.Controls.Add(Me.RL_Comments)
        Me.RPVP_About.Controls.Add(Me.RL_ProductName)
        Me.RPVP_About.Controls.Add(Me.RL_Description)
        Me.RPVP_About.Controls.Add(Me.RL_Version)
        Me.RPVP_About.Controls.Add(Me.RL_License)
        Me.RPVP_About.ItemSize = New System.Drawing.SizeF(48.0!, 29.0!)
        Me.RPVP_About.Location = New System.Drawing.Point(6, 35)
        Me.RPVP_About.Name = "RPVP_About"
        Me.RPVP_About.Size = New System.Drawing.Size(820, 475)
        Me.RPVP_About.Text = "About"
        '
        'PB_AppImage
        '
        Me.PB_AppImage.Image = CType(resources.GetObject("PB_AppImage.Image"), System.Drawing.Image)
        Me.PB_AppImage.Location = New System.Drawing.Point(6, 3)
        Me.PB_AppImage.Name = "PB_AppImage"
        Me.PB_AppImage.Size = New System.Drawing.Size(48, 48)
        Me.PB_AppImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_AppImage.TabIndex = 9
        Me.PB_AppImage.TabStop = False
        '
        'RL_Comments
        '
        Me.RL_Comments.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_Comments.Location = New System.Drawing.Point(61, 128)
        Me.RL_Comments.Name = "RL_Comments"
        Me.RL_Comments.Size = New System.Drawing.Size(357, 178)
        Me.RL_Comments.TabIndex = 13
        Me.RL_Comments.Text = resources.GetString("RL_Comments.Text")
        Me.RL_Comments.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'RL_ProductName
        '
        Me.RL_ProductName.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_ProductName.Location = New System.Drawing.Point(60, 3)
        Me.RL_ProductName.Name = "RL_ProductName"
        Me.RL_ProductName.Size = New System.Drawing.Size(273, 23)
        Me.RL_ProductName.TabIndex = 10
        Me.RL_ProductName.Text = "CHANGED AT RUNTIME - ProductName"
        Me.RL_ProductName.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'RL_Description
        '
        Me.RL_Description.AutoSize = False
        Me.RL_Description.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_Description.Location = New System.Drawing.Point(61, 75)
        Me.RL_Description.Name = "RL_Description"
        Me.RL_Description.Size = New System.Drawing.Size(281, 47)
        Me.RL_Description.TabIndex = 12
        Me.RL_Description.Text = "CHANGED AT RUNTIME - Description"
        Me.RL_Description.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'RL_Version
        '
        Me.RL_Version.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_Version.Location = New System.Drawing.Point(60, 27)
        Me.RL_Version.Name = "RL_Version"
        Me.RL_Version.Size = New System.Drawing.Size(231, 23)
        Me.RL_Version.TabIndex = 11
        Me.RL_Version.Text = "CHANGED AT RUNTIME - Version"
        Me.RL_Version.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'RL_License
        '
        Me.RL_License.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_License.Location = New System.Drawing.Point(61, 51)
        Me.RL_License.Name = "RL_License"
        Me.RL_License.Size = New System.Drawing.Size(230, 23)
        Me.RL_License.TabIndex = 14
        Me.RL_License.Text = "CHANGED AT RUNTIME - License"
        Me.RL_License.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'FSW_SymbolPath
        '
        Me.FSW_SymbolPath.EnableRaisingEvents = True
        Me.FSW_SymbolPath.Filter = "*.pdb"
        Me.FSW_SymbolPath.IncludeSubdirectories = True
        Me.FSW_SymbolPath.SynchronizingObject = Me
        '
        'RDA_DoneNotification
        '
        Me.RDA_DoneNotification.AutoClose = False
        Me.RDA_DoneNotification.AutoCloseDelay = 0
        Me.RDA_DoneNotification.AutoSize = True
        Me.RDA_DoneNotification.CanMove = False
        Me.RDA_DoneNotification.CaptionText = "Debug Symbol Scan complete"
        Me.RDA_DoneNotification.ContentImage = CType(resources.GetObject("RDA_DoneNotification.ContentImage"), System.Drawing.Image)
        Me.RDA_DoneNotification.ContentText = "The Debug Symbol Scan is complete. Return to the ViVeTool GUI Feature Scanner to " &
    "find out more."
        Me.RDA_DoneNotification.FadeAnimationFrames = 50
        Me.RDA_DoneNotification.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeOut
        Me.RDA_DoneNotification.IsPinned = True
        Me.RDA_DoneNotification.Opacity = 1.0!
        Me.RDA_DoneNotification.PopupAnimationDirection = Telerik.WinControls.UI.RadDirection.Up
        Me.RDA_DoneNotification.ShowOptionsButton = False
        Me.RDA_DoneNotification.ShowPinButton = False
        Me.RDA_DoneNotification.ThemeName = ""
        '
        'ScannerUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 516)
        Me.Controls.Add(Me.RPV_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ScannerUI"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "ViVeTool GUI - Feature Scanner"
        CType(Me.RPV_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RPV_Main.ResumeLayout(False)
        Me.RPVP_Setup.ResumeLayout(False)
        Me.RPVP_Setup.PerformLayout()
        CType(Me.RL_Introduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_SymbolPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_DbgPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_Continue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RSS_Setup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_SymbolPath_Browse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_DbgPath_Browse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RTB_SymbolPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RTB_DbgPath, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RPVP_DownloadPDB.ResumeLayout(False)
        Me.RPVP_DownloadPDB.PerformLayout()
        CType(Me.RTB_PDBDownloadStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_DownloadIntroduction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RPVP_ScanPDB.ResumeLayout(False)
        Me.RPVP_ScanPDB.PerformLayout()
        CType(Me.RL_SymbolFolders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_SymbolFiles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_SymbolSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_InfoScan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RPVP_Done.ResumeLayout(False)
        Me.RPVP_Done.PerformLayout()
        CType(Me.RL_OA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_OA_DeleteSymbolPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_OA_CopyFeaturesTXT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Done, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_OutputFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RPVP_About.ResumeLayout(False)
        Me.RPVP_About.PerformLayout()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Comments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_ProductName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Description, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Version, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_License, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FSW_SymbolPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RPV_Main As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RPVP_Setup As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RPVP_DownloadPDB As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RPVP_ScanPDB As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RB_SymbolPath_Browse As Telerik.WinControls.UI.RadButton
    Friend WithEvents RB_DbgPath_Browse As Telerik.WinControls.UI.RadButton
    Friend WithEvents RTB_SymbolPath As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RTB_DbgPath As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RB_Continue As Telerik.WinControls.UI.RadButton
    Friend WithEvents RSS_Setup As Telerik.WinControls.UI.RadStatusStrip
    Friend WithEvents RPBE_StatusProgressBar As Telerik.WinControls.UI.RadProgressBarElement
    Friend WithEvents RLE_StatusAndInfoLabel As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents RL_SymbolPath As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_DbgPath As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_Introduction As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_DownloadIntroduction As Telerik.WinControls.UI.RadLabel
    Friend WithEvents FSW_SymbolPath As IO.FileSystemWatcher
    Friend WithEvents RTB_PDBDownloadStatus As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RL_SymbolFolders As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_SymbolFiles As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_SymbolSize As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_InfoScan As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_OA As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RPVP_Done As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RB_OA_DeleteSymbolPath As Telerik.WinControls.UI.RadButton
    Friend WithEvents RB_OA_CopyFeaturesTXT As Telerik.WinControls.UI.RadButton
    Friend WithEvents RL_Done As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_OutputFile As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RPVP_About As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RDA_DoneNotification As Telerik.WinControls.UI.RadDesktopAlert
    Friend WithEvents PB_AppImage As PictureBox
    Friend WithEvents RL_Comments As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_ProductName As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_Description As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_Version As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_License As Telerik.WinControls.UI.RadLabel
End Class
