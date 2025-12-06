<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AboutAndSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutAndSettings))
        Me.TabControl_Main = New System.Windows.Forms.TabControl()
        Me.TabPage_About = New System.Windows.Forms.TabPage()
        Me.PB_AppImage = New System.Windows.Forms.PictureBox()
        Me.RL_Comments = New System.Windows.Forms.Label()
        Me.RL_ProductName = New System.Windows.Forms.Label()
        Me.RL_Description = New System.Windows.Forms.Label()
        Me.RL_Version = New System.Windows.Forms.Label()
        Me.RL_License = New System.Windows.Forms.Label()
        Me.TabPage_Settings = New System.Windows.Forms.TabPage()
        Me.GroupBox_Other = New System.Windows.Forms.GroupBox()
        Me.RB_ViVeTool_GUI_FeatureScanner = New System.Windows.Forms.Button()
        Me.GroupBox_Theming = New System.Windows.Forms.GroupBox()
        Me.CB_UseSystemTheme = New System.Windows.Forms.CheckBox()
        Me.CB_ThemeToggle = New System.Windows.Forms.CheckBox()
        Me.GroupBox_Behaviour = New System.Windows.Forms.GroupBox()
        Me.RL_AutoLoad = New System.Windows.Forms.Label()
        Me.CB_AutoLoad = New System.Windows.Forms.CheckBox()
        Me.TabControl_Main.SuspendLayout()
        Me.TabPage_About.SuspendLayout()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_Settings.SuspendLayout()
        Me.GroupBox_Other.SuspendLayout()
        Me.GroupBox_Theming.SuspendLayout()
        Me.GroupBox_Behaviour.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl_Main
        '
        Me.TabControl_Main.Controls.Add(Me.TabPage_About)
        Me.TabControl_Main.Controls.Add(Me.TabPage_Settings)
        Me.TabControl_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Main.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_Main.Name = "TabControl_Main"
        Me.TabControl_Main.SelectedIndex = 0
        Me.TabControl_Main.Size = New System.Drawing.Size(450, 360)
        Me.TabControl_Main.TabIndex = 0
        '
        'TabPage_About
        '
        Me.TabPage_About.Controls.Add(Me.PB_AppImage)
        Me.TabPage_About.Controls.Add(Me.RL_Comments)
        Me.TabPage_About.Controls.Add(Me.RL_ProductName)
        Me.TabPage_About.Controls.Add(Me.RL_Description)
        Me.TabPage_About.Controls.Add(Me.RL_Version)
        Me.TabPage_About.Controls.Add(Me.RL_License)
        Me.TabPage_About.Location = New System.Drawing.Point(4, 25)
        Me.TabPage_About.Name = "TabPage_About"
        Me.TabPage_About.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_About.Size = New System.Drawing.Size(442, 331)
        Me.TabPage_About.TabIndex = 0
        Me.TabPage_About.Text = "  About ViVeTool-GUI"
        Me.TabPage_About.UseVisualStyleBackColor = True
        '
        'PB_AppImage
        '
        Me.PB_AppImage.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_advertisement_page_96
        Me.PB_AppImage.Location = New System.Drawing.Point(6, 12)
        Me.PB_AppImage.Name = "PB_AppImage"
        Me.PB_AppImage.Size = New System.Drawing.Size(48, 48)
        Me.PB_AppImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_AppImage.TabIndex = 4
        Me.PB_AppImage.TabStop = False
        '
        'RL_Comments
        '
        Me.RL_Comments.Location = New System.Drawing.Point(61, 137)
        Me.RL_Comments.Name = "RL_Comments"
        Me.RL_Comments.Size = New System.Drawing.Size(340, 180)
        Me.RL_Comments.TabIndex = 8
        Me.RL_Comments.Text = "Icons used in this Application are from Icons8. Font Awesome Icons used when the" &
    "re isn't an Icons8 Icon available." & vbNewLine & "ViVeTool-GUI uses ViVe by @thebookisclosed." & vbNewLine & vbNewLine &
    "For more Information about ViVeTool-GUI, please head to github.com/PeterStrick/ViVeTool-GUI." & vbNewLine & vbNewLine &
    "ViVeTool-GUI uses CrashReporter.NET, AutoUpdater.NET and Newtonsoft JSON."
        '
        'RL_ProductName
        '
        Me.RL_ProductName.AutoSize = True
        Me.RL_ProductName.Location = New System.Drawing.Point(60, 12)
        Me.RL_ProductName.Name = "RL_ProductName"
        Me.RL_ProductName.Size = New System.Drawing.Size(206, 17)
        Me.RL_ProductName.TabIndex = 5
        Me.RL_ProductName.Text = "CHANGED AT RUNTIME - ProductName"
        '
        'RL_Description
        '
        Me.RL_Description.Location = New System.Drawing.Point(61, 84)
        Me.RL_Description.Name = "RL_Description"
        Me.RL_Description.Size = New System.Drawing.Size(340, 47)
        Me.RL_Description.TabIndex = 7
        Me.RL_Description.Text = "CHANGED AT RUNTIME - Description"
        '
        'RL_Version
        '
        Me.RL_Version.AutoSize = True
        Me.RL_Version.Location = New System.Drawing.Point(60, 36)
        Me.RL_Version.Name = "RL_Version"
        Me.RL_Version.Size = New System.Drawing.Size(174, 17)
        Me.RL_Version.TabIndex = 6
        Me.RL_Version.Text = "CHANGED AT RUNTIME - Version"
        '
        'RL_License
        '
        Me.RL_License.AutoSize = True
        Me.RL_License.Location = New System.Drawing.Point(61, 60)
        Me.RL_License.Name = "RL_License"
        Me.RL_License.Size = New System.Drawing.Size(173, 17)
        Me.RL_License.TabIndex = 8
        Me.RL_License.Text = "CHANGED AT RUNTIME - License"
        '
        'TabPage_Settings
        '
        Me.TabPage_Settings.Controls.Add(Me.GroupBox_Other)
        Me.TabPage_Settings.Controls.Add(Me.GroupBox_Theming)
        Me.TabPage_Settings.Controls.Add(Me.GroupBox_Behaviour)
        Me.TabPage_Settings.Location = New System.Drawing.Point(4, 25)
        Me.TabPage_Settings.Name = "TabPage_Settings"
        Me.TabPage_Settings.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Settings.Size = New System.Drawing.Size(442, 331)
        Me.TabPage_Settings.TabIndex = 1
        Me.TabPage_Settings.Text = "  Settings"
        Me.TabPage_Settings.UseVisualStyleBackColor = True
        '
        'GroupBox_Other
        '
        Me.GroupBox_Other.Controls.Add(Me.RB_ViVeTool_GUI_FeatureScanner)
        Me.GroupBox_Other.Location = New System.Drawing.Point(6, 220)
        Me.GroupBox_Other.Name = "GroupBox_Other"
        Me.GroupBox_Other.Size = New System.Drawing.Size(430, 90)
        Me.GroupBox_Other.TabIndex = 5
        Me.GroupBox_Other.TabStop = False
        Me.GroupBox_Other.Text = "Other"
        '
        'RB_ViVeTool_GUI_FeatureScanner
        '
        Me.RB_ViVeTool_GUI_FeatureScanner.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_portrait_mode_scanning_24px
        Me.RB_ViVeTool_GUI_FeatureScanner.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RB_ViVeTool_GUI_FeatureScanner.Location = New System.Drawing.Point(50, 30)
        Me.RB_ViVeTool_GUI_FeatureScanner.Name = "RB_ViVeTool_GUI_FeatureScanner"
        Me.RB_ViVeTool_GUI_FeatureScanner.Padding = New System.Windows.Forms.Padding(4)
        Me.RB_ViVeTool_GUI_FeatureScanner.Size = New System.Drawing.Size(300, 40)
        Me.RB_ViVeTool_GUI_FeatureScanner.TabIndex = 0
        Me.RB_ViVeTool_GUI_FeatureScanner.Text = "Scan this Build for Feature IDs"
        Me.RB_ViVeTool_GUI_FeatureScanner.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.RB_ViVeTool_GUI_FeatureScanner.UseVisualStyleBackColor = True
        '
        'GroupBox_Theming
        '
        Me.GroupBox_Theming.Controls.Add(Me.CB_UseSystemTheme)
        Me.GroupBox_Theming.Controls.Add(Me.CB_ThemeToggle)
        Me.GroupBox_Theming.Location = New System.Drawing.Point(6, 110)
        Me.GroupBox_Theming.Name = "GroupBox_Theming"
        Me.GroupBox_Theming.Size = New System.Drawing.Size(430, 100)
        Me.GroupBox_Theming.TabIndex = 1
        Me.GroupBox_Theming.TabStop = False
        Me.GroupBox_Theming.Text = "Theming"
        '
        'CB_UseSystemTheme
        '
        Me.CB_UseSystemTheme.Appearance = System.Windows.Forms.Appearance.Button
        Me.CB_UseSystemTheme.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_change_theme_24px
        Me.CB_UseSystemTheme.Location = New System.Drawing.Point(200, 30)
        Me.CB_UseSystemTheme.Name = "CB_UseSystemTheme"
        Me.CB_UseSystemTheme.Padding = New System.Windows.Forms.Padding(4)
        Me.CB_UseSystemTheme.Size = New System.Drawing.Size(190, 45)
        Me.CB_UseSystemTheme.TabIndex = 5
        Me.CB_UseSystemTheme.Text = "  Use System Theme"
        Me.CB_UseSystemTheme.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CB_UseSystemTheme.UseVisualStyleBackColor = True
        '
        'CB_ThemeToggle
        '
        Me.CB_ThemeToggle.Appearance = System.Windows.Forms.Appearance.Button
        Me.CB_ThemeToggle.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_sun_24
        Me.CB_ThemeToggle.Location = New System.Drawing.Point(22, 30)
        Me.CB_ThemeToggle.Name = "CB_ThemeToggle"
        Me.CB_ThemeToggle.Padding = New System.Windows.Forms.Padding(4)
        Me.CB_ThemeToggle.Size = New System.Drawing.Size(155, 45)
        Me.CB_ThemeToggle.TabIndex = 4
        Me.CB_ThemeToggle.Text = "  Dark Theme"
        Me.CB_ThemeToggle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CB_ThemeToggle.UseVisualStyleBackColor = True
        '
        'GroupBox_Behaviour
        '
        Me.GroupBox_Behaviour.Controls.Add(Me.RL_AutoLoad)
        Me.GroupBox_Behaviour.Controls.Add(Me.CB_AutoLoad)
        Me.GroupBox_Behaviour.Location = New System.Drawing.Point(6, 12)
        Me.GroupBox_Behaviour.Name = "GroupBox_Behaviour"
        Me.GroupBox_Behaviour.Size = New System.Drawing.Size(430, 90)
        Me.GroupBox_Behaviour.TabIndex = 0
        Me.GroupBox_Behaviour.TabStop = False
        Me.GroupBox_Behaviour.Text = "Behaviour"
        '
        'RL_AutoLoad
        '
        Me.RL_AutoLoad.AutoSize = True
        Me.RL_AutoLoad.Location = New System.Drawing.Point(48, 40)
        Me.RL_AutoLoad.Name = "RL_AutoLoad"
        Me.RL_AutoLoad.Size = New System.Drawing.Size(210, 17)
        Me.RL_AutoLoad.TabIndex = 0
        Me.RL_AutoLoad.Text = "Automatically load the latest Build"
        '
        'CB_AutoLoad
        '
        Me.CB_AutoLoad.AutoSize = True
        Me.CB_AutoLoad.Location = New System.Drawing.Point(320, 40)
        Me.CB_AutoLoad.Name = "CB_AutoLoad"
        Me.CB_AutoLoad.Size = New System.Drawing.Size(18, 17)
        Me.CB_AutoLoad.TabIndex = 0
        Me.CB_AutoLoad.UseVisualStyleBackColor = True
        '
        'AboutAndSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 360)
        Me.Controls.Add(Me.TabControl_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutAndSettings"
        Me.Text = "About & Settings"
        Me.TabControl_Main.ResumeLayout(False)
        Me.TabPage_About.ResumeLayout(False)
        Me.TabPage_About.PerformLayout()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_Settings.ResumeLayout(False)
        Me.GroupBox_Other.ResumeLayout(False)
        Me.GroupBox_Theming.ResumeLayout(False)
        Me.GroupBox_Behaviour.ResumeLayout(False)
        Me.GroupBox_Behaviour.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RL_Description As System.Windows.Forms.Label
    Friend WithEvents RL_License As System.Windows.Forms.Label
    Friend WithEvents RL_Version As System.Windows.Forms.Label
    Friend WithEvents RL_ProductName As System.Windows.Forms.Label
    Friend WithEvents PB_AppImage As PictureBox
    Friend WithEvents RL_Comments As System.Windows.Forms.Label
    Friend WithEvents TabControl_Main As System.Windows.Forms.TabControl
    Friend WithEvents TabPage_About As System.Windows.Forms.TabPage
    Friend WithEvents TabPage_Settings As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox_Theming As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox_Behaviour As System.Windows.Forms.GroupBox
    Friend WithEvents RL_AutoLoad As System.Windows.Forms.Label
    Friend WithEvents CB_AutoLoad As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox_Other As System.Windows.Forms.GroupBox
    Friend WithEvents RB_ViVeTool_GUI_FeatureScanner As System.Windows.Forms.Button
    Friend WithEvents CB_ThemeToggle As System.Windows.Forms.CheckBox
    Friend WithEvents CB_UseSystemTheme As System.Windows.Forms.CheckBox
End Class
