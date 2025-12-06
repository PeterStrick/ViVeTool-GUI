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

Imports ViVeTool_GUI.Wpf.ViewModels

Namespace Views
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits System.Windows.Window

        Private ReadOnly _viewModel As MainViewModel

        ''' <summary>
        ''' Creates a new instance of MainWindow.
        ''' </summary>
        Public Sub New()
            InitializeComponent()

            ' Initialize and set the view model
            _viewModel = New MainViewModel()
            DataContext = _viewModel

            ' Load data when window is loaded
            AddHandler Loaded, AddressOf MainWindow_Loaded
        End Sub

        ''' <summary>
        ''' Handles the window loaded event.
        ''' </summary>
        Private Async Sub MainWindow_Loaded(sender As Object, e As System.Windows.RoutedEventArgs)
            Try
                ' Initialize the view model with data
                Await _viewModel.InitializeAsync()
            Catch ex As Exception
                ' Log or display error - in production this would show a user-friendly message
                System.Diagnostics.Debug.WriteLine($"Error initializing: {ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' Handles PasswordBox password changed event to sync with view model.
        ''' </summary>
        Private Sub GitHubTokenBox_PasswordChanged(sender As Object, e As System.Windows.RoutedEventArgs)
            Dim passwordBox = TryCast(sender, System.Windows.Controls.PasswordBox)
            If passwordBox IsNot Nothing AndAlso _viewModel IsNot Nothing Then
                _viewModel.PublishGitHubToken = passwordBox.Password
            End If
        End Sub
    End Class
End Namespace
