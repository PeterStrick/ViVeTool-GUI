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
Imports MySqlConnector

''' <summary>
''' Database Functions and Variables
''' </summary>
Public Class DatabaseFunctions
    ''' <summary>
    ''' Public, Read-Only Database Connection String
    ''' </summary>
    Public Shared DB_Connection As New MySqlConnectionStringBuilder With {
        .Server = "direct.rawrr.dev",
        .UserID = "ViVeTool_GUI",
        .Password = "ViVeTool_GUI",
        .Database = "ViVeTool_GUI",
        .ConnectionTimeout = 120
    }

    ' Variables for the Comments System
    Public Shared Build_DT As New DataTable
    Public Shared HasDBAvailable As Boolean = False
    Public Shared TableDoesNotExist As Boolean = True

    ''' <summary>
    ''' Database Conenction Test
    ''' </summary>
    ''' <returns>Returns True if the Connection was successful, False if not</returns>
    Public Shared Function ConnectionTest() As Boolean
        Try
            ' Try to connect to the Database Server
            Using con As New MySqlConnection(DB_Connection.ConnectionString)
                con.Close()
            End Using
            Return True ' Return True if the connection was successful
        Catch DBEx As Exception
            Return False ' Otherwise Return False
        End Try
    End Function

    ''' <summary>
    ''' Loads the Comments from the DB for the specified Build
    ''' </summary>
    ''' <param name="Build">Windows Build Number as a String</param>
    Public Shared Sub LoadCommentsFromDB()
        Diagnostics.Debug.WriteLine("LoadCommentsFromDB called.")

        ' Clear local DataTable
        Build_DT.Clear()

        ' Reset Variable
        TableDoesNotExist = False

        Try
            ' Get the latest comments Table, and store it in a local Data Table
            Using con As New MySqlConnection(DB_Connection.ConnectionString)
                Using cmd As New MySqlCommand("SELECT * FROM ViVeTool_GUI.Comments;", con)
                    cmd.CommandType = CommandType.Text
                    Using sda As New MySqlDataAdapter(cmd)
                        sda.Fill(Build_DT)
                    End Using
                End Using

                Diagnostics.Debug.WriteLine("LoadCommentsFromDB Comments loaded.")
                con.Close()
            End Using
        Catch notFoundEx As MySqlException
            Diagnostics.Debug.WriteLine($"LoadCommentsFromDB Error: {notFoundEx.Message}")

            ' Fancy Message Box
            Dim Text, Expander As String

            Select Case notFoundEx.ErrorCode
                Case MySqlErrorCode.NoSuchTable
                    TableDoesNotExist = True
                    Diagnostics.Debug.WriteLine("Table does not exist")
                    Exit Try
                Case MySqlErrorCode.ServerShutdown
                    Text = My.Resources.Comments_DBError_ShuttingDown
                    Expander = notFoundEx.Message
                    HasDBAvailable = False
                Case MySqlErrorCode.UnableToConnectToHost
                    Text = My.Resources.Comments_DBError_Unavailable
                    Expander = notFoundEx.Message
                    HasDBAvailable = False
                Case Else
                    Text = My.Resources.Comments_DBError_CommunicationError
                    Expander = notFoundEx.Message
                    HasDBAvailable = False
            End Select

            RadTD.ShowDialog($" {My.Resources.Error_ADatabaseErrorOccurred}", My.Resources.Error_ADatabaseErrorOccurred, Text, RadTaskDialogIcon.ShieldErrorRedBar,
            notFoundEx, Expander, Expander)
        Catch ex As Exception
            HasDBAvailable = False

            ' Show an Error Dialog
            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
                           Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
        End Try
    End Sub
End Class
