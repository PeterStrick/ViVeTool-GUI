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
Option Strict On
Imports System.IO, System.Security.Cryptography, System.Text, Newtonsoft.Json.Linq

' Class that contains all Functions to work with the ViVe API
Partial Class ViVe_API

    ''' <summary>
    ''' Class that contains all Functions to Manage and View ViVeTool Features
    ''' </summary>
    Public Class Management
        Public Shared CheckedForUpdates As Boolean = False

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

            ''' <summary>
            ''' Get's Names of a specified ViVeTool IDs using the Feature Dictionary
            ''' </summary>
            ''' <param name="featureIDs">A Dictionary of ViVeTool IDs</param>
            ''' <returns>A Dictionary of the provided ViVeTool IDs with their Feature Name</returns>
            Friend Shared Function FindNamesForFeatures(featureIDs As IEnumerable(Of UInteger)) As Dictionary(Of UInteger, String)
                Dim result = New Dictionary(Of UInteger, String)()
                If Not File.Exists(DictFilePath) Then Return Nothing
                Dim idsCommas = featureIDs.[Select](Function(x) "," & x.ToString()).ToList()

                Using reader As New StreamReader(File.OpenRead(DictFilePath))
                    ' Original: While Not reader.EndOfStream
                    While Not reader.EndOfStream AndAlso idsCommas.Count > 0
                        Dim currentLine = reader.ReadLine()

                        For Each ic In idsCommas
                            If currentLine.EndsWith(ic) Then
                                result(UInteger.Parse(ic.Substring(1))) = currentLine.Substring(0, currentLine.IndexOf(","c))
                                idsCommas.Remove(ic)
                                Exit For
                            End If
                        Next

                        'If idsCommas.Count = 0 Then Exit While
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
            ''' <summary>
            ''' Creates a GitHub Compatible SHA1 Hash of an UTF-8 Text File
            ''' </summary>
            ''' <param name="filePath">File Path to the UTF-8 Text File</param>
            ''' <returns>A GitHub compatible SHA1 Hash</returns>
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

                    ' vbNullChar is used here as the equivelent of C#'s \0
                    Dim preface = Encoding.UTF8.GetBytes($"blob {fullLength}{vbNullChar}")
                    sha1Csp.TransformBlock(preface, 0, preface.Length, Nothing, 0)
                    If fullLength > fileBody.Length Then sha1Csp.TransformBlock(preamble, 0, preamble.Length, Nothing, 0)
                    sha1Csp.TransformFinalBlock(fileBody, 0, fileBody.Length)
                    Return BitConverter.ToString(sha1Csp.Hash).Replace("-", "").ToLowerInvariant()
                End Using
            End Function
        End Class

        ''' <summary>
        ''' Checks if a Feature Dictionary Update is available
        ''' </summary>
        Public Shared Sub FeatureDictionaryUpdateCheck()
            ' Stop if an Update Check was already performed
            If CheckedForUpdates Then Return

            ' Stop if the Device is not connected to the Internet
            If Not GUI.HasInternetConnection Then Return

            ' Required Headers for the GitHub API
            Dim WebClientRepo As New WebClient With {.Encoding = Encoding.UTF8}
            WebClientRepo.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8")
            WebClientRepo.Headers.Add(HttpRequestHeader.UserAgent, "PeterStrick/vivetool-gui")

            Try
                ' Check if the Feature Dictionary exists
                If Not File.Exists(FeatureNaming.DictFilePath) Then
                    ' Create a Task Dialog and add the Two Buttons to it
                    Dim Dialog_Missing = RadTD.Generate($" {My.Resources.FeatureDictionary_MissingTitle}", My.Resources.FeatureDictionary_MissingTitle,
                                                        My.Resources.FeatureDictionary_MissingText, New RadTaskDialogIcon(My.Resources.icons8_dictionary_24),
                                                        CommandAreaButtons:=RadTD.Buttons.YesNo)

                    ' Show the Task Dialog and check for what Button was pressed
                    If RadTaskDialog.ShowDialog(Dialog_Missing) = RadTaskDialogButton.Yes Then
                        FeatureDictionaryDownload(Dialog_Missing, WebClientRepo)
                        Return
                    Else
                        ' User chose not to download
                        CheckedForUpdates = True
                        Return
                    End If
                End If

                ' Check for a Feature Dictionary Update, by getting ther SHA1 of the current Feature Dictionary on GitHub
                Dim LocalSHA = UpdateCheck.HashUTF8TextFile(FeatureNaming.DictFilePath)
                Dim RawJSON As String = WebClientRepo.DownloadString("https://api.github.com/repos/thebookisclosed/ViVe/contents/Extra")
                Dim JSONArray As JArray = JArray.Parse(RawJSON)
                Dim JSONObject As JObject = CType(JSONArray(0), JObject)

                If LocalSHA = JSONObject("sha").ToString Then
                    ' Feature Dictionary is current
                    CheckedForUpdates = True
                    Return
                End If

                ' Feature Dictionary is not current
                MsgBox($"Online: {JSONObject("sha")} {Environment.NewLine & Environment.NewLine}Local: {LocalSHA}")

                ' Create a Task Dialog and add the Two Buttons to it
                Dim Dialog_Update = RadTD.Generate($" {My.Resources.FeatureDictionary_UpdateTitle}", My.Resources.FeatureDictionary_UpdateTitle,
                                                   My.Resources.FeatureDictionary_UpdateText, New RadTaskDialogIcon(My.Resources.icons8_update_24),
                                                   CommandAreaButtons:=RadTD.Buttons.YesNo)

                ' Show the Task Dialog and check for what Button was pressed
                If RadTaskDialog.ShowDialog(Dialog_Update) = RadTaskDialogButton.Yes Then
                    FeatureDictionaryDownload(Dialog_Update, WebClientRepo)
                    Return
                Else
                    ' User chose not to update
                    CheckedForUpdates = True
                    Return
                End If
            Catch webex As WebException
                Dim webex_Response As String
                Try
                    webex_Response = My.Resources.Error_NetworkException_GithubAPI_Response & DirectCast(webex.Response, HttpWebResponse).StatusDescription
                Catch ex As Exception
                    webex_Response = webex.ToString
                End Try

                RadTD.ShowDialog($" {My.Resources.Error_ANetworkErrorOccurred}", My.Resources.Error_ANetworkErrorOccurred,
                My.Resources.FeatureDictionary_UpdateError, RadTaskDialogIcon.ShieldWarningYellowBar, webex, webex_Response, webex_Response)
            Catch ex As Exception
                RadTD.ShowDialog($" {My.Resources.Error_AnExceptionOccurred}", My.Resources.Error_AnUnknownExceptionOccurred,
                Nothing, RadTaskDialogIcon.ShieldErrorRedBar, ex, ex.ToString, ex.ToString)
            End Try
        End Sub

        ''' <summary>
        ''' Downloads a new Version of the Feature Dictionary
        ''' </summary>
        Shared Sub FeatureDictionaryDownload(TaskDialog As RadTaskDialogPage, WebClient As WebClient)
            ' Download the Feature Dictionary
            Try
                WebClient.DownloadFile("https://raw.githubusercontent.com/thebookisclosed/ViVe/master/Extra/FeatureDictionary.pfs", FeatureNaming.DictFilePath)

                ' Show a success Page once the download has completed
                RadTD.ShowDialog($" {My.Resources.FeatureDictionary_DownloadTitle}", My.Resources.FeatureDictionary_DownloadTitle, My.Resources.FeatureDictionary_DownloadText, RadTaskDialogIcon.ShieldSuccessGreenBar)
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

            ' Set the CheckedForUpdates Counter to True
            CheckedForUpdates = True
        End Sub
    End Class
End Class