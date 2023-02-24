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
Imports System.Diagnostics, Telerik.WinControls.UI

''' <summary>
''' About and Settings Dialog/Form
''' </summary>
Public NotInheritable Class AboutAndSettings
    ''' <summary>
    ''' Form Load Event
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub AboutAndSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load the About Labels
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

        RL_ProductName.Text = My.Application.Info.ProductName
        RL_Version.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        RL_License.Text = My.Application.Info.Copyright
        RL_Description.Text = My.Application.Info.Description
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

    ''' <summary>
    ''' Changes the Application theme depending on the ToggleState
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    Private Sub RTB_ThemeToggle_ToggleStateChanging(sender As Object, args As StateChangingEventArgs) Handles RTB_ThemeToggle.ToggleStateChanging
        If args.NewValue = Telerik.WinControls.Enumerations.ToggleState.On Then
            Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "FluentDark"
            RTB_ThemeToggle.Text = My.Resources.Generic_DarkTheme
            RTB_ThemeToggle.Image = My.Resources.icons8_moon_and_stars_24
            My.Settings.DarkMode = True
        Else
            Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "Fluent"
            RTB_ThemeToggle.Text = My.Resources.Generic_LightTheme
            RTB_ThemeToggle.Image = My.Resources.icons8_sun_24
            My.Settings.DarkMode = False
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
                Process.Start(Application.StartupPath & "\ViVeTool_GUI.FeatureScanner.exe")
            Catch wex As ComponentModel.Win32Exception
                'Catch Any Exception that may occur
                RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_GenericWin32Exception_Heading,
                My.Resources.Error_GenericWin32Exception_Text, RadTaskDialogIcon.ShieldErrorRedBar,
                wex, wex.ToString, wex.ToString)
            End Try
        Else
            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_FeatureScannerNotFound_N,
            Nothing, RadTaskDialogIcon.Error)
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
    ''' Changes the Application theme, using the System Theme depending on the ToggleState
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="args">StateChanged EventArgs</param>
    Private Sub RTB_UseSystemTheme_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles RTB_UseSystemTheme.ToggleStateChanged
        If args.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off Then
            My.Settings.UseSystemTheme = False
            Return
        End If

        'If args.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On Then
        My.Settings.UseSystemTheme = True
        Dim AppsUseLightTheme_CurrentUserDwordKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
        Dim AppsUseLightTheme_CurrentUserDwordValue As Object = AppsUseLightTheme_CurrentUserDwordKey.GetValue("SystemUsesLightTheme")
#Disable Warning BC42018
        If AppsUseLightTheme_CurrentUserDwordValue = 0 Then
#Enable Warning BC42018
            RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On
            RTB_ThemeToggle.Image = My.Resources.icons8_moon_and_stars_24
        Else
            RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off
            RTB_ThemeToggle.Image = My.Resources.icons8_sun_24
        End If
        'Else
        '    My.Settings.UseSystemTheme = False
        'End If
    End Sub

    ''' <summary>
    ''' Changes if the latest Build should be auto-loaded, depending on the ToggleState
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RTS_AutoLoad_ValueChanged(sender As Object, e As EventArgs) Handles RTS_AutoLoad.ValueChanged
        If RTS_AutoLoad.Value Then
            My.Settings.AutoLoad = True
        Else
            My.Settings.AutoLoad = False
        End If
    End Sub

    ''' <summary>
    ''' Function that saves a Two Character Language Code to Settings while restarting the application afterwards.
    ''' </summary>
    ''' <param name="TwoCharLang">Two Character Language Code. For example: de for German, zh for Chinese, ...</param>
    Private Sub ChangeLanguage(TwoCharLang As String)
        My.Settings.TwoCharLanguageCode = TwoCharLang
        My.Settings.Save()

        RadTD.ShowDialog($" {My.Resources.Language_Heading}", My.Resources.Language_Heading, My.Resources.Language_Text, RadTaskDialogIcon.Information)

        Dim pStart As New ProcessStartInfo With {
            .WindowStyle = ProcessWindowStyle.Normal,
            .FileName = Application.ExecutablePath
        }

        Process.Start(pStart)
        Application.Exit()
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to English
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_English_Click(sender As Object, e As EventArgs) Handles RMI_L_English.Click
        ChangeLanguage("en")
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to German
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_German_Click(sender As Object, e As EventArgs) Handles RMI_L_German.Click
        ChangeLanguage("de")
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to Chinese
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_Chinese_Click(sender As Object, e As EventArgs) Handles RMI_L_Chinese.Click
        ChangeLanguage("zh")
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to Polish
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_Polish_Click(sender As Object, e As EventArgs) Handles RMI_L_Polish.Click
        ChangeLanguage("pl")
    End Sub

    ''' <summary>
    ''' Language Button. Changes the Language to Indonesian
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_L_Indonesian_Click(sender As Object, e As EventArgs) Handles RMI_L_Indonesian.Click
        ChangeLanguage("id")
    End Sub
End Class