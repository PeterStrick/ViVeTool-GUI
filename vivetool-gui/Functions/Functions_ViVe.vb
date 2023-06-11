'ViVeTool GUI - Windows Feature Control GUI for ViVeTool
'Copyright (C) 2023 Peter Strick
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
Imports System.IO, System.Security.Cryptography, System.Text, Newtonsoft.Json.Linq
Imports Albacore.ViVe, Albacore.ViVe.Exceptions, Albacore.ViVe.NativeStructs, Albacore.ViVe.NativeEnums

''' <summary>
''' Class that contains all Functions and Subs to work with the ViVe API
''' </summary>
Public Class Functions_ViVe
    ''' <summary>
    ''' Internal Function that set's Windows Feature Configuration accordingly with the specified ID, Variant, State and Priority
    ''' </summary>
    ''' <param name="ID">ViVeTool ID</param>
    ''' <param name="ID_Variant">ViVeTool ID Variant</param>
    ''' <param name="State">Feature State, can be either Enabled, Disabled or Default</param>
    ''' <param name="Priority">Feature Priority</param>
    ''' <param name="Operation">Feature Operation. Can be either VariantState and FeatureState for Enable/Disable, or alternatively ResetState for Reset</param>
    ''' <param name="Type">The Feature Configuration Store to choose. Can be either Runtime or Boot. Using both is recommended</param>
    ''' <returns>An Integer corresponding to the Seccess Code of FeatureManager.SetFeatureConfigurations()</returns>
    Shared Function SetConfig(ID As UInteger, ID_Variant As UInteger, State As RTL_FEATURE_ENABLED_STATE,
                              Priority As RTL_FEATURE_CONFIGURATION_PRIORITY, Operation As RTL_FEATURE_CONFIGURATION_OPERATION,
                              Type As RTL_FEATURE_CONFIGURATION_TYPE) As Integer
        Dim _configs(0) As RTL_FEATURE_CONFIGURATION_UPDATE

        _configs(0) = New RTL_FEATURE_CONFIGURATION_UPDATE() With {
           .FeatureId = ID,
           .EnabledState = State,
           .EnabledStateOptions = RTL_FEATURE_ENABLED_STATE_OPTIONS.WexpConfig,
           .[Variant] = ID_Variant,
           .VariantPayload = ID_Variant,
           .VariantPayloadKind = RTL_FEATURE_VARIANT_PAYLOAD_KIND.External,
           .Priority = Priority,
           .Operation = Operation
        }

        Return FeatureManager.SetFeatureConfigurations(_configs, Type)
    End Function

    ''' <summary>
    ''' Enables ViVeTool Features using a specified ID and optionally a Variant
    ''' </summary>
    ''' <param name="ID">ViVeTool ID</param>
    ''' <param name="ID_Variant">ViVeTool ID Variant</param>
    Public Shared Sub Enable(ID As UInteger, Optional ID_Variant As UInteger = 0)
        Try
            ' Set the Feature to it's corresponding Enabled State in the Runtime Store
            Dim result_runtime = SetConfig(ID, ID_Variant, RTL_FEATURE_ENABLED_STATE.Enabled, RTL_FEATURE_CONFIGURATION_PRIORITY.User,
                                   RTL_FEATURE_CONFIGURATION_OPERATION.FeatureState Or RTL_FEATURE_CONFIGURATION_OPERATION.VariantState,
                                   RTL_FEATURE_CONFIGURATION_TYPE.Runtime)

            ' Set the Feature to it's corresponding Enabled State in the Boot Store (Feature becomes persistent)
            Dim result_boot = SetConfig(ID, ID_Variant, RTL_FEATURE_ENABLED_STATE.Enabled, RTL_FEATURE_CONFIGURATION_PRIORITY.User,
                                   RTL_FEATURE_CONFIGURATION_OPERATION.FeatureState Or RTL_FEATURE_CONFIGURATION_OPERATION.VariantState,
                                   RTL_FEATURE_CONFIGURATION_TYPE.Boot)

            ' Check if the Return Code for both is 0, meaning Success
            If result_runtime = 0 AndAlso result_boot = 0 Then
                RadTD.ShowDialog(My.Resources.SetConfig_Success,
                String.Format(My.Resources.SetConfig_SuccessfullySetFeatureID, ID, RTL_FEATURE_ENABLED_STATE.Enabled.ToString),
                Nothing, RadTaskDialogIcon.ShieldSuccessGreenBar)
            Else ' If not then show an Error
                RadTD.ShowDialog(My.Resources.Error_Error,
                String.Format(My.Resources.Error_SetConfig, ID, RTL_FEATURE_ENABLED_STATE.Enabled.ToString),
                Nothing, RadTaskDialogIcon.Error, ExpandedText:=$"The Functions returned {result_runtime & result_boot}. Expected 00")
            End If
        Catch fpoe As FeaturePropertyOverflowException
            ' Used within ViVeTool so also catched here
            RadTD.ShowDialog($" {My.Resources.Error_AnErrorOccurred}", My.Resources.Error_AnErrorOccurred, fpoe.Message,
                             RadTaskDialogIcon.Error, fpoe, fpoe.ToString, fpoe.ToString)
        Catch ex As Exception
            ' Catch any other Exception
            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
            Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Disables ViVeTool Features using a specified ID and optionally a Variant
    ''' </summary>
    ''' <param name="ID">ViVeTool ID</param>
    ''' <param name="ID_Variant">ViVeTool ID Variant</param>
    Public Shared Sub Disable(ID As UInteger, ID_Variant As UInteger)
        Try
            ' Set the Feature to it's corresponding Enabled State in the Runtime Store
            Dim result_runtime = SetConfig(ID, ID_Variant, RTL_FEATURE_ENABLED_STATE.Disabled, RTL_FEATURE_CONFIGURATION_PRIORITY.User,
                                   RTL_FEATURE_CONFIGURATION_OPERATION.FeatureState Or RTL_FEATURE_CONFIGURATION_OPERATION.VariantState,
                                   RTL_FEATURE_CONFIGURATION_TYPE.Runtime)

            ' Set the Feature to it's corresponding Enabled State in the Boot Store (Feature becomes persistent)
            Dim result_boot = SetConfig(ID, ID_Variant, RTL_FEATURE_ENABLED_STATE.Disabled, RTL_FEATURE_CONFIGURATION_PRIORITY.User,
                                   RTL_FEATURE_CONFIGURATION_OPERATION.FeatureState Or RTL_FEATURE_CONFIGURATION_OPERATION.VariantState,
                                   RTL_FEATURE_CONFIGURATION_TYPE.Boot)

            ' Check if the Return Code for both is 0, meaning Success
            If result_runtime = 0 AndAlso result_boot = 0 Then
                RadTD.ShowDialog(My.Resources.SetConfig_Success,
                String.Format(My.Resources.SetConfig_SuccessfullySetFeatureID, ID, RTL_FEATURE_ENABLED_STATE.Disabled.ToString),
                Nothing, RadTaskDialogIcon.ShieldSuccessGreenBar)
            Else ' If not then show an Error
                RadTD.ShowDialog(My.Resources.Error_Error,
                String.Format(My.Resources.Error_SetConfig, ID, RTL_FEATURE_ENABLED_STATE.Disabled.ToString),
                Nothing, RadTaskDialogIcon.Error, ExpandedText:=$"The Functions returned {result_runtime & result_boot}. Expected 00")
            End If
        Catch fpoe As FeaturePropertyOverflowException
            ' Used within ViVeTool so also catched here
            RadTD.ShowDialog($" {My.Resources.Error_AnErrorOccurred}", My.Resources.Error_AnErrorOccurred, fpoe.Message,
                             RadTaskDialogIcon.Error, fpoe, fpoe.ToString, fpoe.ToString)
        Catch ex As Exception
            ' Catch any other Exception
            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
            Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Resets ViVeTool Features using a specified ID
    ''' </summary>
    ''' <param name="ID">ViVeTool ID</param>
    Public Shared Sub Reset(ID As UInteger)
        Try
            ' Set the Feature to it's corresponding Enabled State in the Runtime Store
            Dim result_runtime = SetConfig(ID, 0, RTL_FEATURE_ENABLED_STATE.Default, RTL_FEATURE_CONFIGURATION_PRIORITY.User,
                                   RTL_FEATURE_CONFIGURATION_OPERATION.ResetState, RTL_FEATURE_CONFIGURATION_TYPE.Runtime)

            ' Set the Feature to it's corresponding Enabled State in the Boot Store (Feature becomes persistent)
            Dim result_boot = SetConfig(ID, 0, RTL_FEATURE_ENABLED_STATE.Default, RTL_FEATURE_CONFIGURATION_PRIORITY.User,
                                   RTL_FEATURE_CONFIGURATION_OPERATION.ResetState, RTL_FEATURE_CONFIGURATION_TYPE.Boot)

            ' Check if the Return Code for both is 0, meaning Success
            If result_runtime = 0 AndAlso result_boot = 0 Then
                RadTD.ShowDialog(My.Resources.SetConfig_Success,
                String.Format(My.Resources.SetConfig_SuccessfullySetFeatureID, ID, RTL_FEATURE_ENABLED_STATE.Default.ToString),
                Nothing, RadTaskDialogIcon.ShieldSuccessGreenBar)
            Else ' If not then show an Error
                RadTD.ShowDialog(My.Resources.Error_Error,
                String.Format(My.Resources.Error_SetConfig, ID, RTL_FEATURE_ENABLED_STATE.Default.ToString),
                Nothing, RadTaskDialogIcon.Error, ExpandedText:=$"The Functions returned {result_runtime & result_boot}. Expected 00")
            End If
        Catch fpoe As FeaturePropertyOverflowException
            ' Used within ViVeTool so also catched here
            RadTD.ShowDialog($" {My.Resources.Error_AnErrorOccurred}", My.Resources.Error_AnErrorOccurred, fpoe.Message,
                             RadTaskDialogIcon.Error, fpoe, fpoe.ToString, fpoe.ToString)
        Catch ex As Exception
            ' Catch any other Exception
            RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
            Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Querys the Enabled State of a ViVeTool Feature using a specified ID
    ''' </summary>
    ''' <param name="ID">ViVeTool ID</param>
    ''' <returns>ViVeTool ID Enabled State as String</returns>
    Public Shared Function Query(ID As UInteger) As String
        Dim _query = FeatureManager.QueryFeatureConfiguration(ID).GetValueOrDefault
        Return _query.EnabledState.ToString
    End Function

    ''' <summary>
    ''' Returns an Array of Enabled/Disabled Features on the current System
    ''' </summary>
    ''' <param name="Store">The Feature Configuration Store to use. Can be either Runtime or Boot</param>
    ''' <returns>A RTL_FEATURE_CONFIGURATION Array</returns>
    Public Shared Function QueryAll(Store As RTL_FEATURE_CONFIGURATION_TYPE) As RTL_FEATURE_CONFIGURATION()
        Return FeatureManager.QueryAllFeatureConfigurations(Store)
    End Function

    ''' <summary>
    ''' Windows keeps a Backup of Feature Configurations, known as the LKG Store and rolls back to it, if a Feature causes System instabillity.
    ''' 
    ''' The ViVe API has a function to automatically fix the LKG Store, if it has become corrupted due to a use-after-free bug in fcon.dll
    ''' </summary>
    ''' <returns>True if the Fix was successful, False if an Error occurred</returns>
    Public Shared Sub FixLastKnownGood()
        If FeatureManager.FixLKGStore() Then
            RadTD.ShowDialog(My.Resources.SetConfig_Success, "Last Known Good Store was successfully repaired.",
                             Nothing, RadTaskDialogIcon.ShieldSuccessGreenBar)
        Else
            RadTD.ShowDialog("Not required", "Fixing of the Last Known Good Store is not required.",
                             Nothing, RadTaskDialogIcon.Information)
        End If
    End Sub

    ''' <summary>
    ''' This moves ViVeTool Features from the Service Priority to the User Priority. Features in the Service Priority are A/B Tests that can be added and removed remotely by Microsofts A/B Testing Program. Moving them to the User Priority makes them Permanent / not remotely modifiable.
    ''' </summary>
    Public Shared Sub FixPriority()
        Dim FixStatus_Runtime As Integer
        Dim FixStatus_Boot As Integer

        FixStatus_Runtime = FixPriority_HelperFunction(RTL_FEATURE_CONFIGURATION_TYPE.Runtime)
        FixStatus_Boot = FixPriority_HelperFunction(RTL_FEATURE_CONFIGURATION_TYPE.Boot)

        If FixStatus_Runtime = 0 AndAlso FixStatus_Boot = 0 Then
            RadTD.ShowDialog(My.Resources.SetConfig_Success, "Feature Priorities were successfully fixed. A reboot is recommended.",
                             Nothing, RadTaskDialogIcon.ShieldSuccessGreenBar)
        ElseIf FixStatus_Runtime = 1 AndAlso FixStatus_Boot = 1 Then
            RadTD.ShowDialog("Not required", "Fixing of Feature Priorities is not required.",
                             Nothing, RadTaskDialogIcon.Information)
        ElseIf FixStatus_Runtime = 2 AndAlso FixStatus_Boot = 2 Then
            RadTD.ShowDialog(My.Resources.Error_Error, "An Error occurred while trying to fix the Feature Priorities.", Nothing,
                             RadTaskDialogIcon.Error,
                             ExpandedText:=$"The Functions returned {FixStatus_Runtime & FixStatus_Boot}. Expected 00")
        End If
    End Sub

    ''' <summary>
    ''' Helper Function for FixPriority(). Returns Status Codes depending on the Return of FixPriority_HelperFunction_SetConfig()
    ''' </summary>
    ''' <param name="configurationType">Feature Configuration Store to perform the Operation on. Can be either Runtime or Boot</param>
    ''' <returns>0 for Success, 1 if fixing is not required, 2 for any Errors</returns>
    Private Shared Function FixPriority_HelperFunction(configurationType As RTL_FEATURE_CONFIGURATION_TYPE) As Integer
        Dim Features = FeatureManager.QueryAllFeatureConfigurations(configurationType)
        Dim Fixes = FixPriority_Query(Features)

        If Fixes Is Nothing Then Return 1

        Dim FixStatus As Integer
        For Each FeatureFix In Fixes
            FixStatus = SetConfig(FeatureFix.FeatureId, FeatureFix.Variant, FeatureFix.EnabledState, FeatureFix.Priority,
                                  FeatureFix.Operation, configurationType)
        Next

        If FixStatus = 0 Then
            Return 0
        Else
            Return 2
        End If
    End Function

    ''' <summary>
    ''' Helper Function that determines if any Features require a Priority Fix
    ''' </summary>
    ''' <param name="configurations">A Feature Configuration Array returned from FeatureManager.QueryAllFeatureConfigurations()</param>
    ''' <returns>A Feature Configuration Update Array, or Nothing if no Features require fixing</returns>
    Private Shared Function FixPriority_Query(configurations As RTL_FEATURE_CONFIGURATION()) As RTL_FEATURE_CONFIGURATION_UPDATE()
        Dim configsToFix = configurations.Where(Function(x) x.Priority = RTL_FEATURE_CONFIGURATION_PRIORITY.Service AndAlso Not x.IsWexpConfiguration)
        If Not configsToFix.Any() Then Return Nothing

        Dim priorityFixUpdates(configsToFix.Count() * 2) As RTL_FEATURE_CONFIGURATION_UPDATE
        priorityFixUpdates(configsToFix.Count() * 2) = New RTL_FEATURE_CONFIGURATION_UPDATE()
        Dim updatesCreated = 0

        For Each cfg In configsToFix
            priorityFixUpdates(updatesCreated) = New RTL_FEATURE_CONFIGURATION_UPDATE() With {
                .FeatureId = cfg.FeatureId,
                .Priority = cfg.Priority,
                .Operation = RTL_FEATURE_CONFIGURATION_OPERATION.ResetState
            }
            priorityFixUpdates(updatesCreated + 1) = New RTL_FEATURE_CONFIGURATION_UPDATE() With {
                .FeatureId = cfg.FeatureId,
                .Priority = RTL_FEATURE_CONFIGURATION_PRIORITY.User,
                .EnabledState = cfg.EnabledState,
                .[Variant] = cfg.[Variant],
                .VariantPayloadKind = cfg.VariantPayloadKind,
                .VariantPayload = cfg.VariantPayload,
                .Operation = RTL_FEATURE_CONFIGURATION_OPERATION.FeatureState Or RTL_FEATURE_CONFIGURATION_OPERATION.VariantState
            }
            updatesCreated += 2
        Next

        Return priorityFixUpdates
    End Function

    Shared Sub FeatureDictionaryUpdate()
        ' Required Headers for the GitHub API
        Dim WebClientRepo As New WebClient With {.Encoding = Encoding.UTF8}
        WebClientRepo.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8")
        WebClientRepo.Headers.Add(HttpRequestHeader.UserAgent, "PeterStrick/vivetool-gui")

        Try
            ' Check if Dict exists
            If Not File.Exists(FeatureNaming.DictFilePath) Then
                Dim Buttons As New RadItemOwnerGenericCollection(Of RadTaskDialogButton) From {
                    RadTaskDialogButton.Yes,
                    RadTaskDialogButton.No
                }
                Dim Dialog = RadTD.Generate($" {My.Resources.DictUpdate_DictionaryIsMissing}", My.Resources.DictUpdate_DictionaryIsMissing, My.Resources.DictUpdate_Text, RadTaskDialogIcon.ShieldGrayBar, CommandAreaButtons:=Buttons)


                If RadTaskDialog.ShowDialog(Dialog) = RadTaskDialogButton.Yes Then
                    MsgBox("Yes Button")
                Else
                    MsgBox("No Button")
                    Return
                End If
            End If


            Dim LocalSHA = UpdateCheck.HashUTF8TextFile(FeatureNaming.DictFilePath)
            Dim RawJSON As String = WebClientRepo.DownloadString("https://api.github.com/repos/thebookisclosed/ViVe/contents/Extra")
            Dim JSONArray As JArray = JArray.Parse(RawJSON)
            Dim JSONObject As JObject = CType(JSONArray(0), JObject)

            If LocalSHA = JSONObject("sha").ToString Then
                MsgBox("same")
            Else
                MsgBox($"Online: {JSONObject("sha")} {Environment.NewLine & Environment.NewLine}Local: {LocalSHA}")
            End If
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
    End Sub
End Class

''' <summary>
''' ViVeTool 0.3.3 CLI Class used for getting Feature Names from ViVeTool IDs. 
''' 
''' Copyright (C) 2019-2023  @thebookisclosed.
''' 
''' Licensed under GNU General Public License V3
''' </summary>
Friend Class FeatureNaming
    Friend Const DictFileName As String = "FeatureDictionary.pfs"
    Friend Shared DictFilePath As String = Path.Combine(Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location), DictFileName)

    Friend Shared Function FindNamesForFeatures(featureIDs As IEnumerable(Of UInteger)) As Dictionary(Of UInteger, String)
        Dim result = New Dictionary(Of UInteger, String)()
        If Not File.Exists(DictFilePath) Then Return Nothing
        Dim idsCommas = featureIDs.[Select](Function(x) "," & x.ToString()).ToList()

        Using reader As New StreamReader(File.OpenRead(DictFilePath))
            While Not reader.EndOfStream
                Dim currentLine = reader.ReadLine()

                For Each ic In idsCommas
                    If currentLine.EndsWith(ic) Then
                        result(UInteger.Parse(ic.Substring(1))) = currentLine.Substring(0, currentLine.IndexOf(","c))
                        idsCommas.Remove(ic)
                        Exit For
                    End If
                Next

                If idsCommas.Count = 0 Then Exit While
            End While
        End Using

        Return result
    End Function
End Class

''' <summary>
''' ViVeTool 0.3.3 CLI Class used for hashing a UTF-8 Text File. 
''' 
''' Copyright (C) 2019-2023  @thebookisclosed.
''' 
''' Licensed under GNU General Public License V3
''' </summary>
Friend Class UpdateCheck
    Friend Shared Function HashUTF8TextFile(filePath As String) As String
        If Not File.Exists(filePath) Then Return Nothing

        Using sha1Csp = New SHA1CryptoServiceProvider()
            Dim fileBody = Encoding.UTF8.GetBytes(File.ReadAllText(filePath).Replace(vbCrLf, vbCr))
            Dim fullLength = fileBody.Length
            Dim preamble = Encoding.UTF8.GetPreamble()
            Dim filePortion = New Byte(preamble.Length - 1) {}

            Using fs = New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                fs.Read(filePortion, 0, filePortion.Length)
            End Using

            For i As Integer = 0 To preamble.Length - 1
                If preamble(i) = filePortion(i) Then fullLength += 1
            Next

            Dim preface = Encoding.UTF8.GetBytes($"blob {fullLength}{vbNullChar}")
            sha1Csp.TransformBlock(preface, 0, preface.Length, Nothing, 0)
            If fullLength > fileBody.Length Then sha1Csp.TransformBlock(preamble, 0, preamble.Length, Nothing, 0)
            sha1Csp.TransformFinalBlock(fileBody, 0, fileBody.Length)
            Return BitConverter.ToString(sha1Csp.Hash).Replace("-", "").ToLowerInvariant()
        End Using
    End Function
End Class