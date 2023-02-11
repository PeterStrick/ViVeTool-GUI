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
Imports System.Globalization, System.Runtime.InteropServices
Imports AutoUpdaterDotNET, Newtonsoft.Json.Linq, Albacore.ViVe, MySqlConnector
Imports Telerik.WinControls.UI, Telerik.WinControls

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
    Private ReadOnly RMI_AddComment As New RadMenuItem("Add a Comment for this Feature")

    'Variables for the Comments/MySQL System
    Private Build_DT As New DataTable
    Private ReadOnly CommentsImg As Image = My.Resources.icons8_comments_24px
    Private Shared HasInternetConnection As Boolean = False
    Private Shared HasDBAvailable As Boolean = False
    Private Shared TableDoesNotExist As Boolean = True

#If DEBUG Then
    Private Shared EnableDBLoadingForManualTXTLoad As Boolean = False
#End If

#Region "DEBUG Subs and Functions"
    Private Sub __DBG_SetRDDL_Build_Text_Click(sender As Object, e As EventArgs) Handles __DBG_SetRDDL_Build_Text.Click
        RDDL_Build.Text = "25158"
    End Sub

    Private Sub __DBG_SeeCommentsData_Click(sender As Object, e As EventArgs) Handles __DBG_SeeCommentsData.Click
        Diagnostics.Debug.WriteLine($"HasDBAvailable: {HasDBAvailable}")
        Diagnostics.Debug.WriteLine($"HasInternetConnection: {HasInternetConnection}")

        Dim frm1 As New Form With {
            .Size = New Size(640, 480),
            .Text = "DEBUG - Comments Data - Read Only",
            .MaximizeBox = False,
            .FormBorderStyle = FormBorderStyle.FixedSingle,
            .Icon = My.Resources.icons8_comments_24px.ToIcon(True, Color.Transparent)
        }

        Dim DGV As New DataGridView With {
            .Dock = DockStyle.Fill,
            .DataSource = Build_DT,
            .[ReadOnly] = True
        }

        frm1.Controls.Add(DGV)
        frm1.Show()
    End Sub

    Private Sub __DBG_SetRDDL_Build_Text_ToNothing_Click(sender As Object, e As EventArgs) Handles __DBG_SetRDDL_Build_Text_ToNothing.Click
        RDDL_Build.Text = ""
    End Sub

    Private Sub __DBG_GetComments_Click(sender As Object, e As EventArgs) Handles __DBG_GetComments.Click
        LoadCommentsFromDB(RDDL_Build.Text)
    End Sub

    Private Sub __DBG_EnableCommentLoadingFromManualFL_Click(sender As Object, e As EventArgs) Handles __DBG_EnableCommentLoadingFromManualFL.Click
        EnableDBLoadingForManualTXTLoad = True
        MsgBox("EnableDBLoadingForManualTXTLoad enabled")
        MsgBox("Make sure that you have loaded in Comments in the Debug Menu before trying to load a Feature List. Otherwise Exceptions will occur.")
    End Sub

    Private Sub __DBG_DisableCommentLoadingFromManualFL_Click(sender As Object, e As EventArgs) Handles __DBG_DisableCommentLoadingFromManualFL.Click
        EnableDBLoadingForManualTXTLoad = False
        MsgBox("EnableDBLoadingForManualTXTLoad disabled")
    End Sub

    Private Sub __DBG_ChnageLanguage_Click(sender As Object, e As EventArgs) Handles __DBG_ChnageLanguage.Click
        CultureInfo.DefaultThreadCurrentCulture = New CultureInfo("zh")
        CultureInfo.DefaultThreadCurrentUICulture = New CultureInfo("zh")
        MsgBox(CultureInfo.DefaultThreadCurrentCulture.ToString)
        MsgBox(CultureInfo.DefaultThreadCurrentUICulture.ToString)
    End Sub
#End Region

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
#If DEBUG Then
        If Diagnostics.Debugger.IsAttached Then
            __DBG_MainBtn.Enabled = True
            __DBG_MainBtn.Visible = True
            MsgBox("Debugger detected. The Debug Menu is enabled and visible.")
        End If
#End If

        ' Listen to Application Crashes and show CrashReporter.Net if one occurs.
        AddHandler Application.ThreadException, AddressOf CrashReporter.ApplicationThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CrashReporter.CurrentDomainOnUnhandledException

        ' Comment Code
        AddHandler RMI_AddComment.Click, AddressOf ShowCommentForm

        ' Make a Background Thread that handles Background Tasks
        Dim BackgroundThread As New Threading.Thread(AddressOf BackgroundTasks) With {.IsBackground = True}
        BackgroundThread.SetApartmentState(Threading.ApartmentState.STA)
        BackgroundThread.Start()

        ' Disable the close button in the search row
        RGV_MainGridView.MasterView.TableSearchRow.ShowCloseButton = False

        ' Fix Header Text
        ' Feature Name
        RGV_MainGridView.MasterTemplate.Columns(0).HeaderText = My.Resources.Generic_FeatureName

        ' Feature ID
        RGV_MainGridView.MasterTemplate.Columns(1).HeaderText = My.Resources.Generic_FeatureID

        ' Feature State
        RGV_MainGridView.MasterTemplate.Columns(2).HeaderText = My.Resources.Generic_FeatureState
    End Sub

    ''' <summary>
    ''' Background Tasks to be executed in a Thread
    ''' </summary>
    Private Sub BackgroundTasks()
        ' Check for Updates
        AutoUpdater.Start("https://raw.githubusercontent.com/PeterStrick/ViVeTool-GUI/master/UpdaterXML.xml")

        ' Check if the Comments DB Server is online
        Invoke(Sub() RLE_StatusLabel.Text = "Checking if the Comments Database Server is available...")
        If DatabaseFunctions.ConnectionTest() = True Then HasDBAvailable = True

        ' Populate the Build Combo Box, but first check if the PC is connected to the Internet, otherwise the GUI will crash without giving any helpful Information on WHY
        Invoke(Sub() RLE_StatusLabel.Text = "Populating the Build Combo Box...")
        PopulateBuildComboBox_Check()
    End Sub

    ''' <summary>
    ''' Check for Internet Connectivity before trying to populate the Build Combo Box
    ''' </summary>
    Private Sub PopulateBuildComboBox_Check()
        ' Add manual option
        Invoke(Sub() RDDL_Build.Items.Add(My.Resources.Generic_LoadManually))

        If CheckForInternetConnection() = True Then
            HasInternetConnection = True

            Diagnostics.Debug.WriteLine($"HasInternetConnection is: {HasInternetConnection}")

            ' Populate the Build Combo Box
            PopulateBuildComboBox()

            Diagnostics.Debug.WriteLine($"HasInternetConnection is: {HasInternetConnection}")
            ' Set Ready Label
            Invoke(Sub() RLE_StatusLabel.Text = My.Resources.PopulateBuildComboBox_Check_Ready)
        Else
            Invoke(Sub()
                       ' First, disable the Combo Box
                       RDDL_Build.Enabled = True
                       RDDL_Build.Text = My.Resources.Error_NetworkError

                       ' Second, change the Status Label
                       RLE_StatusLabel.Text = My.Resources.Error_NetworkFunctionsDisabledF12

                       ' Third, Show an error message
                       RadTD.Generate($" {My.Resources.Error_ANetworkErrorOccurred}", My.Resources.Error_ANetworkErrorOccurred,
                       My.Resources.Error_NetworkExceptionDetail_N, RadTaskDialogIcon.ShieldWarningYellowBar)
                   End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Populates the Build Combo Box. Used at the Form_Load Event
    ''' </summary>
    Private Sub PopulateBuildComboBox()
        Dim RepoURL As String = "https://api.github.com/repos/riverar/mach2/git/trees/master"
        Dim FeaturesFolderURL As String = String.Empty

        ' Gets the URL of the features Folder that is used in section 2
#Region "1. Get the URL of the features folder"
        ' Required Headers for the GitHub API
        Dim WebClientRepo As New WebClient With {.Encoding = System.Text.Encoding.UTF8}
        WebClientRepo.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8")
        WebClientRepo.Headers.Add(HttpRequestHeader.UserAgent, "PeterStrick/vivetool-gui")

        ' Get the "tree" array from the API JSON Result
        Try
            Dim ContentsJSONRepo As String = WebClientRepo.DownloadString(RepoURL)
            Dim JSONObjectRepo As JObject = JObject.Parse(ContentsJSONRepo)
            Dim JSONArrayRepo As JArray = CType(JSONObjectRepo.SelectToken("tree"), JArray)

            ' Look in the JSON Array for the element: "path" = "features"
            For Each element In JSONArrayRepo
                If element("path").ToString = "features" Then FeaturesFolderURL = element("url").ToString
            Next

        Catch webex As WebException
            Dim webex_Response As String
            Try
                webex_Response = My.Resources.Error_NetworkException_GithubAPI_Response & DirectCast(webex.Response, HttpWebResponse).StatusDescription
            Catch ex As Exception
                webex_Response = webex.ToString
            End Try

            RadTD.ShowDialog($" {My.Resources.Error_ANetworkErrorOccurred}", My.Resources.Error_ANetworkErrorOccurred,
            My.Resources.Error_NetworkException_GithubAPI, RadTaskDialogIcon.ShieldErrorRedBar, webex, webex_Response, webex_Response)
        Catch ex As Exception
            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
            Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
        End Try
#End Region
#Region "2. Get the features folder File Contents"
        ' returns JSON File Contents of riverar/mach2/features

        ' Required Headers for the GitHub API
        Dim WebClientFeatures As New WebClient With {.Encoding = System.Text.Encoding.UTF8}
        WebClientFeatures.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8")
        WebClientFeatures.Headers.Add(HttpRequestHeader.UserAgent, "PeterStrick/vivetool-gui")

        ' Get the "tree" array from the API JSON Result
        Try
            ' [DEV] Use Dev JSON to not get rate limited while Testing/Developing
            ' Dim ContentsJSON As String = TempJSONUsedInDevelopment
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
                       ' Add the Items of tempList to the Combo Box
                       RDDL_Build.Items.AddRange(tempList)

                       ' De-select any Item
                       RDDL_Build.SelectedIndex = -1

                       ' Set default Text
                       RDDL_Build.Text = My.Resources.Generic_SelectBuild

                       ' Add the Handler
                       AddHandler RDDL_Build.SelectedIndexChanged, AddressOf PopulateDataGridView

                       ' Enable the Combo Box
                       RDDL_Build.Enabled = True
                   End Sub)

            ' Auto-load the newest Build if it is Enabled in the Settings
            If My.Settings.AutoLoad Then Invoke(Sub() RDDL_Build.SelectedItem = RDDL_Build.Items.Item(1))
        Catch webex As WebException
            Dim webex_Response As String
            Try
                webex_Response = My.Resources.Error_NetworkException_GithubAPI_Response & DirectCast(webex.Response, HttpWebResponse).StatusDescription
            Catch ex As Exception
                webex_Response = webex.ToString
            End Try

            RadTD.ShowDialog($" {My.Resources.Error_ANetworkErrorOccurred}", My.Resources.Error_ANetworkErrorOccurred,
            My.Resources.Error_NetworkException_GithubAPI, RadTaskDialogIcon.ShieldErrorRedBar, webex, webex_Response, webex_Response)
        Catch ex As Exception
            RadTD.Generate($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
            Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
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
        AppendMenu(hSysMenu, MF_STRING, 2, My.Resources.SystemMenu_ManuallySetFeatureID)

        ' Add a separator
        AppendMenu(hSysMenu, MF_SEPARATOR, 0, String.Empty)

        ' Add the About menu item
        AppendMenu(hSysMenu, MF_STRING, 1, My.Resources.SystemMenu_About)
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
        ' Disable Animations and selection. Helps with flickering
        AnimatedPropertySetting.AnimationsEnabled = False
        RGV_MainGridView.SelectionMode = GridViewSelectionMode.None

        ' Close Combo Box. This needs to be done, because in some cases the Combo Box is half closed and half opened, allowing the user to change it, while the Background Worker is running, which will result in an exception.
        RDDL_Build.CloseDropDown()

        ' Disable Combo Box
        RDDL_Build.Enabled = False

        ' Remove the Search Row temporarily
        RGV_MainGridView.MasterView.TableSearchRow.Search("")
        RGV_MainGridView.MasterView.TableSearchRow.IsVisible = False

        ' If "Load manually..." is selected, then load from a TXT File, else load normally
        If RDDL_Build.Text = My.Resources.Generic_LoadManually Then
            Dim TXTThread As New Threading.Thread(AddressOf LoadFromManualTXT) With {.IsBackground = True}
            TXTThread.SetApartmentState(Threading.ApartmentState.STA)
            TXTThread.Start()
        ElseIf RDDL_Build.Text = Nothing Then
            ' Do Nothing
        Else ' Run Background Worker
            BGW_PopulateGridView.RunWorkerAsync()
        End If
    End Sub

    ''' <summary>
    ''' Same code as BGW_PopulateGridView.RunWorkerAsync(), just that it get's the Feature List locally instead of from GitHub
    ''' </summary>
    Private Sub LoadFromManualTXT()
        ' Make a new OpenFileDialog
        Dim OFD As New OpenFileDialog With {
            .InitialDirectory = "C:\",
            .Title = My.Resources.LoadManually_PathToAFeatureList,
            .Filter = "Feature List|*.txt"
        }

        If OFD.ShowDialog() = DialogResult.OK AndAlso IO.File.Exists(OFD.FileName) Then
            ' Remove all Group Descriptors
            Invoke(Sub() RGV_MainGridView.GroupDescriptors.Clear())

            ' Set Status Label
            Invoke(Sub() RLE_StatusLabel.Text = My.Resources.Generic_PopulatingTheDataGridView)

            ' Clear Data Grid View
            Invoke(Sub() RGV_MainGridView.Rows.Clear())

            ' For each line add a grid view entry
            Try
                For Each Line In IO.File.ReadAllLines(OFD.FileName)
                    If Line = "## Unknown:" Then
                        LineStage = My.Resources.Generic_Modifiable
                    ElseIf Line = "## Always Enabled:" Then
                        LineStage = My.Resources.Generic_AlwaysEnabled
                    ElseIf Line = "## Enabled By Default:" Then
                        LineStage = My.Resources.Generic_EnabledByDefault
                    ElseIf Line = "## Disabled By Default:" Then
                        LineStage = My.Resources.Generic_DisabledByDefault
                    ElseIf Line = "## Always Disabled:" Then
                        LineStage = My.Resources.Generic_AlwaysDisabled
                    End If

                    ' Split the Line at the :
                    Dim Str As String() = Line.Split(CChar(":"))

                    ' Remove any Spaces from the first Str Array (Feature Name) and second Str Array (Feature ID)
                    Str = Str.Select(Function(s) s.Trim).ToArray()

                    ' If the Line is not empty, continue
                    If Line IsNot "" AndAlso Line.Contains("#") = False Then
                        ' Get the Feature Enabled State from the currently processing line.
                        ' RtlFeatureManager.QueryFeatureConfiguration will return Enabled, Disabled or throw a NullReferenceException for Default
                        Try
                            Dim State As String = RtlFeatureManager.QueryFeatureConfiguration(CUInt(Str(1)), FeatureConfigurationSection.Runtime).EnabledState.ToString
                            Dim Image = Nothing

#Region "DEBUG ONLY CODE."
                            If Diagnostics.Debugger.IsAttached And EnableDBLoadingForManualTXTLoad = True Then
                                If HasInternetConnection = True AndAlso HasDBAvailable = True AndAlso TableDoesNotExist = False Then
                                    Dim rows As DataRow() = Build_DT.Select(String.Format("FeatureName = '{0}'", Str(0)))
                                    If rows.Length >= 1 Then Image = CommentsImg
                                End If
                            End If
#End Region

                            Invoke(Sub() RGV_MainGridView.Rows.Add(Str(0), Str(1), State, LineStage, Image))
                        Catch NullEx As NullReferenceException
                            Dim Image = Nothing

#Region "DEBUG ONLY CODE."
                            If Diagnostics.Debugger.IsAttached And EnableDBLoadingForManualTXTLoad = True Then
                                If HasInternetConnection = True AndAlso HasDBAvailable = True AndAlso TableDoesNotExist = False Then
                                    Dim rows As DataRow() = Build_DT.Select(String.Format("FeatureName = '{0}'", Str(0)))
                                    If rows.Length >= 1 Then Image = CommentsImg
                                End If
                            End If
#End Region

                            Invoke(Sub() RGV_MainGridView.Rows.Add(Str(0), Str(1), My.Resources.Generic_Default, LineStage, Image))
                        Catch ex As Exception
                            Invoke(Sub() RGV_MainGridView.Rows.Add(Str(0), Str(1), My.Resources.Generic_Default, LineStage))
                        End Try
                    End If
                Next
                ' Move to the first row, remove the selection and change the Status Label to Done.
                Invoke(Sub()
                           RGV_MainGridView.CurrentRow = RGV_MainGridView.Rows.Item(0)
                           RGV_MainGridView.CurrentRow = Nothing
                           RLE_StatusLabel.Text = My.Resources.Generic_Done
                       End Sub)

                ' Enable Grouping
                Dim LineGroup As New Data.GroupDescriptor()
                LineGroup.GroupNames.Add("FeatureInfo", ComponentModel.ListSortDirection.Ascending)
                Invoke(Sub() RGV_MainGridView.GroupDescriptors.Add(LineGroup))

                ' Clear the selection
                Invoke(Sub()
                           RDDL_Build.SelectedIndex = -1
                           RDDL_Build.Enabled = True
                       End Sub)

                ' Make the Search Row Visible
                Invoke(Sub() RGV_MainGridView.MasterView.TableSearchRow.IsVisible = True)

                ' Enable Animations and selection
                Invoke(Sub()
                           AnimatedPropertySetting.AnimationsEnabled = True
                           RGV_MainGridView.SelectionMode = GridViewSelectionMode.FullRowSelect
                       End Sub)
            Catch ex As Exception
                Invoke(
                    Sub()
                        ' Catch Any Exception that may occur
                        ' Show an Error Dialog
                        RadTD.Generate($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
                        Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)

                        ' Clear the selection
                        RDDL_Build.SelectedIndex = -1
                        RDDL_Build.Enabled = True

                        ' Make the Search Row Visible
                        RGV_MainGridView.MasterView.TableSearchRow.IsVisible = True
                    End Sub)
            End Try
        Else
            ' Clear the selection
            Invoke(Sub()
                       RDDL_Build.SelectedIndex = -1
                       RDDL_Build.Enabled = True
                   End Sub)

            ' Resume searching, make the Search Row Visible
            Invoke(Sub() RGV_MainGridView.MasterView.TableSearchRow.IsVisible = True)
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
            ' Debug
            Diagnostics.Debug.WriteLine("Loading Build " & RDDL_Build.Text)

            ' Remove all Group Descriptors
            Invoke(Sub() RGV_MainGridView.GroupDescriptors.Clear())

            ' Load Comments
            If HasInternetConnection = True Then
                Invoke(Sub() RLE_StatusLabel.Text = "Getting Feature Comments from the Database")
                LoadCommentsFromDB(RDDL_Build.Text)
            End If

            ' Set Status Label
            Invoke(Sub() RLE_StatusLabel.Text = My.Resources.Generic_PopulatingTheDataGridView)

            ' Clear Data Grid View
            ' Fix for a weird Bug that happens randomly while clearing the rows if the search row has text in it
            Try
                Invoke(Sub() RGV_MainGridView.Rows.Clear())
            Catch ex As Exception
                Diagnostics.Debug.WriteLine("Exception while clearing row. Build: " &
                                            RDDL_Build.Text & ". " & ex.Message)
            End Try

            ' Prepare Web Client and download Build TXT
            Dim WebClient As New WebClient With {.Encoding = System.Text.Encoding.UTF8}
            Dim path As String = IO.Path.GetTempPath & RDDL_Build.Text & ".txt"
            WebClient.DownloadFile("https://raw.githubusercontent.com/riverar/mach2/master/features/" &
                                   RDDL_Build.Text & ".txt", path)

            ' For each line add a grid view entry
            For Each Line In IO.File.ReadAllLines(path)
                ' Check Line Stage, used for Grouping
                Try
                    If CInt(RDDL_Build.Text) >= 17704 Then
                        If Line = "## Unknown:" Then
                            LineStage = My.Resources.Generic_Modifiable
                        ElseIf Line = "## Always Enabled:" Then
                            LineStage = My.Resources.Generic_AlwaysEnabled
                        ElseIf Line = "## Enabled By Default:" Then
                            LineStage = My.Resources.Generic_EnabledByDefault
                        ElseIf Line = "## Disabled By Default:" Then
                            LineStage = My.Resources.Generic_DisabledByDefault
                        ElseIf Line = "## Always Disabled:" Then
                            LineStage = My.Resources.Generic_AlwaysDisabled
                        End If
                    Else
                        LineStage = My.Resources.Error_SelectBuild17704OrHigherToUseGrouping
                    End If
                Catch ex As Exception
                    LineStage = My.Resources.Error_Error
                End Try

                ' Split the Line at the :
                Dim Str As String() = Line.Split(CChar(":"))

                ' Remove any Spaces from the first Str Array (Feature Name) and second Str Array (Feature ID)
                Str = Str.Select(Function(s) s.Trim).ToArray()

                ' If the Line is not empty, continue
                If Line IsNot "" AndAlso Line.Contains("#") = False Then
                    ' Get the Feature Enabled State from the currently processing line.
                    ' RtlFeatureManager.QueryFeatureConfiguration will return Enabled, Disabled or throw a NullReferenceException for Default
                    Try
                        Dim State As String = RtlFeatureManager.QueryFeatureConfiguration(CUInt(Str(1)), FeatureConfigurationSection.Runtime).EnabledState.ToString
                        Dim Image = Nothing

                        If HasInternetConnection = True AndAlso HasDBAvailable = True AndAlso TableDoesNotExist = False Then
                            Dim rows As DataRow() = Build_DT.Select(String.Format("FeatureName = '{0}'", Str(0)))
                            If rows.Length >= 1 Then Image = CommentsImg
                        End If

                        Invoke(Sub() RGV_MainGridView.Rows.Add(Str(0), Str(1), State, LineStage, Image))
                    Catch NullEx As NullReferenceException
                        Dim Image = Nothing

                        If HasInternetConnection = True AndAlso HasDBAvailable = True AndAlso TableDoesNotExist = False Then
                            Dim rows As DataRow() = Build_DT.Select(String.Format("FeatureName = '{0}'", Str(0)))
                            If rows.Length >= 1 Then Image = CommentsImg
                        End If

                        Invoke(Sub() RGV_MainGridView.Rows.Add(Str(0), Str(1), My.Resources.Generic_Default, LineStage, Image))
                    Catch ex As Exception
                        Invoke(Sub() RGV_MainGridView.Rows.Add(Str(0), Str(1), My.Resources.Generic_Default, LineStage))
                    End Try
                End If
            Next

            ' Move to the first row, remove the selection and change the Status Label to Done.
            Invoke(Sub()
                       RGV_MainGridView.CurrentRow = RGV_MainGridView.Rows.Item(0)
                       RGV_MainGridView.CurrentRow = Nothing
                       RLE_StatusLabel.Text = My.Resources.Generic_Done
                   End Sub)

            ' Delete Feature List from %TEMP%
            IO.File.Delete(path)

            ' Enable Grouping
            Dim LineGroup As New Data.GroupDescriptor()
            LineGroup.GroupNames.Add("FeatureInfo", ComponentModel.ListSortDirection.Ascending)
            Invoke(Sub() RGV_MainGridView.GroupDescriptors.Add(LineGroup))

            ' Enable Animations and selection
            Invoke(Sub()
                       AnimatedPropertySetting.AnimationsEnabled = True
                       RGV_MainGridView.SelectionMode = GridViewSelectionMode.FullRowSelect
                   End Sub)

            ' Make the Search Row Visible
            Invoke(Sub() RGV_MainGridView.MasterView.TableSearchRow.IsVisible = True)
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
        ' End BGW
        BGW_PopulateGridView.CancelAsync()

        ' Enable the Build Combo Box
        RDDL_Build.Enabled = True
    End Sub

    ''' <summary>
    ''' Enable Feature Button, enables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_ActivateF_Click(sender As Object, e As EventArgs) Handles RMI_ActivateF.Click
        ' Stop Searching temporarily
        RGV_MainGridView.MasterView.TableSearchRow.SuspendSearch()

        ' Set Selected Feature to Enabled
        SetConfig(FeatureEnabledState.Enabled)

        ' Resume Searching
        RGV_MainGridView.MasterView.TableSearchRow.ResumeSearch()
    End Sub

    ''' <summary>
    ''' Disable Feature Button, disables the currently selected Feature.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_DeactivateF_Click(sender As Object, e As EventArgs) Handles RMI_DeactivateF.Click
        ' Stop Searching temporarily
        RGV_MainGridView.MasterView.TableSearchRow.SuspendSearch()

        ' Set Selected Feature to Disabled
        SetConfig(FeatureEnabledState.Disabled)

        ' Resume Searching
        RGV_MainGridView.MasterView.TableSearchRow.ResumeSearch()
    End Sub

    ''' <summary>
    ''' Revert Feature Button, reverts the currently selected Feature back to default values.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Default EventArgs</param>
    Private Sub RMI_RevertF_Click(sender As Object, e As EventArgs) Handles RMI_RevertF.Click
        ' Stop Searching temporarily
        RGV_MainGridView.MasterView.TableSearchRow.SuspendSearch()

        ' Set Selected Feature to Default Values
        SetConfig(FeatureEnabledState.Default)

        ' Resume Searching
        RGV_MainGridView.MasterView.TableSearchRow.ResumeSearch()
    End Sub

    ''' <summary>
    ''' Set's the Feature Configuration. Uses the FeatureEnabledState parameter to set the EnabledState of the Feature
    ''' </summary>
    ''' <param name="FeatureEnabledState">Specifies what Enabled State the Feature should be in. Can be either Enabled, Disabled or Default</param>
    Private Sub SetConfig(FeatureEnabledState As FeatureEnabledState)
        Try
            ' Initialize Variables
            Dim _enabledStateOptions, _variant, _variantPayloadKind, _variantPayload, _group As Integer
            _enabledStateOptions = 1
            _group = 4

            ' FeatureConfiguration Variable
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

            ' Set's the selected Feature to it's specified EnabledState. If anything goes wrong, display a Error Message in the Status Label.
            ' On Successful Operations; 
            ' RtlFeatureManager.SetBootFeatureConfigurations(_configs) returns True
            ' and RtlFeatureManager.SetLiveFeatureConfigurations(_configs, FeatureConfigurationSection.Runtime) returns 0
            If Not RtlFeatureManager.SetBootFeatureConfigurations(_configs) OrElse RtlFeatureManager.SetLiveFeatureConfigurations(_configs, FeatureConfigurationSection.Runtime) >= 1 Then
                ' Set Status Label
                RLE_StatusLabel.Text = String.Format(My.Resources.Error_SettingFeatureConfig, RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString)

                ' Fancy Message Box
                RadTD.ShowDialog($" {My.Resources.Error_AnErrorOccurred}",
                String.Format(My.Resources.Error_SetConfig, RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString, FeatureEnabledState.ToString),
                Nothing, RadTaskDialogIcon.Error)
            Else
                ' Set Status Label
                RLE_StatusLabel.Text = String.Format(My.Resources.SetConfig_SuccessfullySetFeature, RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString, FeatureEnabledState.ToString)

                ' Set Cell Text
                RGV_MainGridView.CurrentRow.Cells.Item(2).Value = FeatureEnabledState.ToString

                ' Fancy Message Box
                RadTD.ShowDialog(My.Resources.SetConfig_Success,
                String.Format(My.Resources.SetConfig_SuccessfullySetFeature, RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString, FeatureEnabledState.ToString),
                Nothing, RadTaskDialogIcon.ShieldSuccessGreenBar)
            End If
        Catch ex As Exception
            ' Catch Any Exception that may occur

            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
            Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
        End Try
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
        AboutAndSettings.ShowDialog()
    End Sub

    ''' <summary>
    ''' Show the Manually Set Feature ID UI when F12 is pressed
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">Key EventArgs</param>
    Private Sub GUI_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 Then SetManual.ShowDialog()
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
    ''' <returns>True if http://www.github.com responds. False if not</returns>
    Public Shared Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.github.com")
                    Diagnostics.Debug.WriteLine("Have Internet")
                    Return True
                End Using
            End Using
        Catch
            Diagnostics.Debug.WriteLine("Don't have Internet")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Form Closing Event. Used to forcefully close ViVeTool GUI and it's Threads
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">FormClosing EventArgs</param>
    Private Sub GUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Exit all running Threads forcefully, should fix ObjectDisposed Exceptions
        Diagnostics.Process.GetCurrentProcess().Kill()
    End Sub

    Private Sub ShowCommentForm(sender As Object, e As EventArgs)
        CommentsClient.ShowDialog()
    End Sub

    Private Sub RGV_MainGridView_ContextMenuOpening(sender As Object, e As ContextMenuOpeningEventArgs) Handles RGV_MainGridView.ContextMenuOpening
        ' DEBUG ONLY
        ' If RGV_MainGridView.SelectedRows.Count >= 1 AndAlso HasInternetConnection = True AndAlso RDDL_Build.Text IsNot String.Empty Then
        If RGV_MainGridView.SelectedRows.Count >= 1 AndAlso HasInternetConnection = True Then
            Try
                e.ContextMenu.Items.Add(New RadMenuSeparatorItem())
                e.ContextMenu.Items.Add(RMI_AddComment)
                CommentsClient.FeatureName = RGV_MainGridView.SelectedRows.Item(0).Cells(0).Value.ToString
                CommentsClient.Build = RDDL_Build.Text
            Catch ex As ArgumentException
                ' Exception that may occur from whilly-nilly spam opening the context menu and scrolling down
            End Try
        End If
    End Sub

    Private Sub LoadCommentsFromDB(Build As String)
        Diagnostics.Debug.WriteLine("LoadCommentsFromDB called.")

        ' Public, Read-Only Access Connection String
        Dim DB_Connection As New MySqlConnectionStringBuilder With {
            .Server = "direct.rawrr.cf",
            .UserID = "ViVeTool_GUI",
            .Password = "ViVeTool_GUI",
            .Database = "ViVeTool_GUI",
            .ConnectionTimeout = 120
        }

        ' Clear local DataTable
        Build_DT.Clear()

        ' Reset Variable
        TableDoesNotExist = False

        Try
            ' Get the latest comments Table for the current Build, and store it in a local Data Table
            Using con As New MySqlConnection(DB_Connection.ConnectionString)
                Using cmd As New MySqlCommand(String.Format("SELECT * FROM ViVeTool_GUI.`{0}`;", Build), con)
                    cmd.CommandType = CommandType.Text
                    Using sda As New MySqlDataAdapter(cmd)
                        sda.Fill(Build_DT)
                    End Using
                End Using
                Diagnostics.Debug.WriteLine("LoadCommentsFromDB Comments loaded.")
                con.Close()
            End Using
        Catch notFoundEx As MySqlException
            Diagnostics.Debug.WriteLine($"LoadCommentsFromDB Error: {notFoundEx.Message}")

            ' Fancy Message Box
            Dim Text, Expander As String

            Select Case notFoundEx.ErrorCode
                Case MySqlErrorCode.NoSuchTable
                    TableDoesNotExist = True
                    Diagnostics.Debug.WriteLine("Table does not exist")
                    Exit Try
                Case MySqlErrorCode.ServerShutdown
                    Text = "Comments couldn't be loaded, because the Database Server is currently shutting down."
                    Expander = notFoundEx.Message
                Case MySqlErrorCode.UnableToConnectToHost
                    Text = "Comments couldn't be loaded, because the Database Server is currently unavailable."
                    Expander = notFoundEx.Message
                Case Else
                    Text = "An Error occurred while communicating with the Comments Database Server"
                    Expander = notFoundEx.Message
            End Select

            RadTD.ShowDialog(" A Database Error occurred", "A Database Error occurred", Text, RadTaskDialogIcon.ShieldErrorRedBar,
            notFoundEx, Expander, Expander)
        Catch ex As Exception
            Invoke(Sub() MsgBox(ex.ToString))
        End Try
    End Sub

    ''' <summary>
    ''' Fix the Group Header. This removes the " : " in-front of a Group Name
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">FormClosing EventArgs</param>
    Private Sub RGV_MainGridView_GroupSummaryEvaluate(sender As Object, e As GroupSummaryEvaluationEventArgs) Handles RGV_MainGridView.GroupSummaryEvaluate
        If e.SummaryItem.Name = "FeatureInfo" Then e.FormatString = String.Format("{0}", e.Value)
    End Sub

    ''' <summary>
    ''' Cell Double Click Event. Used to display a comment if the current cell has a Comment Image
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RGV_MainGridView_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles RGV_MainGridView.CellDoubleClick
        Try
            Dim cRow = RGV_MainGridView.CurrentRow
            Dim cCell = RGV_MainGridView.CurrentCell

            ' Check if the Cell Image matches the Comments Image
            If cCell IsNot Nothing AndAlso cCell.Image Is CommentsImg Then
                ' Display the comment in a message box
                RadTD.ShowDialog(String.Format(" Comment for {0}", cRow.Cells(0).Value), Build_DT.Rows(0)("Comment").ToString,
                Nothing, New RadTaskDialogIcon(My.Resources.icons8_comments_24px))
            End If
        Catch ex As NullReferenceException
            ' Do nothing
        End Try
    End Sub
End Class