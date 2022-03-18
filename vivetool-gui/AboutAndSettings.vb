﻿'ViVeTool-GUI - Windows Feature Control GUI for ViVeTool
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
''' About & Settings Dialog/Form
''' </summary>
Public NotInheritable Class AboutAndSettings
    ''' <summary>
    ''' Form Load Event
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub AboutAndSettings_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Load the About Labels
        LoadAboutLabels()


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
    ''' Change the App Icon depending on the selected RadPageViewPage
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RPV_Main_SelectedPageChanged(sender As Object, e As EventArgs) Handles RPV_Main.SelectedPageChanged
        If RPV_Main.SelectedPage Is RPVP_About Then
            Icon = My.Resources.about
        ElseIf RPV_Main.SelectedPage Is RPVP_Settings Then
            Icon = My.Resources.settings_system_daydream
        End If
    End Sub

    Private Sub RTB_ThemeToggle_ToggleStateChanging(sender As Object, args As Telerik.WinControls.UI.StateChangingEventArgs) Handles RTB_ThemeToggle.ToggleStateChanging
        If args.NewValue = Telerik.WinControls.Enumerations.ToggleState.On Then
            Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "FluentDark"
            RTB_ThemeToggle.Text = "Dark Theme"
            RTB_ThemeToggle.Image = My.Resources.icons8_moon_and_stars_24
            'GUI.RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On
            My.Settings.DarkMode = True
        Else
            Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "Fluent"
            RTB_ThemeToggle.Text = "Light Theme"
            RTB_ThemeToggle.Image = My.Resources.icons8_sun_24
            'GUI.RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off
            My.Settings.DarkMode = False
        End If
    End Sub

    Private Sub RB_ViVeTool_GUI_FeatureScanner_Click(sender As Object, e As EventArgs) Handles RB_ViVeTool_GUI_FeatureScanner.Click
        MsgBox("WIP. Will start ViVeTool-GUI.FeatureScanner.exe when done.")
    End Sub

    Private Sub AboutAndSettings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.Save()
    End Sub

    Private Sub RTB_UseSystemTheme_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RTB_UseSystemTheme.ToggleStateChanged
        If args.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On Then
            My.Settings.UseSystemTheme = True
            Dim AppsUseLightTheme_CurrentUserDwordKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
            Dim AppsUseLightTheme_CurrentUserDwordValue As Object = AppsUseLightTheme_CurrentUserDwordKey.GetValue("SystemUsesLightTheme")
            If AppsUseLightTheme_CurrentUserDwordValue = 0 Then
                RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On
                RTB_ThemeToggle.Image = My.Resources.icons8_moon_and_stars_24
            Else
                RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off
                RTB_ThemeToggle.Image = My.Resources.icons8_sun_24
            End If
        Else
            My.Settings.UseSystemTheme = False
        End If
    End Sub

    Private Sub RTS_AutoLoad_ValueChanged(sender As Object, e As EventArgs) Handles RTS_AutoLoad.ValueChanged
        If RTS_AutoLoad.Value = True Then
            My.Settings.AutoLoad = True
        Else
            My.Settings.AutoLoad = False
        End If
    End Sub
End Class