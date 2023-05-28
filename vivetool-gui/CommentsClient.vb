'ViVeTool GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2023 Peter Strick
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.
'
'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
Imports System.Net.Http, Newtonsoft.Json

''' <summary>
''' 'Send a Comment' Form
''' </summary>
Public Class CommentsClient
    ''' <summary>
    ''' Feature Name and Build as Shared Variables intended to be changed by other Classes
    ''' </summary>
    Public Shared FeatureName, Build As String

    ' Rad TaskDialog Buttons
    Private Shared ReadOnly RTDCLB_I_Agree As New RadTaskDialogCommandLinkButton(My.Resources.Comments_I_Agree, My.Resources.Comments_SendMyComment) With {.AllowCloseDialog = False}
    Private Shared ReadOnly RTDCLB_I_Dont_Agree As New RadTaskDialogCommandLinkButton(My.Resources.Comments_I_Dont_Agree, My.Resources.Comments_I_Dont_Agree_Subtext)
    Private Shared ReadOnly RTDCLB_Finish As New RadTaskDialogCommandLinkButton(My.Resources.General_Finish, My.Resources.Comments_ClosesTheDialog)

    ' Rad TaskDialog Main Page
    Private Shared ReadOnly RTD_main As New RadTaskDialogPage With {
        .Caption = My.Resources.Comments_Spaced_PrivacyInformation,
        .Heading = My.Resources.Comments_PrivacyInformation,
        .Text = My.Resources.Comments_PrivacyText,
        .Icon = New RadTaskDialogIcon(My.Resources.icons8_comments_24px),
        .ContentAreaButtons = New RadItemOwnerGenericCollection(Of RadTaskDialogCommandLinkButton) From {
            RTDCLB_I_Agree, RTDCLB_I_Dont_Agree
        }
    }

    ' Rad TaskDialog Loading Page
    Private Shared ReadOnly RTD_loading As New RadTaskDialogPage With {
        .Caption = My.Resources.Comments_Spaced_SendingYourComment,
        .Heading = My.Resources.Comments_SendingYourComment,
        .Icon = New RadTaskDialogIcon(My.Resources.icons8_comments_24px),
        .ProgressBar = New RadTaskDialogProgressBar With {
            .State = RadTaskDialogProgressBarState.Marquee
        },
        .CommandAreaButtons = New RadItemOwnerGenericCollection(Of RadTaskDialogButton) From
            {New RadTaskDialogButton() With {
                .Enabled = False,
                .Visibility = Telerik.WinControls.ElementVisibility.Hidden
            } ' I haven't found a way to hide the CommandArea completely so this will do
        }
    }

    ' Rad TaskDialog Success Page
    Private Shared ReadOnly RTD_success As New RadTaskDialogPage With {
        .Caption = My.Resources.Comments_Spaced_CommentSent,
        .Heading = My.Resources.Comments_CommentSentSuccessfully,
        .Icon = RadTaskDialogIcon.ShieldSuccessGreenBar,
        .ContentAreaButtons = New RadItemOwnerGenericCollection(Of RadTaskDialogCommandLinkButton) From {
            RTDCLB_Finish
        }
    }

    ' Rad TaskDialog Error Page
    Private Shared ReadOnly RTD_error As New RadTaskDialogPage With {
        .Caption = $" {My.Resources.Error_AnErrorOccurred}",
        .Heading = My.Resources.Error_SendComment,
        .Icon = RadTaskDialogIcon.ShieldErrorRedBar,
        .ProgressBar = New RadTaskDialogProgressBar With {
            .State = RadTaskDialogProgressBarState.Error,
            .Value = 100
        }
    }

    ''' <summary>
    ''' Form_Load Event, set's Placeholder Text and Adds Handlers to the Rad TaskDialog Buttons
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub Comment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Display Placeholder Comment Text
        RL_Comments.Text = My.Resources.Comments_Form_EditIntroducionText
        RL_AddComment.Text = String.Format(RL_AddComment.Text, FeatureName, Build)

        ' Add Handlers to the Rad TaskDialog Buttons
        AddHandler RTDCLB_I_Agree.Click, AddressOf SendComment
        AddHandler RTDCLB_I_Dont_Agree.Click, AddressOf Close
        AddHandler RTDCLB_Finish.Click, AddressOf Close
    End Sub

    ''' <summary>
    ''' Send Comment Button. Shows RTD_main
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_SendComment_Click(sender As Object, e As EventArgs) Handles RB_SendComment.Click
        RadTaskDialog.ShowDialog(RTD_main)
    End Sub

    ''' <summary>
    ''' Edit Text Button. Shows a Rad MarkupDialog and set's RL_Comments.Text to it's Value
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_EditText_Click(sender As Object, e As EventArgs) Handles RB_EditText.Click
        Dim r As DialogResult = RMD_CommentEditor.ShowDialog()
        If r = DialogResult.OK Then RL_Comments.Text = RMD_CommentEditor.Value
    End Sub

    ''' <summary>
    ''' Sends the Comment. Used by the 'I Agree' Button
    ''' </summary>
    Private Sub SendComment()
        ' Switch to loading Task Dialog
        RTD_main.Navigate(RTD_loading)

        Dim BT As New Threading.Thread(AddressOf SendComment_Thread) With {.IsBackground = True}
        BT.SetApartmentState(Threading.ApartmentState.STA)
        BT.Start()
    End Sub

    ''' <summary>
    ''' Background Thread that handles the sending of the Comment
    ''' </summary>
    Private Async Sub SendComment_Thread()
        ' To fix String Escaping, " will be replaced by $@$
        Dim CommentString As String = RL_Comments.Text.Replace(Chr(34), Chr(36) & Chr(64) & Chr(36))

        ' Create a JSON Dictionary
        Dim JSONDict As New Dictionary(Of String, Object) From {
            {"comment", $"{CommentString}"},
            {"build", $"{Build}"},
            {"featurename", $"{FeatureName}"}
        }

        ' Create a new HttpClient to send the Comment
        Using CommentsHTTPClient As New HttpClient()
            Using CommentsHTTPClient_Request As New HttpRequestMessage(New HttpMethod("POST"), "https://direct.rawrr.cf/vAPI/send_comment")
                ' Create a new JSON String to be sent using a POST Request
                CommentsHTTPClient_Request.Content = New StringContent(JsonConvert.SerializeObject(JSONDict, Formatting.Indented))
                CommentsHTTPClient_Request.Content.Headers.ContentType = Headers.MediaTypeHeaderValue.Parse("application/json")

                ' Send the Comment
                Try
                    Dim Response = Await CommentsHTTPClient.SendAsync(CommentsHTTPClient_Request)

                    ' Throw a catch-ed Exception if the Status Code is not a successful one
                    If Not (Response.IsSuccessStatusCode) Then Throw New HttpRequestExceptionPP(Response.StatusCode, Response.ReasonPhrase)

                    Invoke(Sub() RTD_loading.Navigate(RTD_success))
                Catch HttpEx As HttpRequestExceptionPP
                    Select Case HttpEx.StatusCode
                        Case HttpStatusCode.GatewayTimeout ' HTTP 504
                            Invoke(Sub() RTD_loading.Navigate(RadTD.Generate(
                                                              $" {My.Resources.Error_ANetworkErrorOccurred}",
                                                              My.Resources.Error_ANetworkErrorOccurred,
                                                              My.Resources.Comments_ServerError_HTTP504,
                                                              RadTaskDialogIcon.Error)))
                        Case HttpStatusCode.ServiceUnavailable ' HTTP 503
                            Invoke(Sub() RTD_loading.Navigate(RadTD.Generate(
                                                              $" {My.Resources.Error_ANetworkErrorOccurred}",
                                                              My.Resources.Error_ANetworkErrorOccurred,
                                                              String.Format(My.Resources.Comments_ServerError_HTTP503, HttpEx.ReasonPhrase),
                                                              RadTaskDialogIcon.Error)))
                        Case HttpStatusCode.BadGateway ' HTTP 502
                            Invoke(Sub() RTD_loading.Navigate(RadTD.Generate(
                                                              $" {My.Resources.Error_ANetworkErrorOccurred}",
                                                              My.Resources.Error_ANetworkErrorOccurred,
                                                              My.Resources.Comments_ServerError_HTTP502,
                                                              RadTaskDialogIcon.Error)))
                        Case HttpStatusCode.InternalServerError ' HTTP 500
                            Invoke(Sub() RTD_loading.Navigate(RadTD.Generate(
                                                              $" {My.Resources.Error_ANetworkErrorOccurred}",
                                                              My.Resources.Error_ANetworkErrorOccurred,
                                                              My.Resources.Comments_ServerError_HTTP500,
                                                              RadTaskDialogIcon.Error)))
                        Case HttpStatusCode.NotFound ' HTTP 404
                            Invoke(Sub() RTD_loading.Navigate(RadTD.Generate(
                                                              $" {My.Resources.Error_ANetworkErrorOccurred}",
                                                              My.Resources.Error_ANetworkErrorOccurred,
                                                              My.Resources.Comments_ServerError_HTTP404,
                                                              RadTaskDialogIcon.Error)))
                        Case HttpStatusCode.Forbidden ' HTTP 403
                            Invoke(Sub() RTD_loading.Navigate(RadTD.Generate(
                                                              $" {My.Resources.Error_ANetworkErrorOccurred}",
                                                              My.Resources.Error_ANetworkErrorOccurred,
                                                              My.Resources.Comments_ServerError_HTTP403,
                                                              RadTaskDialogIcon.Error)))
                        Case HttpStatusCode.BadRequest ' HTTP 400
                            Invoke(Sub() RTD_loading.Navigate(RadTD.Generate(
                                                              $" {My.Resources.Error_ANetworkErrorOccurred}",
                                                              My.Resources.Error_ANetworkErrorOccurred,
                                                              My.Resources.Comments_ServerError_HTTP400,
                                                              RadTaskDialogIcon.Error)))
                        Case Else
                            Invoke(Sub() RTD_loading.Navigate(RadTD.Generate(
                                                              $" {My.Resources.Error_ANetworkErrorOccurred}",
                                                              My.Resources.Error_ANetworkErrorOccurred,
                                                              String.Format(My.Resources.Comments_ServerError_Generic, HttpEx.ReasonPhrase),
                                                              RadTaskDialogIcon.Error)))
                    End Select
                End Try
            End Using
        End Using
    End Sub
End Class