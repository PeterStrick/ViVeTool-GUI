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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScannerUI))
        Me.RPV_Main = New Telerik.WinControls.UI.RadPageView()
        Me.RPVP_Setup = New Telerik.WinControls.UI.RadPageViewPage()
        Me.__DBG_OPTIONS = New Telerik.WinControls.UI.RadDropDownButton()
        Me.__DBG_UnlockAllTabs = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SEP1 = New Telerik.WinControls.UI.RadMenuSeparatorItem()
        Me.__DBG_AddJunctions = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_RemoveJunctions = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SEP2 = New Telerik.WinControls.UI.RadMenuSeparatorItem()
        Me.__DBG_TestSymDownloadedText = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_ScanSymbols = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SEP3 = New Telerik.WinControls.UI.RadMenuSeparatorItem()
        Me.__DBG_CopyFeatureList = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SEP4 = New Telerik.WinControls.UI.RadMenuSeparatorItem()
        Me.__DBG_SendToastNotification = New Telerik.WinControls.UI.RadMenuItem()
        Me.WB_Introduction = New System.Windows.Forms.WebBrowser()
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
        Me.RPVP_AboutAndSettings = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RDDB_Language = New Telerik.WinControls.UI.RadDropDownButton()
        Me.RMI_L_English = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMI_L_German = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMI_L_Polish = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMI_L_Chinese = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMI_L_Indonesian = New Telerik.WinControls.UI.RadMenuItem()
        Me.RGB_Theming = New Telerik.WinControls.UI.RadGroupBox()
        Me.RTB_UseSystemTheme = New Telerik.WinControls.UI.RadToggleButton()
        Me.RTB_ThemeToggle = New Telerik.WinControls.UI.RadToggleButton()
        Me.RL_Comments = New Telerik.WinControls.UI.RadLabel()
        Me.RL_ProductName = New Telerik.WinControls.UI.RadLabel()
        Me.RL_Description = New Telerik.WinControls.UI.RadLabel()
        Me.RL_Version = New Telerik.WinControls.UI.RadLabel()
        Me.RL_License = New Telerik.WinControls.UI.RadLabel()
        Me.PB_AppImage = New System.Windows.Forms.PictureBox()
        Me.FSW_SymbolPath = New System.IO.FileSystemWatcher()
        Me.FluentLight = New Telerik.WinControls.Themes.FluentTheme()
        Me.FluentDark = New Telerik.WinControls.Themes.FluentDarkTheme()
        CType(Me.RPV_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RPV_Main.SuspendLayout()
        Me.RPVP_Setup.SuspendLayout()
        CType(Me.__DBG_OPTIONS, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RPVP_AboutAndSettings.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RDDB_Language, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RGB_Theming, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RGB_Theming.SuspendLayout()
        CType(Me.RTB_UseSystemTheme, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RTB_ThemeToggle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Comments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_ProductName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Description, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Version, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_License, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FSW_SymbolPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RPV_Main
        '
        resources.ApplyResources(Me.RPV_Main, "RPV_Main")
        Me.RPV_Main.Controls.Add(Me.RPVP_Setup)
        Me.RPV_Main.Controls.Add(Me.RPVP_DownloadPDB)
        Me.RPV_Main.Controls.Add(Me.RPVP_ScanPDB)
        Me.RPV_Main.Controls.Add(Me.RPVP_Done)
        Me.RPV_Main.Controls.Add(Me.RPVP_AboutAndSettings)
        Me.RPV_Main.DefaultPage = Me.RPVP_Setup
        Me.RPV_Main.Name = "RPV_Main"
        Me.RPV_Main.SelectedPage = Me.RPVP_Setup
        CType(Me.RPV_Main.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RPV_Main.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ShowItemCloseButton = False
        '
        'RPVP_Setup
        '
        resources.ApplyResources(Me.RPVP_Setup, "RPVP_Setup")
        Me.RPVP_Setup.Controls.Add(Me.__DBG_OPTIONS)
        Me.RPVP_Setup.Controls.Add(Me.WB_Introduction)
        Me.RPVP_Setup.Controls.Add(Me.RL_SymbolPath)
        Me.RPVP_Setup.Controls.Add(Me.RL_DbgPath)
        Me.RPVP_Setup.Controls.Add(Me.RB_Continue)
        Me.RPVP_Setup.Controls.Add(Me.RSS_Setup)
        Me.RPVP_Setup.Controls.Add(Me.RB_SymbolPath_Browse)
        Me.RPVP_Setup.Controls.Add(Me.RB_DbgPath_Browse)
        Me.RPVP_Setup.Controls.Add(Me.RTB_SymbolPath)
        Me.RPVP_Setup.Controls.Add(Me.RTB_DbgPath)
        Me.RPVP_Setup.ItemSize = New System.Drawing.SizeF(141.0!, 29.0!)
        Me.RPVP_Setup.Name = "RPVP_Setup"
        '
        '__DBG_OPTIONS
        '
        resources.ApplyResources(Me.__DBG_OPTIONS, "__DBG_OPTIONS")
        Me.__DBG_OPTIONS.Items.AddRange(New Telerik.WinControls.RadItem() {Me.__DBG_UnlockAllTabs, Me.__DBG_SEP1, Me.__DBG_AddJunctions, Me.__DBG_RemoveJunctions, Me.__DBG_SEP2, Me.__DBG_TestSymDownloadedText, Me.__DBG_ScanSymbols, Me.__DBG_SEP3, Me.__DBG_CopyFeatureList, Me.__DBG_SEP4, Me.__DBG_SendToastNotification})
        Me.__DBG_OPTIONS.Name = "__DBG_OPTIONS"
        Me.__DBG_OPTIONS.TabStop = False
        '
        '__DBG_UnlockAllTabs
        '
        resources.ApplyResources(Me.__DBG_UnlockAllTabs, "__DBG_UnlockAllTabs")
        Me.__DBG_UnlockAllTabs.Name = "__DBG_UnlockAllTabs"
        '
        '__DBG_SEP1
        '
        resources.ApplyResources(Me.__DBG_SEP1, "__DBG_SEP1")
        Me.__DBG_SEP1.Name = "__DBG_SEP1"
        '
        '__DBG_AddJunctions
        '
        resources.ApplyResources(Me.__DBG_AddJunctions, "__DBG_AddJunctions")
        Me.__DBG_AddJunctions.Name = "__DBG_AddJunctions"
        '
        '__DBG_RemoveJunctions
        '
        resources.ApplyResources(Me.__DBG_RemoveJunctions, "__DBG_RemoveJunctions")
        Me.__DBG_RemoveJunctions.Name = "__DBG_RemoveJunctions"
        '
        '__DBG_SEP2
        '
        resources.ApplyResources(Me.__DBG_SEP2, "__DBG_SEP2")
        Me.__DBG_SEP2.Name = "__DBG_SEP2"
        '
        '__DBG_TestSymDownloadedText
        '
        resources.ApplyResources(Me.__DBG_TestSymDownloadedText, "__DBG_TestSymDownloadedText")
        Me.__DBG_TestSymDownloadedText.Name = "__DBG_TestSymDownloadedText"
        '
        '__DBG_ScanSymbols
        '
        resources.ApplyResources(Me.__DBG_ScanSymbols, "__DBG_ScanSymbols")
        Me.__DBG_ScanSymbols.Name = "__DBG_ScanSymbols"
        '
        '__DBG_SEP3
        '
        resources.ApplyResources(Me.__DBG_SEP3, "__DBG_SEP3")
        Me.__DBG_SEP3.Name = "__DBG_SEP3"
        '
        '__DBG_CopyFeatureList
        '
        resources.ApplyResources(Me.__DBG_CopyFeatureList, "__DBG_CopyFeatureList")
        Me.__DBG_CopyFeatureList.Name = "__DBG_CopyFeatureList"
        '
        '__DBG_SEP4
        '
        resources.ApplyResources(Me.__DBG_SEP4, "__DBG_SEP4")
        Me.__DBG_SEP4.Name = "__DBG_SEP4"
        '
        '__DBG_SendToastNotification
        '
        resources.ApplyResources(Me.__DBG_SendToastNotification, "__DBG_SendToastNotification")
        Me.__DBG_SendToastNotification.Name = "__DBG_SendToastNotification"
        '
        'WB_Introduction
        '
        resources.ApplyResources(Me.WB_Introduction, "WB_Introduction")
        Me.WB_Introduction.AllowNavigation = False
        Me.WB_Introduction.AllowWebBrowserDrop = False
        Me.WB_Introduction.IsWebBrowserContextMenuEnabled = False
        Me.WB_Introduction.Name = "WB_Introduction"
        Me.WB_Introduction.TabStop = False
        Me.WB_Introduction.Url = New System.Uri("", System.UriKind.Relative)
        Me.WB_Introduction.WebBrowserShortcutsEnabled = False
        '
        'RL_SymbolPath
        '
        resources.ApplyResources(Me.RL_SymbolPath, "RL_SymbolPath")
        Me.RL_SymbolPath.Name = "RL_SymbolPath"
        '
        'RL_DbgPath
        '
        resources.ApplyResources(Me.RL_DbgPath, "RL_DbgPath")
        Me.RL_DbgPath.Name = "RL_DbgPath"
        '
        'RB_Continue
        '
        resources.ApplyResources(Me.RB_Continue, "RB_Continue")
        Me.RB_Continue.Name = "RB_Continue"
        '
        'RSS_Setup
        '
        resources.ApplyResources(Me.RSS_Setup, "RSS_Setup")
        Me.RSS_Setup.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RPBE_StatusProgressBar, Me.RLE_StatusAndInfoLabel})
        Me.RSS_Setup.Name = "RSS_Setup"
        '
        'RPBE_StatusProgressBar
        '
        resources.ApplyResources(Me.RPBE_StatusProgressBar, "RPBE_StatusProgressBar")
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
        Me.RPBE_StatusProgressBar.Value1 = 0
        Me.RPBE_StatusProgressBar.Value2 = 0
        '
        'RLE_StatusAndInfoLabel
        '
        resources.ApplyResources(Me.RLE_StatusAndInfoLabel, "RLE_StatusAndInfoLabel")
        Me.RLE_StatusAndInfoLabel.Name = "RLE_StatusAndInfoLabel"
        Me.RSS_Setup.SetSpring(Me.RLE_StatusAndInfoLabel, False)
        Me.RLE_StatusAndInfoLabel.TextWrap = True
        '
        'RB_SymbolPath_Browse
        '
        resources.ApplyResources(Me.RB_SymbolPath_Browse, "RB_SymbolPath_Browse")
        Me.RB_SymbolPath_Browse.Name = "RB_SymbolPath_Browse"
        '
        'RB_DbgPath_Browse
        '
        resources.ApplyResources(Me.RB_DbgPath_Browse, "RB_DbgPath_Browse")
        Me.RB_DbgPath_Browse.Name = "RB_DbgPath_Browse"
        '
        'RTB_SymbolPath
        '
        resources.ApplyResources(Me.RTB_SymbolPath, "RTB_SymbolPath")
        Me.RTB_SymbolPath.Name = "RTB_SymbolPath"
        Me.RTB_SymbolPath.ReadOnly = True
        Me.RTB_SymbolPath.ShowNullText = True
        '
        'RTB_DbgPath
        '
        resources.ApplyResources(Me.RTB_DbgPath, "RTB_DbgPath")
        Me.RTB_DbgPath.Name = "RTB_DbgPath"
        Me.RTB_DbgPath.ReadOnly = True
        Me.RTB_DbgPath.ShowNullText = True
        '
        'RPVP_DownloadPDB
        '
        resources.ApplyResources(Me.RPVP_DownloadPDB, "RPVP_DownloadPDB")
        Me.RPVP_DownloadPDB.Controls.Add(Me.RTB_PDBDownloadStatus)
        Me.RPVP_DownloadPDB.Controls.Add(Me.RL_DownloadIntroduction)
        Me.RPVP_DownloadPDB.ItemSize = New System.Drawing.SizeF(160.0!, 29.0!)
        Me.RPVP_DownloadPDB.Name = "RPVP_DownloadPDB"
        '
        'RTB_PDBDownloadStatus
        '
        resources.ApplyResources(Me.RTB_PDBDownloadStatus, "RTB_PDBDownloadStatus")
        Me.RTB_PDBDownloadStatus.Multiline = True
        Me.RTB_PDBDownloadStatus.Name = "RTB_PDBDownloadStatus"
        Me.RTB_PDBDownloadStatus.ReadOnly = True
        '
        '
        '
        Me.RTB_PDBDownloadStatus.RootElement.StretchVertically = True
        Me.RTB_PDBDownloadStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        '
        'RL_DownloadIntroduction
        '
        resources.ApplyResources(Me.RL_DownloadIntroduction, "RL_DownloadIntroduction")
        Me.RL_DownloadIntroduction.Name = "RL_DownloadIntroduction"
        '
        'RPVP_ScanPDB
        '
        resources.ApplyResources(Me.RPVP_ScanPDB, "RPVP_ScanPDB")
        Me.RPVP_ScanPDB.Controls.Add(Me.RL_SymbolFolders)
        Me.RPVP_ScanPDB.Controls.Add(Me.RL_SymbolFiles)
        Me.RPVP_ScanPDB.Controls.Add(Me.RL_SymbolSize)
        Me.RPVP_ScanPDB.Controls.Add(Me.RL_InfoScan)
        Me.RPVP_ScanPDB.ItemSize = New System.Drawing.SizeF(213.0!, 29.0!)
        Me.RPVP_ScanPDB.Name = "RPVP_ScanPDB"
        '
        'RL_SymbolFolders
        '
        resources.ApplyResources(Me.RL_SymbolFolders, "RL_SymbolFolders")
        Me.RL_SymbolFolders.Name = "RL_SymbolFolders"
        '
        'RL_SymbolFiles
        '
        resources.ApplyResources(Me.RL_SymbolFiles, "RL_SymbolFiles")
        Me.RL_SymbolFiles.Name = "RL_SymbolFiles"
        '
        'RL_SymbolSize
        '
        resources.ApplyResources(Me.RL_SymbolSize, "RL_SymbolSize")
        Me.RL_SymbolSize.Name = "RL_SymbolSize"
        '
        'RL_InfoScan
        '
        resources.ApplyResources(Me.RL_InfoScan, "RL_InfoScan")
        Me.RL_InfoScan.Name = "RL_InfoScan"
        '
        'RPVP_Done
        '
        resources.ApplyResources(Me.RPVP_Done, "RPVP_Done")
        Me.RPVP_Done.Controls.Add(Me.RL_OA)
        Me.RPVP_Done.Controls.Add(Me.RB_OA_DeleteSymbolPath)
        Me.RPVP_Done.Controls.Add(Me.RB_OA_CopyFeaturesTXT)
        Me.RPVP_Done.Controls.Add(Me.RL_Done)
        Me.RPVP_Done.Controls.Add(Me.RL_OutputFile)
        Me.RPVP_Done.ItemSize = New System.Drawing.SizeF(44.0!, 29.0!)
        Me.RPVP_Done.Name = "RPVP_Done"
        '
        'RL_OA
        '
        resources.ApplyResources(Me.RL_OA, "RL_OA")
        Me.RL_OA.Name = "RL_OA"
        '
        'RB_OA_DeleteSymbolPath
        '
        resources.ApplyResources(Me.RB_OA_DeleteSymbolPath, "RB_OA_DeleteSymbolPath")
        Me.RB_OA_DeleteSymbolPath.Name = "RB_OA_DeleteSymbolPath"
        '
        'RB_OA_CopyFeaturesTXT
        '
        resources.ApplyResources(Me.RB_OA_CopyFeaturesTXT, "RB_OA_CopyFeaturesTXT")
        Me.RB_OA_CopyFeaturesTXT.Name = "RB_OA_CopyFeaturesTXT"
        '
        'RL_Done
        '
        resources.ApplyResources(Me.RL_Done, "RL_Done")
        Me.RL_Done.Name = "RL_Done"
        '
        'RL_OutputFile
        '
        resources.ApplyResources(Me.RL_OutputFile, "RL_OutputFile")
        Me.RL_OutputFile.Name = "RL_OutputFile"
        '
        'RPVP_AboutAndSettings
        '
        resources.ApplyResources(Me.RPVP_AboutAndSettings, "RPVP_AboutAndSettings")
        Me.RPVP_AboutAndSettings.Controls.Add(Me.RadGroupBox1)
        Me.RPVP_AboutAndSettings.Controls.Add(Me.RGB_Theming)
        Me.RPVP_AboutAndSettings.Controls.Add(Me.RL_Comments)
        Me.RPVP_AboutAndSettings.Controls.Add(Me.RL_ProductName)
        Me.RPVP_AboutAndSettings.Controls.Add(Me.RL_Description)
        Me.RPVP_AboutAndSettings.Controls.Add(Me.RL_Version)
        Me.RPVP_AboutAndSettings.Controls.Add(Me.RL_License)
        Me.RPVP_AboutAndSettings.Controls.Add(Me.PB_AppImage)
        Me.RPVP_AboutAndSettings.ItemSize = New System.Drawing.SizeF(109.0!, 29.0!)
        Me.RPVP_AboutAndSettings.Name = "RPVP_AboutAndSettings"
        '
        'RadGroupBox1
        '
        resources.ApplyResources(Me.RadGroupBox1, "RadGroupBox1")
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RDDB_Language)
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        'RDDB_Language
        '
        resources.ApplyResources(Me.RDDB_Language, "RDDB_Language")
        Me.RDDB_Language.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMI_L_English, Me.RMI_L_German, Me.RMI_L_Polish, Me.RMI_L_Chinese, Me.RMI_L_Indonesian})
        Me.RDDB_Language.Name = "RDDB_Language"
        '
        'RMI_L_English
        '
        resources.ApplyResources(Me.RMI_L_English, "RMI_L_English")
        Me.RMI_L_English.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RMI_L_English.Name = "RMI_L_English"
        Me.RMI_L_English.SvgImageXml = resources.GetString("RMI_L_English.SvgImageXml")
        Me.RMI_L_English.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.RMI_L_English.UseCompatibleTextRendering = False
        '
        'RMI_L_German
        '
        resources.ApplyResources(Me.RMI_L_German, "RMI_L_German")
        Me.RMI_L_German.Name = "RMI_L_German"
        Me.RMI_L_German.SvgImageXml = resources.GetString("RMI_L_German.SvgImageXml")
        Me.RMI_L_German.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.RMI_L_German.UseCompatibleTextRendering = False
        '
        'RMI_L_Polish
        '
        resources.ApplyResources(Me.RMI_L_Polish, "RMI_L_Polish")
        Me.RMI_L_Polish.Name = "RMI_L_Polish"
        Me.RMI_L_Polish.SvgImageXml = resources.GetString("RMI_L_Polish.SvgImageXml")
        Me.RMI_L_Polish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.RMI_L_Polish.UseCompatibleTextRendering = False
        '
        'RMI_L_Chinese
        '
        resources.ApplyResources(Me.RMI_L_Chinese, "RMI_L_Chinese")
        Me.RMI_L_Chinese.Name = "RMI_L_Chinese"
        Me.RMI_L_Chinese.SvgImageXml = resources.GetString("RMI_L_Chinese.SvgImageXml")
        Me.RMI_L_Chinese.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.RMI_L_Chinese.UseCompatibleTextRendering = False
        '
        'RMI_L_Indonesian
        '
        resources.ApplyResources(Me.RMI_L_Indonesian, "RMI_L_Indonesian")
        Me.RMI_L_Indonesian.Name = "RMI_L_Indonesian"
        Me.RMI_L_Indonesian.SvgImageXml = resources.GetString("RMI_L_Indonesian.SvgImageXml")
        Me.RMI_L_Indonesian.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.RMI_L_Indonesian.UseCompatibleTextRendering = False
        '
        'RGB_Theming
        '
        resources.ApplyResources(Me.RGB_Theming, "RGB_Theming")
        Me.RGB_Theming.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RGB_Theming.Controls.Add(Me.RTB_UseSystemTheme)
        Me.RGB_Theming.Controls.Add(Me.RTB_ThemeToggle)
        Me.RGB_Theming.HeaderMargin = New System.Windows.Forms.Padding(1)
        Me.RGB_Theming.Name = "RGB_Theming"
        '
        'RTB_UseSystemTheme
        '
        resources.ApplyResources(Me.RTB_UseSystemTheme, "RTB_UseSystemTheme")
        Me.RTB_UseSystemTheme.Image = Global.ViVeTool_GUI.FeatureScanner.My.Resources.Resources.icons8_change_theme_24px
        Me.RTB_UseSystemTheme.Name = "RTB_UseSystemTheme"
        Me.RTB_UseSystemTheme.ThemeName = "Fluent"
        '
        'RTB_ThemeToggle
        '
        resources.ApplyResources(Me.RTB_ThemeToggle, "RTB_ThemeToggle")
        Me.RTB_ThemeToggle.Image = Global.ViVeTool_GUI.FeatureScanner.My.Resources.Resources.icons8_sun_24
        Me.RTB_ThemeToggle.Name = "RTB_ThemeToggle"
        Me.RTB_ThemeToggle.ThemeName = "Fluent"
        '
        'RL_Comments
        '
        resources.ApplyResources(Me.RL_Comments, "RL_Comments")
        Me.RL_Comments.Name = "RL_Comments"
        '
        'RL_ProductName
        '
        resources.ApplyResources(Me.RL_ProductName, "RL_ProductName")
        Me.RL_ProductName.Name = "RL_ProductName"
        '
        'RL_Description
        '
        resources.ApplyResources(Me.RL_Description, "RL_Description")
        Me.RL_Description.Name = "RL_Description"
        '
        'RL_Version
        '
        resources.ApplyResources(Me.RL_Version, "RL_Version")
        Me.RL_Version.Name = "RL_Version"
        '
        'RL_License
        '
        resources.ApplyResources(Me.RL_License, "RL_License")
        Me.RL_License.Name = "RL_License"
        '
        'PB_AppImage
        '
        resources.ApplyResources(Me.PB_AppImage, "PB_AppImage")
        Me.PB_AppImage.Name = "PB_AppImage"
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
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RPV_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "ScannerUI"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        CType(Me.RPV_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RPV_Main.ResumeLayout(False)
        Me.RPVP_Setup.ResumeLayout(False)
        Me.RPVP_Setup.PerformLayout()
        CType(Me.__DBG_OPTIONS, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.RPVP_AboutAndSettings.ResumeLayout(False)
        Me.RPVP_AboutAndSettings.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RDDB_Language, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RGB_Theming, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RGB_Theming.ResumeLayout(False)
        CType(Me.RTB_UseSystemTheme, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RTB_ThemeToggle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Comments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_ProductName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Description, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Version, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_License, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RPVP_AboutAndSettings As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents PB_AppImage As PictureBox
    Friend WithEvents RL_Comments As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_ProductName As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_Description As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_Version As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_License As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RGB_Theming As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RTB_UseSystemTheme As Telerik.WinControls.UI.RadToggleButton
    Friend WithEvents RTB_ThemeToggle As Telerik.WinControls.UI.RadToggleButton
    Friend WithEvents FluentLight As Telerik.WinControls.Themes.FluentTheme
    Friend WithEvents FluentDark As Telerik.WinControls.Themes.FluentDarkTheme
    Friend WithEvents WB_Introduction As WebBrowser
    Friend WithEvents __DBG_OPTIONS As Telerik.WinControls.UI.RadDropDownButton
    Friend WithEvents __DBG_RemoveJunctions As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_ScanSymbols As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_TestSymDownloadedText As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_UnlockAllTabs As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_SEP1 As Telerik.WinControls.UI.RadMenuSeparatorItem
    Friend WithEvents __DBG_AddJunctions As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_SEP2 As Telerik.WinControls.UI.RadMenuSeparatorItem
    Friend WithEvents __DBG_SEP3 As Telerik.WinControls.UI.RadMenuSeparatorItem
    Friend WithEvents __DBG_CopyFeatureList As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_SEP4 As Telerik.WinControls.UI.RadMenuSeparatorItem
    Friend WithEvents __DBG_SendToastNotification As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RDDB_Language As Telerik.WinControls.UI.RadDropDownButton
    Friend WithEvents RMI_L_English As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMI_L_German As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMI_L_Polish As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMI_L_Chinese As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMI_L_Indonesian As Telerik.WinControls.UI.RadMenuItem
End Class
