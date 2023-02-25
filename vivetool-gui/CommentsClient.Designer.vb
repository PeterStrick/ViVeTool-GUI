<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CommentsClient
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CommentsClient))
        Me.RL_AddComment = New Telerik.WinControls.UI.RadLabel()
        Me.RB_SendComment = New Telerik.WinControls.UI.RadButton()
        Me.RL_CommentInfo = New Telerik.WinControls.UI.RadLabel()
        Me.RL_Comments = New Telerik.WinControls.UI.RadLabel()
        Me.RB_EditText = New Telerik.WinControls.UI.RadButton()
        Me.RMD_CommentEditor = New Telerik.WinControls.UI.RadMarkupDialog()
        CType(Me.RL_AddComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_SendComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_CommentInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RL_Comments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RB_EditText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RL_AddComment
        '
        resources.ApplyResources(Me.RL_AddComment, "RL_AddComment")
        Me.RL_AddComment.Name = "RL_AddComment"
        '
        'RB_SendComment
        '
        resources.ApplyResources(Me.RB_SendComment, "RB_SendComment")
        Me.RB_SendComment.Name = "RB_SendComment"
        '
        'RL_CommentInfo
        '
        resources.ApplyResources(Me.RL_CommentInfo, "RL_CommentInfo")
        Me.RL_CommentInfo.Name = "RL_CommentInfo"
        '
        'RL_Comments
        '
        resources.ApplyResources(Me.RL_Comments, "RL_Comments")
        Me.RL_Comments.BorderVisible = True
        Me.RL_Comments.Name = "RL_Comments"
        '
        'RB_EditText
        '
        resources.ApplyResources(Me.RB_EditText, "RB_EditText")
        Me.RB_EditText.Name = "RB_EditText"
        '
        'RMD_CommentEditor
        '
        Me.RMD_CommentEditor.DefaultFont = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'CommentsClient
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RB_EditText)
        Me.Controls.Add(Me.RL_Comments)
        Me.Controls.Add(Me.RL_CommentInfo)
        Me.Controls.Add(Me.RB_SendComment)
        Me.Controls.Add(Me.RL_AddComment)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CommentsClient"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        CType(Me.RL_AddComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_SendComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_CommentInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RL_Comments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RB_EditText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RL_AddComment As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RL_CommentInfo As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RB_SendComment As Telerik.WinControls.UI.RadButton
    Friend WithEvents RL_Comments As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RB_EditText As Telerik.WinControls.UI.RadButton
    Friend WithEvents RMD_CommentEditor As Telerik.WinControls.UI.RadMarkupDialog
End Class

