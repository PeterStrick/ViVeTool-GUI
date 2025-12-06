'ViVeTool-GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2022  Peter Strick / Peters Software Solutions
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

''' <summary>
''' Helper class for common dialog operations
''' </summary>
Public NotInheritable Class DialogHelper
    ''' <summary>
    ''' Shows an exception dialog with a "Copy Exception and Close" button
    ''' </summary>
    ''' <param name="caption">Dialog caption</param>
    ''' <param name="heading">Dialog heading</param>
    ''' <param name="exceptionText">Exception text to display in expander</param>
    ''' <param name="additionalText">Optional additional text to display</param>
    Public Shared Sub ShowExceptionDialog(caption As String, heading As String, exceptionText As String, Optional additionalText As String = Nothing)
        Dim message As String = heading
        If additionalText IsNot Nothing Then
            message &= vbNewLine & vbNewLine & additionalText
        End If
        message &= vbNewLine & vbNewLine & "Exception Details:" & vbNewLine & exceptionText

        Dim result = MessageBox.Show(message & vbNewLine & vbNewLine & "Click Yes to copy exception to clipboard.", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
        If result = DialogResult.Yes Then
            Try
                My.Computer.Clipboard.SetText(exceptionText)
            Catch clipex As Exception
                'Do nothing
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Shows an exception dialog for a generic unknown exception
    ''' </summary>
    ''' <param name="ex">The exception to display</param>
    Public Shared Sub ShowUnknownExceptionDialog(ex As Exception)
        ShowExceptionDialog(" An Exception occurred", "An unknown Exception occurred.", ex.ToString)
    End Sub

    ''' <summary>
    ''' Shows a network exception dialog
    ''' </summary>
    ''' <param name="webex">The WebException to display</param>
    Public Shared Sub ShowNetworkExceptionDialog(webex As WebException)
        Dim exceptionText As String
        Try
            exceptionText = "GitHub API Response: " & DirectCast(webex.Response, HttpWebResponse).StatusDescription
        Catch ex As Exception
            exceptionText = webex.ToString
        End Try

        Dim message As String = "A Network Exception occurred. Your IP may have been temporarily rate limited by the GitHub API for an hour."
        message &= vbNewLine & vbNewLine & "Exception Details:" & vbNewLine & exceptionText

        Dim result = MessageBox.Show(message & vbNewLine & vbNewLine & "Click Yes to copy exception to clipboard.", " A Network Exception occurred", MessageBoxButtons.YesNo, MessageBoxIcon.Error)
        If result = DialogResult.Yes Then
            Try
                Dim statusDesc As String
                Try
                    statusDesc = DirectCast(webex.Response, HttpWebResponse).StatusDescription
                Catch
                    statusDesc = webex.ToString
                End Try
                My.Computer.Clipboard.SetText(statusDesc)
            Catch clipex As Exception
                'Do nothing
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Shows a simple error dialog without exception details
    ''' </summary>
    ''' <param name="caption">Dialog caption</param>
    ''' <param name="heading">Dialog heading</param>
    ''' <param name="text">Optional additional text</param>
    ''' <param name="icon">Optional icon (defaults to Error)</param>
    Public Shared Sub ShowErrorDialog(caption As String, heading As String, Optional text As String = Nothing, Optional icon As MessageBoxIcon = MessageBoxIcon.Error)
        Dim message As String = heading
        If text IsNot Nothing Then
            message &= vbNewLine & vbNewLine & text
        End If
        MessageBox.Show(message, caption, MessageBoxButtons.OK, icon)
    End Sub

    ''' <summary>
    ''' Shows a warning dialog
    ''' </summary>
    ''' <param name="caption">Dialog caption</param>
    ''' <param name="heading">Dialog heading</param>
    ''' <param name="text">Optional additional text</param>
    Public Shared Sub ShowWarningDialog(caption As String, heading As String, Optional text As String = Nothing)
        Dim message As String = heading
        If text IsNot Nothing Then
            message &= vbNewLine & vbNewLine & text
        End If
        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ''' <summary>
    ''' Shows a success dialog
    ''' </summary>
    ''' <param name="caption">Dialog caption</param>
    ''' <param name="heading">Dialog heading</param>
    Public Shared Sub ShowSuccessDialog(caption As String, heading As String)
        MessageBox.Show(heading, caption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
