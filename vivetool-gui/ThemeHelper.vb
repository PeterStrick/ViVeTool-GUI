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
''' Helper class for theme-related operations
''' Note: Standard WinForms uses system theme by default. This class is maintained for settings compatibility.
''' </summary>
Public NotInheritable Class ThemeHelper
    ''' <summary>
    ''' Applies dark theme to the application (stores preference)
    ''' </summary>
    ''' <param name="toggleButton">The toggle button to update</param>
    ''' <param name="darkModeImage">Image to use for dark mode</param>
    Public Shared Sub ApplyDarkTheme(toggleButton As System.Windows.Forms.CheckBox, darkModeImage As System.Drawing.Image)
        toggleButton.Text = "Dark Theme"
        toggleButton.Image = darkModeImage
        My.Settings.DarkMode = True
    End Sub

    ''' <summary>
    ''' Applies light theme to the application (stores preference)
    ''' </summary>
    ''' <param name="toggleButton">The toggle button to update</param>
    ''' <param name="lightModeImage">Image to use for light mode</param>
    Public Shared Sub ApplyLightTheme(toggleButton As System.Windows.Forms.CheckBox, lightModeImage As System.Drawing.Image)
        toggleButton.Text = "Light Theme"
        toggleButton.Image = lightModeImage
        My.Settings.DarkMode = False
    End Sub

    ''' <summary>
    ''' Applies system theme based on Windows settings
    ''' </summary>
    ''' <param name="toggleButton">The toggle button to update</param>
    ''' <param name="darkModeImage">Image to use for dark mode</param>
    ''' <param name="lightModeImage">Image to use for light mode</param>
    Public Shared Sub ApplySystemTheme(toggleButton As System.Windows.Forms.CheckBox, darkModeImage As System.Drawing.Image, lightModeImage As System.Drawing.Image)
        My.Settings.UseSystemTheme = True
        Dim AppsUseLightTheme_CurrentUserDwordKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
        If AppsUseLightTheme_CurrentUserDwordKey Is Nothing Then
            ' Registry key not found, default to light theme
            toggleButton.Checked = False
            toggleButton.Image = lightModeImage
            Return
        End If
        Dim AppsUseLightTheme_CurrentUserDwordValue As Object = AppsUseLightTheme_CurrentUserDwordKey.GetValue("SystemUsesLightTheme")
        If AppsUseLightTheme_CurrentUserDwordValue IsNot Nothing AndAlso CInt(AppsUseLightTheme_CurrentUserDwordValue) = 0 Then
            toggleButton.Checked = True
            toggleButton.Image = darkModeImage
        Else
            toggleButton.Checked = False
            toggleButton.Image = lightModeImage
        End If
    End Sub

    ''' <summary>
    ''' Disables system theme usage
    ''' </summary>
    Public Shared Sub DisableSystemTheme()
        My.Settings.UseSystemTheme = False
    End Sub
End Class
