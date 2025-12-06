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

Imports System.Collections.ObjectModel
Imports Albacore.ViVe
Imports ViVeTool_GUI.Wpf.Models

Namespace Services
    ''' <summary>
    ''' Service for managing Windows feature configurations.
    ''' Uses Albacore.ViVe.RtlFeatureManager to query and modify Windows features.
    ''' Supports feed-first loading with optional RTL overlay.
    ''' </summary>
    Public Class FeatureService
        ' Constants for FeatureConfiguration settings
        ' EnabledStateOptions = 1: Standard option for feature state updates
        Private Const EnabledStateOptionsValue As Integer = 1
        ' Group = 4: Standard group value for user-configured features
        Private Const GroupValue As Integer = 4
        ' Variant values (0 = default/no variant)
        Private Const VariantValue As Integer = 0
        Private Const VariantPayloadValue As Integer = 0
        Private Const VariantPayloadKindValue As Integer = 0

        ''' <summary>
        ''' Gets the last error message from a failed operation.
        ''' </summary>
        Private _lastErrorMessage As String = String.Empty

        ''' <summary>
        ''' Reference to GitHubService for feed-based loading.
        ''' </summary>
        Private ReadOnly _gitHubService As GitHubService

        ''' <summary>
        ''' Gets the last error message from a failed operation.
        ''' </summary>
        Public ReadOnly Property LastErrorMessage As String
            Get
                Return _lastErrorMessage
            End Get
        End Property

        ''' <summary>
        ''' Creates a new instance of FeatureService.
        ''' </summary>
        Public Sub New()
            Me.New(New GitHubService())
        End Sub

        ''' <summary>
        ''' Creates a new instance of FeatureService with a custom GitHubService.
        ''' </summary>
        Public Sub New(gitHubService As GitHubService)
            _gitHubService = gitHubService
        End Sub
        ''' <summary>
        ''' Gets all available features from the system by querying RtlFeatureManager.
        ''' This is the legacy method - prefer using feed-first loading methods.
        ''' </summary>
        ''' <returns>A collection of feature items.</returns>
        Public Async Function GetFeaturesAsync() As Task(Of ObservableCollection(Of FeatureItem))
            Return Await Task.Run(Function() GetFeaturesCore())
        End Function

        ''' <summary>
        ''' Gets features from GitHub feed for a specific build with optional RTL overlay.
        ''' This is the preferred method for loading features.
        ''' </summary>
        ''' <param name="buildNumber">The Windows build number (e.g., "26100"). If Nothing, uses latest.</param>
        ''' <param name="applyRtlOverlay">Whether to overlay RTL feature states on top of feed data.</param>
        ''' <returns>A collection of feature items.</returns>
        Public Async Function GetFeaturesFromFeedAsync(buildNumber As String, Optional applyRtlOverlay As Boolean = True) As Task(Of ObservableCollection(Of FeatureItem))
            Return Await GetFeaturesFromFeedCoreAsync(buildNumber, applyRtlOverlay)
        End Function

        ''' <summary>
        ''' Loads features from a local file (txt/csv/json format) with optional RTL overlay.
        ''' </summary>
        ''' <param name="filePath">Path to the local feature list file.</param>
        ''' <param name="applyRtlOverlay">Whether to overlay RTL feature states on top of file data.</param>
        ''' <returns>A collection of feature items.</returns>
        Public Async Function GetFeaturesFromFileAsync(filePath As String, Optional applyRtlOverlay As Boolean = True) As Task(Of ObservableCollection(Of FeatureItem))
            Return Await GetFeaturesFromFileCoreAsync(filePath, applyRtlOverlay)
        End Function

        ' Maximum allowed group value according to Windows RTL Feature Manager
        Private Const MaxGroupValue As Integer = 14

        ''' <summary>
        ''' List of warning messages from the last operation.
        ''' </summary>
        Private _warnings As New List(Of String)()

        ''' <summary>
        ''' Gets the warning messages from the last operation.
        ''' </summary>
        Public ReadOnly Property Warnings As List(Of String)
            Get
                Return _warnings
            End Get
        End Property

        ''' <summary>
        ''' Core method to query all feature configurations from the system.
        ''' Implements defensive handling to continue loading even if some features fail.
        ''' </summary>
        Private Function GetFeaturesCore() As ObservableCollection(Of FeatureItem)
            Dim result As New ObservableCollection(Of FeatureItem)()
            _warnings.Clear()

            ' Create a dictionary to merge configurations by FeatureId
            Dim featureDict As New Dictionary(Of UInteger, FeatureItem)()
            Dim runtimeConfigs As IEnumerable(Of FeatureConfiguration) = Nothing
            Dim bootConfigs As IEnumerable(Of FeatureConfiguration) = Nothing

            ' Query runtime configurations with defensive handling
            Try
                runtimeConfigs = RtlFeatureManager.QueryAllFeatureConfigurations(FeatureConfigurationSection.Runtime)
            Catch ex As Exception
                ' Log warning but continue - we may still get boot configs
                Dim warning = If(ex.Message.Contains("Group"),
                    "Warning: Could not query runtime features. The library encountered features with Group values above its supported maximum (14).",
                    $"Warning: Could not query runtime features: {ex.Message}")
                _warnings.Add(warning)
                System.Diagnostics.Debug.WriteLine(warning)
            End Try

            ' Query boot configurations with defensive handling
            Try
                bootConfigs = RtlFeatureManager.QueryAllFeatureConfigurations(FeatureConfigurationSection.Boot)
            Catch ex As Exception
                ' Log warning but continue - we may still have runtime configs
                Dim warning = If(ex.Message.Contains("Group"),
                    "Warning: Could not query boot features. The library encountered features with Group values above its supported maximum (14).",
                    $"Warning: Could not query boot features: {ex.Message}")
                _warnings.Add(warning)
                System.Diagnostics.Debug.WriteLine(warning)
            End Try

            ' Process runtime configurations first with per-item error handling
            If runtimeConfigs IsNot Nothing Then
                For Each config In runtimeConfigs
                    Try
                        ' Validate and clamp group value if needed
                        Dim groupValue = config.Group
                        Dim groupWarning As String = Nothing
                        If groupValue > MaxGroupValue Then
                            groupWarning = $"Group value {groupValue} clamped to {MaxGroupValue}"
                            groupValue = MaxGroupValue
                        End If

                        Dim stateStr As String = config.EnabledState.ToString()
                        Dim isEnabled As Boolean = (config.EnabledState = FeatureEnabledState.Enabled)
                        Dim notes = $"Group: {config.Group}, Variant: {config.Variant}"
                        If groupWarning IsNot Nothing Then
                            notes = notes & $" ({groupWarning})"
                        End If

                        Dim featureItem As New FeatureItem(
                            CInt(config.FeatureId),
                            $"Feature {config.FeatureId}",
                            stateStr,
                            isEnabled,
                            notes
                        )
                        featureDict(config.FeatureId) = featureItem
                    Catch ex As Exception
                        ' Skip individual problematic features and log warning
                        Dim warning = $"Warning: Skipped feature {config.FeatureId}: {ex.Message}"
                        _warnings.Add(warning)
                        System.Diagnostics.Debug.WriteLine(warning)
                    End Try
                Next
            End If

            ' Process boot configurations (may override or add to runtime) with per-item error handling
            If bootConfigs IsNot Nothing Then
                For Each config In bootConfigs
                    Try
                        If Not featureDict.ContainsKey(config.FeatureId) Then
                            ' Validate and clamp group value if needed
                            Dim groupValue = config.Group
                            Dim groupWarning As String = Nothing
                            If groupValue > MaxGroupValue Then
                                groupWarning = $"Group value {groupValue} clamped to {MaxGroupValue}"
                                groupValue = MaxGroupValue
                            End If

                            Dim stateStr As String = config.EnabledState.ToString()
                            Dim isEnabled As Boolean = (config.EnabledState = FeatureEnabledState.Enabled)
                            Dim notes = $"Group: {config.Group}, Variant: {config.Variant} (Boot)"
                            If groupWarning IsNot Nothing Then
                                notes = notes & $" ({groupWarning})"
                            End If

                            Dim featureItem As New FeatureItem(
                                CInt(config.FeatureId),
                                $"Feature {config.FeatureId}",
                                stateStr,
                                isEnabled,
                                notes
                            )
                            featureDict(config.FeatureId) = featureItem
                        End If
                    Catch ex As Exception
                        ' Skip individual problematic features and log warning
                        Dim warning = $"Warning: Skipped boot feature {config.FeatureId}: {ex.Message}"
                        _warnings.Add(warning)
                        System.Diagnostics.Debug.WriteLine(warning)
                    End Try
                Next
            End If

            ' Add all successfully loaded features to the result collection
            For Each kvp In featureDict.OrderBy(Function(x) x.Key)
                result.Add(kvp.Value)
            Next

            ' Handle warnings and error messages
            If _warnings.Count > 0 Then
                _lastErrorMessage = String.Join("; ", _warnings)
            Else
                _lastErrorMessage = String.Empty
            End If

            ' If no features were loaded, add an informational item that won't break the UI
            If result.Count = 0 Then
                Dim message As String
                If _warnings.Count > 0 Then
                    message = "Unable to load system features due to library compatibility issues. " &
                              "Please check for library updates or use an alternative method to manage features."
                Else
                    message = "No features found on this system."
                End If

                result.Add(New FeatureItem(
                    0,
                    "No features available",
                    "Information",
                    False,
                    message
                ))
            End If

            Return result
        End Function

        ''' <summary>
        ''' Enables a specific feature by setting its EnabledState to Enabled.
        ''' </summary>
        ''' <param name="featureId">The feature ID to enable.</param>
        ''' <returns>True if successful, false otherwise.</returns>
        Public Async Function EnableFeatureAsync(featureId As Integer) As Task(Of Boolean)
            Return Await Task.Run(Function() SetFeatureConfiguration(CUInt(featureId), FeatureEnabledState.Enabled))
        End Function

        ''' <summary>
        ''' Disables a specific feature by setting its EnabledState to Disabled.
        ''' </summary>
        ''' <param name="featureId">The feature ID to disable.</param>
        ''' <returns>True if successful, false otherwise.</returns>
        Public Async Function DisableFeatureAsync(featureId As Integer) As Task(Of Boolean)
            Return Await Task.Run(Function() SetFeatureConfiguration(CUInt(featureId), FeatureEnabledState.Disabled))
        End Function

        ''' <summary>
        ''' Reverts a specific feature to its default state.
        ''' </summary>
        ''' <param name="featureId">The feature ID to revert.</param>
        ''' <returns>True if successful, false otherwise.</returns>
        Public Async Function RevertFeatureAsync(featureId As Integer) As Task(Of Boolean)
            Return Await Task.Run(Function() SetFeatureConfiguration(CUInt(featureId), FeatureEnabledState.Default))
        End Function

        ''' <summary>
        ''' Applies a feature configuration (enable or disable).
        ''' </summary>
        ''' <param name="featureId">The feature ID.</param>
        ''' <param name="enable">True to enable, false to disable.</param>
        ''' <returns>True if successful, false otherwise.</returns>
        Public Async Function ApplyFeatureAsync(featureId As Integer, enable As Boolean) As Task(Of Boolean)
            If enable Then
                Return Await EnableFeatureAsync(featureId)
            Else
                Return Await DisableFeatureAsync(featureId)
            End If
        End Function

        ''' <summary>
        ''' Sets a feature configuration using RtlFeatureManager.
        ''' This method sets both boot and live configurations.
        ''' </summary>
        ''' <param name="featureId">The feature ID to configure.</param>
        ''' <param name="enabledState">The desired enabled state.</param>
        ''' <returns>True if successful, false otherwise.</returns>
        Private Function SetFeatureConfiguration(featureId As UInteger, enabledState As FeatureEnabledState) As Boolean
            Try
                ' Create the feature configuration using defined constants
                Dim config As New FeatureConfiguration() With {
                    .FeatureId = featureId,
                    .EnabledState = enabledState,
                    .EnabledStateOptions = EnabledStateOptionsValue,
                    .Group = GroupValue,
                    .Variant = VariantValue,
                    .VariantPayload = VariantPayloadValue,
                    .VariantPayloadKind = VariantPayloadKindValue,
                    .Action = FeatureConfigurationAction.UpdateEnabledState
                }

                Dim configs As New List(Of FeatureConfiguration) From {config}

                ' Set both boot and live configurations
                ' On successful operations:
                ' - SetBootFeatureConfigurations returns True
                ' - SetLiveFeatureConfigurations returns 0
                Dim bootResult As Boolean = RtlFeatureManager.SetBootFeatureConfigurations(configs)
                Dim liveResult As Integer = RtlFeatureManager.SetLiveFeatureConfigurations(configs, FeatureConfigurationSection.Runtime)

                Return bootResult AndAlso (liveResult = 0)
            Catch ex As Exception
                ' Store error message for debugging
                _lastErrorMessage = ex.Message
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Core method to load features from GitHub feed with optional RTL overlay.
        ''' </summary>
        Private Async Function GetFeaturesFromFeedCoreAsync(buildNumber As String, applyRtlOverlay As Boolean) As Task(Of ObservableCollection(Of FeatureItem))
            Dim result As New ObservableCollection(Of FeatureItem)()
            _warnings.Clear()

            Try
                ' Determine which build to use
                Dim targetBuild = buildNumber
                If String.IsNullOrWhiteSpace(targetBuild) Then
                    ' Get latest build from feed
                    Dim feedInfo = Await _gitHubService.GetLatestFeedInfoAsync()
                    If feedInfo IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(feedInfo.LatestBuild) Then
                        targetBuild = feedInfo.LatestBuild
                    Else
                        Dim warning = "Could not determine latest build from feed"
                        _warnings.Add(warning)
                        _lastErrorMessage = warning
                        result.Add(New FeatureItem(0, "No features loaded", "Warning", False, warning))
                        Return result
                    End If
                End If

                ' Load feature entries from feed
                Dim entries = Await _gitHubService.GetFeatureEntriesForBuildAsync(targetBuild)
                If entries Is Nothing OrElse entries.Count = 0 Then
                    Dim warning = $"No features found in feed for build {targetBuild}"
                    _warnings.Add(warning)
                    _lastErrorMessage = warning
                    result.Add(New FeatureItem(0, "No features loaded", "Warning", False, warning))
                    Return result
                End If

                ' Convert entries to FeatureItems
                Dim featureDict As New Dictionary(Of UInteger, FeatureItem)()
                For Each entry In entries
                    Try
                        Dim notes = If(String.IsNullOrWhiteSpace(entry.Group), "From feed", $"Group: {entry.Group}")
                        Dim featureItem As New FeatureItem(
                            entry.Id,
                            If(String.IsNullOrWhiteSpace(entry.Name), $"Feature {entry.Id}", entry.Name),
                            "Default",
                            False,
                            notes
                        )
                        featureDict(CUInt(entry.Id)) = featureItem
                    Catch ex As Exception
                        Dim warning = $"Warning: Skipped feature {entry.Id}: {ex.Message}"
                        _warnings.Add(warning)
                        System.Diagnostics.Debug.WriteLine(warning)
                    End Try
                Next

                ' Apply RTL overlay if requested
                If applyRtlOverlay Then
                    Call ApplyRtlOverlayToFeatures(featureDict)
                End If

                ' Add all features to result
                For Each kvp In featureDict.OrderBy(Function(x) x.Key)
                    result.Add(kvp.Value)
                Next

                If _warnings.Count > 0 Then
                    _lastErrorMessage = String.Join("; ", _warnings)
                Else
                    _lastErrorMessage = String.Empty
                End If

            Catch ex As Exception
                Dim warning = $"Error loading features from feed: {ex.Message}"
                _warnings.Add(warning)
                _lastErrorMessage = warning
                result.Add(New FeatureItem(0, "No features loaded", "Error", False, warning))
            End Try

            Return result
        End Function

        ''' <summary>
        ''' Core method to load features from a local file with optional RTL overlay.
        ''' </summary>
        Private Async Function GetFeaturesFromFileCoreAsync(filePath As String, applyRtlOverlay As Boolean) As Task(Of ObservableCollection(Of FeatureItem))
            Dim result As New ObservableCollection(Of FeatureItem)()
            _warnings.Clear()

            Try
                If Not System.IO.File.Exists(filePath) Then
                    Dim warning = $"File not found: {filePath}"
                    _warnings.Add(warning)
                    _lastErrorMessage = warning
                    result.Add(New FeatureItem(0, "No features loaded", "Error", False, warning))
                    Return result
                End If

                ' Read file content (use Task.Run for file I/O to avoid blocking UI)
                Dim content = Await Task.Run(Function() System.IO.File.ReadAllText(filePath))
                Dim entries As List(Of FeatureEntry) = Nothing

                ' Try to parse based on file extension
                Dim extension = System.IO.Path.GetExtension(filePath).ToLowerInvariant()
                Select Case extension
                    Case ".json"
                        ' Parse as JSON
                        entries = ParseJsonFeatures(content)
                    Case ".csv"
                        ' Parse as CSV
                        entries = ParseCsvFeaturesLocal(content)
                    Case ".txt"
                        ' Parse as legacy TXT format (mach2 style)
                        entries = ParseTxtFeatures(content)
                    Case Else
                        ' Try to auto-detect format
                        If content.TrimStart().StartsWith("[") OrElse content.TrimStart().StartsWith("{") Then
                            entries = ParseJsonFeatures(content)
                        ElseIf content.Contains("##") Then
                            entries = ParseTxtFeatures(content)
                        Else
                            entries = ParseCsvFeaturesLocal(content)
                        End If
                End Select

                If entries Is Nothing OrElse entries.Count = 0 Then
                    Dim warning = $"No features found in file: {filePath}"
                    _warnings.Add(warning)
                    _lastErrorMessage = warning
                    result.Add(New FeatureItem(0, "No features loaded", "Warning", False, warning))
                    Return result
                End If

                ' Convert entries to FeatureItems
                Dim featureDict As New Dictionary(Of UInteger, FeatureItem)()
                For Each entry In entries
                    Try
                        Dim notes = If(String.IsNullOrWhiteSpace(entry.Group), "From file", $"Group: {entry.Group}")
                        Dim featureItem As New FeatureItem(
                            entry.Id,
                            If(String.IsNullOrWhiteSpace(entry.Name), $"Feature {entry.Id}", entry.Name),
                            "Default",
                            False,
                            notes
                        )
                        featureDict(CUInt(entry.Id)) = featureItem
                    Catch ex As Exception
                        Dim warning = $"Warning: Skipped feature {entry.Id}: {ex.Message}"
                        _warnings.Add(warning)
                        System.Diagnostics.Debug.WriteLine(warning)
                    End Try
                Next

                ' Apply RTL overlay if requested
                If applyRtlOverlay Then
                    Call ApplyRtlOverlayToFeatures(featureDict)
                End If

                ' Add all features to result
                For Each kvp In featureDict.OrderBy(Function(x) x.Key)
                    result.Add(kvp.Value)
                Next

                If _warnings.Count > 0 Then
                    _lastErrorMessage = String.Join("; ", _warnings)
                Else
                    _lastErrorMessage = String.Empty
                End If

            Catch ex As Exception
                Dim warning = $"Error loading features from file: {ex.Message}"
                _warnings.Add(warning)
                _lastErrorMessage = warning
                result.Add(New FeatureItem(0, "No features loaded", "Error", False, warning))
            End Try

            Return result
        End Function

        ''' <summary>
        ''' Applies RTL overlay by querying RtlFeatureManager and updating feature states.
        ''' </summary>
        Private Sub ApplyRtlOverlayToFeatures(featureDict As Dictionary(Of UInteger, FeatureItem))
            Dim runtimeConfigs As IEnumerable(Of FeatureConfiguration) = Nothing
            Dim bootConfigs As IEnumerable(Of FeatureConfiguration) = Nothing

            ' Query runtime configurations with defensive handling
            Try
                runtimeConfigs = RtlFeatureManager.QueryAllFeatureConfigurations(FeatureConfigurationSection.Runtime)
            Catch ex As Exception
                Dim warning = $"RTL Runtime overlay unavailable: {ex.Message}"
                _warnings.Add(warning)
                System.Diagnostics.Debug.WriteLine(warning)
            End Try

            ' Query boot configurations with defensive handling
            Try
                bootConfigs = RtlFeatureManager.QueryAllFeatureConfigurations(FeatureConfigurationSection.Boot)
            Catch ex As Exception
                Dim warning = $"RTL Boot overlay unavailable: {ex.Message}"
                _warnings.Add(warning)
                System.Diagnostics.Debug.WriteLine(warning)
            End Try

            ' Process runtime configurations
            If runtimeConfigs IsNot Nothing Then
                For Each config In runtimeConfigs
                    Try
                        If featureDict.ContainsKey(config.FeatureId) Then
                            ' Update existing feature with RTL state
                            Dim feature = featureDict(config.FeatureId)
                            feature.State = config.EnabledState.ToString()
                            feature.IsEnabled = (config.EnabledState = FeatureEnabledState.Enabled)

                            ' Validate and clamp group value
                            Dim groupValue = config.Group
                            Dim groupWarning As String = Nothing
                            If groupValue > MaxGroupValue Then
                                groupWarning = $"Group {groupValue} clamped to {MaxGroupValue}"
                                groupValue = MaxGroupValue
                            End If

                            Dim notes = $"Group: {config.Group}, Variant: {config.Variant} (RTL)"
                            If groupWarning IsNot Nothing Then
                                notes = notes & $" ({groupWarning})"
                            End If
                            feature.Notes = notes
                        Else
                            ' RTL-only feature not in feed - optionally add it
                            Dim groupValue = config.Group
                            Dim groupWarning As String = Nothing
                            If groupValue > MaxGroupValue Then
                                groupWarning = $"Group {groupValue} clamped to {MaxGroupValue}"
                                groupValue = MaxGroupValue
                            End If

                            Dim stateStr = config.EnabledState.ToString()
                            Dim isEnabled = (config.EnabledState = FeatureEnabledState.Enabled)
                            Dim notes = $"Group: {config.Group}, Variant: {config.Variant} (RTL-only, unlisted)"
                            If groupWarning IsNot Nothing Then
                                notes = notes & $" ({groupWarning})"
                            End If

                            Dim featureItem As New FeatureItem(
                                CInt(config.FeatureId),
                                $"Feature {config.FeatureId}",
                                stateStr,
                                isEnabled,
                                notes
                            )
                            featureDict(config.FeatureId) = featureItem
                        End If
                    Catch ex As Exception
                        Dim warning = $"Warning: RTL overlay failed for feature {config.FeatureId}: {ex.Message}"
                        _warnings.Add(warning)
                        System.Diagnostics.Debug.WriteLine(warning)
                    End Try
                Next
            End If

            ' Process boot configurations
            If bootConfigs IsNot Nothing Then
                For Each config In bootConfigs
                    Try
                        If Not featureDict.ContainsKey(config.FeatureId) Then
                            ' Boot-only feature not in feed or runtime
                            Dim groupValue = config.Group
                            Dim groupWarning As String = Nothing
                            If groupValue > MaxGroupValue Then
                                groupWarning = $"Group {groupValue} clamped to {MaxGroupValue}"
                                groupValue = MaxGroupValue
                            End If

                            Dim stateStr = config.EnabledState.ToString()
                            Dim isEnabled = (config.EnabledState = FeatureEnabledState.Enabled)
                            Dim notes = $"Group: {config.Group}, Variant: {config.Variant} (Boot, unlisted)"
                            If groupWarning IsNot Nothing Then
                                notes = notes & $" ({groupWarning})"
                            End If

                            Dim featureItem As New FeatureItem(
                                CInt(config.FeatureId),
                                $"Feature {config.FeatureId}",
                                stateStr,
                                isEnabled,
                                notes
                            )
                            featureDict(config.FeatureId) = featureItem
                        End If
                    Catch ex As Exception
                        Dim warning = $"Warning: RTL boot overlay failed for feature {config.FeatureId}: {ex.Message}"
                        _warnings.Add(warning)
                        System.Diagnostics.Debug.WriteLine(warning)
                    End Try
                Next
            End If
        End Sub

        ''' <summary>
        ''' Parses JSON format feature list.
        ''' </summary>
        Private Function ParseJsonFeatures(content As String) As List(Of FeatureEntry)
            Try
                Return System.Text.Json.JsonSerializer.Deserialize(Of List(Of FeatureEntry))(content)
            Catch ex As Exception
                _warnings.Add($"JSON parse error: {ex.Message}")
                Return New List(Of FeatureEntry)()
            End Try
        End Function

        ''' <summary>
        ''' Parses CSV format feature list (local file version).
        ''' </summary>
        Private Function ParseCsvFeaturesLocal(content As String) As List(Of FeatureEntry)
            Dim entries = New List(Of FeatureEntry)()
            Dim lines = content.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)

            Dim nameIndex = 0
            Dim idIndex = 1
            Dim groupIndex = -1
            Dim startLine = 0

            If lines.Length > 0 Then
                Dim firstLine = lines(0).ToLowerInvariant()
                If firstLine.Contains("name") OrElse firstLine.Contains("id") Then
                    Dim headers = lines(0).Split(","c)
                    For i = 0 To headers.Length - 1
                        Dim h = headers(i).Trim().ToLowerInvariant()
                        Select Case h
                            Case "name"
                                nameIndex = i
                            Case "id"
                                idIndex = i
                            Case "group"
                                groupIndex = i
                        End Select
                    Next
                    startLine = 1
                End If
            End If

            For i = startLine To lines.Length - 1
                Dim line = lines(i)
                If String.IsNullOrWhiteSpace(line) OrElse line.StartsWith("#") Then
                    Continue For
                End If

                Dim parts = line.Split(","c)
                If parts.Length > Math.Max(nameIndex, idIndex) Then
                    Try
                        Dim name = parts(nameIndex).Trim()
                        Dim id = Integer.Parse(parts(idIndex).Trim())
                        Dim group = If(groupIndex >= 0 AndAlso parts.Length > groupIndex, parts(groupIndex).Trim(), String.Empty)
                        entries.Add(New FeatureEntry(name, id, group))
                    Catch ex As FormatException
                        _warnings.Add($"Skipping malformed CSV line: {ex.Message}")
                    End Try
                End If
            Next

            Return entries
        End Function

        ''' <summary>
        ''' Parses TXT format feature list (mach2/legacy style).
        ''' </summary>
        Private Function ParseTxtFeatures(content As String) As List(Of FeatureEntry)
            Dim entries = New List(Of FeatureEntry)()
            Dim currentGroup = String.Empty
            Dim lines = content.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.None)

            For Each line In lines
                ' Check for group headers
                If line.StartsWith("## Unknown:") Then
                    currentGroup = "Modifiable"
                ElseIf line.StartsWith("## Always Enabled:") Then
                    currentGroup = "Always Enabled"
                ElseIf line.StartsWith("## Enabled By Default:") Then
                    currentGroup = "Enabled By Default"
                ElseIf line.StartsWith("## Disabled By Default:") Then
                    currentGroup = "Disabled by Default"
                ElseIf line.StartsWith("## Always Disabled:") Then
                    currentGroup = "Always Disabled"
                ElseIf Not String.IsNullOrWhiteSpace(line) AndAlso Not line.StartsWith("#") Then
                    ' Parse feature line
                    Dim parts = line.Split(":"c)
                    If parts.Length >= 2 Then
                        Dim name = parts(0).Trim()
                        Dim idStr = parts(1).Trim()
                        Dim id As Integer
                        If Integer.TryParse(idStr, id) Then
                            entries.Add(New FeatureEntry(name, id, currentGroup))
                        End If
                    End If
                End If
            Next

            Return entries
        End Function

        ''' <summary>
        ''' Queries a single feature's current configuration.
        ''' </summary>
        ''' <param name="featureId">The feature ID to query.</param>
        ''' <returns>A FeatureItem if found, Nothing otherwise.</returns>
        Public Async Function GetFeatureAsync(featureId As Integer) As Task(Of FeatureItem)
            Return Await Task.Run(Function() GetFeatureCore(CUInt(featureId)))
        End Function

        ''' <summary>
        ''' Core method to query a single feature configuration.
        ''' </summary>
        Private Function GetFeatureCore(featureId As UInteger) As FeatureItem
            Try
                Dim config = RtlFeatureManager.QueryFeatureConfiguration(featureId, FeatureConfigurationSection.Runtime)
                
                ' Check for null explicitly instead of catching NullReferenceException
                If config Is Nothing Then
                    ' Feature not configured, return default state
                    Return New FeatureItem(
                        CInt(featureId),
                        $"Feature {featureId}",
                        "Default",
                        False,
                        "Not configured"
                    )
                End If

                Dim stateStr As String = config.EnabledState.ToString()
                Dim isEnabled As Boolean = (config.EnabledState = FeatureEnabledState.Enabled)
                Return New FeatureItem(
                    CInt(config.FeatureId),
                    $"Feature {config.FeatureId}",
                    stateStr,
                    isEnabled,
                    $"Group: {config.Group}, Variant: {config.Variant}"
                )
            Catch ex As Exception
                _lastErrorMessage = ex.Message
            End Try

            Return Nothing
        End Function
    End Class
End Namespace
