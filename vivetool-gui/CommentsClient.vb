Imports System.Net.Http, Newtonsoft.Json, Telerik.WinControls.UI

Public Class CommentsClient
    Public Shared FeatureName, Build As String

    Private Shared ReadOnly RTD_main As New RadTaskDialogPage With {
            .Caption = My.Resources.Comments_Spaced_PrivacyInformation,
            .Heading = My.Resources.Comments_PrivacyInformation,
            .Text = My.Resources.Comments_PrivacyText,
            .Icon = New RadTaskDialogIcon(My.Resources.icons8_comments_24px)
        }

    Private Shared ReadOnly RTD_next As New RadTaskDialogPage With {
        .Caption = "Test",
        .ProgressBar =
    }

    Private Sub Comment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RL_Comments.Text = "<html><p>Press on ""<strong>Edit Text</strong>"" to edit the Comment Text.</p><p></p><p>You can either write a Comment using normal Text, or <strong><em>HTML-like Text Formatting</em></strong></p></html>"
        RL_AddComment.Text = String.Format(RL_AddComment.Text, FeatureName, Build)
    End Sub

    Private Sub RB_SendComment_Click(sender As Object, e As EventArgs) Handles RB_SendComment.Click


        Dim RTDCLB_I_Agree As New RadTaskDialogCommandLinkButton(My.Resources.Comments_I_Agree, My.Resources.Comments_SendMyComment)
        Dim RTDCLB_I_Dont_Agree As New RadTaskDialogCommandLinkButton(My.Resources.Comments_I_Dont_Agree, My.Resources.Comments_I_Dont_Agree_Subtext)

        RTD_main.ContentAreaButtons.Add(RTDCLB_I_Agree)
        RTD_main.ContentAreaButtons.Add(RTDCLB_I_Dont_Agree)

        AddHandler RTDCLB_I_Agree.Click, AddressOf I_Agree
        AddHandler RTDCLB_I_Dont_Agree.Click, AddressOf I_Dont_Agree

        RadTaskDialog.ShowDialog(RTD_main)
    End Sub

    Private Sub I_Agree()



        MsgBox("Not implemented yet")
        Close()
    End Sub

    Private Sub I_Dont_Agree()
        Close()
    End Sub

    Private Sub RB_EditText_Click(sender As Object, e As EventArgs) Handles RB_EditText.Click
        Dim r As DialogResult = RMD_CommentEditor.ShowDialog()
        If r = DialogResult.OK Then RL_Comments.Text = RMD_CommentEditor.Value
    End Sub

    Private Async Sub SendComment()
        'To fix String Escaping, " will be replaced by ""
        Dim CommentString As String = RL_Comments.Text.Replace(Chr(34), Chr(34) & Chr(34))

        'Create JSON Dictionary
        Dim JSONDict As New Dictionary(Of String, Object) From {
            {"comment", $"{CommentString}"},
            {"build", $"{Build}"},
            {"featurename", $"{FeatureName}"}
        }

        Using CommentsHTTPClient As New HttpClient()
            Using CommentsHTTPClient_Request As New HttpRequestMessage(New HttpMethod("POST"), "https://direct.rawrr.cf/vAPI/send_comment")
                CommentsHTTPClient_Request.Content = New StringContent(JsonConvert.SerializeObject(JSONDict, Formatting.Indented))
                CommentsHTTPClient_Request.Content.Headers.ContentType = Headers.MediaTypeHeaderValue.Parse("application/json")

                Dim Response = Await CommentsHTTPClient.SendAsync(CommentsHTTPClient_Request)

                MsgBox(Response.Content, vbOKOnly, Response.StatusCode)
            End Using
        End Using
    End Sub
End Class