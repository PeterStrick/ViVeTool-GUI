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
    ''' Represents a single feature entry from the per-build feature file.
    ''' Supports both CSV and JSON formats with graceful handling of missing columns.
    ''' </summary>
    Public Class FeatureEntry
        ''' <summary>
        ''' The feature name/symbol.
        ''' </summary>
        <JsonPropertyName("name")>
        Public Property Name As String

        ''' <summary>
        ''' The feature ID.
        ''' </summary>
        <JsonPropertyName("id")>
        Public Property Id As Integer

        ''' <summary>
        ''' The feature group/stage (e.g., "Modifiable", "Always Enabled").
        ''' May be empty for older builds that don't have this information.
        ''' </summary>
        <JsonPropertyName("group")>
        Public Property Group As String

        ''' <summary>
        ''' Creates a new instance of FeatureEntry.
        ''' </summary>
        Public Sub New()
            Name = String.Empty
            Group = String.Empty
        End Sub

        ''' <summary>
        ''' Creates a new instance of FeatureEntry with specified values.
        ''' </summary>
        Public Sub New(name As String, id As Integer, Optional group As String = "")
            Me.Name = If(name, String.Empty)
            Me.Id = id
            Me.Group = If(group, String.Empty)
        End Sub
    End Class
End Namespace
