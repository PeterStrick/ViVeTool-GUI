'ViVeTool-GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2022  Peter Strick / Peters Software Solutions
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

''' <summary>
''' About && Settings Dialog/Form
''' </summary>
Public NotInheritable Class AboutAndSettings
    ''' <summary>
    ''' Form Load Event
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub AboutAndSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load the About Labels
        LoadAboutLabels()

        'Load settings
        CB_AutoLoad.Checked = My.Settings.AutoLoad
    End Sub

    ''' <summary>
    ''' Dynamically set's the Product Name, version, Copyright, Company Name and Description Labels on Load
    ''' </summary>
    Private Sub LoadAboutLabels()
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        'Me.Text = String.Format("About {0}", ApplicationTitle)
        Me.RL_ProductName.Text = My.Application.Info.ProductName
        Me.RL_Version.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.RL_License.Text = My.Application.Info.Copyright
        Me.RL_Description.Text = My.Application.Info.Description
    End Sub

    ''' <summary>
    ''' Change the App Icon depending on the selected TabPage
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub TabControl_Main_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl_Main.SelectedIndexChanged
        If TabControl_Main.SelectedTab Is TabPage_About Then
            Icon = My.Resources.about
        ElseIf TabControl_Main.SelectedTab Is TabPage_Settings Then
            Icon = My.Resources.settings_system_daydream
        End If
    End Sub

    ''' <summary>
    ''' Changes the Application theme depending on the CheckState
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CB_ThemeToggle_CheckedChanged(sender As Object, e As EventArgs) Handles CB_ThemeToggle.CheckedChanged
        If CB_ThemeToggle.Checked Then
            ThemeHelper.ApplyDarkTheme(CB_ThemeToggle, My.Resources.icons8_moon_and_stars_24)
        Else
            ThemeHelper.ApplyLightTheme(CB_ThemeToggle, My.Resources.icons8_sun_24)
        End If
    End Sub

    ''' <summary>
    ''' Starts ViVeTool GUI - Feature Scanner if it is present in the Application Directory
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RB_ViVeTool_GUI_FeatureScanner_Click(sender As Object, e As EventArgs) Handles RB_ViVeTool_GUI_FeatureScanner.Click
        If IO.File.Exists(Application.StartupPath & "\ViVeTool_GUI.FeatureScanner.exe") Then
            Try
                Diagnostics.Process.Start(Application.StartupPath & "\ViVeTool_GUI.FeatureScanner.exe")
            Catch wex As System.ComponentModel.Win32Exception
                'Catch Any Exception that may occur
                DialogHelper.ShowExceptionDialog(" An Exception occurred", "A generic Win32 Exception occurred.", wex.ToString, "There could be multiple causes for Win32 Exceptions, but they usually narrow down to Antivirus Software interfering with ViVeTool GUI, or Permission problems.")
            End Try
        Else
            DialogHelper.ShowErrorDialog(" An Error occurred", "An Error occurred while trying to start ViVeTool GUI - Feature Scanner." & vbNewLine & vbNewLine & "The File doesn't exist.")
        End If
    End Sub

    ''' <summary>
    ''' FormClosing Event. Saves My.Settings
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AboutAndSettings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' Changes the Application theme, using the System Theme depending on the CheckState
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub CB_UseSystemTheme_CheckedChanged(sender As Object, e As EventArgs) Handles CB_UseSystemTheme.CheckedChanged
        If CB_UseSystemTheme.Checked Then
            ThemeHelper.ApplySystemTheme(CB_ThemeToggle, My.Resources.icons8_moon_and_stars_24, My.Resources.icons8_sun_24)
        Else
            ThemeHelper.DisableSystemTheme()
        End If
    End Sub

    ''' <summary>
    ''' Changes if the latest Build should be auto-loaded, depending on the CheckState
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub CB_AutoLoad_CheckedChanged(sender As Object, e As EventArgs) Handles CB_AutoLoad.CheckedChanged
        My.Settings.AutoLoad = CB_AutoLoad.Checked
    End Sub
End Class