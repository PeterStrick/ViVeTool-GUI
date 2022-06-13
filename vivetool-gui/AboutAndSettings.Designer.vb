Imports Telerik
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AboutAndSettings
    Inherits Telerik.WinControls.UI.RadForm

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
        Me.RPV_Main = New Telerik.WinControls.UI.RadPageView()
        Me.RPVP_About = New Telerik.WinControls.UI.RadPageViewPage()
        Me.PB_AppImage = New System.Windows.Forms.PictureBox()
        Me.RL_Comments = New Telerik.WinControls.UI.RadLabel()
        Me.RL_ProductName = New Telerik.WinControls.UI.RadLabel()
        Me.RL_Description = New Telerik.WinControls.UI.RadLabel()
        Me.RL_Version = New Telerik.WinControls.UI.RadLabel()
        Me.RL_License = New Telerik.WinControls.UI.RadLabel()
        Me.RPVP_Settings = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RB_ViVeTool_GUI_FeatureScanner = New Telerik.WinControls.UI.RadButton()
        Me.RGB_Theming = New Telerik.WinControls.UI.RadGroupBox()
        Me.RTB_UseSystemTheme = New Telerik.WinControls.UI.RadToggleButton()
        Me.RTB_ThemeToggle = New Telerik.WinControls.UI.RadToggleButton()
        Me.RGB_Behaviour = New Telerik.WinControls.UI.RadGroupBox()
        Me.RL_AutoLoad = New Telerik.WinControls.UI.RadLabel()
        Me.RTS_AutoLoad = New Telerik.WinControls.UI.RadToggleSwitch()
        CType(Me.RPV_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RPV_Main.SuspendLayout()
        Me.RPVP_About.SuspendLayout()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Comments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_ProductName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Description, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Version, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_License, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RPVP_Settings.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RB_ViVeTool_GUI_FeatureScanner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RGB_Theming, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RGB_Theming.SuspendLayout()
        CType(Me.RTB_UseSystemTheme, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RTB_ThemeToggle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RGB_Behaviour, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RGB_Behaviour.SuspendLayout()
        CType(Me.RL_AutoLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RTS_AutoLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RPV_Main
        '
        resources.ApplyResources(Me.RPV_Main, "RPV_Main")
        Me.RPV_Main.Controls.Add(Me.RPVP_About)
        Me.RPV_Main.Controls.Add(Me.RPVP_Settings)
        Me.RPV_Main.DefaultPage = Me.RPVP_About
        Me.RPV_Main.Name = "RPV_Main"
        Me.RPV_Main.SelectedPage = Me.RPVP_Settings
        CType(Me.RPV_Main.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ShowItemPinButton = False
        CType(Me.RPV_Main.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RPV_Main.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ShowItemCloseButton = False
        '
        'RPVP_About
        '
        resources.ApplyResources(Me.RPVP_About, "RPVP_About")
        Me.RPVP_About.Controls.Add(Me.PB_AppImage)
        Me.RPVP_About.Controls.Add(Me.RL_Comments)
        Me.RPVP_About.Controls.Add(Me.RL_ProductName)
        Me.RPVP_About.Controls.Add(Me.RL_Description)
        Me.RPVP_About.Controls.Add(Me.RL_Version)
        Me.RPVP_About.Controls.Add(Me.RL_License)
        Me.RPVP_About.Image = My.Resources.Resources.icons8_about_24
        Me.RPVP_About.ItemSize = New System.Drawing.SizeF(156.0!, 36.0!)
        Me.RPVP_About.Name = "RPVP_About"
        '
        'PB_AppImage
        '
        resources.ApplyResources(Me.PB_AppImage, "PB_AppImage")
        Me.PB_AppImage.Image = My.Resources.Resources.icons8_advertisement_page_96
        Me.PB_AppImage.Name = "PB_AppImage"
        Me.PB_AppImage.TabStop = False
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
        'RPVP_Settings
        '
        resources.ApplyResources(Me.RPVP_Settings, "RPVP_Settings")
        Me.RPVP_Settings.Controls.Add(Me.RadGroupBox1)
        Me.RPVP_Settings.Controls.Add(Me.RGB_Theming)
        Me.RPVP_Settings.Controls.Add(Me.RGB_Behaviour)
        Me.RPVP_Settings.Image = CType(resources.GetObject("RPVP_Settings.Image"), System.Drawing.Image)
        Me.RPVP_Settings.ItemSize = New System.Drawing.SizeF(89.0!, 36.0!)
        Me.RPVP_Settings.Name = "RPVP_Settings"
        '
        'RadGroupBox1
        '
        resources.ApplyResources(Me.RadGroupBox1, "RadGroupBox1")
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RB_ViVeTool_GUI_FeatureScanner)
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        'RB_ViVeTool_GUI_FeatureScanner
        '
        resources.ApplyResources(Me.RB_ViVeTool_GUI_FeatureScanner, "RB_ViVeTool_GUI_FeatureScanner")
        Me.RB_ViVeTool_GUI_FeatureScanner.Image = My.Resources.Resources.icons8_portrait_mode_scanning_24px
        Me.RB_ViVeTool_GUI_FeatureScanner.Name = "RB_ViVeTool_GUI_FeatureScanner"
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
        Me.RTB_UseSystemTheme.Image = My.Resources.Resources.icons8_change_theme_24px
        Me.RTB_UseSystemTheme.Name = "RTB_UseSystemTheme"
        Me.RTB_UseSystemTheme.ThemeName = "Fluent"
        '
        'RTB_ThemeToggle
        '
        Me.RTB_ThemeToggle.Image = My.Resources.Resources.icons8_sun_24
        resources.ApplyResources(Me.RTB_ThemeToggle, "RTB_ThemeToggle")
        Me.RTB_ThemeToggle.Name = "RTB_ThemeToggle"
        Me.RTB_ThemeToggle.ThemeName = "Fluent"
        '
        'RGB_Behaviour
        '
        resources.ApplyResources(Me.RGB_Behaviour, "RGB_Behaviour")
        Me.RGB_Behaviour.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RGB_Behaviour.Controls.Add(Me.RL_AutoLoad)
        Me.RGB_Behaviour.Controls.Add(Me.RTS_AutoLoad)
        Me.RGB_Behaviour.HeaderMargin = New System.Windows.Forms.Padding(1)
        Me.RGB_Behaviour.Name = "RGB_Behaviour"
        '
        'RL_AutoLoad
        '
        resources.ApplyResources(Me.RL_AutoLoad, "RL_AutoLoad")
        Me.RL_AutoLoad.Name = "RL_AutoLoad"
        '
        'RTS_AutoLoad
        '
        resources.ApplyResources(Me.RTS_AutoLoad, "RTS_AutoLoad")
        Me.RTS_AutoLoad.Name = "RTS_AutoLoad"
        '
        'AboutAndSettings
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RPV_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutAndSettings"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.ThemeName = "Fluent"
        CType(Me.RPV_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RPV_Main.ResumeLayout(False)
        Me.RPVP_About.ResumeLayout(False)
        Me.RPVP_About.PerformLayout()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Comments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_ProductName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Description, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Version, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_License, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RPVP_Settings.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RB_ViVeTool_GUI_FeatureScanner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RGB_Theming, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RGB_Theming.ResumeLayout(False)
        CType(Me.RTB_UseSystemTheme, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RTB_ThemeToggle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RGB_Behaviour, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RGB_Behaviour.ResumeLayout(False)
        Me.RGB_Behaviour.PerformLayout()
        CType(Me.RL_AutoLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RTS_AutoLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RL_Description As WinControls.UI.RadLabel
    Friend WithEvents RL_License As WinControls.UI.RadLabel
    Friend WithEvents RL_Version As WinControls.UI.RadLabel
    Friend WithEvents RL_ProductName As WinControls.UI.RadLabel
    Friend WithEvents PB_AppImage As PictureBox
    Friend WithEvents RL_Comments As WinControls.UI.RadLabel
    Friend WithEvents RPV_Main As WinControls.UI.RadPageView
    Friend WithEvents RPVP_About As WinControls.UI.RadPageViewPage
    Friend WithEvents RPVP_Settings As WinControls.UI.RadPageViewPage
    Friend WithEvents RGB_Theming As WinControls.UI.RadGroupBox
    Friend WithEvents RGB_Behaviour As WinControls.UI.RadGroupBox
    Friend WithEvents RL_AutoLoad As WinControls.UI.RadLabel
    Friend WithEvents RTS_AutoLoad As WinControls.UI.RadToggleSwitch
    Friend WithEvents RadGroupBox1 As WinControls.UI.RadGroupBox
    Friend WithEvents RB_ViVeTool_GUI_FeatureScanner As WinControls.UI.RadButton
    Friend WithEvents RTB_ThemeToggle As WinControls.UI.RadToggleButton
    Friend WithEvents RTB_UseSystemTheme As WinControls.UI.RadToggleButton
End Class
