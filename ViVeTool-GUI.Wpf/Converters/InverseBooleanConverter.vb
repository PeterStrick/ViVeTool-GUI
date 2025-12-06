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

Imports System.Globalization
Imports System.Windows.Data

Namespace Converters
    ''' <summary>
    ''' Converts a Boolean value to its inverse.
    ''' </summary>
    Public Class InverseBooleanConverter
        Implements IValueConverter

        ''' <summary>
        ''' Converts a Boolean value to its inverse.
        ''' </summary>
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean Then
                Return Not CBool(value)
            End If
            Return value
        End Function

        ''' <summary>
        ''' Converts back from the inverse Boolean value.
        ''' </summary>
        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Boolean Then
                Return Not CBool(value)
            End If
            Return value
        End Function
    End Class
End Namespace
