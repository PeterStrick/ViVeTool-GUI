<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SetManual
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetManual))
        Me.RTB_FeatureID = New System.Windows.Forms.TextBox()
        Me.RDDB_PerformAction = New System.Windows.Forms.Button()
        Me.ActionContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RMI_ActivateF = New System.Windows.Forms.ToolStripMenuItem()
        Me.RMI_DeactivateF = New System.Windows.Forms.ToolStripMenuItem()
        Me.RMI_RevertF = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActionContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'RTB_FeatureID
        '
        Me.RTB_FeatureID.Location = New System.Drawing.Point(15, 17)
        Me.RTB_FeatureID.Name = "RTB_FeatureID"
        Me.RTB_FeatureID.Size = New System.Drawing.Size(250, 22)
        Me.RTB_FeatureID.TabIndex = 0
        '
        'RDDB_PerformAction
        '
        Me.RDDB_PerformAction.Enabled = False
        Me.RDDB_PerformAction.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_start_24
        Me.RDDB_PerformAction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RDDB_PerformAction.Location = New System.Drawing.Point(275, 12)
        Me.RDDB_PerformAction.Name = "RDDB_PerformAction"
        Me.RDDB_PerformAction.Padding = New System.Windows.Forms.Padding(4)
        Me.RDDB_PerformAction.Size = New System.Drawing.Size(175, 35)
        Me.RDDB_PerformAction.TabIndex = 1
        Me.RDDB_PerformAction.Text = "Perform Action ▼"
        Me.RDDB_PerformAction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.RDDB_PerformAction.UseVisualStyleBackColor = True
        '
        'ActionContextMenu
        '
        Me.ActionContextMenu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ActionContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RMI_ActivateF, Me.RMI_DeactivateF, Me.RMI_RevertF})
        Me.ActionContextMenu.Name = "ActionContextMenu"
        Me.ActionContextMenu.Size = New System.Drawing.Size(280, 76)
        '
        'RMI_ActivateF
        '
        Me.RMI_ActivateF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_toggle_on_24
        Me.RMI_ActivateF.Name = "RMI_ActivateF"
        Me.RMI_ActivateF.Size = New System.Drawing.Size(279, 24)
        Me.RMI_ActivateF.Text = "  Activate Feature"
        '
        'RMI_DeactivateF
        '
        Me.RMI_DeactivateF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_toggle_off_24
        Me.RMI_DeactivateF.Name = "RMI_DeactivateF"
        Me.RMI_DeactivateF.Size = New System.Drawing.Size(279, 24)
        Me.RMI_DeactivateF.Text = "  Deactivate Feature"
        '
        'RMI_RevertF
        '
        Me.RMI_RevertF.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_rollback_24
        Me.RMI_RevertF.Name = "RMI_RevertF"
        Me.RMI_RevertF.Size = New System.Drawing.Size(279, 24)
        Me.RMI_RevertF.Text = "  Revert Feature to Default Settings"
        '
        'SetManual
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 60)
        Me.Controls.Add(Me.RDDB_PerformAction)
        Me.Controls.Add(Me.RTB_FeatureID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SetManual"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Manually change a Feature - Enter a Feature ID"
        Me.ActionContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RTB_FeatureID As System.Windows.Forms.TextBox
    Friend WithEvents RDDB_PerformAction As System.Windows.Forms.Button
    Friend WithEvents ActionContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RMI_ActivateF As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RMI_DeactivateF As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RMI_RevertF As System.Windows.Forms.ToolStripMenuItem
End Class

