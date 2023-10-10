<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SetManual
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetManual))
        Me.RTB_FeatureID = New Telerik.WinControls.UI.RadTextBox()
        Me.RDDB_PerformAction = New Telerik.WinControls.UI.RadDropDownButton()
        Me.RMI_ActivateF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMI_DeactivateF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMI_RevertF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RTB_VariantID = New Telerik.WinControls.UI.RadTextBox()
        Me.RB_FixLKG = New Telerik.WinControls.UI.RadButton()
        Me.RB_FixPriority = New Telerik.WinControls.UI.RadButton()
        CType(Me.RTB_FeatureID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RDDB_PerformAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RTB_VariantID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_FixLKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_FixPriority, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RTB_FeatureID
        '
        resources.ApplyResources(Me.RTB_FeatureID, "RTB_FeatureID")
        Me.RTB_FeatureID.Name = "RTB_FeatureID"
        Me.RTB_FeatureID.TabStop = False
        '
        'RDDB_PerformAction
        '
        resources.ApplyResources(Me.RDDB_PerformAction, "RDDB_PerformAction")
        Me.RDDB_PerformAction.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_start_24
        Me.RDDB_PerformAction.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMI_ActivateF, Me.RMI_DeactivateF, Me.RMI_RevertF})
        Me.RDDB_PerformAction.Name = "RDDB_PerformAction"
        Me.RDDB_PerformAction.TabStop = False
        Me.RDDB_PerformAction.ThemeName = "Fluent"
        '
        'RMI_ActivateF
        '
        resources.ApplyResources(Me.RMI_ActivateF, "RMI_ActivateF")
        Me.RMI_ActivateF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_toggle_on_24
        Me.RMI_ActivateF.Name = "RMI_ActivateF"
        Me.RMI_ActivateF.UseCompatibleTextRendering = False
        '
        'RMI_DeactivateF
        '
        resources.ApplyResources(Me.RMI_DeactivateF, "RMI_DeactivateF")
        Me.RMI_DeactivateF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_toggle_off_24
        Me.RMI_DeactivateF.Name = "RMI_DeactivateF"
        Me.RMI_DeactivateF.UseCompatibleTextRendering = False
        '
        'RMI_RevertF
        '
        resources.ApplyResources(Me.RMI_RevertF, "RMI_RevertF")
        Me.RMI_RevertF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_rollback_24
        Me.RMI_RevertF.Name = "RMI_RevertF"
        Me.RMI_RevertF.UseCompatibleTextRendering = False
        '
        'RTB_VariantID
        '
        resources.ApplyResources(Me.RTB_VariantID, "RTB_VariantID")
        Me.RTB_VariantID.Name = "RTB_VariantID"
        Me.RTB_VariantID.TabStop = False
        '
        'RB_FixLKG
        '
        resources.ApplyResources(Me.RB_FixLKG, "RB_FixLKG")
        Me.RB_FixLKG.Name = "RB_FixLKG"
        '
        'RB_FixPriority
        '
        resources.ApplyResources(Me.RB_FixPriority, "RB_FixPriority")
        Me.RB_FixPriority.Name = "RB_FixPriority"
        '
        'SetManual
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RB_FixPriority)
        Me.Controls.Add(Me.RB_FixLKG)
        Me.Controls.Add(Me.RTB_VariantID)
        Me.Controls.Add(Me.RDDB_PerformAction)
        Me.Controls.Add(Me.RTB_FeatureID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SetManual"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        CType(Me.RTB_FeatureID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RDDB_PerformAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RTB_VariantID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_FixLKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_FixPriority, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RTB_FeatureID As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RDDB_PerformAction As Telerik.WinControls.UI.RadDropDownButton
    Friend WithEvents RMI_ActivateF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMI_DeactivateF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMI_RevertF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RTB_VariantID As RadTextBox
    Friend WithEvents RB_FixLKG As RadButton
    Friend WithEvents RB_FixPriority As RadButton
End Class

