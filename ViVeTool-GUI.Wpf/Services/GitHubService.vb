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

Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text.Json
Imports ViVeTool_GUI.Wpf.Models

Namespace Services
    ''' <summary>
    ''' Service for fetching feature data from GitHub-hosted feed.
    ''' Uses latest.json and per-build features/ with ETag caching and offline fallback.
    ''' </summary>
    Public Class GitHubService
        Implements IDisposable

        Private ReadOnly _httpClient As HttpClient
        Private ReadOnly _cacheDirectory As String
        Private _disposed As Boolean = False

        ' GitHub-hosted feed URLs (configurable for different repo locations)
        Private Const DefaultFeedBaseUrl As String = "https://raw.githubusercontent.com/PeterStrick/ViVeTool-GUI/main/"
        Private Const LatestJsonPath As String = "latest.json"
        Private Const FeaturesDirectory As String = "features/"

        ' Legacy mach2 fallback URL
        Private Const LegacyFeatureListBaseUrl As String = "https://raw.githubusercontent.com/riverar/mach2/master/features/"

        ' Cache file names
        Private Const LatestCacheFile As String = "latest.json"
        Private Const ETagCacheFile As String = "etags.json"

        Private _feedBaseUrl As String

        ''' <summary>
        ''' ETag cache for conditional requests.
        ''' </summary>
        Private _etagCache As Dictionary(Of String, String)

        ''' <summary>
        ''' Creates a new instance of GitHubService with default settings.
        ''' </summary>
        Public Sub New()
            Me.New(Nothing, Nothing)
        End Sub

        ''' <summary>
        ''' Creates a new instance of GitHubService with a custom HttpClient.
        ''' </summary>
        ''' <param name="httpClient">The HttpClient to use for requests.</param>
        Public Sub New(httpClient As HttpClient)
            Me.New(httpClient, Nothing)
        End Sub

        ''' <summary>
        ''' Creates a new instance of GitHubService with custom HttpClient and feed base URL.
        ''' </summary>
        ''' <param name="httpClient">The HttpClient to use for requests (or Nothing for default).</param>
        ''' <param name="feedBaseUrl">The base URL for the feed (or Nothing for default).</param>
        Public Sub New(httpClient As HttpClient, feedBaseUrl As String)
            If httpClient Is Nothing Then
                _httpClient = New HttpClient()
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "ViVeTool-GUI-WPF")
            Else
                _httpClient = httpClient
            End If

            _feedBaseUrl = If(String.IsNullOrWhiteSpace(feedBaseUrl), DefaultFeedBaseUrl, feedBaseUrl)
            _cacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ViVeTool-GUI", "Cache")
            _etagCache = New Dictionary(Of String, String)()

            ' Ensure cache directory exists
            If Not Directory.Exists(_cacheDirectory) Then
                Directory.CreateDirectory(_cacheDirectory)
            End If

            ' Load ETag cache
            LoadETagCache()
        End Sub

        ''' <summary>
        ''' Gets available Windows build numbers from the GitHub-hosted feed.
        ''' Uses ETag caching and falls back to cached data if fetch fails.
        ''' </summary>
        ''' <returns>A list of build numbers.</returns>
        Public Async Function GetAvailableBuildsAsync() As Task(Of List(Of String))
            Dim feedInfo = Await GetLatestFeedInfoAsync()
            If feedInfo IsNot Nothing AndAlso feedInfo.Builds IsNot Nothing Then
                Return feedInfo.Builds
            End If

            ' Return empty list if feed is unavailable
            Return New List(Of String)()
        End Function

        ''' <summary>
        ''' Gets the latest feed info from latest.json with ETag caching and offline fallback.
        ''' </summary>
        ''' <returns>The LatestFeedInfo or Nothing if unavailable.</returns>
        Public Async Function GetLatestFeedInfoAsync() As Task(Of LatestFeedInfo)
            Dim url = _feedBaseUrl & LatestJsonPath
            Dim cacheFile = Path.Combine(_cacheDirectory, LatestCacheFile)

            Try
                ' Try to fetch with ETag conditional request
                Dim content = Await FetchWithETagAsync(url, cacheFile)
                If Not String.IsNullOrWhiteSpace(content) Then
                    Dim feedInfo = JsonSerializer.Deserialize(Of LatestFeedInfo)(content)
                    Return feedInfo
                End If
            Catch ex As HttpRequestException
                ' Network error - try offline fallback
                System.Diagnostics.Debug.WriteLine($"Network error fetching latest.json: {ex.Message}")
            Catch ex As JsonException
                ' JSON parsing error
                System.Diagnostics.Debug.WriteLine($"JSON parsing error for latest.json: {ex.Message}")
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Error fetching latest.json: {ex.Message}")
            End Try

            ' Offline fallback: try to load from cache
            Return LoadCachedLatestFeedInfo()
        End Function

        ''' <summary>
        ''' Gets feature entries for a specific Windows build.
        ''' Supports both CSV and JSON formats with graceful handling of missing columns.
        ''' </summary>
        ''' <param name="buildNumber">The Windows build number.</param>
        ''' <returns>A list of feature entries.</returns>
        Public Async Function GetFeatureEntriesForBuildAsync(buildNumber As String) As Task(Of List(Of FeatureEntry))
            Dim entries = New List(Of FeatureEntry)()

            ' Try JSON format first (preferred)
            Try
                Dim jsonUrl = _feedBaseUrl & FeaturesDirectory & buildNumber & "/features.json"
                Dim jsonCacheFile = Path.Combine(_cacheDirectory, $"{buildNumber}_features.json")
                Dim jsonContent = Await FetchWithETagAsync(jsonUrl, jsonCacheFile)

                If Not String.IsNullOrWhiteSpace(jsonContent) Then
                    Dim parsed = JsonSerializer.Deserialize(Of List(Of FeatureEntry))(jsonContent)
                    If parsed IsNot Nothing Then
                        Return parsed
                    End If
                End If
            Catch ex As Exception
                ' JSON not available, try CSV
                System.Diagnostics.Debug.WriteLine($"JSON format not available for build {buildNumber}: {ex.Message}")
            End Try

            ' Try CSV format
            Try
                Dim csvUrl = _feedBaseUrl & FeaturesDirectory & buildNumber & "/features.csv"
                Dim csvCacheFile = Path.Combine(_cacheDirectory, $"{buildNumber}_features.csv")
                Dim csvContent = Await FetchWithETagAsync(csvUrl, csvCacheFile)

                If Not String.IsNullOrWhiteSpace(csvContent) Then
                    entries = ParseCsvFeatures(csvContent)
                    If entries.Count > 0 Then
                        Return entries
                    End If
                End If
            Catch ex As Exception
                ' CSV not available, try legacy format
                System.Diagnostics.Debug.WriteLine($"CSV format not available for build {buildNumber}: {ex.Message}")
            End Try

            ' Fall back to legacy mach2 TXT format for backwards compatibility
            entries = Await GetFeaturesFromLegacyFormatAsync(buildNumber)
            Return entries
        End Function

        ''' <summary>
        ''' Gets feature data for a specific Windows build.
        ''' </summary>
        ''' <param name="buildNumber">The Windows build number.</param>
        ''' <returns>A dictionary of feature IDs to feature names.</returns>
        Public Async Function GetFeaturesForBuildAsync(buildNumber As String) As Task(Of Dictionary(Of Integer, String))
            Dim entries = Await GetFeatureEntriesForBuildAsync(buildNumber)
            Dim result = New Dictionary(Of Integer, String)()

            For Each entry In entries
                If Not result.ContainsKey(entry.Id) Then
                    result.Add(entry.Id, entry.Name)
                End If
            Next

            Return result
        End Function

        ''' <summary>
        ''' Downloads the raw feature list content for a build.
        ''' </summary>
        ''' <param name="buildNumber">The Windows build number.</param>
        ''' <returns>The raw content of the feature list file.</returns>
        Public Async Function DownloadFeatureListAsync(buildNumber As String) As Task(Of String)
            ' Try new feed format first
            Dim csvUrl = _feedBaseUrl & FeaturesDirectory & buildNumber & "/features.csv"
            Dim csvCacheFile = Path.Combine(_cacheDirectory, $"{buildNumber}_features.csv")

            Try
                Dim content = Await FetchWithETagAsync(csvUrl, csvCacheFile)
                If Not String.IsNullOrWhiteSpace(content) Then
                    Return content
                End If
            Catch
                ' Fall back to legacy format
            End Try

            ' Fall back to legacy mach2 format
            Dim legacyUrl = LegacyFeatureListBaseUrl & buildNumber & ".txt"
            Dim legacyCacheFile = Path.Combine(_cacheDirectory, $"{buildNumber}_legacy.txt")

            Try
                Return Await FetchWithETagAsync(legacyUrl, legacyCacheFile)
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Error downloading feature list: {ex.Message}")
                Return String.Empty
            End Try
        End Function

        ''' <summary>
        ''' Checks if a new version of ViVeTool-GUI is available.
        ''' </summary>
        ''' <returns>The latest version string if available, or Nothing if current is latest.</returns>
        Public Async Function CheckForUpdatesAsync() As Task(Of String)
            Try
                Dim response = Await _httpClient.GetStringAsync("https://api.github.com/repos/PeterStrick/ViVeTool-GUI/releases/latest")
                Dim doc = JsonDocument.Parse(response)
                Dim tagName = doc.RootElement.GetProperty("tag_name").GetString()
                Return tagName
            Catch ex As Exception
                ' Ignore errors during update check
                System.Diagnostics.Debug.WriteLine($"Update check failed: {ex.Message}")
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Fetches content from a URL with ETag caching support.
        ''' Returns cached content if the resource hasn't changed (304 Not Modified).
        ''' </summary>
        ''' <param name="url">The URL to fetch.</param>
        ''' <param name="cacheFile">The local cache file path.</param>
        ''' <returns>The content string, either fresh or cached.</returns>
        Private Async Function FetchWithETagAsync(url As String, cacheFile As String) As Task(Of String)
            Dim request = New HttpRequestMessage(HttpMethod.Get, url)

            ' Add If-None-Match header if we have a cached ETag
            If _etagCache.ContainsKey(url) Then
                request.Headers.TryAddWithoutValidation("If-None-Match", _etagCache(url))
            End If

            Try
                Dim response = Await _httpClient.SendAsync(request)

                If response.StatusCode = HttpStatusCode.NotModified Then
                    ' Resource hasn't changed, use cached version
                    If File.Exists(cacheFile) Then
                        Return File.ReadAllText(cacheFile)
                    End If
                End If

                response.EnsureSuccessStatusCode()

                Dim content = Await response.Content.ReadAsStringAsync()

                ' Cache the content and ETag
                File.WriteAllText(cacheFile, content)

                If response.Headers.ETag IsNot Nothing Then
                    _etagCache(url) = response.Headers.ETag.Tag
                    SaveETagCache()
                End If

                Return content
            Catch ex As HttpRequestException
                ' Network error - try to return cached content
                If File.Exists(cacheFile) Then
                    System.Diagnostics.Debug.WriteLine($"Using cached content for {url} due to network error")
                    Return File.ReadAllText(cacheFile)
                End If
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Parses CSV format feature list with graceful handling of missing columns.
        ''' </summary>
        ''' <param name="csvContent">The CSV content to parse.</param>
        ''' <returns>A list of feature entries.</returns>
        Private Function ParseCsvFeatures(csvContent As String) As List(Of FeatureEntry)
            Dim entries = New List(Of FeatureEntry)()
            Dim lines = csvContent.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)

            ' Determine column indices from header (if present)
            Dim nameIndex = 0
            Dim idIndex = 1
            Dim groupIndex = -1
            Dim startLine = 0

            If lines.Length > 0 Then
                Dim firstLine = lines(0).ToLowerInvariant()
                If firstLine.Contains("name") OrElse firstLine.Contains("id") Then
                    ' Header row detected
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
                        ' Skip malformed lines
                        System.Diagnostics.Debug.WriteLine($"Skipping malformed CSV line: {ex.Message}")
                    End Try
                End If
            Next

            Return entries
        End Function

        ''' <summary>
        ''' Gets features from the legacy mach2 TXT format.
        ''' Maps the old format to the new FeatureEntry schema.
        ''' </summary>
        ''' <param name="buildNumber">The Windows build number.</param>
        ''' <returns>A list of feature entries.</returns>
        Private Async Function GetFeaturesFromLegacyFormatAsync(buildNumber As String) As Task(Of List(Of FeatureEntry))
            Dim entries = New List(Of FeatureEntry)()

            Try
                Dim url = LegacyFeatureListBaseUrl & buildNumber & ".txt"
                Dim cacheFile = Path.Combine(_cacheDirectory, $"{buildNumber}_legacy.txt")
                Dim content = Await FetchWithETagAsync(url, cacheFile)

                If String.IsNullOrWhiteSpace(content) Then
                    Return entries
                End If

                Dim currentGroup = String.Empty
                Dim lines = content.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.None)

                For Each line In lines
                    ' Check for group headers (mach2 format)
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
                        ' Parse feature line (format: "FeatureName: 12345678")
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
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Error fetching legacy features: {ex.Message}")
            End Try

            Return entries
        End Function

        ''' <summary>
        ''' Loads the cached LatestFeedInfo from disk.
        ''' </summary>
        ''' <returns>The cached LatestFeedInfo or Nothing if not available.</returns>
        Private Function LoadCachedLatestFeedInfo() As LatestFeedInfo
            Dim cacheFile = Path.Combine(_cacheDirectory, LatestCacheFile)
            If File.Exists(cacheFile) Then
                Try
                    Dim content = File.ReadAllText(cacheFile)
                    Return JsonSerializer.Deserialize(Of LatestFeedInfo)(content)
                Catch ex As Exception
                    ' Cache file corrupted, return Nothing
                    System.Diagnostics.Debug.WriteLine($"Failed to load cached latest feed info: {ex.Message}")
                End Try
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Loads the ETag cache from disk.
        ''' </summary>
        Private Sub LoadETagCache()
            Dim cacheFile = Path.Combine(_cacheDirectory, ETagCacheFile)
            If File.Exists(cacheFile) Then
                Try
                    Dim content = File.ReadAllText(cacheFile)
                    _etagCache = JsonSerializer.Deserialize(Of Dictionary(Of String, String))(content)
                    If _etagCache Is Nothing Then
                        _etagCache = New Dictionary(Of String, String)()
                    End If
                Catch ex As Exception
                    ' ETag cache corrupted, reinitialize
                    System.Diagnostics.Debug.WriteLine($"Failed to load ETag cache: {ex.Message}")
                    _etagCache = New Dictionary(Of String, String)()
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Saves the ETag cache to disk.
        ''' </summary>
        Private Sub SaveETagCache()
            Dim cacheFile = Path.Combine(_cacheDirectory, ETagCacheFile)
            Try
                Dim content = JsonSerializer.Serialize(_etagCache)
                File.WriteAllText(cacheFile, content)
            Catch ex As Exception
                ' Ignore cache save errors
                System.Diagnostics.Debug.WriteLine($"Failed to save ETag cache: {ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' Disposes the HttpClient resources.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Disposes resources.
        ''' </summary>
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    _httpClient?.Dispose()
                End If
                _disposed = True
            End If
        End Sub
    End Class
End Namespace
