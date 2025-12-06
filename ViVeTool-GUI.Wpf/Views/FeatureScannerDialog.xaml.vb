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

Imports System.Diagnostics
Imports System.Windows.Navigation
Imports ViVeTool_GUI.Wpf.Models
Imports ViVeTool_GUI.Wpf.ViewModels

Namespace Views
    ''' <summary>
    ''' Interaction logic for FeatureScannerDialog.xaml
    ''' </summary>
    Partial Public Class FeatureScannerDialog
        Inherits System.Windows.Window

        Private ReadOnly _viewModel As FeatureScannerViewModel

        ''' <summary>
        ''' Gets the scan result if the scan completed successfully.
        ''' </summary>
        Public Property ScanResult As ScannerResult

        ''' <summary>
        ''' Creates a new instance of FeatureScannerDialog.
        ''' </summary>
        Public Sub New()
            InitializeComponent()

            _viewModel = New FeatureScannerViewModel()
            DataContext = _viewModel

            ' Wire up events
            AddHandler _viewModel.RequestClose, AddressOf OnRequestClose
            AddHandler _viewModel.ScanCompleted, AddressOf OnScanCompleted
        End Sub

        ''' <summary>
        ''' Creates a new instance of FeatureScannerDialog with an owner window.
        ''' </summary>
        ''' <param name="owner">The owner window.</param>
        Public Sub New(owner As System.Windows.Window)
            Me.New()
            Me.Owner = owner
        End Sub

        ''' <summary>
        ''' Handles the hyperlink click to open the Windows SDK download page.
        ''' </summary>
        Private Sub Hyperlink_RequestNavigate(sender As Object, e As RequestNavigateEventArgs)
            Try
                Process.Start(New ProcessStartInfo(e.Uri.AbsoluteUri) With {
                    .UseShellExecute = True
                })
                e.Handled = True
            Catch ex As Exception
                ' Ignore errors opening the browser
                Debug.WriteLine($"Error opening URL: {ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' Handles the request to close the dialog.
        ''' </summary>
        Private Sub OnRequestClose(sender As Object, e As EventArgs)
            DialogResult = ScanResult IsNot Nothing AndAlso ScanResult.Success
            Close()
        End Sub

        ''' <summary>
        ''' Handles the scan completed event.
        ''' </summary>
        Private Sub OnScanCompleted(sender As Object, e As ScannerResult)
            ScanResult = e
        End Sub

        ''' <summary>
        ''' Clean up resources when the window is closed.
        ''' </summary>
        Protected Overrides Sub OnClosed(e As EventArgs)
            MyBase.OnClosed(e)

            RemoveHandler _viewModel.RequestClose, AddressOf OnRequestClose
            RemoveHandler _viewModel.ScanCompleted, AddressOf OnScanCompleted
            _viewModel.Dispose()
        End Sub
    End Class
End Namespace
