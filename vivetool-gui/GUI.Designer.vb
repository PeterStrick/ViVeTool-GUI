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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewImageColumn1 As Telerik.WinControls.UI.GridViewImageColumn = New Telerik.WinControls.UI.GridViewImageColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RDDL_Build = New Telerik.WinControls.UI.RadDropDownList()
        Me.RSS_MainStatusStrip = New Telerik.WinControls.UI.RadStatusStrip()
        Me.__DBG_MainBtn = New Telerik.WinControls.UI.RadDropDownButton()
        Me.__DBG_RHI = New Telerik.WinControls.UI.RadMenuHeaderItem()
        Me.__DBG_GetComments = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SeeCommentsData = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SEP1 = New Telerik.WinControls.UI.RadMenuSeparatorItem()
        Me.__DBG_SetRDDL_Build_Text = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SetRDDL_Build_Text_ToNothing = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SEP2 = New Telerik.WinControls.UI.RadMenuSeparatorItem()
        Me.__DBG_EnableCommentLoadingFromManualFL = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_DisableCommentLoadingFromManualFL = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SEP3 = New Telerik.WinControls.UI.RadMenuSeparatorItem()
        Me.__DBG_ChnageLanguage = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SEP4 = New Telerik.WinControls.UI.RadMenuSeparatorItem()
        Me.__DBG_SetFeaturePriorityToServiceAsTest = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_QueryEnabledState_401122637 = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_SEP5 = New Telerik.WinControls.UI.RadMenuSeparatorItem()
        Me.__DBG_FeatureNaming_DictUpdate = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_AutoLoad_Current_Test = New Telerik.WinControls.UI.RadMenuItem()
        Me.__DBG_RDDL_Build_SelectedIndexTest = New Telerik.WinControls.UI.RadMenuItem()
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
        Me.RSS_MainStatusStrip.SuspendLayout()
        CType(Me.__DBG_MainBtn, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RDDL_Build.DropDownMaxSize = New System.Drawing.Size(400, 455)
        Me.RDDL_Build.DropDownMinSize = New System.Drawing.Size(400, 455)
        Me.RDDL_Build.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.RDDL_Build.Name = "RDDL_Build"
        Me.RDDL_Build.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Descending
        Me.RDDL_Build.ThemeName = "Fluent"
        '
        'RSS_MainStatusStrip
        '
        Me.RSS_MainStatusStrip.Controls.Add(Me.__DBG_MainBtn)
        Me.RSS_MainStatusStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.RSS_MainStatusStrip.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RLE_StatusLabel})
        resources.ApplyResources(Me.RSS_MainStatusStrip, "RSS_MainStatusStrip")
        Me.RSS_MainStatusStrip.Name = "RSS_MainStatusStrip"
        Me.RSS_MainStatusStrip.SizingGrip = False
        Me.RSS_MainStatusStrip.ThemeName = "Fluent"
        '
        '__DBG_MainBtn
        '
        resources.ApplyResources(Me.__DBG_MainBtn, "__DBG_MainBtn")
        Me.__DBG_MainBtn.Items.AddRange(New Telerik.WinControls.RadItem() {Me.__DBG_RHI, Me.__DBG_GetComments, Me.__DBG_SeeCommentsData, Me.__DBG_SEP1, Me.__DBG_SetRDDL_Build_Text, Me.__DBG_SetRDDL_Build_Text_ToNothing, Me.__DBG_SEP2, Me.__DBG_EnableCommentLoadingFromManualFL, Me.__DBG_DisableCommentLoadingFromManualFL, Me.__DBG_SEP3, Me.__DBG_ChnageLanguage, Me.__DBG_SEP4, Me.__DBG_SetFeaturePriorityToServiceAsTest, Me.__DBG_QueryEnabledState_401122637, Me.__DBG_SEP5, Me.__DBG_FeatureNaming_DictUpdate, Me.__DBG_AutoLoad_Current_Test, Me.__DBG_RDDL_Build_SelectedIndexTest})
        Me.__DBG_MainBtn.Name = "__DBG_MainBtn"
        Me.__DBG_MainBtn.TabStop = False
        '
        '__DBG_RHI
        '
        Me.__DBG_RHI.Name = "__DBG_RHI"
        resources.ApplyResources(Me.__DBG_RHI, "__DBG_RHI")
        '
        '__DBG_GetComments
        '
        Me.__DBG_GetComments.Name = "__DBG_GetComments"
        resources.ApplyResources(Me.__DBG_GetComments, "__DBG_GetComments")
        '
        '__DBG_SeeCommentsData
        '
        Me.__DBG_SeeCommentsData.Name = "__DBG_SeeCommentsData"
        resources.ApplyResources(Me.__DBG_SeeCommentsData, "__DBG_SeeCommentsData")
        '
        '__DBG_SEP1
        '
        Me.__DBG_SEP1.Name = "__DBG_SEP1"
        resources.ApplyResources(Me.__DBG_SEP1, "__DBG_SEP1")
        '
        '__DBG_SetRDDL_Build_Text
        '
        Me.__DBG_SetRDDL_Build_Text.Name = "__DBG_SetRDDL_Build_Text"
        resources.ApplyResources(Me.__DBG_SetRDDL_Build_Text, "__DBG_SetRDDL_Build_Text")
        '
        '__DBG_SetRDDL_Build_Text_ToNothing
        '
        Me.__DBG_SetRDDL_Build_Text_ToNothing.Name = "__DBG_SetRDDL_Build_Text_ToNothing"
        resources.ApplyResources(Me.__DBG_SetRDDL_Build_Text_ToNothing, "__DBG_SetRDDL_Build_Text_ToNothing")
        '
        '__DBG_SEP2
        '
        Me.__DBG_SEP2.Name = "__DBG_SEP2"
        resources.ApplyResources(Me.__DBG_SEP2, "__DBG_SEP2")
        '
        '__DBG_EnableCommentLoadingFromManualFL
        '
        Me.__DBG_EnableCommentLoadingFromManualFL.Name = "__DBG_EnableCommentLoadingFromManualFL"
        resources.ApplyResources(Me.__DBG_EnableCommentLoadingFromManualFL, "__DBG_EnableCommentLoadingFromManualFL")
        '
        '__DBG_DisableCommentLoadingFromManualFL
        '
        Me.__DBG_DisableCommentLoadingFromManualFL.Name = "__DBG_DisableCommentLoadingFromManualFL"
        resources.ApplyResources(Me.__DBG_DisableCommentLoadingFromManualFL, "__DBG_DisableCommentLoadingFromManualFL")
        '
        '__DBG_SEP3
        '
        Me.__DBG_SEP3.Name = "__DBG_SEP3"
        resources.ApplyResources(Me.__DBG_SEP3, "__DBG_SEP3")
        '
        '__DBG_ChnageLanguage
        '
        Me.__DBG_ChnageLanguage.Name = "__DBG_ChnageLanguage"
        resources.ApplyResources(Me.__DBG_ChnageLanguage, "__DBG_ChnageLanguage")
        '
        '__DBG_SEP4
        '
        Me.__DBG_SEP4.Name = "__DBG_SEP4"
        resources.ApplyResources(Me.__DBG_SEP4, "__DBG_SEP4")
        '
        '__DBG_SetFeaturePriorityToServiceAsTest
        '
        Me.__DBG_SetFeaturePriorityToServiceAsTest.Name = "__DBG_SetFeaturePriorityToServiceAsTest"
        resources.ApplyResources(Me.__DBG_SetFeaturePriorityToServiceAsTest, "__DBG_SetFeaturePriorityToServiceAsTest")
        '
        '__DBG_QueryEnabledState_401122637
        '
        Me.__DBG_QueryEnabledState_401122637.Name = "__DBG_QueryEnabledState_401122637"
        resources.ApplyResources(Me.__DBG_QueryEnabledState_401122637, "__DBG_QueryEnabledState_401122637")
        '
        '__DBG_SEP5
        '
        Me.__DBG_SEP5.Name = "__DBG_SEP5"
        resources.ApplyResources(Me.__DBG_SEP5, "__DBG_SEP5")
        '
        '__DBG_FeatureNaming_DictUpdate
        '
        Me.__DBG_FeatureNaming_DictUpdate.Name = "__DBG_FeatureNaming_DictUpdate"
        resources.ApplyResources(Me.__DBG_FeatureNaming_DictUpdate, "__DBG_FeatureNaming_DictUpdate")
        '
        '__DBG_AutoLoad_Current_Test
        '
        Me.__DBG_AutoLoad_Current_Test.Name = "__DBG_AutoLoad_Current_Test"
        resources.ApplyResources(Me.__DBG_AutoLoad_Current_Test, "__DBG_AutoLoad_Current_Test")
        '
        '__DBG_RDDL_Build_SelectedIndexTest
        '
        Me.__DBG_RDDL_Build_SelectedIndexTest.Name = "__DBG_RDDL_Build_SelectedIndexTest"
        resources.ApplyResources(Me.__DBG_RDDL_Build_SelectedIndexTest, "__DBG_RDDL_Build_SelectedIndexTest")
        '
        'RLE_StatusLabel
        '
        Me.RLE_StatusLabel.Name = "RLE_StatusLabel"
        Me.RSS_MainStatusStrip.SetSpring(Me.RLE_StatusLabel, False)
        resources.ApplyResources(Me.RLE_StatusLabel, "RLE_StatusLabel")
        Me.RLE_StatusLabel.TextWrap = True
        '
        'RGV_MainGridView
        '
        Me.RGV_MainGridView.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditProgrammatically
        resources.ApplyResources(Me.RGV_MainGridView, "RGV_MainGridView")
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
        GridViewTextBoxColumn1.MinWidth = 6
        GridViewTextBoxColumn1.Name = "FeatureName"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 535
        GridViewTextBoxColumn2.AllowGroup = False
        GridViewTextBoxColumn2.AllowHide = False
        GridViewTextBoxColumn2.AllowReorder = False
        GridViewTextBoxColumn2.MinWidth = 6
        GridViewTextBoxColumn2.Name = "FeatureID"
        GridViewTextBoxColumn2.ReadOnly = True
        GridViewTextBoxColumn2.Width = 80
        GridViewTextBoxColumn3.Name = "FeatureState"
        GridViewTextBoxColumn3.Width = 100
        GridViewTextBoxColumn4.AllowFiltering = False
        GridViewTextBoxColumn4.AllowGroup = False
        GridViewTextBoxColumn4.AllowHide = False
        GridViewTextBoxColumn4.AllowNaturalSort = False
        GridViewTextBoxColumn4.AllowReorder = False
        GridViewTextBoxColumn4.AllowResize = False
        GridViewTextBoxColumn4.AllowSearching = False
        GridViewTextBoxColumn4.AllowSort = False
        GridViewTextBoxColumn4.IsVisible = False
        GridViewTextBoxColumn4.Name = "FeatureInfo"
        GridViewTextBoxColumn4.VisibleInColumnChooser = False
        GridViewTextBoxColumn4.Width = 20
        GridViewImageColumn1.AllowFiltering = False
        GridViewImageColumn1.AllowHide = False
        GridViewImageColumn1.AllowNaturalSort = False
        GridViewImageColumn1.AllowReorder = False
        GridViewImageColumn1.AllowResize = False
        GridViewImageColumn1.AllowSearching = False
        GridViewImageColumn1.AllowSort = False
        GridViewImageColumn1.HeaderImage = Global.ViVeTool_GUI.My.Resources.Resources.icons8_comments_24px
        GridViewImageColumn1.Name = "Comments"
        GridViewImageColumn1.VisibleInColumnChooser = False
        Me.RGV_MainGridView.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewImageColumn1})
        Me.RGV_MainGridView.MasterTemplate.EnableFiltering = True
        Me.RGV_MainGridView.MasterTemplate.ShowFilteringRow = False
        Me.RGV_MainGridView.MasterTemplate.ViewDefinition = TableViewDefinition1
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
        Me.P_CommandPanel.Controls.Add(Me.RS_1)
        Me.P_CommandPanel.Controls.Add(Me.RL_BuildComboBoxORManaully)
        Me.P_CommandPanel.Controls.Add(Me.RB_ManuallySetFeature)
        Me.P_CommandPanel.Controls.Add(Me.RB_About)
        Me.P_CommandPanel.Controls.Add(Me.RDDL_Build)
        Me.P_CommandPanel.Controls.Add(Me.RDDB_PerformAction)
        resources.ApplyResources(Me.P_CommandPanel, "P_CommandPanel")
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
        Me.RB_ManuallySetFeature.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_registration_24px
        resources.ApplyResources(Me.RB_ManuallySetFeature, "RB_ManuallySetFeature")
        Me.RB_ManuallySetFeature.Name = "RB_ManuallySetFeature"
        '
        'RB_About
        '
        Me.RB_About.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_about_24
        resources.ApplyResources(Me.RB_About, "RB_About")
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
        Me.RMI_ActivateF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_toggle_on_24
        Me.RMI_ActivateF.Name = "RMI_ActivateF"
        resources.ApplyResources(Me.RMI_ActivateF, "RMI_ActivateF")
        '
        'RMI_DeactivateF
        '
        Me.RMI_DeactivateF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_toggle_off_24
        Me.RMI_DeactivateF.Name = "RMI_DeactivateF"
        resources.ApplyResources(Me.RMI_DeactivateF, "RMI_DeactivateF")
        '
        'RMI_RevertF
        '
        Me.RMI_RevertF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_rollback_24
        Me.RMI_RevertF.Name = "RMI_RevertF"
        resources.ApplyResources(Me.RMI_RevertF, "RMI_RevertF")
        '
        'P_DataPanel
        '
        Me.P_DataPanel.Controls.Add(Me.RGV_MainGridView)
        resources.ApplyResources(Me.P_DataPanel, "P_DataPanel")
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
        Me.RSS_MainStatusStrip.ResumeLayout(False)
        CType(Me.__DBG_MainBtn, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents __DBG_MainBtn As Telerik.WinControls.UI.RadDropDownButton
    Friend WithEvents __DBG_GetComments As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_SetRDDL_Build_Text As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_SeeCommentsData As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_SetRDDL_Build_Text_ToNothing As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_SEP1 As Telerik.WinControls.UI.RadMenuSeparatorItem
    Friend WithEvents __DBG_RHI As Telerik.WinControls.UI.RadMenuHeaderItem
    Friend WithEvents __DBG_SEP2 As Telerik.WinControls.UI.RadMenuSeparatorItem
    Friend WithEvents __DBG_EnableCommentLoadingFromManualFL As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_DisableCommentLoadingFromManualFL As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_SEP3 As Telerik.WinControls.UI.RadMenuSeparatorItem
    Friend WithEvents __DBG_ChnageLanguage As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents __DBG_SEP4 As RadMenuSeparatorItem
    Friend WithEvents __DBG_SetFeaturePriorityToServiceAsTest As RadMenuItem
    Friend WithEvents __DBG_QueryEnabledState_401122637 As RadMenuItem
    Friend WithEvents __DBG_SEP5 As RadMenuSeparatorItem
    Friend WithEvents __DBG_FeatureNaming_DictUpdate As RadMenuItem
    Friend WithEvents __DBG_AutoLoad_Current_Test As RadMenuItem
    Friend WithEvents __DBG_RDDL_Build_SelectedIndexTest As RadMenuItem
End Class
