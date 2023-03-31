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
Imports System.Net.Http

''' <summary>
''' HttpRequestException++, Extended HttpRequestException Exception Class that exposes the HTTP Status Code
''' 
''' In .Net 5 and above this is not needed because the StatusCode is exposed as a Property within HttpRequestException
''' </summary>
Public Class HttpRequestExceptionPP : Inherits HttpRequestException

    ''' <summary>
    ''' New Sub(). Initializes the Variables used for the Exception Class
    ''' </summary>
    ''' <param name="HttpStatusCode"></param>
    ''' <param name="HttpReasonPhrase"></param>
    Public Sub New(HttpStatusCode As HttpStatusCode, HttpReasonPhrase As String)
        StatusCode = HttpStatusCode
        ReasonPhrase = HttpReasonPhrase
    End Sub

    ''' <summary>
    ''' New StatusCode Property for the custom Exception Class
    ''' </summary>
    ''' <returns>HTTP Status Codes as an Integer</returns>
    Public Property StatusCode As HttpStatusCode

    ''' <summary>
    ''' New ReasonPhrase Property for the custom Exception Class
    ''' </summary>
    ''' <returns>HTTP Reason Phrase as an String</returns>
    Public Property ReasonPhrase As String
End Class