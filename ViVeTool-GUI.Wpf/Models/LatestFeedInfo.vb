' ViVeTool-GUI - Windows Feature Control GUI for ViVeTool
' Copyright (C) 2022  Peter Strick / Peters Software Solutions
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <https://www.gnu.org/licenses/>.

Option Explicit On
Option Strict On

Imports System.Text.Json.Serialization

Namespace Models
    ''' <summary>
    ''' Represents the latest.json feed metadata.
    ''' Tracks current/latest build and available builds in the feed.
    ''' </summary>
    Public Class LatestFeedInfo
        ''' <summary>
        ''' The most recent/latest build number available in the feed.
        ''' </summary>
        <JsonPropertyName("latestBuild")>
        Public Property LatestBuild As String

        ''' <summary>
        ''' List of all available build numbers in the feed.
        ''' </summary>
        <JsonPropertyName("builds")>
        Public Property Builds As List(Of String)

        ''' <summary>
        ''' Timestamp when the feed was last updated (ISO 8601 format).
        ''' </summary>
        <JsonPropertyName("lastUpdated")>
        Public Property LastUpdated As String

        ''' <summary>
        ''' Creates a new instance of LatestFeedInfo.
        ''' </summary>
        Public Sub New()
            Builds = New List(Of String)()
        End Sub
    End Class
End Namespace
