'ViVeTool GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2024 Peter Strick
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
Imports System.Diagnostics

''' <summary>
''' Miscellaneous Functions
''' </summary>
Public Class Functions
    ''' <summary>
    ''' Basic Internet Connectivity Check by trying to check if github.com is accessible
    ''' </summary>
    ''' <returns>True if http://www.github.com responds. False if not</returns>
    Public Shared Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.github.com")
                    Debug.WriteLine("Have Internet")
                    Return True
                End Using
            End Using
        Catch
            Debug.WriteLine("Don't have Internet")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Function that saves a Two Character Language Code to Settings while restarting the application afterwards.
    ''' </summary>
    ''' <param name="TwoCharLang">Two Character Language Code. For example: de for German, zh for Chinese, ...</param>
    Public Shared Sub ChangeLanguage(TwoCharLang As String)
        My.Settings.TwoCharLanguageCode = TwoCharLang
        My.Settings.Save()

        RadTD.ShowDialog($" {My.Resources.Language_Heading}", My.Resources.Language_Text, Nothing, RadTaskDialogIcon.Information)

        Dim pStart As New ProcessStartInfo With {
            .WindowStyle = ProcessWindowStyle.Normal,
            .FileName = Application.ExecutablePath
        }

        Process.Start(pStart)
        Application.Exit()
    End Sub
End Class
