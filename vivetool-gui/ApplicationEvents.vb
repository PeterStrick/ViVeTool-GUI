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
Namespace My
    ' Für MyApplication sind folgende Ereignisse verfügbar:
    ' Startup: Wird beim Starten der Anwendung noch vor dem Erstellen des Startformulars ausgelöst.
    ' Shutdown: Wird nach dem Schließen aller Anwendungsformulare ausgelöst.  Dieses Ereignis wird nicht ausgelöst, wenn die Anwendung mit einem Fehler beendet wird.
    ' UnhandledException: Wird bei einem Ausnahmefehler ausgelöst.
    ' StartupNextInstance: Wird beim Starten einer Einzelinstanzanwendung ausgelöst, wenn die Anwendung bereits aktiv ist. 
    ' NetworkAvailabilityChanged: Wird beim Herstellen oder Trennen der Netzwerkverbindung ausgelöst.
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
            ' Check for Build
            If Environment.OSVersion.Version.Build >= 18963 Then
                ' OS Build Check passed.
            Else
                MsgBox("You are running a unsupported Windows 10 Build. ViVe, ViVeTool and ViVeTool-GUI require Windows 10 Build 18963 or higher. Your Build is: " & Environment.OSVersion.Version.Build.ToString, vbCritical, "Unsupported Build")
                Environment.Exit(-1)
            End If

            'Load Settings States from My.Settings
            'Set ToggleState for RTS_AutoLoad
            If My.Settings.AutoLoad = True Then
                AboutAndSettings.RTS_AutoLoad.SetToggleState(True)
            Else
                AboutAndSettings.RTS_AutoLoad.SetToggleState(False)
            End If

            'Set ToggleState for RTB_UseSystemTheme
            If My.Settings.UseSystemTheme = True Then
                AboutAndSettings.RTB_UseSystemTheme.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On
            Else
                AboutAndSettings.RTB_UseSystemTheme.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off
            End If

            'Check if DynamicTheme is enabled, else Enable Dark Mode if previously turned on
            If My.Settings.UseSystemTheme = True Then
                'Get Regsitry Key Value
                Dim AppsUseLightTheme_CurrentUserDwordKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
                Dim AppsUseLightTheme_CurrentUserDwordValue As Object = AppsUseLightTheme_CurrentUserDwordKey.GetValue("SystemUsesLightTheme")

                'If the Value is 0 then Light Mode is Disabled, if it is 1 then it is Enabled
                If AppsUseLightTheme_CurrentUserDwordValue = 0 Then
                    AboutAndSettings.RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On
                    AboutAndSettings.RTB_ThemeToggle.Image = My.Resources.icons8_moon_and_stars_24
                Else
                    AboutAndSettings.RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off
                    AboutAndSettings.RTB_ThemeToggle.Image = My.Resources.icons8_sun_24
                End If
            Else
                If Settings.DarkMode Then
                    Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "FluentDark"
                    AboutAndSettings.RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On
                    AboutAndSettings.RTB_ThemeToggle.Image = My.Resources.icons8_moon_and_stars_24
                Else
                    Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "Fluent"
                    AboutAndSettings.RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off
                    AboutAndSettings.RTB_ThemeToggle.Image = My.Resources.icons8_sun_24
                End If
            End If
        End Sub
    End Class
End Namespace