<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ScannerUI
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScannerUI))
        Me.TabControl_Main = New System.Windows.Forms.TabControl()
        Me.TabPage_Setup = New System.Windows.Forms.TabPage()
        Me.LL_Introduction = New System.Windows.Forms.LinkLabel()
        Me.RL_SymbolPath = New System.Windows.Forms.Label()
        Me.RL_DbgPath = New System.Windows.Forms.Label()
        Me.RB_Continue = New System.Windows.Forms.Button()
        Me.StatusStrip_Setup = New System.Windows.Forms.StatusStrip()
        Me.RPBE_StatusProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.RLE_StatusAndInfoLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.RB_SymbolPath_Browse = New System.Windows.Forms.Button()
        Me.RB_DbgPath_Browse = New System.Windows.Forms.Button()
        Me.RTB_SymbolPath = New System.Windows.Forms.TextBox()
        Me.RTB_DbgPath = New System.Windows.Forms.TextBox()
        Me.TabPage_DownloadPDB = New System.Windows.Forms.TabPage()
        Me.RTB_PDBDownloadStatus = New System.Windows.Forms.TextBox()
        Me.RL_DownloadIntroduction = New System.Windows.Forms.Label()
        Me.TabPage_ScanPDB = New System.Windows.Forms.TabPage()
        Me.RL_SymbolFolders = New System.Windows.Forms.Label()
        Me.RL_SymbolFiles = New System.Windows.Forms.Label()
        Me.RL_SymbolSize = New System.Windows.Forms.Label()
        Me.RL_InfoScan = New System.Windows.Forms.Label()
        Me.TabPage_Done = New System.Windows.Forms.TabPage()
        Me.RL_OA = New System.Windows.Forms.Label()
        Me.RB_OA_DeleteSymbolPath = New System.Windows.Forms.Button()
        Me.RB_OA_CopyFeaturesTXT = New System.Windows.Forms.Button()
        Me.RL_Done = New System.Windows.Forms.Label()
        Me.RL_OutputFile = New System.Windows.Forms.Label()
        Me.TabPage_AboutAndSettings = New System.Windows.Forms.TabPage()
        Me.GroupBox_Theming = New System.Windows.Forms.GroupBox()
        Me.CB_UseSystemTheme = New System.Windows.Forms.CheckBox()
        Me.CB_ThemeToggle = New System.Windows.Forms.CheckBox()
        Me.RL_Comments = New System.Windows.Forms.Label()
        Me.RL_ProductName = New System.Windows.Forms.Label()
        Me.RL_Description = New System.Windows.Forms.Label()
        Me.RL_Version = New System.Windows.Forms.Label()
        Me.RL_License = New System.Windows.Forms.Label()
        Me.PB_AppImage = New System.Windows.Forms.PictureBox()
        Me.FSW_SymbolPath = New System.IO.FileSystemWatcher()
        Me.TabControl_Main.SuspendLayout()
        Me.TabPage_Setup.SuspendLayout()
        Me.StatusStrip_Setup.SuspendLayout()
        Me.TabPage_DownloadPDB.SuspendLayout()
        Me.TabPage_ScanPDB.SuspendLayout()
        Me.TabPage_Done.SuspendLayout()
        Me.TabPage_AboutAndSettings.SuspendLayout()
        Me.GroupBox_Theming.SuspendLayout()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FSW_SymbolPath, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl_Main
        '
        Me.TabControl_Main.Controls.Add(Me.TabPage_Setup)
        Me.TabControl_Main.Controls.Add(Me.TabPage_DownloadPDB)
        Me.TabControl_Main.Controls.Add(Me.TabPage_ScanPDB)
        Me.TabControl_Main.Controls.Add(Me.TabPage_Done)
        Me.TabControl_Main.Controls.Add(Me.TabPage_AboutAndSettings)
        Me.TabControl_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Main.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_Main.Name = "TabControl_Main"
        Me.TabControl_Main.SelectedIndex = 0
        Me.TabControl_Main.Size = New System.Drawing.Size(832, 516)
        Me.TabControl_Main.TabIndex = 0
        '
        'TabPage_Setup
        '
        Me.TabPage_Setup.Controls.Add(Me.LL_Introduction)
        Me.TabPage_Setup.Controls.Add(Me.RL_SymbolPath)
        Me.TabPage_Setup.Controls.Add(Me.RL_DbgPath)
        Me.TabPage_Setup.Controls.Add(Me.RB_Continue)
        Me.TabPage_Setup.Controls.Add(Me.StatusStrip_Setup)
        Me.TabPage_Setup.Controls.Add(Me.RB_SymbolPath_Browse)
        Me.TabPage_Setup.Controls.Add(Me.RB_DbgPath_Browse)
        Me.TabPage_Setup.Controls.Add(Me.RTB_SymbolPath)
        Me.TabPage_Setup.Controls.Add(Me.RTB_DbgPath)
        Me.TabPage_Setup.Location = New System.Drawing.Point(4, 25)
        Me.TabPage_Setup.Name = "TabPage_Setup"
        Me.TabPage_Setup.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Setup.Size = New System.Drawing.Size(824, 487)
        Me.TabPage_Setup.TabIndex = 0
        Me.TabPage_Setup.Text = "Introduction and Setup"
        Me.TabPage_Setup.UseVisualStyleBackColor = True
        '
        'LL_Introduction
        '
        Me.LL_Introduction.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.LL_Introduction.Location = New System.Drawing.Point(0, 3)
        Me.LL_Introduction.Name = "LL_Introduction"
        Me.LL_Introduction.Size = New System.Drawing.Size(820, 293)
        Me.LL_Introduction.TabIndex = 6
        Me.LL_Introduction.TabStop = True
        Me.LL_Introduction.Text = "Welcome to ViVeTool GUI - Feature Scanner!" & vbNewLine & vbNewLine & "This tool will help you scan your system for Feature IDs." & vbNewLine & vbNewLine & "To get started, you need to:" & vbNewLine & "1. Install the Windows SDK with Debugging Tools" & vbNewLine & "2. Specify the path to symchk.exe" & vbNewLine & "3. Choose a folder to store the downloaded debug symbols"
        Me.LL_Introduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RL_SymbolPath
        '
        Me.RL_SymbolPath.AutoSize = True
        Me.RL_SymbolPath.Location = New System.Drawing.Point(6, 399)
        Me.RL_SymbolPath.Name = "RL_SymbolPath"
        Me.RL_SymbolPath.Size = New System.Drawing.Size(140, 17)
        Me.RL_SymbolPath.TabIndex = 1
        Me.RL_SymbolPath.Text = "Path to store PDB Files"
        '
        'RL_DbgPath
        '
        Me.RL_DbgPath.AutoSize = True
        Me.RL_DbgPath.Location = New System.Drawing.Point(6, 356)
        Me.RL_DbgPath.Name = "RL_DbgPath"
        Me.RL_DbgPath.Size = New System.Drawing.Size(140, 17)
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
        Me.RB_Continue.UseVisualStyleBackColor = True
        '
        'StatusStrip_Setup
        '
        Me.StatusStrip_Setup.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RPBE_StatusProgressBar, Me.RLE_StatusAndInfoLabel})
        Me.StatusStrip_Setup.Location = New System.Drawing.Point(3, 460)
        Me.StatusStrip_Setup.Name = "StatusStrip_Setup"
        Me.StatusStrip_Setup.Size = New System.Drawing.Size(818, 24)
        Me.StatusStrip_Setup.TabIndex = 4
        '
        'RPBE_StatusProgressBar
        '
        Me.RPBE_StatusProgressBar.Name = "RPBE_StatusProgressBar"
        Me.RPBE_StatusProgressBar.Size = New System.Drawing.Size(100, 18)
        '
        'RLE_StatusAndInfoLabel
        '
        Me.RLE_StatusAndInfoLabel.Name = "RLE_StatusAndInfoLabel"
        Me.RLE_StatusAndInfoLabel.Size = New System.Drawing.Size(0, 19)
        '
        'RB_SymbolPath_Browse
        '
        Me.RB_SymbolPath_Browse.Location = New System.Drawing.Point(718, 394)
        Me.RB_SymbolPath_Browse.Name = "RB_SymbolPath_Browse"
        Me.RB_SymbolPath_Browse.Size = New System.Drawing.Size(96, 28)
        Me.RB_SymbolPath_Browse.TabIndex = 3
        Me.RB_SymbolPath_Browse.Text = "Browse"
        Me.RB_SymbolPath_Browse.UseVisualStyleBackColor = True
        '
        'RB_DbgPath_Browse
        '
        Me.RB_DbgPath_Browse.Location = New System.Drawing.Point(718, 351)
        Me.RB_DbgPath_Browse.Name = "RB_DbgPath_Browse"
        Me.RB_DbgPath_Browse.Size = New System.Drawing.Size(96, 28)
        Me.RB_DbgPath_Browse.TabIndex = 2
        Me.RB_DbgPath_Browse.Text = "Browse"
        Me.RB_DbgPath_Browse.UseVisualStyleBackColor = True
        '
        'RTB_SymbolPath
        '
        Me.RTB_SymbolPath.Location = New System.Drawing.Point(155, 396)
        Me.RTB_SymbolPath.Name = "RTB_SymbolPath"
        Me.RTB_SymbolPath.ReadOnly = True
        Me.RTB_SymbolPath.Size = New System.Drawing.Size(550, 22)
        Me.RTB_SymbolPath.TabIndex = 1
        '
        'RTB_DbgPath
        '
        Me.RTB_DbgPath.Location = New System.Drawing.Point(155, 353)
        Me.RTB_DbgPath.Name = "RTB_DbgPath"
        Me.RTB_DbgPath.ReadOnly = True
        Me.RTB_DbgPath.Size = New System.Drawing.Size(550, 22)
        Me.RTB_DbgPath.TabIndex = 0
        '
        'TabPage_DownloadPDB
        '
        Me.TabPage_DownloadPDB.Controls.Add(Me.RTB_PDBDownloadStatus)
        Me.TabPage_DownloadPDB.Controls.Add(Me.RL_DownloadIntroduction)
        Me.TabPage_DownloadPDB.Enabled = False
        Me.TabPage_DownloadPDB.Location = New System.Drawing.Point(4, 25)
        Me.TabPage_DownloadPDB.Name = "TabPage_DownloadPDB"
        Me.TabPage_DownloadPDB.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_DownloadPDB.Size = New System.Drawing.Size(824, 487)
        Me.TabPage_DownloadPDB.TabIndex = 1
        Me.TabPage_DownloadPDB.Text = "Download Debug Symbols"
        Me.TabPage_DownloadPDB.UseVisualStyleBackColor = True
        '
        'RTB_PDBDownloadStatus
        '
        Me.RTB_PDBDownloadStatus.Location = New System.Drawing.Point(6, 184)
        Me.RTB_PDBDownloadStatus.Multiline = True
        Me.RTB_PDBDownloadStatus.Name = "RTB_PDBDownloadStatus"
        Me.RTB_PDBDownloadStatus.ReadOnly = True
        Me.RTB_PDBDownloadStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.RTB_PDBDownloadStatus.Size = New System.Drawing.Size(808, 285)
        Me.RTB_PDBDownloadStatus.TabIndex = 0
        '
        'RL_DownloadIntroduction
        '
        Me.RL_DownloadIntroduction.Location = New System.Drawing.Point(6, 3)
        Me.RL_DownloadIntroduction.Name = "RL_DownloadIntroduction"
        Me.RL_DownloadIntroduction.Size = New System.Drawing.Size(808, 175)
        Me.RL_DownloadIntroduction.TabIndex = 0
        Me.RL_DownloadIntroduction.Text = "Downloading debug symbols from Microsoft Symbol Server..." & vbNewLine & vbNewLine & "This process may take a while depending on your internet connection." & vbNewLine & "The downloaded symbols can be up to 5-8GB in size."
        '
        'TabPage_ScanPDB
        '
        Me.TabPage_ScanPDB.Controls.Add(Me.RL_SymbolFolders)
        Me.TabPage_ScanPDB.Controls.Add(Me.RL_SymbolFiles)
        Me.TabPage_ScanPDB.Controls.Add(Me.RL_SymbolSize)
        Me.TabPage_ScanPDB.Controls.Add(Me.RL_InfoScan)
        Me.TabPage_ScanPDB.Enabled = False
        Me.TabPage_ScanPDB.Location = New System.Drawing.Point(4, 25)
        Me.TabPage_ScanPDB.Name = "TabPage_ScanPDB"
        Me.TabPage_ScanPDB.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_ScanPDB.Size = New System.Drawing.Size(824, 487)
        Me.TabPage_ScanPDB.TabIndex = 2
        Me.TabPage_ScanPDB.Text = "Scan Debug Symbols for Feature IDs"
        Me.TabPage_ScanPDB.UseVisualStyleBackColor = True
        '
        'RL_SymbolFolders
        '
        Me.RL_SymbolFolders.AutoSize = True
        Me.RL_SymbolFolders.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_SymbolFolders.Location = New System.Drawing.Point(6, 241)
        Me.RL_SymbolFolders.Name = "RL_SymbolFolders"
        Me.RL_SymbolFolders.Size = New System.Drawing.Size(287, 20)
        Me.RL_SymbolFolders.TabIndex = 3
        Me.RL_SymbolFolders.Text = "Total Folders in {My.Settings.SymbolPath}:"
        '
        'RL_SymbolFiles
        '
        Me.RL_SymbolFiles.AutoSize = True
        Me.RL_SymbolFiles.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_SymbolFiles.Location = New System.Drawing.Point(6, 197)
        Me.RL_SymbolFiles.Name = "RL_SymbolFiles"
        Me.RL_SymbolFiles.Size = New System.Drawing.Size(267, 20)
        Me.RL_SymbolFiles.TabIndex = 2
        Me.RL_SymbolFiles.Text = "Total Files in {My.Settings.SymbolPath}:"
        '
        'RL_SymbolSize
        '
        Me.RL_SymbolSize.AutoSize = True
        Me.RL_SymbolSize.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_SymbolSize.Location = New System.Drawing.Point(6, 153)
        Me.RL_SymbolSize.Name = "RL_SymbolSize"
        Me.RL_SymbolSize.Size = New System.Drawing.Size(281, 20)
        Me.RL_SymbolSize.TabIndex = 1
        Me.RL_SymbolSize.Text = "Current size of {My.Settings.SymbolPath}:"
        '
        'RL_InfoScan
        '
        Me.RL_InfoScan.Location = New System.Drawing.Point(6, 3)
        Me.RL_InfoScan.Name = "RL_InfoScan"
        Me.RL_InfoScan.Size = New System.Drawing.Size(571, 90)
        Me.RL_InfoScan.TabIndex = 0
        Me.RL_InfoScan.Text = "Scanning debug symbols for Feature IDs using mach2..." & vbNewLine & vbNewLine & "This process scans the downloaded PDB files to extract Feature ID information."
        '
        'TabPage_Done
        '
        Me.TabPage_Done.Controls.Add(Me.RL_OA)
        Me.TabPage_Done.Controls.Add(Me.RB_OA_DeleteSymbolPath)
        Me.TabPage_Done.Controls.Add(Me.RB_OA_CopyFeaturesTXT)
        Me.TabPage_Done.Controls.Add(Me.RL_Done)
        Me.TabPage_Done.Controls.Add(Me.RL_OutputFile)
        Me.TabPage_Done.Enabled = False
        Me.TabPage_Done.Location = New System.Drawing.Point(4, 25)
        Me.TabPage_Done.Name = "TabPage_Done"
        Me.TabPage_Done.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Done.Size = New System.Drawing.Size(824, 487)
        Me.TabPage_Done.TabIndex = 3
        Me.TabPage_Done.Text = "Done"
        Me.TabPage_Done.UseVisualStyleBackColor = True
        '
        'RL_OA
        '
        Me.RL_OA.AutoSize = True
        Me.RL_OA.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_OA.Location = New System.Drawing.Point(6, 287)
        Me.RL_OA.Name = "RL_OA"
        Me.RL_OA.Size = New System.Drawing.Size(122, 20)
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
        Me.RB_OA_DeleteSymbolPath.UseVisualStyleBackColor = True
        '
        'RB_OA_CopyFeaturesTXT
        '
        Me.RB_OA_CopyFeaturesTXT.Location = New System.Drawing.Point(6, 316)
        Me.RB_OA_CopyFeaturesTXT.Name = "RB_OA_CopyFeaturesTXT"
        Me.RB_OA_CopyFeaturesTXT.Size = New System.Drawing.Size(263, 39)
        Me.RB_OA_CopyFeaturesTXT.TabIndex = 7
        Me.RB_OA_CopyFeaturesTXT.Text = "Copy the Features.txt File to your Desktop"
        Me.RB_OA_CopyFeaturesTXT.UseVisualStyleBackColor = True
        '
        'RL_Done
        '
        Me.RL_Done.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_Done.Location = New System.Drawing.Point(6, 3)
        Me.RL_Done.Name = "RL_Done"
        Me.RL_Done.Size = New System.Drawing.Size(814, 186)
        Me.RL_Done.TabIndex = 6
        Me.RL_Done.Text = "Scan Complete!" & vbNewLine & vbNewLine & "The Feature ID scan has completed successfully." & vbNewLine & "You can now use the generated feature list with ViVeTool GUI."
        '
        'RL_OutputFile
        '
        Me.RL_OutputFile.AutoSize = True
        Me.RL_OutputFile.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_OutputFile.Location = New System.Drawing.Point(6, 227)
        Me.RL_OutputFile.Name = "RL_OutputFile"
        Me.RL_OutputFile.Size = New System.Drawing.Size(89, 20)
        Me.RL_OutputFile.TabIndex = 5
        Me.RL_OutputFile.Text = "Output File: "
        '
        'TabPage_AboutAndSettings
        '
        Me.TabPage_AboutAndSettings.Controls.Add(Me.GroupBox_Theming)
        Me.TabPage_AboutAndSettings.Controls.Add(Me.RL_Comments)
        Me.TabPage_AboutAndSettings.Controls.Add(Me.RL_ProductName)
        Me.TabPage_AboutAndSettings.Controls.Add(Me.RL_Description)
        Me.TabPage_AboutAndSettings.Controls.Add(Me.RL_Version)
        Me.TabPage_AboutAndSettings.Controls.Add(Me.RL_License)
        Me.TabPage_AboutAndSettings.Controls.Add(Me.PB_AppImage)
        Me.TabPage_AboutAndSettings.Location = New System.Drawing.Point(4, 25)
        Me.TabPage_AboutAndSettings.Name = "TabPage_AboutAndSettings"
        Me.TabPage_AboutAndSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_AboutAndSettings.Size = New System.Drawing.Size(824, 487)
        Me.TabPage_AboutAndSettings.TabIndex = 4
        Me.TabPage_AboutAndSettings.Text = "About & Settings"
        Me.TabPage_AboutAndSettings.UseVisualStyleBackColor = True
        '
        'GroupBox_Theming
        '
        Me.GroupBox_Theming.Controls.Add(Me.CB_UseSystemTheme)
        Me.GroupBox_Theming.Controls.Add(Me.CB_ThemeToggle)
        Me.GroupBox_Theming.Location = New System.Drawing.Point(61, 336)
        Me.GroupBox_Theming.Name = "GroupBox_Theming"
        Me.GroupBox_Theming.Size = New System.Drawing.Size(360, 80)
        Me.GroupBox_Theming.TabIndex = 2
        Me.GroupBox_Theming.TabStop = False
        Me.GroupBox_Theming.Text = "Theming"
        '
        'CB_UseSystemTheme
        '
        Me.CB_UseSystemTheme.Appearance = System.Windows.Forms.Appearance.Button
        Me.CB_UseSystemTheme.Image = Global.ViVeTool_GUI.FeatureScanner.My.Resources.Resources.icons8_change_theme_24px
        Me.CB_UseSystemTheme.Location = New System.Drawing.Point(180, 23)
        Me.CB_UseSystemTheme.Name = "CB_UseSystemTheme"
        Me.CB_UseSystemTheme.Size = New System.Drawing.Size(158, 35)
        Me.CB_UseSystemTheme.TabIndex = 5
        Me.CB_UseSystemTheme.Text = "  Use System Theme"
        Me.CB_UseSystemTheme.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CB_UseSystemTheme.UseVisualStyleBackColor = True
        '
        'CB_ThemeToggle
        '
        Me.CB_ThemeToggle.Appearance = System.Windows.Forms.Appearance.Button
        Me.CB_ThemeToggle.Image = Global.ViVeTool_GUI.FeatureScanner.My.Resources.Resources.icons8_sun_24
        Me.CB_ThemeToggle.Location = New System.Drawing.Point(22, 23)
        Me.CB_ThemeToggle.Name = "CB_ThemeToggle"
        Me.CB_ThemeToggle.Size = New System.Drawing.Size(138, 35)
        Me.CB_ThemeToggle.TabIndex = 4
        Me.CB_ThemeToggle.Text = "  Light Theme"
        Me.CB_ThemeToggle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CB_ThemeToggle.UseVisualStyleBackColor = True
        '
        'RL_Comments
        '
        Me.RL_Comments.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_Comments.Location = New System.Drawing.Point(61, 128)
        Me.RL_Comments.Name = "RL_Comments"
        Me.RL_Comments.Size = New System.Drawing.Size(357, 178)
        Me.RL_Comments.TabIndex = 13
        Me.RL_Comments.Text = "Icons used in this Application are from Icons8." & vbNewLine & "ViVeTool-GUI uses ViVe by @thebookisclosed." & vbNewLine & vbNewLine & "For more Information about ViVeTool-GUI, please head to github.com/PeterStrick/ViVeTool-GUI."
        '
        'RL_ProductName
        '
        Me.RL_ProductName.AutoSize = True
        Me.RL_ProductName.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_ProductName.Location = New System.Drawing.Point(60, 3)
        Me.RL_ProductName.Name = "RL_ProductName"
        Me.RL_ProductName.Size = New System.Drawing.Size(273, 20)
        Me.RL_ProductName.TabIndex = 10
        Me.RL_ProductName.Text = "CHANGED AT RUNTIME - ProductName"
        '
        'RL_Description
        '
        Me.RL_Description.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_Description.Location = New System.Drawing.Point(61, 75)
        Me.RL_Description.Name = "RL_Description"
        Me.RL_Description.Size = New System.Drawing.Size(281, 47)
        Me.RL_Description.TabIndex = 12
        Me.RL_Description.Text = "CHANGED AT RUNTIME - Description"
        '
        'RL_Version
        '
        Me.RL_Version.AutoSize = True
        Me.RL_Version.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_Version.Location = New System.Drawing.Point(60, 27)
        Me.RL_Version.Name = "RL_Version"
        Me.RL_Version.Size = New System.Drawing.Size(231, 20)
        Me.RL_Version.TabIndex = 11
        Me.RL_Version.Text = "CHANGED AT RUNTIME - Version"
        '
        'RL_License
        '
        Me.RL_License.AutoSize = True
        Me.RL_License.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.RL_License.Location = New System.Drawing.Point(61, 51)
        Me.RL_License.Name = "RL_License"
        Me.RL_License.Size = New System.Drawing.Size(230, 20)
        Me.RL_License.TabIndex = 14
        Me.RL_License.Text = "CHANGED AT RUNTIME - License"
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
        'FSW_SymbolPath
        '
        Me.FSW_SymbolPath.EnableRaisingEvents = True
        Me.FSW_SymbolPath.Filter = "*.pdb"
        Me.FSW_SymbolPath.IncludeSubdirectories = True
        Me.FSW_SymbolPath.SynchronizingObject = Me
        '
        'ScannerUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 516)
        Me.Controls.Add(Me.TabControl_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ScannerUI"
        Me.Text = "ViVeTool GUI - Feature Scanner"
        Me.TabControl_Main.ResumeLayout(False)
        Me.TabPage_Setup.ResumeLayout(False)
        Me.TabPage_Setup.PerformLayout()
        Me.StatusStrip_Setup.ResumeLayout(False)
        Me.StatusStrip_Setup.PerformLayout()
        Me.TabPage_DownloadPDB.ResumeLayout(False)
        Me.TabPage_DownloadPDB.PerformLayout()
        Me.TabPage_ScanPDB.ResumeLayout(False)
        Me.TabPage_ScanPDB.PerformLayout()
        Me.TabPage_Done.ResumeLayout(False)
        Me.TabPage_Done.PerformLayout()
        Me.TabPage_AboutAndSettings.ResumeLayout(False)
        Me.TabPage_AboutAndSettings.PerformLayout()
        Me.GroupBox_Theming.ResumeLayout(False)
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FSW_SymbolPath, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl_Main As System.Windows.Forms.TabControl
    Friend WithEvents TabPage_Setup As System.Windows.Forms.TabPage
    Friend WithEvents TabPage_DownloadPDB As System.Windows.Forms.TabPage
    Friend WithEvents TabPage_ScanPDB As System.Windows.Forms.TabPage
    Friend WithEvents RB_SymbolPath_Browse As System.Windows.Forms.Button
    Friend WithEvents RB_DbgPath_Browse As System.Windows.Forms.Button
    Friend WithEvents RTB_SymbolPath As System.Windows.Forms.TextBox
    Friend WithEvents RTB_DbgPath As System.Windows.Forms.TextBox
    Friend WithEvents RB_Continue As System.Windows.Forms.Button
    Friend WithEvents StatusStrip_Setup As System.Windows.Forms.StatusStrip
    Friend WithEvents RPBE_StatusProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents RLE_StatusAndInfoLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents RL_SymbolPath As System.Windows.Forms.Label
    Friend WithEvents RL_DbgPath As System.Windows.Forms.Label
    Friend WithEvents LL_Introduction As System.Windows.Forms.LinkLabel
    Friend WithEvents RL_DownloadIntroduction As System.Windows.Forms.Label
    Friend WithEvents FSW_SymbolPath As IO.FileSystemWatcher
    Friend WithEvents RTB_PDBDownloadStatus As System.Windows.Forms.TextBox
    Friend WithEvents RL_SymbolFolders As System.Windows.Forms.Label
    Friend WithEvents RL_SymbolFiles As System.Windows.Forms.Label
    Friend WithEvents RL_SymbolSize As System.Windows.Forms.Label
    Friend WithEvents RL_InfoScan As System.Windows.Forms.Label
    Friend WithEvents RL_OA As System.Windows.Forms.Label
    Friend WithEvents TabPage_Done As System.Windows.Forms.TabPage
    Friend WithEvents RB_OA_DeleteSymbolPath As System.Windows.Forms.Button
    Friend WithEvents RB_OA_CopyFeaturesTXT As System.Windows.Forms.Button
    Friend WithEvents RL_Done As System.Windows.Forms.Label
    Friend WithEvents RL_OutputFile As System.Windows.Forms.Label
    Friend WithEvents TabPage_AboutAndSettings As System.Windows.Forms.TabPage
    Friend WithEvents PB_AppImage As PictureBox
    Friend WithEvents RL_Comments As System.Windows.Forms.Label
    Friend WithEvents RL_ProductName As System.Windows.Forms.Label
    Friend WithEvents RL_Description As System.Windows.Forms.Label
    Friend WithEvents RL_Version As System.Windows.Forms.Label
    Friend WithEvents RL_License As System.Windows.Forms.Label
    Friend WithEvents GroupBox_Theming As System.Windows.Forms.GroupBox
    Friend WithEvents CB_UseSystemTheme As System.Windows.Forms.CheckBox
    Friend WithEvents CB_ThemeToggle As System.Windows.Forms.CheckBox
End Class
