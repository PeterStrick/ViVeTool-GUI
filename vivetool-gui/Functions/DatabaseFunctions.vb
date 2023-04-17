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
Imports MySqlConnector

''' <summary>
''' Database Functions and Variables
''' </summary>
Public Class DatabaseFunctions

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
    ''' Public, Read-Only Database Connection String
    ''' </summary>
    Public Shared DB_Connection As New MySqlConnectionStringBuilder With {
        .Server = "direct.rawrr.cf",
        .UserID = "ViVeTool_GUI",
        .Password = "ViVeTool_GUI",
        .Database = "ViVeTool_GUI",
        .ConnectionTimeout = 120
    }
End Class
