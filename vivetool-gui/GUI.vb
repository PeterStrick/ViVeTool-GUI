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
Imports AutoUpdaterDotNET, Newtonsoft.Json.Linq, Telerik.WinControls.Data, Albacore.ViVe, System.Runtime.InteropServices, Telerik.WinControls.UI

''' <summary>
''' ViVeTool GUI
''' </summary>
Public Class GUI
    ''' <summary>
    ''' P/Invoke constants
    ''' </summary>
    Private Const WM_SYSCOMMAND As Integer = &H112
    Private Const MF_STRING As Integer = &H0
    Private Const MF_SEPARATOR As Integer = &H800
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
    Dim LineStage As String = String.Empty

    ''' <summary>
    ''' P/Invoke declaration. Used to Insert the About Menu Element, into the System Menu. Function get's the System Menu
    ''' </summary>
    ''' <param name="hWnd"></param>
    ''' <param name="bRevert"></param>
    ''' <returns></returns>
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetSystemMenu(hWnd As IntPtr, bRevert As Boolean) As IntPtr
    End Function

    ''' <summary>
    ''' P/Invoke declaration. Used to Insert the About Menu Element, into the System Menu. Function Appends to the System Menu
    ''' </summary>
    ''' <param name="hMenu"></param>
    ''' <param name="uFlags"></param>
    ''' <param name="uIDNewItem"></param>
    ''' <param name="lpNewItem"></param>
    ''' <returns></returns>
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function AppendMenu(hMenu As IntPtr, uFlags As Integer, uIDNewItem As Integer, lpNewItem As String) As Boolean
    End Function

    ''' <summary>
    ''' Load Event, Populates the Build Combo Box
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub GUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Check for Updates
        AutoUpdater.Start("https://raw.githubusercontent.com/PeterStrick/ViVeTool-GUI/master/UpdaterXML.xml")

        'Populate the Build Combo Box
        PopulateBuildComboBox()

        'Disable the close button in the search row
        RGV_MainGridView.MasterView.TableSearchRow.ShowCloseButton = False
    End Sub

    ''' <summary>
    ''' Populates the Build Combo Box. Used at the Form_Load Event
    ''' </summary>
    Private Sub PopulateBuildComboBox()
        Dim RepoURL As String = "https://api.github.com/repos/riverar/mach2/git/trees/master"
        Dim FeaturesFolderURL As String = String.Empty

        'Gets the URL of the features Folder that is used in section 2
#Region "1. Get the URL of the features folder"
        'Required Headers for the GitHub API
        Dim WebClientRepo As New WebClient With {
            .Encoding = System.Text.Encoding.UTF8
        }
        WebClientRepo.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8")
        WebClientRepo.Headers.Add(HttpRequestHeader.UserAgent, "PeterStrick/vivetool-gui")

        'Get the "tree" array from the API JSON Result
        Try
            Dim ContentsJSONRepo As String = WebClientRepo.DownloadString(RepoURL)
            Dim JSONObjectRepo As JObject = JObject.Parse(ContentsJSONRepo)
            Dim JSONArrayRepo As JArray = CType(JSONObjectRepo.SelectToken("tree"), JArray)

            'Look in the JSON Array for the element: "path" = "features"
            For Each element In JSONArrayRepo
                If element("path").ToString = "features" Then
                    FeaturesFolderURL = element("url").ToString
                End If
            Next
        Catch ex As WebException
            Dim CopyExAndClose As New RadTaskDialogButton With {
                .Text = "Copy Exception and Close"
            }
            AddHandler CopyExAndClose.Click, New EventHandler(Sub() My.Computer.Clipboard.SetText(DirectCast(ex.Response, HttpWebResponse).StatusDescription))

            Dim RTD As New RadTaskDialogPage With {
                    .Caption = " A Network Exception occurred",
                    .Heading = "A Network Exception occurred. Your IP may have been temporarily rate limited by the GitHub API for an hour.",
                    .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                }
            RTD.Expander.Text = DirectCast(ex.Response, HttpWebResponse).StatusDescription
            RTD.Expander.ExpandedButtonText = "Collapse Exception"
            RTD.Expander.CollapsedButtonText = "Show Exception"
            RTD.CommandAreaButtons.Add(CopyExAndClose)
            RadTaskDialog.ShowDialog(RTD)
        Catch ex As Exception
            Dim CopyExAndClose As New RadTaskDialogButton With {
                .Text = "Copy Exception and Close"
            }
            AddHandler CopyExAndClose.Click, New EventHandler(Sub() My.Computer.Clipboard.SetText(ex.ToString))

            Dim RTD As New RadTaskDialogPage With {
                    .Caption = " An Exception occurred",
                    .Heading = "An unknown Exception occurred.",
                    .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                }
            RTD.Expander.Text = ex.ToString
            RTD.Expander.ExpandedButtonText = "Collapse Exception"
            RTD.Expander.CollapsedButtonText = "Show Exception"
            RTD.CommandAreaButtons.Add(CopyExAndClose)
            RadTaskDialog.ShowDialog(RTD)
        End Try
#End Region

        'returns JSON File Contents of riverar/mach2/features
#Region "2. Get the features folder File Contents"
        'Required Headers for the GitHub API
        Dim WebClientFeatures As New WebClient With {
            .Encoding = System.Text.Encoding.UTF8
        }
        WebClientFeatures.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8")
        WebClientFeatures.Headers.Add(HttpRequestHeader.UserAgent, "PeterStrick/vivetool-gui")

        'Get the "tree" array from the API JSON Result
        Try
            '[DEV] Use Dev JSON to not get rate limited while Testing/Developing
            'Dim ContentsJSON As String = TempJSONUsedInDevelopment
            Dim ContentsJSONFeatures As String = WebClientFeatures.DownloadString(FeaturesFolderURL)
            Dim JSONObjectFeatures As JObject = JObject.Parse(ContentsJSONFeatures)
            Dim JSONArrayFeatures As JArray = CType(JSONObjectFeatures.SelectToken("tree"), JArray)

            'For each element in the Array, add a Combo Box Item with the name of the path element
            For Each element In JSONArrayFeatures
                Dim Name As String() = element("path").ToString.Split(CChar("."))
                If Name(1) = "txt" Then RDDL_Build.Items.Add(Name(0))
            Next
        Catch ex As WebException
            Dim CopyExAndClose As New RadTaskDialogButton With {
                .Text = "Copy Exception and Close"
            }
            AddHandler CopyExAndClose.Click, New EventHandler(Sub() My.Computer.Clipboard.SetText(DirectCast(ex.Response, HttpWebResponse).StatusDescription))

            Dim RTD As New RadTaskDialogPage With {
                    .Caption = " A Network Exception occurred",
                    .Heading = "A Network Exception occurred. Your IP may have been temporarily rate limited by the GitHub API for an hour.",
                    .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                }
            RTD.Expander.Text = DirectCast(ex.Response, HttpWebResponse).StatusDescription
            RTD.Expander.ExpandedButtonText = "Collapse Exception"
            RTD.Expander.CollapsedButtonText = "Show Exception"
            RTD.CommandAreaButtons.Add(CopyExAndClose)
            RadTaskDialog.ShowDialog(RTD)
        Catch ex As Exception
            Dim CopyExAndClose As New RadTaskDialogButton With {
                .Text = "Copy Exception and Close"
            }
            AddHandler CopyExAndClose.Click, New EventHandler(Sub() My.Computer.Clipboard.SetText(ex.ToString))

            Dim RTD As New RadTaskDialogPage With {
                    .Caption = " An Exception occurred",
                    .Heading = "An unknown Exception occurred.",
                    .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                }
            RTD.Expander.Text = ex.ToString
            RTD.Expander.ExpandedButtonText = "Collapse Exception"
            RTD.Expander.CollapsedButtonText = "Show Exception"
            RTD.CommandAreaButtons.Add(CopyExAndClose)
            RadTaskDialog.ShowDialog(RTD)
        End Try
#End Region
    End Sub

    ''' <summary>
    ''' Override of OnHandleCreated(e As EventArgs).
    ''' Appends the About Element into the System Menu
    ''' </summary>
    ''' <param name="e">Default EventArgs</param>
    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)

        ' Get a handle to a copy of this form's system (window) menu
        Dim hSysMenu As IntPtr = GetSystemMenu(Me.Handle, False)

        ' Add a separator
        AppendMenu(hSysMenu, MF_SEPARATOR, 0, String.Empty)

        ' Add the Manually set Feature ID menu item
        AppendMenu(hSysMenu, MF_STRING, 2, "Manually Set Feature ID")

        ' Add a separator
        AppendMenu(hSysMenu, MF_SEPARATOR, 0, String.Empty)

        ' Add the About menu item
        AppendMenu(hSysMenu, MF_STRING, 1, "&About…")
    End Sub

    ''' <summary>
    ''' Overrides WndProc(ByRef m As Message).
    ''' Checks if the message ID and performs an action depending on the ID. Example: ID 1 Shows the About Dialog.
    ''' </summary>
    ''' <param name="m">Windows Forms Message to be sent.</param>
    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)

        ' Test if the About item was selected from the system menu
        If (m.Msg = WM_SYSCOMMAND) AndAlso (CInt(m.WParam) = 1) Then
            About.ShowDialog()
        ElseIf (m.Msg = WM_SYSCOMMAND) AndAlso (CInt(m.WParam) = 2) Then
            SetManual.ShowDialog()
        End If
    End Sub

    ''' <summary>
    ''' Disables the Combo Box and runs the Background Worker each time the Combo Box Selected Index changes.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub PopulateDataGridView(sender As Object, e As EventArgs) Handles RDDL_Build.SelectedIndexChanged
        'Close Combo Box. This needs to be done, because in some cases the Combo Box is half closed and half opened, allowing the user to change it, while the Background Worker is running, which will result in an exception.
        RDDL_Build.CloseDropDown()

        'Disable Combo Box
        RDDL_Build.Enabled = False

        'Run Background Worker
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
        If Not BGW_PopulateGridView.CancellationPending Then
            'Remove all Group Descriptors
            Invoke(Sub() RGV_MainGridView.GroupDescriptors.Clear())

            'Set Status Label
            Invoke(Sub() RLE_StatusLabel.Text = "Populating the Data Grid View... This can take a while.")

            'Clear Data Grid View
            Invoke(Sub() RGV_MainGridView.Rows.Clear())

            'Prepare Web Client and download Build TXT
            Dim WebClient As New WebClient With {
                .Encoding = System.Text.Encoding.UTF8
            }
            Dim path As String = IO.Path.GetTempPath & RDDL_Build.Text & ".txt"
            WebClient.DownloadFile("https://raw.githubusercontent.com/riverar/mach2/master/features/" & RDDL_Build.Text & ".txt", path)

            'For each line add a grid view entry
            For Each Line In IO.File.ReadAllLines(path)

                ' Check Line Stage, used for Grouping
                If CInt(RDDL_Build.Text) >= 17704 Then
                    If Line = "## Unknown:" Then
                        LineStage = "Modifiable"
                    ElseIf Line = "## Always Enabled:" Then
                        LineStage = "Always Enabled"
                    ElseIf Line = "## Enabled By Default:" Then
                        LineStage = "Enabled By Default"
                    ElseIf Line = "## Disabled By Default:" Then
                        LineStage = "Disabled by Default"
                    ElseIf Line = "## Always Disabled:" Then
                        LineStage = "Always Disabled"
                    End If
                Else
                    LineStage = "Select Build 17704 or higher to use Grouping"
                End If

                'Split the Line at the :
                Dim Str As String() = Line.Split(CChar(":"))

                'If the Line is not empty, continue
                If Line IsNot "" AndAlso Line.Contains("#") = False Then
                    'Remove any Spaces from the first Str Array (Feature Name) and second Str Array (Feature ID)
                    Str(0).Replace(" ", "")
                    Str(1).Replace(" ", "")

                    'Get the Feature Enabled State from the currently processing line.
                    'RtlFeatureManager.QueryFeatureConfiguration will return Enabled, Disabled or throw a NullReferenceException for Default
                    Try
                        Dim State As String = RtlFeatureManager.QueryFeatureConfiguration(CUInt(Str(1)), FeatureConfigurationSection.Runtime).EnabledState.ToString
                        Invoke(Sub() RGV_MainGridView.Rows.Add(Str(0), Str(1), State, LineStage))
                    Catch ex As NullReferenceException
                        Invoke(Sub() RGV_MainGridView.Rows.Add(Str(0), Str(1), "Default", LineStage))
                    End Try
                End If
            Next

            'Move to the first row, remove the selection and change the Status Label to Done.
            Invoke(Sub()
                       RGV_MainGridView.CurrentRow = RGV_MainGridView.Rows.Item(0)
                       RGV_MainGridView.CurrentRow = Nothing
                       RLE_StatusLabel.Text = "Done."
                   End Sub)

            'Delete Feature List from %TEMP%
            IO.File.Delete(path)

            ''Enable Grouping
            'Dim LineGroup As New GroupDescriptor()
            'LineGroup.GroupNames.Add("FeatureInfo", System.ComponentModel.ListSortDirection.Ascending)
            'Invoke(Sub() Me.RGV_MainGridView.GroupDescriptors.Add(LineGroup))
        Else
            Return
        End If
    End Sub

    ''' <summary>
    ''' Upon Background Worker Completion, stop the Background Worker and re-enable the Combo Box
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub BGW_PopulateGridView_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGW_PopulateGridView.RunWorkerCompleted
        'End BGW
        BGW_PopulateGridView.CancelAsync()

        'Enable the Build Combo Box
        RDDL_Build.Enabled = True
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
            RTB_ThemeToggle.Image = My.Resources.icons8_moon_and_stars_24
        Else
            RTB_ThemeToggle.Text = "Light Theme"
            Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "Fluent"
            RTB_ThemeToggle.Image = My.Resources.icons8_sun_24
        End If
    End Sub

    ''' <summary>
    ''' Enable Feature Button, enables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_ActivateF_Click(sender As Object, e As EventArgs) Handles RMI_ActivateF.Click
        'Stop Searching temporarily
        RGV_MainGridView.MasterView.TableSearchRow.SuspendSearch()

        'Set Selected Feature to Enabled
        SetConfig(FeatureEnabledState.Enabled)

        'Resume Searching
        RGV_MainGridView.MasterView.TableSearchRow.ResumeSearch()
    End Sub

    ''' <summary>
    ''' Disable Feature Button, disables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_DeactivateF_Click(sender As Object, e As EventArgs) Handles RMI_DeactivateF.Click
        'Stop Searching temporarily
        RGV_MainGridView.MasterView.TableSearchRow.SuspendSearch()

        'Set Selected Feature to Disabled
        SetConfig(FeatureEnabledState.Disabled)

        'Resume Searching
        RGV_MainGridView.MasterView.TableSearchRow.ResumeSearch()
    End Sub

    ''' <summary>
    ''' Revert Feature Button, reverts the currently selected Feature back to default values.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_RevertF_Click(sender As Object, e As EventArgs) Handles RMI_RevertF.Click
        'Stop Searching temporarily
        RGV_MainGridView.MasterView.TableSearchRow.SuspendSearch()

        'Set Selected Feature to Default Values
        SetConfig(FeatureEnabledState.Default)

        'Resume Searching
        RGV_MainGridView.MasterView.TableSearchRow.ResumeSearch()
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
            If Not RtlFeatureManager.SetBootFeatureConfigurations(_configs) OrElse RtlFeatureManager.SetLiveFeatureConfigurations(_configs, FeatureConfigurationSection.Runtime) >= 1 Then
                'Set Status Label
                RLE_StatusLabel.Text = "An error occurred while setting a feature configuration for " & RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString

                'Fancy Message Box
                Dim RTD As New RadTaskDialogPage With {
                    .Caption = " An Error occurred",
                    .Heading = "An Error occurred while trying to set Feature " & RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString & " to " & FeatureEnabledState.ToString,
                    .Icon = RadTaskDialogIcon.Error
                }

                'Add a Close Button instead of a OK Button
                RTD.CommandAreaButtons.Add(RadTaskDialogButton.Close)

                'Show the Message Box
                RadTaskDialog.ShowDialog(RTD)
            Else
                'Set Status Label
                RLE_StatusLabel.Text = "Successfully set feature configuration for" & RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString & " with Value " & FeatureEnabledState.ToString

                'Set Cell Text
                RGV_MainGridView.CurrentRow.Cells.Item(2).Value = FeatureEnabledState.ToString

                'Fancy Message Box
                Dim RTD As New RadTaskDialogPage With {
                    .Caption = " Success",
                    .Heading = "Successfully set Feature ID " & RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString & " to " & FeatureEnabledState.ToString,
                    .Icon = RadTaskDialogIcon.ShieldSuccessGreenBar
                }

                'Add a Close Button instead of a OK Button
                RTD.CommandAreaButtons.Add(RadTaskDialogButton.Close)

                'Show the Message Box
                RadTaskDialog.ShowDialog(RTD)
            End If
        Catch ex As Exception
            'Catch Any Exception that may occur

            'Create a Button that on Click, copies the Exception Text
            Dim CopyExAndClose As New RadTaskDialogButton With {
                .Text = "Copy Exception and Close"
            }
            AddHandler CopyExAndClose.Click, New EventHandler(Sub() My.Computer.Clipboard.SetText(ex.ToString))

            'Fancy Message Box
            Dim RTD As New RadTaskDialogPage With {
                    .Caption = " An Exception occurred",
                    .Heading = "An unknown Exception occurred.",
                    .Icon = RadTaskDialogIcon.ShieldErrorRedBar
                }

            'Add the Exception Text to the Expander
            RTD.Expander.Text = ex.ToString

            'Set the Text for the "Collapse Info" and "More Info" Buttons
            RTD.Expander.ExpandedButtonText = "Collapse Exception"
            RTD.Expander.CollapsedButtonText = "Show Exception"

            'Add the Button to the Message Box
            RTD.CommandAreaButtons.Add(CopyExAndClose)

            'Show the Message Box
            RadTaskDialog.ShowDialog(RTD)
        End Try
    End Sub

    ''' <summary>
    ''' Form Closing Event. Used to store the My.Settings.DarkMode parameter by determining the Toggle State of RTB_ThemeToggle
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub GUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If RTB_ThemeToggle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On Then
            My.Settings.DarkMode = True
            My.Settings.Save()
        Else
            My.Settings.DarkMode = False
            My.Settings.Save()
        End If
    End Sub

    ''' <summary>
    ''' Selection Changed Event. Used to enable the RDDB_PerformAction Button, upon selecting a row.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RGV_MainGridView_SelectionChanged(sender As Object, e As EventArgs) Handles RGV_MainGridView.SelectionChanged
        If RGV_MainGridView.CurrentRow Is Nothing Then
            RDDB_PerformAction.Enabled = False
        Else
            RDDB_PerformAction.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Click Event. Used to show the About Box
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_About_Click(sender As Object, e As EventArgs) Handles RB_About.Click
        About.ShowDialog()
    End Sub

    ''' <summary>
    ''' Show the Manually Set Feature ID UI when F12 is pressed
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Key EventArgs</param>
    Private Sub GUI_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 Then
            SetManual.ShowDialog()
        End If
    End Sub

    ''' <summary>
    ''' Shows the UI to manually set a Feature ID
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RB_ManuallySetFeature_Click(sender As Object, e As EventArgs) Handles RB_ManuallySetFeature.Click
        SetManual.ShowDialog()
    End Sub
End Class