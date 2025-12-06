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
Imports System.Configuration

Namespace My
    ''' <summary>
    ''' .Net Framework Application Framework
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Partial Friend Class MyApplication
        ''' <summary>
        ''' Used to set the Application Theme on Launch by determining, if My.Settings.DarkMode is True
        ''' </summary>
        ''' <param name="sender">Default sender Object</param>
        ''' <param name="e">Default EventArgs</param>
        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            'Check for Build
            If Environment.OSVersion.Version.Build >= 18963 Then
                'OS Build Check passed.
            Else
                MsgBox("You are running a unsupported Windows 10 Build. ViVe, ViVeTool and ViVeTool-GUI require Windows 10 Build 18963 or higher. Your Build is: " & Environment.OSVersion.Version.Build.ToString, vbCritical, "Unsupported Build")
                Environment.Exit(-1)
            End If

            'Transfers older My.Settings to newer ViVeTool GUI Versions if applicable.
            If Not ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).HasFile Then Settings.Upgrade()

            'Load Settings States from My.Settings
            'Set CheckState for CB_AutoLoad
            AboutAndSettings.CB_AutoLoad.Checked = Settings.AutoLoad

            'Check if DynamicTheme is enabled, else Enable Dark Mode if previously turned on
            If Settings.UseSystemTheme Then
                'Set CheckState for CB_UseSystemTheme
                AboutAndSettings.CB_UseSystemTheme.Checked = True

                'Get Registry Key Value
                Dim AppsUseLightTheme_CurrentUserDwordKey As Microsoft.Win32.RegistryKey = Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
                Dim AppsUseLightTheme_CurrentUserDwordValue As Object = AppsUseLightTheme_CurrentUserDwordKey.GetValue("SystemUsesLightTheme")

                'If the Value is 0 then Light Mode is Disabled, if it is 1 then it is Enabled
#Disable Warning BC42018
                If AppsUseLightTheme_CurrentUserDwordValue = 0 Then
#Enable Warning BC42018
                    AboutAndSettings.CB_ThemeToggle.Checked = True
                    AboutAndSettings.CB_ThemeToggle.Image = Resources.icons8_moon_and_stars_24
                Else
                    AboutAndSettings.CB_ThemeToggle.Checked = False
                    AboutAndSettings.CB_ThemeToggle.Image = Resources.icons8_sun_24
                End If
            Else
                'Set CheckState for CB_UseSystemTheme
                AboutAndSettings.CB_UseSystemTheme.Checked = False

                If Settings.DarkMode Then
                    AboutAndSettings.CB_ThemeToggle.Checked = True
                    AboutAndSettings.CB_ThemeToggle.Image = Resources.icons8_moon_and_stars_24
                Else
                    AboutAndSettings.CB_ThemeToggle.Checked = False
                    AboutAndSettings.CB_ThemeToggle.Image = Resources.icons8_sun_24
                End If
            End If
        End Sub
    End Class
End Namespace