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

Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports ViVeTool_GUI.Wpf.Models

Namespace Services
    ''' <summary>
    ''' Service for publishing feature lists via GitHub Actions workflow_dispatch.
    ''' </summary>
    Public Class PublishService
        Implements IDisposable

        Private ReadOnly _httpClient As HttpClient
        Private _disposed As Boolean = False

        ' GitHub API configuration
        Private Const GitHubApiBaseUrl As String = "https://api.github.com"
        Private Const DefaultOwner As String = "mta1124-1629472"
        Private Const DefaultRepo As String = "ViVeTool-GUI"
        Private Const WorkflowFileName As String = "publish-feature-list.yml"

        Private ReadOnly _owner As String
        Private ReadOnly _repo As String

        ''' <summary>
        ''' Creates a new instance of PublishService with default settings.
        ''' </summary>
        Public Sub New()
            Me.New(Nothing, Nothing, Nothing)
        End Sub

        ''' <summary>
        ''' Creates a new instance of PublishService with custom settings.
        ''' </summary>
        ''' <param name="httpClient">The HttpClient to use for requests (or Nothing for default).</param>
        ''' <param name="owner">The repository owner (or Nothing for default).</param>
        ''' <param name="repo">The repository name (or Nothing for default).</param>
        Public Sub New(httpClient As HttpClient, owner As String, repo As String)
            If httpClient Is Nothing Then
                _httpClient = New HttpClient()
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "ViVeTool-GUI-WPF")
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json")
                _httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28")
            Else
                _httpClient = httpClient
            End If

            _owner = If(String.IsNullOrWhiteSpace(owner), DefaultOwner, owner)
            _repo = If(String.IsNullOrWhiteSpace(repo), DefaultRepo, repo)
        End Sub

        ''' <summary>
        ''' Dispatches the publish-feature-list workflow via GitHub Actions.
        ''' </summary>
        ''' <param name="build">The Windows build number.</param>
        ''' <param name="artifactPath">The local artifact path (optional, use either this or artifactUrl).</param>
        ''' <param name="artifactUrl">The artifact URL (optional, use either this or artifactPath).</param>
        ''' <param name="format">The output format (csv or json).</param>
        ''' <param name="token">The GitHub personal access token with workflow permissions.</param>
        ''' <returns>A PublishResult indicating success or failure.</returns>
        Public Async Function DispatchPublishWorkflowAsync(build As String, artifactPath As String, artifactUrl As String, format As String, token As String) As Task(Of PublishResult)
            If String.IsNullOrWhiteSpace(build) Then
                Return PublishResult.CreateFailure("Build number is required.")
            End If

            If String.IsNullOrWhiteSpace(artifactPath) AndAlso String.IsNullOrWhiteSpace(artifactUrl) Then
                Return PublishResult.CreateFailure("Either artifact path or artifact URL is required.")
            End If

            If String.IsNullOrWhiteSpace(format) Then
                format = "csv"
            End If

            If String.IsNullOrWhiteSpace(token) Then
                Return PublishResult.CreateFailure("GitHub token is required to dispatch the workflow.")
            End If

            ' Build the API URL for workflow dispatch
            Dim apiUrl = $"{GitHubApiBaseUrl}/repos/{_owner}/{_repo}/actions/workflows/{WorkflowFileName}/dispatches"

            ' Build the request payload
            Dim inputs = New Dictionary(Of String, String) From {
                {"build", build},
                {"format", format}
            }

            If Not String.IsNullOrWhiteSpace(artifactUrl) Then
                inputs.Add("artifact_url", artifactUrl)
            End If

            If Not String.IsNullOrWhiteSpace(artifactPath) Then
                inputs.Add("artifact_path", artifactPath)
            End If

            Dim payload = New Dictionary(Of String, Object) From {
                {"ref", "main"},
                {"inputs", inputs}
            }

            Dim jsonPayload = JsonSerializer.Serialize(payload)
            Dim content = New StringContent(jsonPayload, Encoding.UTF8, "application/json")

            Try
                ' Create request with authorization header
                Dim request = New HttpRequestMessage(HttpMethod.Post, apiUrl)
                request.Content = content
                request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}")

                Dim response = Await _httpClient.SendAsync(request)

                If response.StatusCode = HttpStatusCode.NoContent OrElse response.StatusCode = HttpStatusCode.Accepted Then
                    ' Success - workflow dispatch returns 204 No Content on success
                    Return PublishResult.CreateSuccess()
                ElseIf response.StatusCode = HttpStatusCode.Forbidden Then
                    ' 403 Forbidden - maintainer-only access
                    Return PublishResult.CreateFailure("Access denied. Publishing feature lists is restricted to repository maintainers only.", True)
                ElseIf response.StatusCode = HttpStatusCode.Unauthorized Then
                    Return PublishResult.CreateFailure("Invalid or expired GitHub token. Please check your token and try again.")
                ElseIf response.StatusCode = HttpStatusCode.NotFound Then
                    Return PublishResult.CreateFailure("Workflow not found. Please ensure the publish-feature-list.yml workflow exists in the repository.")
                Else
                    Dim errorContent = Await response.Content.ReadAsStringAsync()
                    Return PublishResult.CreateFailure($"Failed to dispatch workflow. Status: {response.StatusCode}. {errorContent}")
                End If
            Catch ex As HttpRequestException
                Return PublishResult.CreateFailure($"Network error: {ex.Message}")
            Catch ex As TaskCanceledException
                Return PublishResult.CreateFailure("Request timed out. Please check your network connection and try again.")
            Catch ex As Exception
                Return PublishResult.CreateFailure($"Unexpected error: {ex.Message}")
            End Try
        End Function

        ''' <summary>
        ''' Validates that the current user has permission to dispatch workflows.
        ''' </summary>
        ''' <param name="token">The GitHub personal access token.</param>
        ''' <returns>True if the user can dispatch workflows, false otherwise.</returns>
        Public Async Function CanDispatchWorkflowAsync(token As String) As Task(Of Boolean)
            If String.IsNullOrWhiteSpace(token) Then
                Return False
            End If

            Try
                ' Check repository permissions
                Dim apiUrl = $"{GitHubApiBaseUrl}/repos/{_owner}/{_repo}"

                Dim request = New HttpRequestMessage(HttpMethod.Get, apiUrl)
                request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}")

                Dim response = Await _httpClient.SendAsync(request)

                If response.IsSuccessStatusCode Then
                    Dim content = Await response.Content.ReadAsStringAsync()
                    Dim doc = JsonDocument.Parse(content)

                    ' Check if permissions object exists and has push or admin access
                    Dim permissionsElement As JsonElement = Nothing
                    If doc.RootElement.TryGetProperty("permissions", permissionsElement) Then
                        Dim pushElement As JsonElement = Nothing
                        Dim adminElement As JsonElement = Nothing
                        Dim hasPush = permissionsElement.TryGetProperty("push", pushElement) AndAlso pushElement.GetBoolean()
                        Dim hasAdmin = permissionsElement.TryGetProperty("admin", adminElement) AndAlso adminElement.GetBoolean()
                        Return hasPush OrElse hasAdmin
                    End If
                End If

                Return False
            Catch
                Return False
            End Try
        End Function

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
