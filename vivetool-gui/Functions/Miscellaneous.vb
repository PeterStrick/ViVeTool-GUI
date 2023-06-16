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
Option Strict On

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
                    Diagnostics.Debug.WriteLine("Have Internet")
                    Return True
                End Using
            End Using
        Catch
            Diagnostics.Debug.WriteLine("Don't have Internet")
            Return False
        End Try
    End Function
End Class
