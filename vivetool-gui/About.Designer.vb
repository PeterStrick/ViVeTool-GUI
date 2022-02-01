Imports Telerik
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class About
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        Me.RL_Description = New Telerik.WinControls.UI.RadLabel()
        Me.RL_License = New Telerik.WinControls.UI.RadLabel()
        Me.RL_Version = New Telerik.WinControls.UI.RadLabel()
        Me.RL_ProductName = New Telerik.WinControls.UI.RadLabel()
        Me.RL_Comments = New Telerik.WinControls.UI.RadLabel()
        Me.PB_AppImage = New System.Windows.Forms.PictureBox()
        CType(Me.RL_Description, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_License, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Version, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_ProductName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Comments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RL_Description
        '
        Me.RL_Description.AutoSize = False
        Me.RL_Description.Location = New System.Drawing.Point(70, 88)
        Me.RL_Description.Name = "RL_Description"
        Me.RL_Description.Size = New System.Drawing.Size(281, 47)
        Me.RL_Description.TabIndex = 7
        Me.RL_Description.Text = "CHANGED AT RUNTIME - Description"
        '
        'RL_License
        '
        Me.RL_License.Location = New System.Drawing.Point(70, 64)
        Me.RL_License.Name = "RL_License"
        Me.RL_License.Size = New System.Drawing.Size(173, 18)
        Me.RL_License.TabIndex = 8
        Me.RL_License.Text = "CHANGED AT RUNTIME - License"
        '
        'RL_Version
        '
        Me.RL_Version.Location = New System.Drawing.Point(69, 40)
        Me.RL_Version.Name = "RL_Version"
        Me.RL_Version.Size = New System.Drawing.Size(174, 18)
        Me.RL_Version.TabIndex = 6
        Me.RL_Version.Text = "CHANGED AT RUNTIME - Version"
        '
        'RL_ProductName
        '
        Me.RL_ProductName.Location = New System.Drawing.Point(69, 16)
        Me.RL_ProductName.Name = "RL_ProductName"
        Me.RL_ProductName.Size = New System.Drawing.Size(206, 18)
        Me.RL_ProductName.TabIndex = 5
        Me.RL_ProductName.Text = "CHANGED AT RUNTIME - ProductName"
        '
        'RL_Comments
        '
        Me.RL_Comments.Location = New System.Drawing.Point(70, 141)
        Me.RL_Comments.Name = "RL_Comments"
        Me.RL_Comments.Size = New System.Drawing.Size(269, 134)
        Me.RL_Comments.TabIndex = 8
        Me.RL_Comments.Text = resources.GetString("RL_Comments.Text")
        '
        'PB_AppImage
        '
        Me.PB_AppImage.Image = Global.ViVeTool_GUI.My.Resources.Resources.icons8_advertisement_page_96
        Me.PB_AppImage.Location = New System.Drawing.Point(15, 16)
        Me.PB_AppImage.Name = "PB_AppImage"
        Me.PB_AppImage.Size = New System.Drawing.Size(48, 48)
        Me.PB_AppImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_AppImage.TabIndex = 4
        Me.PB_AppImage.TabStop = False
        '
        'About
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(366, 296)
        Me.Controls.Add(Me.RL_Comments)
        Me.Controls.Add(Me.RL_Description)
        Me.Controls.Add(Me.RL_License)
        Me.Controls.Add(Me.RL_Version)
        Me.Controls.Add(Me.RL_ProductName)
        Me.Controls.Add(Me.PB_AppImage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "About"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "CHNAGED AT RUNTIME - Title"
        Me.ThemeName = "Fluent"
        CType(Me.RL_Description, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_License, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Version, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_ProductName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Comments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_AppImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RL_Description As WinControls.UI.RadLabel
    Friend WithEvents RL_License As WinControls.UI.RadLabel
    Friend WithEvents RL_Version As WinControls.UI.RadLabel
    Friend WithEvents RL_ProductName As WinControls.UI.RadLabel
    Friend WithEvents PB_AppImage As PictureBox
    Friend WithEvents RL_Comments As WinControls.UI.RadLabel
End Class
