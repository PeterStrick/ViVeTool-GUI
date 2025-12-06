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
Option Strict On
Imports AutoUpdaterDotNET, Newtonsoft.Json.Linq, Albacore.ViVe, System.Runtime.InteropServices

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

    ''' <summary>
    ''' GitHub repository owner for update checks and User-Agent identification
    ''' </summary>
    Private Const GITHUB_REPO_OWNER As String = "mta1124-1629472"
    ReadOnly TempJSONUsedInDevelopment As String = "{
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
    ''' Gets the User-Agent string for HTTP requests
    ''' </summary>
    Private ReadOnly Property UserAgent As String
        Get
            Return String.Format("{0}/{1} (+https://github.com/{2}/ViVeTool-GUI)", My.Application.Info.ProductName, My.Application.Info.Version.ToString, GITHUB_REPO_OWNER)
        End Get
    End Property

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
        'Listen to Application Crashes and show CrashReporter.Net if one occurs.
        AddHandler Application.ThreadException, AddressOf CrashReporter.ApplicationThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CrashReporter.CurrentDomainOnUnhandledException

        'Make a Background Thread that handles Background Tasks
        Dim BackgroundThread As New Threading.Thread(AddressOf BackgroundTasks) With {
            .IsBackground = True
        }
        BackgroundThread.SetApartmentState(Threading.ApartmentState.STA)
        BackgroundThread.Start()

        'Setup search functionality
        AddHandler TB_Search.TextChanged, AddressOf TB_Search_TextChanged

        'Setup dropdown button to show context menu
        AddHandler RDDB_PerformAction.Click, AddressOf RDDB_PerformAction_Click
    End Sub

    ''' <summary>
    ''' Background Tasks to be executed in a Thread
    ''' </summary>
    Private Sub BackgroundTasks()
        'Check for Updates
        AutoUpdater.Start("https://raw.githubusercontent.com/PeterStrick/ViVeTool-GUI/master/UpdaterXML.xml")

        'Populate the Build Combo Box, but first check if the PC is connected to the Internet, otherwise the GUI will crash without giving any helpful Information on WHY
        PopulateBuildComboBox_Check()
    End Sub

    ''' <summary>
    ''' Check for Internet Connectivity before trying to populate the Build Combo Box
    ''' </summary>
    Private Sub PopulateBuildComboBox_Check()
        'Add manual option
        Invoke(Sub() RDDL_Build.Items.Add("Load manually..."))

        If CheckForInternetConnection() Then
            'Populate the Build Combo Box
            PopulateBuildComboBox()

            'Set Ready Label
            Invoke(Sub() RLE_StatusLabel.Text = "Ready. Select a build from the Combo Box to get started, or alternatively press F12 to manually change a Feature.")
        Else
            Invoke(Sub()
                       'First, disable the Combo Box
                       RDDL_Build.Enabled = True
                       RDDL_Build.Text = "Network Error"

                       'Second, change the Status Label
                       RLE_StatusLabel.Text = "Network Functions disabled. Press F12 to manually change a Feature."

                       'Third, Show an error message
                       DialogHelper.ShowWarningDialog(" A Network Exception occurred",
                           "A Network Exception occurred",
                           "ViVeTool-GUI is unable to populate the Build Combo Box, if the Device isn't connected to the Internet, or if the GitHub API is unreachable." & vbNewLine & vbNewLine & "You are still able to manually change a Feature ID by pressing F12, and able to load a local Feature List.")
                   End Sub)
        End If
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
        WebClientRepo.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)

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

        Catch webex As WebException
            DialogHelper.ShowNetworkExceptionDialog(webex)
        Catch ex As Exception
            DialogHelper.ShowUnknownExceptionDialog(ex)
        End Try
#End Region
#Region "2. Get the features folder File Contents"
        'returns JSON File Contents of riverar/mach2/features

        'Required Headers for the GitHub API
        Dim WebClientFeatures As New WebClient With {
            .Encoding = System.Text.Encoding.UTF8
        }
        WebClientFeatures.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8")
        WebClientFeatures.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)

        'Get the "tree" array from the API JSON Result
        Try
            '[DEV] Use Dev JSON to not get rate limited while Testing/Developing
            'Dim ContentsJSON As String = TempJSONUsedInDevelopment
            Dim ContentsJSONFeatures As String = WebClientFeatures.DownloadString(FeaturesFolderURL)
            Dim JSONObjectFeatures As JObject = JObject.Parse(ContentsJSONFeatures)
            Dim JSONArrayFeatures As JArray = CType(JSONObjectFeatures.SelectToken("tree"), JArray)

            Dim tempList As New List(Of String)

            For Each element In JSONArrayFeatures
                Select Case element("path").ToString.Split(CChar(".")).Length
                    Case 0 ' No File name or Extension. Not used in the Mach2 repo and impossible
                        ' Do nothing
                    Case 1 ' Filename; Not used in the Mach2 repo
                        ' Do nothing
                    Case 2 ' Filename.Extension; Ex: 22449.txt
                        If element("path").ToString.Split(CChar("."))(1) = "txt" Then
                            tempList.Add(element("path").ToString.Split(CChar("."))(0))
                        End If
                    Case 3 ' File.File.Extension; Ex: 22000.1.txt or 22449_22454_diff.patch
                        If element("path").ToString.Split(CChar("."))(2) = "txt" Then
                            tempList.Add(element("path").ToString.Split(CChar("."))(0) & "." & element("path").ToString.Split(CChar("."))(1))
                        End If
                    Case 4 ' File.File.File.Extension; Ex: 18980.1_18985.1_diff.patch. Usually used for Diffs in the Mach2 Repo
                        ' Do Nothing
                End Select
            Next

            Invoke(Sub()
                       'Add the Items of tempList to the Combo Box
                       For Each item In tempList
                           RDDL_Build.Items.Add(item)
                       Next

                       'Deselect any Item - with SelectedIndex = -1, the ComboBox shows empty
                       RDDL_Build.SelectedIndex = -1

                       'Add the Handler
                       AddHandler RDDL_Build.SelectedIndexChanged, AddressOf PopulateDataGridView
                   End Sub)
            'Enable the Combo Box
            Invoke(Sub() RDDL_Build.Enabled = True)

            'Auto-load the newest Build if it is Enabled in the Settings
            If My.Settings.AutoLoad Then
                Invoke(Sub()
                           Dim count = RDDL_Build.Items.Count
                           If count > 1 Then
                               ' Assuming index 0 = "Load manually...", select first build
                               RDDL_Build.SelectedIndex = 1
                           ElseIf count = 1 Then
                               ' No builds available; skip auto-load
                               RDDL_Build.SelectedIndex = -1
                               RLE_StatusLabel.Text = "No builds available. Load manually or try again."
                           End If
                       End Sub)
            End If
        Catch webex As WebException
            DialogHelper.ShowNetworkExceptionDialog(webex)
        Catch ex As Exception
            DialogHelper.ShowUnknownExceptionDialog(ex)
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
        AppendMenu(hSysMenu, MF_STRING, 1, "&About�")
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
            AboutAndSettings.ShowDialog()
        ElseIf (m.Msg = WM_SYSCOMMAND) AndAlso (CInt(m.WParam) = 2) Then
            SetManual.ShowDialog()
        End If
    End Sub

    ''' <summary>
    ''' Disables the Combo Box and runs the Background Worker each time the Combo Box Selected Index changes.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub PopulateDataGridView(sender As Object, e As EventArgs) 'Handles RDDL_Build.SelectedIndexChanged
        'Disable selection temporarily
        RGV_MainGridView.ClearSelection()

        'Disable Combo Box
        RDDL_Build.Enabled = False

        'Clear the search box
        TB_Search.Text = String.Empty

        'Get the selected text
        Dim selectedText As String = If(RDDL_Build.SelectedItem IsNot Nothing, RDDL_Build.SelectedItem.ToString(), String.Empty)

        'If "Load manually..." is selected, then load from a TXT File, else load normally
        If selectedText = "Load manually..." Then
            Dim TXTThread As New Threading.Thread(AddressOf LoadFromManualTXT) With {
                .IsBackground = True
            }
            TXTThread.SetApartmentState(Threading.ApartmentState.STA)
            TXTThread.Start()
        ElseIf String.IsNullOrEmpty(selectedText) Then
            'Do Nothing
        Else
            'Run Background Worker
            BGW_PopulateGridView.RunWorkerAsync()
        End If
    End Sub

    ''' <summary>
    ''' Same code as BGW_PopulateGridView.RunWorkerAsync(), just that it get's the Feature List locally instead of from GitHub
    ''' </summary>
    Private Sub LoadFromManualTXT()
        'Make a new OpenFileDialog
        Dim OFD As New OpenFileDialog With {
                .InitialDirectory = "C:\",
                .Title = "Path to a Feature List",
                .Filter = "Feature List|*.txt"
            }

        If OFD.ShowDialog() = DialogResult.OK AndAlso IO.File.Exists(OFD.FileName) Then
            'Set Status Label
            Invoke(Sub() RLE_StatusLabel.Text = "Populating the Data Grid View... This can take a while.")
            'Clear Data Grid View
            Invoke(Sub() RGV_MainGridView.Rows.Clear())

            'For each line add a grid view entry
            Try
                'Collect all row data first to avoid repeated UI thread invocations
                Dim rowData As New List(Of Object())

                For Each rawLine In IO.File.ReadAllLines(OFD.FileName)
                    Dim Line = rawLine.Trim()
                    If Line = "## Unknown:" Then
                        LineStage = "Modifiable"
                        Continue For
                    ElseIf Line = "## Always Enabled:" Then
                        LineStage = "Always Enabled"
                        Continue For
                    ElseIf Line = "## Enabled By Default:" Then
                        LineStage = "Enabled By Default"
                        Continue For
                    ElseIf Line = "## Disabled By Default:" Then
                        LineStage = "Disabled by Default"
                        Continue For
                    ElseIf Line = "## Always Disabled:" Then
                        LineStage = "Always Disabled"
                        Continue For
                    End If

                    If String.IsNullOrWhiteSpace(Line) OrElse Line.StartsWith("#") OrElse Not Line.Contains(":") Then
                        Continue For
                    End If

                    Dim parts As String() = Line.Split(New Char() {":"c}, 2)
                    If parts.Length < 2 Then Continue For

                    Dim featureName As String = parts(0).Replace(" ", "").Trim()
                    Dim featureIdStr As String = parts(1).Replace(" ", "").Trim()

                    Dim featureIdValue As UInteger
                    If Not UInteger.TryParse(featureIdStr, featureIdValue) Then
                        ' invalid ID: treat as default / skip
                        rowData.Add(New Object() {featureName, featureIdStr, "Default", LineStage})
                        Continue For
                    End If

                    Dim State As String = "Default"
                    Try
                        Dim cfg = RtlFeatureManager.QueryFeatureConfiguration(featureIdValue, FeatureConfigurationSection.Runtime)
                        If cfg IsNot Nothing Then
                            ' EnabledState is a value type; directly convert to string
                            State = cfg.EnabledState.ToString()
                        Else
                            State = "Default"
                        End If
                    Catch
                        ' Any unexpected exception here: treat as Default for display
                        State = "Default"
                    End Try

                    rowData.Add(New Object() {featureName, featureIdStr, State, LineStage})
                Next

                'Add all rows to the grid in a single batch on the UI thread
                Invoke(Sub()
                           For Each row As Object() In rowData
                               RGV_MainGridView.Rows.Add(row)
                           Next
                       End Sub)

                'Move to the first row, remove the selection and change the Status Label to Done.
                Invoke(Sub()
                           If RGV_MainGridView.Rows.Count > 0 Then
                               RGV_MainGridView.ClearSelection()
                           End If
                           RLE_StatusLabel.Text = "Done."
                       End Sub)
            Catch ex As Exception
                Invoke(Sub()
                           'Catch Any Exception that may occur
                           DialogHelper.ShowUnknownExceptionDialog(ex)

                           'Clear the selection
                           RDDL_Build.SelectedIndex = -1
                           RDDL_Build.Enabled = True
                       End Sub)
            End Try
        Else
            'Clear the selection
            Invoke(Sub()
                       RDDL_Build.SelectedIndex = -1
                       RDDL_Build.Enabled = True
                   End Sub)
        End If
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
            'Get the selected build text
            Dim selectedBuild As String = String.Empty
            Invoke(Sub() selectedBuild = If(RDDL_Build.SelectedItem IsNot Nothing, RDDL_Build.SelectedItem.ToString(), String.Empty))

            'Debug
            Diagnostics.Debug.WriteLine("Loading Build " & selectedBuild)

            'Set Status Label
            Invoke(Sub() RLE_StatusLabel.Text = "Populating the Data Grid View... This can take a while.")

            'Clear Data Grid View
            'Fix for a weird Bug that happens randomly while clearing the rows if the search row has text in it
            Try
                Invoke(Sub() RGV_MainGridView.Rows.Clear())
            Catch ex As Exception
                Diagnostics.Debug.WriteLine("Exception while clearing row. Build: " & selectedBuild & ". " & ex.Message)
            End Try

            'Prepare Web Client and download Build TXT
            Dim WebClient As New WebClient With {
                    .Encoding = System.Text.Encoding.UTF8
                }
            WebClient.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
            Dim path As String = IO.Path.GetTempPath & selectedBuild & ".txt"
            WebClient.DownloadFile("https://raw.githubusercontent.com/riverar/mach2/master/features/" & selectedBuild & ".txt", path)

            'For each line add a grid view entry
            'Collect all row data first to avoid repeated UI thread invocations
            Dim rowData As New List(Of Object())

            For Each Line In IO.File.ReadAllLines(path)

                'Check Line Stage, used for Grouping
                Try
                    If CInt(selectedBuild) >= 17704 Then
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
                Catch ex As Exception
                    LineStage = "Error"
                End Try

                'Split the Line at the :
                Dim Str As String() = Line.Split(CChar(":"))

                'If the Line is not empty, continue
                If Line IsNot "" AndAlso Line.Contains("#") = False Then
                    'Remove any Spaces from the first Str Array (Feature Name) and second Str Array (Feature ID)
                    Dim featureName As String = Str(0).Replace(" ", "")
                    Dim featureId As String = Str(1).Replace(" ", "")

                    'Get the Feature Enabled State from the currently processing line.
                    'RtlFeatureManager.QueryFeatureConfiguration will return Enabled, Disabled or throw a NullReferenceException for Default
                    Try
                        Dim State As String = RtlFeatureManager.QueryFeatureConfiguration(CUInt(featureId), FeatureConfigurationSection.Runtime).EnabledState.ToString
                        rowData.Add(New Object() {featureName, featureId, State, LineStage})
                    Catch ex As Exception
                        rowData.Add(New Object() {featureName, featureId, "Default", LineStage})
                    End Try
                End If
            Next

            'Add all rows to the grid in a single batch on the UI thread
            Invoke(Sub()
                       For Each row As Object() In rowData
                           RGV_MainGridView.Rows.Add(row)
                       Next
                   End Sub)

            'Move to the first row, remove the selection and change the Status Label to Done.
            Invoke(Sub()
                       If RGV_MainGridView.Rows.Count > 0 Then
                           RGV_MainGridView.ClearSelection()
                       End If
                       RLE_StatusLabel.Text = "Done."
                   End Sub)

            'Delete Feature List from %TEMP%
            IO.File.Delete(path)
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
    ''' Enable Feature Button, enables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_ActivateF_Click(sender As Object, e As EventArgs) Handles RMI_ActivateF.Click
        'Set Selected Feature to Enabled
        SetConfig(FeatureEnabledState.Enabled)
    End Sub

    ''' <summary>
    ''' Disable Feature Button, disables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_DeactivateF_Click(sender As Object, e As EventArgs) Handles RMI_DeactivateF.Click
        'Set Selected Feature to Disabled
        SetConfig(FeatureEnabledState.Disabled)
    End Sub

    ''' <summary>
    ''' Revert Feature Button, reverts the currently selected Feature back to default values.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_RevertF_Click(sender As Object, e As EventArgs) Handles RMI_RevertF.Click
        'Set Selected Feature to Default Values
        SetConfig(FeatureEnabledState.Default)
    End Sub

    ''' <summary>
    ''' Set's the Feature Configuration. Uses the FeatureEnabledState parameter to set the EnabledState of the Feature
    ''' </summary>
    ''' <param name="FeatureEnabledState">Specifies what Enabled State the Feature should be in. Can be either Enabled, Disabled or Default</param>
    Private Sub SetConfig(FeatureEnabledState As FeatureEnabledState)
        Try
            If RGV_MainGridView.SelectedRows.Count = 0 Then
                Return
            End If

            'Initialize Variables
            Dim _enabledStateOptions, _variant, _variantPayloadKind, _variantPayload, _group As Integer
            _enabledStateOptions = 1
            _group = 4

            Dim selectedRow = RGV_MainGridView.SelectedRows(0)

            'FeatureConfiguration Variable
            Dim _configs As New List(Of FeatureConfiguration) From {
                New FeatureConfiguration() With {
                    .FeatureId = CUInt(selectedRow.Cells(1).Value),
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
                RLE_StatusLabel.Text = "An error occurred while setting a feature configuration for " & selectedRow.Cells(0).Value.ToString

                'Show error dialog
                DialogHelper.ShowErrorDialog(" An Error occurred", "An Error occurred while trying to set Feature " & selectedRow.Cells(0).Value.ToString & " to " & FeatureEnabledState.ToString)
            Else
                'Set Status Label
                RLE_StatusLabel.Text = "Successfully set feature configuration for" & selectedRow.Cells(0).Value.ToString & " with Value " & FeatureEnabledState.ToString

                'Set Cell Text
                selectedRow.Cells(2).Value = FeatureEnabledState.ToString

                'Show success dialog
                DialogHelper.ShowSuccessDialog(" Success", "Successfully set Feature " & selectedRow.Cells(0).Value.ToString & " to " & FeatureEnabledState.ToString)
            End If
        Catch ex As Exception
            'Catch Any Exception that may occur
            DialogHelper.ShowUnknownExceptionDialog(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Selection Changed Event. Used to enable the RDDB_PerformAction Button, upon selecting a row.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RGV_MainGridView_SelectionChanged(sender As Object, e As EventArgs) Handles RGV_MainGridView.SelectionChanged
        If RGV_MainGridView.SelectedRows.Count = 0 Then
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
        AboutAndSettings.ShowDialog()
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
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RB_ManuallySetFeature_Click(sender As Object, e As EventArgs) Handles RB_ManuallySetFeature.Click
        SetManual.ShowDialog()
    End Sub

    ''' <summary>
    ''' Basic Internet Connectivity Check by trying to check if github.com is accessible
    ''' </summary>
    ''' <returns>True if https://www.github.com responds. False if not</returns>
    Public Shared Function CheckForInternetConnection() As Boolean
        Try
            'Use a HEAD request instead of downloading content - more efficient
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create("https://www.github.com"), HttpWebRequest)
            request.Method = "HEAD"
            request.Timeout = 5000 'Set a reasonable timeout of 5 seconds
            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Return response.StatusCode = HttpStatusCode.OK OrElse
                       response.StatusCode = HttpStatusCode.MovedPermanently
            End Using
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Form Closing Event. Used to forcefully close ViVeTool GUI and it's Threads
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">FormClosing EventArgs</param>
    Private Sub GUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Exit all running Threads forcefully, should fix ObjectDisposed Exceptions
        Diagnostics.Process.GetCurrentProcess().Kill()
    End Sub

    ''' <summary>
    ''' Handle the Perform Action button click to show the context menu
    ''' </summary>
    Private Sub RDDB_PerformAction_Click(sender As Object, e As EventArgs)
        ActionContextMenu.Show(RDDB_PerformAction, New System.Drawing.Point(0, RDDB_PerformAction.Height))
    End Sub

    ''' <summary>
    ''' Handle search text changed to filter the grid
    ''' </summary>
    Private Sub TB_Search_TextChanged(sender As Object, e As EventArgs)
        Dim searchText As String = TB_Search.Text.ToLower()
        For Each row As DataGridViewRow In RGV_MainGridView.Rows
            If row.IsNewRow Then Continue For
            Dim visible As Boolean = False
            For Each cell As DataGridViewCell In row.Cells
                If cell.Value IsNot Nothing AndAlso cell.Value.ToString().ToLower().Contains(searchText) Then
                    visible = True
                    Exit For
                End If
            Next
            row.Visible = visible OrElse String.IsNullOrEmpty(searchText)
        Next
    End Sub
End Class