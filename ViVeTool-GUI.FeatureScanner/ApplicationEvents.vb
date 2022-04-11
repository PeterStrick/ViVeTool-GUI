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
Imports System.Configuration, Telerik.WinControls.UI

Namespace My
    ' Für MyApplication sind folgende Ereignisse verfügbar:
    ' Startup: Wird beim Starten der Anwendung noch vor dem Erstellen des Startformulars ausgelöst.
    ' Shutdown: Wird nach dem Schließen aller Anwendungsformulare ausgelöst.  Dieses Ereignis wird nicht ausgelöst, wenn die Anwendung mit einem Fehler beendet wird.
    ' UnhandledException: Wird bei einem Ausnahmefehler ausgelöst.
    ' StartupNextInstance: Wird beim Starten einer Einzelinstanzanwendung ausgelöst, wenn die Anwendung bereits aktiv ist. 
    ' NetworkAvailabilityChanged: Wird beim Herstellen oder Trennen der Netzwerkverbindung ausgelöst.
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            'Transfers older My.Settings to newer ViVeTool GUI Versions if applicable.
            If Not ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).HasFile Then Settings.Upgrade()

            'Load the Text Boxes
            ScannerUI.RTB_DbgPath.Text = My.Settings.DebuggerPath
            ScannerUI.RTB_SymbolPath.Text = My.Settings.SymbolPath

            'Check if mach2.exe and msdia140.dll are present
            If IO.File.Exists(Application.Info.DirectoryPath & "\mach2.exe") = False OrElse IO.File.Exists(Application.Info.DirectoryPath & "\msdia140.dll") = False Then
                Dim RTD As New RadTaskDialogPage With {
                       .Caption = " An Error occurred",
                       .Heading = "An Error occurred. Files required for ViVeTool GUI - Feature Scanner to work properly are missing.",
                       .Text = "Please check that the following files are present:" & vbNewLine & vbNewLine & "mach2.exe" & vbNewLine & "msdia140.dll",
                       .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                   }
                'Show the Message Box
                RadTaskDialog.ShowDialog(RTD)
                End
            End If

            'Check if DynamicTheme is enabled, else Enable Dark Mode if previously turned on
            If Settings.UseSystemTheme Then
                'Set ToggleState for RTB_UseSystemTheme
                ScannerUI.RTB_UseSystemTheme.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On

                'Get Regsitry Key Value
                Dim AppsUseLightTheme_CurrentUserDwordKey As Microsoft.Win32.RegistryKey = Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
                Dim AppsUseLightTheme_CurrentUserDwordValue As Object = AppsUseLightTheme_CurrentUserDwordKey.GetValue("SystemUsesLightTheme")

                'If the Value is 0 then Light Mode is Disabled, if it is 1 then it is Enabled
#Disable Warning BC42018 ' Für den Operator werden Operanden vom Typ "Object" verwendet.
                If AppsUseLightTheme_CurrentUserDwordValue = 0 Then
#Enable Warning BC42018 ' Für den Operator werden Operanden vom Typ "Object" verwendet.
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
