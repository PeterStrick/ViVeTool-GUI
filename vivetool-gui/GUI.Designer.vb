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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GUI))
        Dim GridViewTextBoxColumn9 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn10 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn11 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn12 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RDDL_Build = New Telerik.WinControls.UI.RadDropDownList()
        Me.RSS_MainStatusStrip = New Telerik.WinControls.UI.RadStatusStrip()
        Me.RLE_StatusLabel = New Telerik.WinControls.UI.RadLabelElement()
        Me.RGV_MainGridView = New Telerik.WinControls.UI.RadGridView()
        Me.BGW_PopulateGridView = New System.ComponentModel.BackgroundWorker()
        Me.FluentDark = New Telerik.WinControls.Themes.FluentDarkTheme()
        Me.P_CommandPanel = New System.Windows.Forms.Panel()
        Me.RS_1 = New Telerik.WinControls.UI.RadSeparator()
        Me.RL_BuildComboBoxORManaully = New Telerik.WinControls.UI.RadLabel()
        Me.RB_ManuallySetFeature = New Telerik.WinControls.UI.RadButton()
        Me.RB_About = New Telerik.WinControls.UI.RadButton()
        Me.RDDB_PerformAction = New Telerik.WinControls.UI.RadDropDownButton()
        Me.RMI_ActivateF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMI_DeactivateF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMI_RevertF = New Telerik.WinControls.UI.RadMenuItem()
        Me.P_DataPanel = New System.Windows.Forms.Panel()
        Me.FluentTheme = New Telerik.WinControls.Themes.FluentTheme()
        CType(Me.RDDL_Build, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RSS_MainStatusStrip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RGV_MainGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RGV_MainGridView.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.P_CommandPanel.SuspendLayout()
        CType(Me.RS_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_BuildComboBoxORManaully, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_ManuallySetFeature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_About, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RDDB_PerformAction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.P_DataPanel.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RDDL_Build
        '
        resources.ApplyResources(Me.RDDL_Build, "RDDL_Build")
        Me.RDDL_Build.DropDownAnimationEnabled = True
        Me.RDDL_Build.DropDownMaxSize = New System.Drawing.Size(125, 455)
        Me.RDDL_Build.DropDownMinSize = New System.Drawing.Size(125, 455)
        Me.RDDL_Build.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.RDDL_Build.Name = "RDDL_Build"
        Me.RDDL_Build.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Descending
        Me.RDDL_Build.ThemeName = "Fluent"
        '
        'RSS_MainStatusStrip
        '
        resources.ApplyResources(Me.RSS_MainStatusStrip, "RSS_MainStatusStrip")
        Me.RSS_MainStatusStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.RSS_MainStatusStrip.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RLE_StatusLabel})
        Me.RSS_MainStatusStrip.Name = "RSS_MainStatusStrip"
        Me.RSS_MainStatusStrip.SizingGrip = False
        Me.RSS_MainStatusStrip.ThemeName = "Fluent"
        '
        'RLE_StatusLabel
        '
        resources.ApplyResources(Me.RLE_StatusLabel, "RLE_StatusLabel")
        Me.RLE_StatusLabel.Name = "RLE_StatusLabel"
        Me.RSS_MainStatusStrip.SetSpring(Me.RLE_StatusLabel, False)
        Me.RLE_StatusLabel.TextWrap = True
        '
        'RGV_MainGridView
        '
        resources.ApplyResources(Me.RGV_MainGridView, "RGV_MainGridView")
        Me.RGV_MainGridView.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditProgrammatically
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
        Me.RGV_MainGridView.MasterTemplate.Caption = resources.GetString("RGV_MainGridView.MasterTemplate.Caption")
        Me.RGV_MainGridView.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable
        GridViewTextBoxColumn9.AllowGroup = False
        GridViewTextBoxColumn9.AllowHide = False
        GridViewTextBoxColumn9.AllowReorder = False
        resources.ApplyResources(GridViewTextBoxColumn9, "GridViewTextBoxColumn9")
        GridViewTextBoxColumn9.MinWidth = 6
        GridViewTextBoxColumn9.Name = "FeatureName"
        GridViewTextBoxColumn9.ReadOnly = True
        GridViewTextBoxColumn9.Width = 535
        GridViewTextBoxColumn10.AllowGroup = False
        GridViewTextBoxColumn10.AllowHide = False
        GridViewTextBoxColumn10.AllowReorder = False
        resources.ApplyResources(GridViewTextBoxColumn10, "GridViewTextBoxColumn10")
        GridViewTextBoxColumn10.MinWidth = 6
        GridViewTextBoxColumn10.Name = "FeatureID"
        GridViewTextBoxColumn10.ReadOnly = True
        GridViewTextBoxColumn10.Width = 80
        resources.ApplyResources(GridViewTextBoxColumn11, "GridViewTextBoxColumn11")
        GridViewTextBoxColumn11.Name = "FeatureState"
        GridViewTextBoxColumn11.Width = 100
        resources.ApplyResources(GridViewTextBoxColumn12, "GridViewTextBoxColumn12")
        GridViewTextBoxColumn12.IsVisible = False
        GridViewTextBoxColumn12.Name = "FeatureInfo"
        GridViewTextBoxColumn12.VisibleInColumnChooser = False
        GridViewTextBoxColumn12.Width = 20
        Me.RGV_MainGridView.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn9, GridViewTextBoxColumn10, GridViewTextBoxColumn11, GridViewTextBoxColumn12})
        Me.RGV_MainGridView.MasterTemplate.EnableFiltering = True
        Me.RGV_MainGridView.MasterTemplate.ShowFilteringRow = False
        Me.RGV_MainGridView.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.RGV_MainGridView.Name = "RGV_MainGridView"
        Me.RGV_MainGridView.ReadOnly = True
        Me.RGV_MainGridView.ShowGroupPanel = False
        Me.RGV_MainGridView.ShowGroupPanelScrollbars = False
        Me.RGV_MainGridView.ThemeName = "Fluent"
        '
        'BGW_PopulateGridView
        '
        Me.BGW_PopulateGridView.WorkerSupportsCancellation = True
        '
        'P_CommandPanel
        '
        resources.ApplyResources(Me.P_CommandPanel, "P_CommandPanel")
        Me.P_CommandPanel.Controls.Add(Me.RS_1)
        Me.P_CommandPanel.Controls.Add(Me.RL_BuildComboBoxORManaully)
        Me.P_CommandPanel.Controls.Add(Me.RB_ManuallySetFeature)
        Me.P_CommandPanel.Controls.Add(Me.RB_About)
        Me.P_CommandPanel.Controls.Add(Me.RDDL_Build)
        Me.P_CommandPanel.Controls.Add(Me.RDDB_PerformAction)
        Me.P_CommandPanel.Name = "P_CommandPanel"
        '
        'RS_1
        '
        resources.ApplyResources(Me.RS_1, "RS_1")
        Me.RS_1.Name = "RS_1"
        Me.RS_1.Orientation = System.Windows.Forms.Orientation.Vertical
        '
        'RL_BuildComboBoxORManaully
        '
        resources.ApplyResources(Me.RL_BuildComboBoxORManaully, "RL_BuildComboBoxORManaully")
        Me.RL_BuildComboBoxORManaully.Name = "RL_BuildComboBoxORManaully"
        '
        'RB_ManuallySetFeature
        '
        resources.ApplyResources(Me.RB_ManuallySetFeature, "RB_ManuallySetFeature")
        Me.RB_ManuallySetFeature.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_registration_24px
        Me.RB_ManuallySetFeature.Name = "RB_ManuallySetFeature"
        '
        'RB_About
        '
        resources.ApplyResources(Me.RB_About, "RB_About")
        Me.RB_About.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_about_24
        Me.RB_About.Name = "RB_About"
        '
        'RDDB_PerformAction
        '
        resources.ApplyResources(Me.RDDB_PerformAction, "RDDB_PerformAction")
        Me.RDDB_PerformAction.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_start_24
        Me.RDDB_PerformAction.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMI_ActivateF, Me.RMI_DeactivateF, Me.RMI_RevertF})
        Me.RDDB_PerformAction.Name = "RDDB_PerformAction"
        Me.RDDB_PerformAction.ThemeName = "Fluent"
        '
        'RMI_ActivateF
        '
        resources.ApplyResources(Me.RMI_ActivateF, "RMI_ActivateF")
        Me.RMI_ActivateF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_toggle_on_24
        Me.RMI_ActivateF.Name = "RMI_ActivateF"
        '
        'RMI_DeactivateF
        '
        resources.ApplyResources(Me.RMI_DeactivateF, "RMI_DeactivateF")
        Me.RMI_DeactivateF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_toggle_off_24
        Me.RMI_DeactivateF.Name = "RMI_DeactivateF"
        '
        'RMI_RevertF
        '
        resources.ApplyResources(Me.RMI_RevertF, "RMI_RevertF")
        Me.RMI_RevertF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_rollback_24
        Me.RMI_RevertF.Name = "RMI_RevertF"
        '
        'P_DataPanel
        '
        resources.ApplyResources(Me.P_DataPanel, "P_DataPanel")
        Me.P_DataPanel.Controls.Add(Me.RGV_MainGridView)
        Me.P_DataPanel.Name = "P_DataPanel"
        '
        'GUI
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.P_DataPanel)
        Me.Controls.Add(Me.P_CommandPanel)
        Me.Controls.Add(Me.RSS_MainStatusStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "GUI"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.ThemeName = "Fluent"
        CType(Me.RDDL_Build, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RSS_MainStatusStrip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RGV_MainGridView.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RGV_MainGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.P_CommandPanel.ResumeLayout(False)
        Me.P_CommandPanel.PerformLayout()
        CType(Me.RS_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_BuildComboBoxORManaully, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_ManuallySetFeature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_About, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RDDB_PerformAction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.P_DataPanel.ResumeLayout(False)
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
    Friend WithEvents P_CommandPanel As Panel
    Friend WithEvents P_DataPanel As Panel
    Friend WithEvents FluentTheme As Telerik.WinControls.Themes.FluentTheme
    Friend WithEvents RB_About As Telerik.WinControls.UI.RadButton
    Friend WithEvents RB_ManuallySetFeature As Telerik.WinControls.UI.RadButton
    Friend WithEvents RS_1 As Telerik.WinControls.UI.RadSeparator
    Friend WithEvents RL_BuildComboBoxORManaully As Telerik.WinControls.UI.RadLabel
End Class
