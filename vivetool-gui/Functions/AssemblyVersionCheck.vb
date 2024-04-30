'ViVeTool GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2024 Peter Strick
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
Imports System.Diagnostics, System.Reflection

''' <summary>
''' Module for Checking differences between Referenced Assembly Version and DLL Version
''' </summary>
Module AssemblyVersion
    ''' <summary>
    ''' Sub that Checks the ViVe API, AutoUpdater.NET, Sentry, WebView2 and Telerik DLLs for Version differences
    ''' </summary>
    Public Sub CheckAssemblyVersion()
        For Each Reference In Assembly.GetExecutingAssembly().GetReferencedAssemblies()
            Select Case Reference.Name
                Case "Albacore.ViVe"
                    Check(Application.StartupPath & "\Albacore.ViVe.dll", Reference.Version.ToString)
                Case "AutoUpdater.NET"
                    Check(Application.StartupPath & "\AutoUpdater.NET.dll", Reference.Version.ToString)
                Case "Sentry"
                    Check(Application.StartupPath & "\Sentry.dll", Reference.Version.ToString)
                Case "Microsoft.Web.WebView2.Core"
                    Check(Application.StartupPath & "\Microsoft.Web.WebView2.Core.dll", Reference.Version.ToString)
                Case "Microsoft.Web.WebView2.WinForms"
                    Check(Application.StartupPath & "\Microsoft.Web.WebView2.WinForms.dll", Reference.Version.ToString)
                Case "Telerik.WinControls"
                    Check(Application.StartupPath & "\Telerik.WinControls.dll", Reference.Version.ToString)
                Case "Telerik.WinControls.GridView"
                    Check(Application.StartupPath & "\Telerik.WinControls.GridView.dll", Reference.Version.ToString)
                Case "Telerik.WinControls.RadMarkupEditor"
                    Check(Application.StartupPath & "\Telerik.WinControls.RadMarkupEditor.dll", Reference.Version.ToString)
                Case "Telerik.WinControls.Themes.Fluent"
                    Check(Application.StartupPath & "\Telerik.WinControls.Themes.Fluent.dll", Reference.Version.ToString)
                Case "Telerik.WinControls.Themes.FluentDark"
                    Check(Application.StartupPath & "\Telerik.WinControls.Themes.FluentDark.dll", Reference.Version.ToString)
                Case "Telerik.WinControls.UI"
                    Check(Application.StartupPath & "\Telerik.WinControls.UI.dll", Reference.Version.ToString)
                Case "TelerikCommon"
                    Check(Application.StartupPath & "\TelerikCommon.dll", Reference.Version.ToString)
            End Select
        Next
    End Sub

    ''' <summary>
    ''' Helper Function that checks if two Strings are the same, shows a Message and Quits the Application if they aren't
    ''' </summary>
    ''' <param name="DLLFileName">Absolute Path to a DLL File</param>
    ''' <param name="VersionToCheck">String containing a Version to Check against</param>
    Private Sub Check(DLLFileName As String, VersionToCheck As String)
        Try
            If Not FileVersionInfo.GetVersionInfo(DLLFileName).FileVersion = VersionToCheck Then
                MsgBox(My.Resources.Check_AssemblyVersions_Text, MsgBoxStyle.Critical, My.Resources.Check_AssemblyVersions_Title)
                Environment.Exit(-1)
            End If
        Catch ex As Exception
            MsgBox(My.Resources.Check_AssemblyVersions_Text, MsgBoxStyle.Critical, My.Resources.Check_AssemblyVersions_Title)
            Environment.Exit(-1)
        End Try
    End Sub
End Module