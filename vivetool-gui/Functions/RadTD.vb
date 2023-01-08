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
Option Strict On
Imports Telerik.WinControls.UI

Public Class RadTD
    Private Shared ReadOnly CopyExAndClose As New RadTaskDialogButton With {
        .Text = My.Resources.Generic_Close,
        .ToolTipText = My.Resources.Error_CopyExceptionAndClose_ToolTip
    }

    ''' <summary>
    ''' Generates and shows a Rad Task Dialog as an Error Dialog
    ''' </summary>
    ''' <param name="Caption">Task Dialog Caption</param>
    ''' <param name="Heading">Task Dialog Heading</param>
    ''' <param name="Text">Task Dialog Text</param>
    ''' <param name="Icon">Task Dialog Icon/Shield Bar</param>
    ''' <param name="ex">Optional: Exception</param>
    ''' <param name="exCustomClipboard">Optional: Custom Clipboard Text in conjunction with ex</param>
    ''' <param name="ExpandedText">Optional: Text that shows after expanding the Task Dialog</param>
    Public Shared Sub Show(Caption As String, Heading As String, Text As String, Icon As RadTaskDialogIcon, Optional ex As Exception = Nothing, Optional exCustomClipboard As String = Nothing, Optional ExpandedText As String = Nothing)
        ' Generate Rad TaskDialog Page
        Dim RTD As New RadTaskDialogPage With {
            .Caption = Caption,
            .Heading = Heading,
            .Text = Text,
            .Icon = Icon
        }

        ' Add Copy Exception and Close button if ex is specified
        If ex IsNot Nothing Then
            AddHandler CopyExAndClose.Click, New EventHandler(
                Sub()
                    Try
                        Dim ClipText As String = ex.ToString
                        If exCustomClipboard IsNot Nothing Then ClipText = exCustomClipboard
                        My.Computer.Clipboard.SetText(ClipText)
                    Catch clipex As Exception
                        'Do nothing
                    End Try
                End Sub)
            RTD.CommandAreaButtons.Add(CopyExAndClose)
        Else RTD.CommandAreaButtons.Add(RadTaskDialogButton.Close)
        End If

        ' Add Expander if it is specified
        If ExpandedText IsNot Nothing Then
            RTD.Expander.Text = ExpandedText
            RTD.Expander.ExpandedButtonText = My.Resources.Error_CollapseException
            RTD.Expander.CollapsedButtonText = My.Resources.Error_ShowException
        End If

        ' Show the Dialog
        RadTaskDialog.ShowDialog(RTD)
    End Sub

    ''' <summary>
    ''' Generates and returns a Rad Task Dialog as an Error Dialog
    ''' </summary>
    ''' <param name="Caption">Task Dialog Caption</param>
    ''' <param name="Heading">Task Dialog Heading</param>
    ''' <param name="Text">Task Dialog Text</param>
    ''' <param name="Icon">Task Dialog Icon/Shield Bar</param>
    ''' <param name="ex">Optional: Exception</param>
    ''' <param name="exCustomClipboard">Optional: Custom Clipboard Text in conjunction with ex</param>
    ''' <param name="ExpandedText">Optional: Text that shows after expanding the Task Dialog</param>
    ''' <returns>The generated Error Dialog as RadTaskDialogPageS</returns>
    Public Shared Function Generate(Caption As String, Heading As String, Text As String, Icon As RadTaskDialogIcon, Optional ex As Exception = Nothing, Optional exCustomClipboard As String = Nothing, Optional ExpandedText As String = Nothing) As RadTaskDialogPage
        ' Generate Rad TaskDialog Page
        Dim RTD As New RadTaskDialogPage With {
            .Caption = Caption,
            .Heading = Heading,
            .Text = Text,
            .Icon = Icon
        }

        ' Add Copy Exception and Close button if ex is specified
        If ex IsNot Nothing Then
            AddHandler CopyExAndClose.Click, New EventHandler(
                Sub()
                    Try
                        Dim ClipText As String = ex.ToString
                        If exCustomClipboard IsNot Nothing Then ClipText = exCustomClipboard
                        My.Computer.Clipboard.SetText(ClipText)
                    Catch clipex As Exception
                        ' Do nothing
                    End Try
                End Sub)
            RTD.CommandAreaButtons.Add(CopyExAndClose)
        Else RTD.CommandAreaButtons.Add(RadTaskDialogButton.Close)
        End If

        ' Add Expander if it is specified
        If ExpandedText IsNot Nothing Then
            RTD.Expander.Text = ExpandedText
            RTD.Expander.ExpandedButtonText = My.Resources.Error_CollapseException
            RTD.Expander.CollapsedButtonText = My.Resources.Error_ShowException
        End If

        ' Return the Dialog
        Return RTD
    End Function
End Class