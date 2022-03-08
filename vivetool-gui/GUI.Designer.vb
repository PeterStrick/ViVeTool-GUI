<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GUI

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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GUI))
        Me.RDDL_Build = New Telerik.WinControls.UI.RadDropDownList()
        Me.RSS_MainStatusStrip = New Telerik.WinControls.UI.RadStatusStrip()
        Me.RLE_StatusLabel = New Telerik.WinControls.UI.RadLabelElement()
        Me.RGV_MainGridView = New Telerik.WinControls.UI.RadGridView()
        Me.BGW_PopulateGridView = New System.ComponentModel.BackgroundWorker()
        Me.FluentDark = New Telerik.WinControls.Themes.FluentDarkTheme()
        Me.P_CommandPanel = New System.Windows.Forms.Panel()
        Me.RB_ManuallySetFeature = New Telerik.WinControls.UI.RadButton()
        Me.RB_About = New Telerik.WinControls.UI.RadButton()
        Me.RTB_ThemeToggle = New Telerik.WinControls.UI.RadToggleButton()
        Me.RDDB_PerformAction = New Telerik.WinControls.UI.RadDropDownButton()
        Me.RMI_ActivateF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMI_DeactivateF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMI_RevertF = New Telerik.WinControls.UI.RadMenuItem()
        Me.P_DataPanel = New System.Windows.Forms.Panel()
        Me.FluentTheme = New Telerik.WinControls.Themes.FluentTheme()
        Me.RL_BuildComboBoxORManaully = New Telerik.WinControls.UI.RadLabel()
        Me.RS_1 = New Telerik.WinControls.UI.RadSeparator()
        CType(Me.RDDL_Build, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RSS_MainStatusStrip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RGV_MainGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RGV_MainGridView.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.P_CommandPanel.SuspendLayout()
        CType(Me.RB_ManuallySetFeature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_About, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RTB_ThemeToggle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RDDB_PerformAction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.P_DataPanel.SuspendLayout()
        CType(Me.RL_BuildComboBoxORManaully, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RS_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RDDL_Build
        '
        Me.RDDL_Build.AutoSize = False
        Me.RDDL_Build.DropDownAnimationEnabled = True
        Me.RDDL_Build.DropDownMaxSize = New System.Drawing.Size(125, 455)
        Me.RDDL_Build.DropDownMinSize = New System.Drawing.Size(125, 455)
        Me.RDDL_Build.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.RDDL_Build.Location = New System.Drawing.Point(3, 5)
        Me.RDDL_Build.Name = "RDDL_Build"
        Me.RDDL_Build.NullText = "Select Build..."
        Me.RDDL_Build.Size = New System.Drawing.Size(125, 28)
        Me.RDDL_Build.TabIndex = 0
        Me.RDDL_Build.Text = "Select Build..."
        Me.RDDL_Build.ThemeName = "Fluent"
        '
        'RSS_MainStatusStrip
        '
        Me.RSS_MainStatusStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.RSS_MainStatusStrip.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RLE_StatusLabel})
        Me.RSS_MainStatusStrip.Location = New System.Drawing.Point(0, 543)
        Me.RSS_MainStatusStrip.Name = "RSS_MainStatusStrip"
        Me.RSS_MainStatusStrip.Size = New System.Drawing.Size(792, 24)
        Me.RSS_MainStatusStrip.SizingGrip = False
        Me.RSS_MainStatusStrip.TabIndex = 3
        Me.RSS_MainStatusStrip.ThemeName = "Fluent"
        '
        'RLE_StatusLabel
        '
        Me.RLE_StatusLabel.Name = "RLE_StatusLabel"
        Me.RSS_MainStatusStrip.SetSpring(Me.RLE_StatusLabel, False)
        Me.RLE_StatusLabel.Text = "Ready. Select a build from the Combo Box to get started, or alternatively press F" &
    "12 to manually change a Feature ID."
        Me.RLE_StatusLabel.TextWrap = True
        '
        'RGV_MainGridView
        '
        Me.RGV_MainGridView.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditProgrammatically
        Me.RGV_MainGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RGV_MainGridView.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.RGV_MainGridView.MasterTemplate.AllowAddNewRow = False
        Me.RGV_MainGridView.MasterTemplate.AllowColumnChooser = False
        Me.RGV_MainGridView.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.RGV_MainGridView.MasterTemplate.AllowColumnReorder = False
        Me.RGV_MainGridView.MasterTemplate.AllowColumnResize = False
        Me.RGV_MainGridView.MasterTemplate.AllowDeleteRow = False
        Me.RGV_MainGridView.MasterTemplate.AllowDragToGroup = False
        Me.RGV_MainGridView.MasterTemplate.AllowEditRow = False
        Me.RGV_MainGridView.MasterTemplate.AllowRowHeaderContextMenu = False
        Me.RGV_MainGridView.MasterTemplate.AllowRowResize = False
        Me.RGV_MainGridView.MasterTemplate.AllowSearchRow = True
        Me.RGV_MainGridView.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable
        GridViewTextBoxColumn1.AllowGroup = False
        GridViewTextBoxColumn1.AllowHide = False
        GridViewTextBoxColumn1.AllowReorder = False
        GridViewTextBoxColumn1.HeaderText = "Feature Name"
        GridViewTextBoxColumn1.MinWidth = 6
        GridViewTextBoxColumn1.Name = "FeatureName"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 535
        GridViewTextBoxColumn2.AllowGroup = False
        GridViewTextBoxColumn2.AllowHide = False
        GridViewTextBoxColumn2.AllowReorder = False
        GridViewTextBoxColumn2.HeaderText = "Feature ID"
        GridViewTextBoxColumn2.MinWidth = 6
        GridViewTextBoxColumn2.Name = "FeatureID"
        GridViewTextBoxColumn2.ReadOnly = True
        GridViewTextBoxColumn2.Width = 80
        GridViewTextBoxColumn3.HeaderText = "Feature State"
        GridViewTextBoxColumn3.Name = "FeatureState"
        GridViewTextBoxColumn3.Width = 100
        GridViewTextBoxColumn4.HeaderText = "Features that are"
        GridViewTextBoxColumn4.IsVisible = False
        GridViewTextBoxColumn4.Name = "FeatureInfo"
        GridViewTextBoxColumn4.VisibleInColumnChooser = False
        GridViewTextBoxColumn4.Width = 20
        Me.RGV_MainGridView.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewTextBoxColumn4})
        Me.RGV_MainGridView.MasterTemplate.EnableFiltering = True
        Me.RGV_MainGridView.MasterTemplate.ShowFilteringRow = False
        Me.RGV_MainGridView.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.RGV_MainGridView.Name = "RGV_MainGridView"
        Me.RGV_MainGridView.ReadOnly = True
        Me.RGV_MainGridView.ShowGroupPanel = False
        Me.RGV_MainGridView.ShowGroupPanelScrollbars = False
        Me.RGV_MainGridView.Size = New System.Drawing.Size(792, 505)
        Me.RGV_MainGridView.TabIndex = 5
        Me.RGV_MainGridView.ThemeName = "Fluent"
        '
        'BGW_PopulateGridView
        '
        Me.BGW_PopulateGridView.WorkerSupportsCancellation = True
        '
        'P_CommandPanel
        '
        Me.P_CommandPanel.Controls.Add(Me.RS_1)
        Me.P_CommandPanel.Controls.Add(Me.RL_BuildComboBoxORManaully)
        Me.P_CommandPanel.Controls.Add(Me.RB_ManuallySetFeature)
        Me.P_CommandPanel.Controls.Add(Me.RB_About)
        Me.P_CommandPanel.Controls.Add(Me.RDDL_Build)
        Me.P_CommandPanel.Controls.Add(Me.RTB_ThemeToggle)
        Me.P_CommandPanel.Controls.Add(Me.RDDB_PerformAction)
        Me.P_CommandPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.P_CommandPanel.Location = New System.Drawing.Point(0, 0)
        Me.P_CommandPanel.Name = "P_CommandPanel"
        Me.P_CommandPanel.Size = New System.Drawing.Size(792, 38)
        Me.P_CommandPanel.TabIndex = 6
        '
        'RB_ManuallySetFeature
        '
        Me.RB_ManuallySetFeature.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_registration_24px
        Me.RB_ManuallySetFeature.Location = New System.Drawing.Point(198, 4)
        Me.RB_ManuallySetFeature.Name = "RB_ManuallySetFeature"
        Me.RB_ManuallySetFeature.Size = New System.Drawing.Size(195, 30)
        Me.RB_ManuallySetFeature.TabIndex = 8
        Me.RB_ManuallySetFeature.Text = "Manually change Feature (F12)"
        Me.RB_ManuallySetFeature.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        '
        'RB_About
        '
        Me.RB_About.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_about_24
        Me.RB_About.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RB_About.Location = New System.Drawing.Point(756, 4)
        Me.RB_About.Name = "RB_About"
        Me.RB_About.Size = New System.Drawing.Size(30, 30)
        Me.RB_About.TabIndex = 4
        '
        'RTB_ThemeToggle
        '
        Me.RTB_ThemeToggle.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_sun_24
        Me.RTB_ThemeToggle.Location = New System.Drawing.Point(592, 4)
        Me.RTB_ThemeToggle.Name = "RTB_ThemeToggle"
        Me.RTB_ThemeToggle.Size = New System.Drawing.Size(158, 30)
        Me.RTB_ThemeToggle.TabIndex = 3
        Me.RTB_ThemeToggle.Text = "  Light Theme"
        Me.RTB_ThemeToggle.ThemeName = "Fluent"
        '
        'RDDB_PerformAction
        '
        Me.RDDB_PerformAction.Enabled = False
        Me.RDDB_PerformAction.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_start_24
        Me.RDDB_PerformAction.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMI_ActivateF, Me.RMI_DeactivateF, Me.RMI_RevertF})
        Me.RDDB_PerformAction.Location = New System.Drawing.Point(432, 4)
        Me.RDDB_PerformAction.Name = "RDDB_PerformAction"
        Me.RDDB_PerformAction.Size = New System.Drawing.Size(154, 30)
        Me.RDDB_PerformAction.TabIndex = 2
        Me.RDDB_PerformAction.Text = "      Perform Action"
        Me.RDDB_PerformAction.ThemeName = "Fluent"
        '
        'RMI_ActivateF
        '
        Me.RMI_ActivateF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_toggle_on_24
        Me.RMI_ActivateF.Name = "RMI_ActivateF"
        Me.RMI_ActivateF.Text = "  Activate Feature"
        '
        'RMI_DeactivateF
        '
        Me.RMI_DeactivateF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_toggle_off_24
        Me.RMI_DeactivateF.Name = "RMI_DeactivateF"
        Me.RMI_DeactivateF.Text = "  Deactivate Feature"
        '
        'RMI_RevertF
        '
        Me.RMI_RevertF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_rollback_24
        Me.RMI_RevertF.Name = "RMI_RevertF"
        Me.RMI_RevertF.Text = "  Revert Feature to Default Values"
        '
        'P_DataPanel
        '
        Me.P_DataPanel.Controls.Add(Me.RGV_MainGridView)
        Me.P_DataPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.P_DataPanel.Location = New System.Drawing.Point(0, 38)
        Me.P_DataPanel.Name = "P_DataPanel"
        Me.P_DataPanel.Size = New System.Drawing.Size(792, 505)
        Me.P_DataPanel.TabIndex = 7
        '
        'RL_BuildComboBoxORManaully
        '
        Me.RL_BuildComboBoxORManaully.Location = New System.Drawing.Point(153, 10)
        Me.RL_BuildComboBoxORManaully.Name = "RL_BuildComboBoxORManaully"
        Me.RL_BuildComboBoxORManaully.Size = New System.Drawing.Size(17, 18)
        Me.RL_BuildComboBoxORManaully.TabIndex = 9
        Me.RL_BuildComboBoxORManaully.Text = "or"
        '
        'RS_1
        '
        Me.RS_1.Location = New System.Drawing.Point(405, 4)
        Me.RS_1.Name = "RS_1"
        Me.RS_1.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.RS_1.Size = New System.Drawing.Size(13, 30)
        Me.RS_1.TabIndex = 10
        '
        'GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 567)
        Me.Controls.Add(Me.P_DataPanel)
        Me.Controls.Add(Me.P_CommandPanel)
        Me.Controls.Add(Me.RSS_MainStatusStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "GUI"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "ViVeTool GUI"
        Me.ThemeName = "Fluent"
        CType(Me.RDDL_Build, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RSS_MainStatusStrip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RGV_MainGridView.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RGV_MainGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.P_CommandPanel.ResumeLayout(False)
        Me.P_CommandPanel.PerformLayout()
        CType(Me.RB_ManuallySetFeature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_About, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RTB_ThemeToggle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RDDB_PerformAction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.P_DataPanel.ResumeLayout(False)
        CType(Me.RL_BuildComboBoxORManaully, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RS_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RDDL_Build As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents RSS_MainStatusStrip As Telerik.WinControls.UI.RadStatusStrip
    Friend WithEvents RGV_MainGridView As Telerik.WinControls.UI.RadGridView
    Friend WithEvents FeatureName As Telerik.WinControls.UI.GridViewTextBoxColumn
    Friend WithEvents FeatureID As Telerik.WinControls.UI.GridViewTextBoxColumn
    Friend WithEvents RLE_StatusLabel As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents BGW_PopulateGridView As System.ComponentModel.BackgroundWorker
    Friend WithEvents RDDB_PerformAction As Telerik.WinControls.UI.RadDropDownButton
    Friend WithEvents RMI_ActivateF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMI_DeactivateF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMI_RevertF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents FluentDark As Telerik.WinControls.Themes.FluentDarkTheme
    Friend WithEvents RTB_ThemeToggle As Telerik.WinControls.UI.RadToggleButton
    Friend WithEvents P_CommandPanel As Panel
    Friend WithEvents P_DataPanel As Panel
    Friend WithEvents FluentTheme As Telerik.WinControls.Themes.FluentTheme
    Friend WithEvents RB_About As Telerik.WinControls.UI.RadButton
    Friend WithEvents RB_ManuallySetFeature As Telerik.WinControls.UI.RadButton
    Friend WithEvents RS_1 As Telerik.WinControls.UI.RadSeparator
    Friend WithEvents RL_BuildComboBoxORManaully As Telerik.WinControls.UI.RadLabel
End Class
