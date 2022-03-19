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
        Me.RL_Introduction = New Telerik.WinControls.UI.RadLabel()
        Me.RL_SymbolPath = New Telerik.WinControls.UI.RadLabel()
        Me.RL_DbgPath = New Telerik.WinControls.UI.RadLabel()
        Me.RB_Continue = New Telerik.WinControls.UI.RadButton()
        Me.RadStatusStrip1 = New Telerik.WinControls.UI.RadStatusStrip()
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
        Me.FSW_SymbolPath = New System.IO.FileSystemWatcher()
        CType(Me.RPV_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RPV_Main.SuspendLayout()
        Me.RPVP_Setup.SuspendLayout()
        CType(Me.RL_Introduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_SymbolPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_DbgPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_Continue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadStatusStrip1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_SymbolPath_Browse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_DbgPath_Browse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RTB_SymbolPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RTB_DbgPath, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RPVP_DownloadPDB.SuspendLayout()
        CType(Me.RTB_PDBDownloadStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_DownloadIntroduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FSW_SymbolPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RPV_Main
        '
        Me.RPV_Main.Controls.Add(Me.RPVP_Setup)
        Me.RPV_Main.Controls.Add(Me.RPVP_DownloadPDB)
        Me.RPV_Main.Controls.Add(Me.RPVP_ScanPDB)
        Me.RPV_Main.DefaultPage = Me.RPVP_Setup
        Me.RPV_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RPV_Main.Location = New System.Drawing.Point(0, 0)
        Me.RPV_Main.Name = "RPV_Main"
        Me.RPV_Main.SelectedPage = Me.RPVP_DownloadPDB
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
        Me.RPVP_Setup.Controls.Add(Me.RadStatusStrip1)
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
        'RadStatusStrip1
        '
        Me.RadStatusStrip1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RPBE_StatusProgressBar, Me.RLE_StatusAndInfoLabel})
        Me.RadStatusStrip1.Location = New System.Drawing.Point(0, 451)
        Me.RadStatusStrip1.Name = "RadStatusStrip1"
        Me.RadStatusStrip1.Size = New System.Drawing.Size(820, 24)
        Me.RadStatusStrip1.TabIndex = 4
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
        Me.RadStatusStrip1.SetSpring(Me.RPBE_StatusProgressBar, False)
        Me.RPBE_StatusProgressBar.StepWidth = 14
        Me.RPBE_StatusProgressBar.SweepAngle = 90
        Me.RPBE_StatusProgressBar.Text = ""
        Me.RPBE_StatusProgressBar.Value1 = 0
        Me.RPBE_StatusProgressBar.Value2 = 0
        '
        'RLE_StatusAndInfoLabel
        '
        Me.RLE_StatusAndInfoLabel.Name = "RLE_StatusAndInfoLabel"
        Me.RadStatusStrip1.SetSpring(Me.RLE_StatusAndInfoLabel, False)
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
        Me.RPVP_ScanPDB.Enabled = False
        Me.RPVP_ScanPDB.ItemSize = New System.Drawing.SizeF(213.0!, 29.0!)
        Me.RPVP_ScanPDB.Location = New System.Drawing.Point(6, 35)
        Me.RPVP_ScanPDB.Name = "RPVP_ScanPDB"
        Me.RPVP_ScanPDB.Size = New System.Drawing.Size(820, 475)
        Me.RPVP_ScanPDB.Text = "Scan Debug Symbols for Feature IDs"
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
        Me.Controls.Add(Me.RPV_Main)
        Me.Name = "ScannerUI"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "[VERSION 1.6 PRE-RELEASE 2] ViVeTool GUI - Feature Scanner"
        CType(Me.RPV_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RPV_Main.ResumeLayout(False)
        Me.RPVP_Setup.ResumeLayout(False)
        Me.RPVP_Setup.PerformLayout()
        CType(Me.RL_Introduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_SymbolPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_DbgPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_Continue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadStatusStrip1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_SymbolPath_Browse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_DbgPath_Browse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RTB_SymbolPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RTB_DbgPath, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RPVP_DownloadPDB.ResumeLayout(False)
        Me.RPVP_DownloadPDB.PerformLayout()
        CType(Me.RTB_PDBDownloadStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_DownloadIntroduction, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadStatusStrip1 As Telerik.WinControls.UI.RadStatusStrip
    Friend WithEvents RPBE_StatusProgressBar As Telerik.WinControls.UI.RadProgressBarElement
    Friend WithEvents RLE_StatusAndInfoLabel As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents RL_SymbolPath As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_DbgPath As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_Introduction As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_DownloadIntroduction As Telerik.WinControls.UI.RadLabel
    Friend WithEvents FSW_SymbolPath As IO.FileSystemWatcher
    Friend WithEvents RTB_PDBDownloadStatus As Telerik.WinControls.UI.RadTextBox
End Class
