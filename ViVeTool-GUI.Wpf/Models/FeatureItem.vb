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

Imports CommunityToolkit.Mvvm.ComponentModel

Namespace Models
    ''' <summary>
    ''' Represents a Windows feature configuration item.
    ''' </summary>
    Partial Public Class FeatureItem
        Inherits ObservableObject

        Private _id As Integer
        Private _name As String = String.Empty
        Private _state As String = String.Empty
        Private _isEnabled As Boolean
        Private _notes As String = String.Empty

        ''' <summary>
        ''' Gets or sets the feature ID.
        ''' </summary>
        Public Property Id As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                SetProperty(_id, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the feature name.
        ''' </summary>
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                SetProperty(_name, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the feature state (e.g., "Enabled", "Disabled", "Default").
        ''' </summary>
        Public Property State As String
            Get
                Return _state
            End Get
            Set(value As String)
                SetProperty(_state, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the feature is currently enabled.
        ''' </summary>
        Public Property IsEnabled As Boolean
            Get
                Return _isEnabled
            End Get
            Set(value As Boolean)
                SetProperty(_isEnabled, value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets any notes or descriptions for the feature.
        ''' </summary>
        Public Property Notes As String
            Get
                Return _notes
            End Get
            Set(value As String)
                SetProperty(_notes, value)
            End Set
        End Property

        ''' <summary>
        ''' Creates a new instance of FeatureItem.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Creates a new instance of FeatureItem with specified values.
        ''' </summary>
        Public Sub New(id As Integer, name As String, state As String, isEnabled As Boolean, Optional notes As String = "")
            _id = id
            _name = name
            _state = state
            _isEnabled = isEnabled
            _notes = notes
        End Sub
    End Class
End Namespace
