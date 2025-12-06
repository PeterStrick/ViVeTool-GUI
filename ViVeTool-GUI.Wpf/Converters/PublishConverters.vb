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
Imports System.Windows
Imports System.Windows.Data
Imports ViVeTool_GUI.Wpf.Models

Namespace Converters
    ''' <summary>
    ''' Converts a PublishStatus to button text.
    ''' </summary>
    Public Class PublishStatusToButtonTextConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is PublishStatus Then
                Dim status = DirectCast(value, PublishStatus)
                Select Case status
                    Case PublishStatus.Publishing
                        Return "Publishing..."
                    Case PublishStatus.Success
                        Return "Published ✓"
                    Case PublishStatus.Failure
                        Return "Retry Publish"
                    Case Else
                        Return "Publish via GitHub Actions"
                End Select
            End If
            Return "Publish via GitHub Actions"
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    ''' <summary>
    ''' Converts PublishStatus.Success to Visibility.Visible.
    ''' </summary>
    Public Class PublishSuccessToVisibilityConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is PublishStatus Then
                Dim status = DirectCast(value, PublishStatus)
                Return If(status = PublishStatus.Success, Visibility.Visible, Visibility.Collapsed)
            End If
            Return Visibility.Collapsed
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    ''' <summary>
    ''' Converts a non-empty string to Visibility.Visible.
    ''' </summary>
    Public Class StringToVisibilityConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is String Then
                Dim str = DirectCast(value, String)
                Return If(String.IsNullOrWhiteSpace(str), Visibility.Collapsed, Visibility.Visible)
            End If
            Return Visibility.Collapsed
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    ''' <summary>
    ''' Converts a Boolean to Visibility.
    ''' </summary>
    Public Class BooleanToVisibilityConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean Then
                Return If(CBool(value), Visibility.Visible, Visibility.Collapsed)
            End If
            Return Visibility.Collapsed
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Visibility Then
                Return DirectCast(value, Visibility) = Visibility.Visible
            End If
            Return False
        End Function
    End Class

    ''' <summary>
    ''' Converts a Boolean to Visibility (inverse: False = Visible, True = Collapsed).
    ''' </summary>
    Public Class InverseBoolToVisibilityConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean Then
                Return If(CBool(value), Visibility.Collapsed, Visibility.Visible)
            End If
            Return Visibility.Visible
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Visibility Then
                Return DirectCast(value, Visibility) = Visibility.Collapsed
            End If
            Return True
        End Function
    End Class
End Namespace
