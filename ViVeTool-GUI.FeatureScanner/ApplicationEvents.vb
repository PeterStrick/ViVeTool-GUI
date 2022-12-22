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
Imports System.Configuration, Telerik.WinControls.UI

Namespace My
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            'Check for Build
            If Environment.OSVersion.Version.Build >= 18963 Then
                'OS Build Check passed.
            Else
                RadTD.Show(Resources.Error_Spaced_UnsupportedBuild, Resources.Error_UnsupportedBuild,
                           String.Format(Resources.Error_UnsupportedBuild_Text_N, Environment.OSVersion.Version.Build.ToString),
                           RadTaskDialogIcon.ShieldErrorRedBar)
                End
            End If

            'Transfers older My.Settings to newer ViVeTool GUI Versions if applicable.
            If Not ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).HasFile Then Settings.Upgrade()

            'Load the Text Boxes
            ScannerUI.RTB_DbgPath.Text = Settings.DebuggerPath
            ScannerUI.RTB_SymbolPath.Text = Settings.SymbolPath

            'Check if mach2.exe and msdia140.dll are present
            If IO.File.Exists(Application.Info.DirectoryPath & "\mach2\mach2.exe") = False OrElse IO.File.Exists(Application.Info.DirectoryPath & "\mach2\msdia140.dll") = False Then
                'Show the Message Box
                RadTD.Show(Resources.Error_Spaced_AnErrorOccurred, Resources.Error_MissingFiles_Heading,
                           Resources.Error_MissingFiles_Text & vbNewLine & vbNewLine & "mach2.exe" & vbNewLine & "msdia140.dll",
                           RadTaskDialogIcon.ShieldErrorRedBar)
                End
            End If

            'Check if DynamicTheme is enabled, else Enable Dark Mode if previously turned on
            If Settings.UseSystemTheme Then
                'Set ToggleState for RTB_UseSystemTheme
                ScannerUI.RTB_UseSystemTheme.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On

                'Get Registry Key Value
                Dim AppsUseLightTheme_CurrentUserDwordKey As Microsoft.Win32.RegistryKey = Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
                Dim AppsUseLightTheme_CurrentUserDwordValue As Object = AppsUseLightTheme_CurrentUserDwordKey.GetValue("SystemUsesLightTheme")

                'If the Value is 0 then Light Mode is Disabled, if it is 1 then it is Enabled
#Disable Warning BC42018
                If AppsUseLightTheme_CurrentUserDwordValue = 0 Then
#Enable Warning BC42018
                    ScannerUI.RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On
                    ScannerUI.RTB_ThemeToggle.Image = Resources.icons8_moon_and_stars_24
                Else
                    ScannerUI.RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off
                    ScannerUI.RTB_ThemeToggle.Image = Resources.icons8_sun_24
                End If
            Else
                'Set ToggleState for RTB_UseSystemTheme
                ScannerUI.RTB_UseSystemTheme.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off

                If Settings.DarkMode Then
                    Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "FluentDark"
                    ScannerUI.RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On
                    ScannerUI.RTB_ThemeToggle.Image = Resources.icons8_moon_and_stars_24
                Else
                    Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "Fluent"
                    ScannerUI.RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off
                    ScannerUI.RTB_ThemeToggle.Image = Resources.icons8_sun_24
                End If
            End If
        End Sub
    End Class
End Namespace