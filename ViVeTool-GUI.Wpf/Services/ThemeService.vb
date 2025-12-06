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

Imports System.Windows

Namespace Services
    ''' <summary>
    ''' Theme mode options for the application.
    ''' </summary>
    Public Enum AppThemeMode
        Light
        Dark
        System
    End Enum

    ''' <summary>
    ''' Service for managing application themes (Light/Dark/System) with .NET 9 Fluent theme support.
    ''' </summary>
    Public Class ThemeService
        Private Shared _instance As ThemeService
        Private Shared ReadOnly _lock As New Object()
        
        Private _currentTheme As AppThemeMode = AppThemeMode.System
        Private _useFluentTheme As Boolean = True
        
        Private Const FluentThemeUri As String = "/PresentationFramework.Fluent;component/Themes/Fluent.xaml"
        
        ''' <summary>
        ''' Gets the singleton instance of ThemeService.
        ''' </summary>
        Public Shared ReadOnly Property Instance As ThemeService
            Get
                If _instance Is Nothing Then
                    SyncLock _lock
                        If _instance Is Nothing Then
                            _instance = New ThemeService()
                        End If
                    End SyncLock
                End If
                Return _instance
            End Get
        End Property

        ''' <summary>
        ''' Gets the current theme mode name for display.
        ''' </summary>
        Public ReadOnly Property CurrentTheme As String
            Get
                Select Case _currentTheme
                    Case AppThemeMode.Light
                        Return "Light"
                    Case AppThemeMode.Dark
                        Return "Dark"
                    Case Else
                        Return "System"
                End Select
            End Get
        End Property

        ''' <summary>
        ''' Gets the current theme mode.
        ''' </summary>
        Public ReadOnly Property CurrentThemeMode As AppThemeMode
            Get
                Return _currentTheme
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets whether the Fluent theme is enabled.
        ''' When disabled, falls back to Aero2 (classic WPF theme).
        ''' </summary>
        Public Property UseFluentTheme As Boolean
            Get
                Return _useFluentTheme
            End Get
            Set(value As Boolean)
                If _useFluentTheme <> value Then
                    _useFluentTheme = value
                    ApplyTheme(_currentTheme)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Private constructor for singleton pattern.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Applies the specified theme mode to the application.
        ''' </summary>
        ''' <param name="themeMode">The theme mode to apply.</param>
        Public Sub ApplyTheme(themeMode As AppThemeMode)
            _currentTheme = themeMode

            ' Set the Application ThemeMode property for .NET 9 Fluent theme
            Dim app = Application.Current
            If app IsNot Nothing Then
                Select Case themeMode
                    Case AppThemeMode.Light
                        app.ThemeMode = System.Windows.ThemeMode.Light
                    Case AppThemeMode.Dark
                        app.ThemeMode = System.Windows.ThemeMode.Dark
                    Case Else
                        app.ThemeMode = System.Windows.ThemeMode.System
                End Select

                ' Update Fluent theme dictionary based on UseFluentTheme setting
                UpdateFluentThemeDictionary(app)

                ' Update app-specific theme dictionary
                UpdateAppThemeDictionary(app)
            End If
        End Sub

        ''' <summary>
        ''' Applies the specified theme to the application (legacy string-based overload).
        ''' </summary>
        ''' <param name="themeName">The theme name ("Light", "Dark", or "System").</param>
        Public Sub ApplyTheme(themeName As String)
            If String.IsNullOrEmpty(themeName) Then
                themeName = "System"
            End If

            Select Case themeName.ToUpperInvariant()
                Case "LIGHT"
                    ApplyTheme(AppThemeMode.Light)
                Case "DARK"
                    ApplyTheme(AppThemeMode.Dark)
                Case Else
                    ApplyTheme(AppThemeMode.System)
            End Select
        End Sub

        ''' <summary>
        ''' Updates the Fluent theme dictionary based on UseFluentTheme setting.
        ''' </summary>
        Private Sub UpdateFluentThemeDictionary(app As Application)
            Dim mergedDictionaries = app.Resources.MergedDictionaries
            
            ' Find existing Fluent theme dictionary
            Dim existingFluent = mergedDictionaries.FirstOrDefault(Function(d) 
                Return d.Source IsNot Nothing AndAlso 
                       d.Source.ToString().Contains("Fluent.xaml")
            End Function)

            If _useFluentTheme Then
                ' Add Fluent theme if not present
                If existingFluent Is Nothing Then
                    Dim fluentDictionary As New ResourceDictionary()
                    fluentDictionary.Source = New Uri(FluentThemeUri, UriKind.Relative)
                    ' Insert at the beginning so app themes can override
                    mergedDictionaries.Insert(0, fluentDictionary)
                End If
            Else
                ' Remove Fluent theme if present (falls back to Aero2)
                If existingFluent IsNot Nothing Then
                    mergedDictionaries.Remove(existingFluent)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Updates the app-specific theme dictionary based on the current theme mode.
        ''' </summary>
        Private Sub UpdateAppThemeDictionary(app As Application)
            Dim mergedDictionaries = app.Resources.MergedDictionaries
            
            ' Determine which app theme to use based on current mode
            Dim useDarkAppTheme As Boolean
            Select Case _currentTheme
                Case AppThemeMode.Light
                    useDarkAppTheme = False
                Case AppThemeMode.Dark
                    useDarkAppTheme = True
                Case Else
                    ' For System mode, detect system preference
                    useDarkAppTheme = IsSystemDarkMode()
            End Select

            Dim themeUri As Uri
            If useDarkAppTheme Then
                themeUri = New Uri("pack://application:,,,/ViVeTool_GUI.Wpf;component/Themes/Dark.xaml", UriKind.Absolute)
            Else
                themeUri = New Uri("pack://application:,,,/ViVeTool_GUI.Wpf;component/Themes/Light.xaml", UriKind.Absolute)
            End If

            ' Find and remove existing app theme dictionary
            Dim existingTheme = mergedDictionaries.FirstOrDefault(Function(d) 
                Return d.Source IsNot Nothing AndAlso 
                       (d.Source.ToString().Contains("Themes/Light.xaml") OrElse 
                        d.Source.ToString().Contains("Themes/Dark.xaml"))
            End Function)

            If existingTheme IsNot Nothing Then
                mergedDictionaries.Remove(existingTheme)
            End If

            ' Add the new theme dictionary at the end (highest priority)
            Dim themeDictionary As New ResourceDictionary()
            themeDictionary.Source = themeUri
            mergedDictionaries.Add(themeDictionary)
        End Sub

        ''' <summary>
        ''' Detects if Windows is in dark mode.
        ''' </summary>
        Private Function IsSystemDarkMode() As Boolean
            Try
                Using key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize")
                    If key IsNot Nothing Then
                        Dim value = key.GetValue("AppsUseLightTheme")
                        If value IsNot Nothing Then
                            Return CInt(value) = 0
                        End If
                    End If
                End Using
            Catch
                ' Fall back to light mode on error
            End Try
            Return False
        End Function

        ''' <summary>
        ''' Cycles through theme modes: Light -> Dark -> System -> Light.
        ''' </summary>
        Public Sub CycleTheme()
            Select Case _currentTheme
                Case AppThemeMode.Light
                    ApplyTheme(AppThemeMode.Dark)
                Case AppThemeMode.Dark
                    ApplyTheme(AppThemeMode.System)
                Case Else
                    ApplyTheme(AppThemeMode.Light)
            End Select
        End Sub

        ''' <summary>
        ''' Toggles between Light and Dark themes (legacy behavior).
        ''' </summary>
        Public Sub ToggleTheme()
            CycleTheme()
        End Sub

        ''' <summary>
        ''' Loads the saved theme preference and applies it.
        ''' </summary>
        Public Sub LoadSavedTheme()
            ' TODO: Load theme from persisted settings
            ' Example: Dim savedTheme = My.Settings.Theme
            ' If String.IsNullOrEmpty(savedTheme) Then savedTheme = "System"
            ' ApplyTheme(savedTheme)
            
            ' Default to System theme mode for Fluent theme
            ApplyTheme(AppThemeMode.System)
        End Sub

        ''' <summary>
        ''' Sets whether to use Fluent theme or fall back to Aero2.
        ''' </summary>
        ''' <param name="useFluent">True to use Fluent theme, False to fall back to Aero2.</param>
        Public Sub SetFluentThemeEnabled(useFluent As Boolean)
            UseFluentTheme = useFluent
        End Sub
    End Class
End Namespace
