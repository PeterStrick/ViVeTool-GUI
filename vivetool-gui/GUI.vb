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
Imports System.Collections.Generic, Newtonsoft.Json.Linq, System.Net, Telerik.WinControls.Data, Albacore.ViVe

''' <summary>
''' ViVeTool GUI
''' </summary>
Public Class GUI
    ''' <summary>
    ''' Load Event, Populates the Build Combo Box
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub GUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim TempJSONUsedInDevelopment As String = "{
  ""sha"": ""afeb63367f1bd15d63cfe30541a9a6ee51b940dd"",
  ""url"": ""https://api.github.com/repos/riverar/mach2/git/trees/afeb63367f1bd15d63cfe30541a9a6ee51b940dd"",
  ""tree"": [
    {
      ""path"": ""17643.txt"",
      ""mode"": ""100644"",
      ""type"": ""blob"",
      ""sha"": ""ad8db3758b98fe1e6501077a06af93671f82a5d6"",
      ""size"": 2534810,
      ""url"": ""https://api.github.com/repos/riverar/mach2/git/blobs/ad8db3758b98fe1e6501077a06af93671f82a5d6""
    },
    {
      ""path"": ""17643_17650_diff.patch"",
      ""mode"": ""100644"",
      ""type"": ""blob"",
      ""sha"": ""d977490592e8ccf31238b08b9c99550c703e5271"",
      ""size"": 7575,
      ""url"": ""https://api.github.com/repos/riverar/mach2/git/blobs/d977490592e8ccf31238b08b9c99550c703e5271""
    },
    {
      ""path"": ""17650.txt"",
      ""mode"": ""100644"",
      ""type"": ""blob"",
      ""sha"": ""61f1358312c832eae48d218d0ac86c1b3e576540"",
      ""size"": 39631,
      ""url"": ""https://api.github.com/repos/riverar/mach2/git/blobs/61f1358312c832eae48d218d0ac86c1b3e576540""
    },
    {
      ""path"": ""17650_17655_diff.patch"",
      ""mode"": ""100644"",
      ""type"": ""blob"",
      ""sha"": ""428821fa035728c38fc22d681fdc1e9748516bcc"",
      ""size"": 2496,
      ""url"": ""https://api.github.com/repos/riverar/mach2/git/blobs/428821fa035728c38fc22d681fdc1e9748516bcc""
    },
    {
      ""path"": ""22543.txt"",
      ""mode"": ""100644"",
      ""type"": ""blob"",
      ""sha"": ""2217ec332eccb0094cfd04cb94e1ff77b636da81"",
      ""size"": 73548,
      ""url"": ""https://api.github.com/repos/riverar/mach2/git/blobs/2217ec332eccb0094cfd04cb94e1ff77b636da81""
    }  ],
  ""truncated"": false
}"
        'returns JSON File Contents of riverar/mach2/features
        Dim URL As String = "https://api.github.com/repos/riverar/mach2/git/trees/afeb63367f1bd15d63cfe30541a9a6ee51b940dd"

        'Required Headers for the GitHub API
        Dim WebClient As New WebClient With {
            .Encoding = System.Text.Encoding.UTF8
        }
        WebClient.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8")
        WebClient.Headers.Add(HttpRequestHeader.UserAgent, "PeterStrick/mach2-gui")

        'Get the "tree" array from the API JSON Result

        '[DEV] Use Demo JSON to not get rate limited
        'Dim ContentsJSON As String = WebClient.DownloadString(URL)
        Dim ContentsJSON As String = TempJSONUsedInDevelopment

        Dim JSONObject As JObject = JObject.Parse(ContentsJSON)
        Dim JSONArray As JArray = CType(JSONObject.SelectToken("tree"), JArray)

        'For each element in the Array, add a Combo Box Item with the name of the path element
        For Each element In JSONArray

            Dim Name() As String = element("path").ToString.Split(".")

            If Name(1) = "txt" Then
                RDDL_Build.Items.Add(Name(0))
            Else
                'MsgBox("Fail. Extension is " & Name(1))
            End If
        Next

        '[DEV] Dummy Row
        'RGV_MainGridView.Rows.Add("Dummy - 36808198", "36808198", "Unknown")

    End Sub

    ''' <summary>
    ''' Disables the Combo Box and runs the Background Worker each time the Combo Box Selected Index changes.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub PopulateDataGridView(sender As Object, e As EventArgs) Handles RDDL_Build.SelectedIndexChanged
        RDDL_Build.Enabled = False
        BGW_PopulateGridView.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' Background Worker that populates the Grid View with the following steps:
    ''' 1. Set Status Label and Clear Grid View Rows
    ''' 2. Prepare WebClient and Download a FeatureID Text File, corresponding to the selected Build to %TEMP%
    ''' 3. Fix the Line Formatting of the Text File and remove Comments
    ''' 4. For Each Line, add the Feature Name and Feature ID to the Grid View, while also determining the Feature EnabledState and adding that to the Grid View as well.
    ''' 5. At last, Move to the First Row, Clear the selection and change the Status Label to Done.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub BGW_PopulateGridView_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGW_PopulateGridView.DoWork
        If BGW_PopulateGridView.CancellationPending = False Then
            'Set Status Label
            Invoke(Sub() RLE_StatusLabel.Text = "Populating Data Grid View... This can take a while.")

            'Clear Data Grid View
            Invoke(Sub() RGV_MainGridView.Rows.Clear())

            'Prepare Web Client and download Build TXT
            Dim WebClient As New WebClient With {
                .Encoding = System.Text.Encoding.UTF8
            }
            Dim path As String = IO.Path.GetTempPath & RDDL_Build.Text & ".txt"
            WebClient.DownloadFile("https://raw.githubusercontent.com/riverar/mach2/master/features/" + RDDL_Build.Text + ".txt", path)

            'Fix Text File Formatting and remove comments
            Dim newLines = From line In IO.File.ReadAllLines(path)
                           Where Not line.StartsWith("#")
            IO.File.WriteAllLines(path, newLines)

            'For each line add a grid view entry
            For Each Line In IO.File.ReadAllLines(path)
                'Split the Line at the :
                Dim Str() As String = Line.Split(": ")

                'Remove any Spaces from the first Str Array (Feature Name)


                'If the Line is not empty, continue
                If Line IsNot "" Then
                    'Get the Feature Enabled State from the currently processing line.
                    'RtlFeatureManager.QueryFeatureConfiguration will return Enabled, Disabled or throw a NullReferenceException for Default
                    Try
                        Dim State As String = RtlFeatureManager.QueryFeatureConfiguration(CUInt(Str(1)), FeatureConfigurationSection.Runtime).EnabledState.ToString
                        Invoke(Sub() RGV_MainGridView.Rows.Add(Str(0), Str(1), State))
                    Catch ex As NullReferenceException
                        Invoke(Sub() RGV_MainGridView.Rows.Add(Str(0), Str(1), "Default"))
                    End Try
                End If
            Next

            'Move to the first row, remove the selection and change the Status Label to Done.
            Invoke(Sub()
                       RGV_MainGridView.CurrentRow = RGV_MainGridView.Rows.Item(0)
                       RGV_MainGridView.CurrentRow = Nothing
                       RLE_StatusLabel.Text = "Done."
                   End Sub)
        Else
            Return
        End If
    End Sub

    ''' <summary>
    ''' Upon Background Worker Completion, stop the Background Worker and re-enable the COmbo Box
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub BGW_PopulateGridView_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGW_PopulateGridView.RunWorkerCompleted
        'End BGW
        BGW_PopulateGridView.CancelAsync()

        'Enable Build Combo Box
        RDDL_Build.Enabled = True
    End Sub

    ''' <summary>
    ''' Filter the Feature Name Column, with the RTB_SearchF Text on each TextChanged Event
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RTB_SearchF_TextChanged(sender As Object, e As EventArgs) Handles RTB_SearchF.TextChanged
        Dim SearchBoxFilter As New FilterDescriptor With {
            .Operator = FilterOperator.Contains,
            .Value = RTB_SearchF.Text,
            .IsFilterEditor = True
        }
        RGV_MainGridView.Columns("FeatureName").FilterDescriptor = SearchBoxFilter
    End Sub

    ''' <summary>
    ''' Button to toggle between Light and Dark Fluent Themes
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RTB_DarkTheme_ToggleStateChanging(sender As Object, args As Telerik.WinControls.UI.StateChangingEventArgs) Handles RTB_ThemeToggle.ToggleStateChanging
        If args.NewValue = Telerik.WinControls.Enumerations.ToggleState.On Then
            RTB_ThemeToggle.Text = "Dark Theme"
            Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "FluentDark"
        Else
            RTB_ThemeToggle.Text = "Light Theme"
            Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "Fluent"
        End If
    End Sub

    ''' <summary>
    ''' Enable Feature Button, enables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_ActivateF_Click(sender As Object, e As EventArgs) Handles RMI_ActivateF.Click
        SetConfig(FeatureEnabledState.Enabled)
    End Sub

    ''' <summary>
    ''' Disable Feature Button, disables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_DeactivateF_Click(sender As Object, e As EventArgs) Handles RMI_DeactivateF.Click
        SetConfig(FeatureEnabledState.Disabled)
    End Sub

    ''' <summary>
    ''' Revert Feature Button, reverts the currently selected Feature back to default settings.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_RevertF_Click(sender As Object, e As EventArgs) Handles RMI_RevertF.Click
        SetConfig(FeatureEnabledState.Default)
    End Sub

    ''' <summary>
    ''' Set's the Feature Configuration. Uses the FeatureEnabledState parameter to set the EnabledState of the Feature
    ''' </summary>
    ''' <param name="FeatureEnabledState">Specifies what Enabled State the Feature should be in. Can be either Enabled, Disabled or Default</param>
    Private Sub SetConfig(FeatureEnabledState As FeatureEnabledState)
        Try
            'Initialize Variables
            Dim _enabledStateOptions, _variant, _variantPayloadKind, _variantPayload, _group As Integer
            _enabledStateOptions = 1
            _group = 4

            'FeatureConfiguration Variable
            Dim _configs As New List(Of FeatureConfiguration) From {
                New FeatureConfiguration() With {
                    .FeatureId = CUInt(RGV_MainGridView.SelectedRows.Item(0).Cells(1).Value),
                    .EnabledState = FeatureEnabledState,
                    .EnabledStateOptions = _enabledStateOptions,
                    .Group = _group,
                    .[Variant] = _variant,
                    .VariantPayload = _variantPayload,
                    .VariantPayloadKind = _variantPayloadKind,
                    .Action = FeatureConfigurationAction.UpdateEnabledState
                }
            }

            'Set's the selected Feature to it's specified EnabledState. If anything goes wrong, display a Error Message in the Status Label.
            'On Successful Operations; 
            'RtlFeatureManager.SetBootFeatureConfigurations(_configs) returns True
            'and RtlFeatureManager.SetLiveFeatureConfigurations(_configs, FeatureConfigurationSection.Runtime) returns 0
            If RtlFeatureManager.SetBootFeatureConfigurations(_configs) = False Or RtlFeatureManager.SetLiveFeatureConfigurations(_configs, FeatureConfigurationSection.Runtime) >= 1 Then
                RLE_StatusLabel.Text = "An error occurred while setting a feature configuration for " + RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString
            Else
                RLE_StatusLabel.Text = "Successfully set feature configuration for " + RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString + " with Value " + FeatureEnabledState.ToString
            End If

            'Set Cell Text
            RGV_MainGridView.CurrentRow.Cells.Item(2).Value = FeatureEnabledState.ToString

        Catch ex As Exception
            'Catch Any Exception that may occur
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
