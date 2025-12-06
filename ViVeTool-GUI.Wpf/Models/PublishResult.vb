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

Namespace Models
    ''' <summary>
    ''' Represents the status of a feature list publish operation.
    ''' </summary>
    Public Enum PublishStatus
        ''' <summary>
        ''' No publish operation is in progress.
        ''' </summary>
        Idle

        ''' <summary>
        ''' A publish operation is currently in progress.
        ''' </summary>
        Publishing

        ''' <summary>
        ''' The publish operation completed successfully.
        ''' </summary>
        Success

        ''' <summary>
        ''' The publish operation failed.
        ''' </summary>
        Failure
    End Enum

    ''' <summary>
    ''' Represents the result of a feature list publish operation.
    ''' </summary>
    Public Class PublishResult
        ''' <summary>
        ''' Gets or sets the status of the publish operation.
        ''' </summary>
        Public Property Status As PublishStatus

        ''' <summary>
        ''' Gets or sets the error message if the operation failed.
        ''' </summary>
        Public Property ErrorMessage As String

        ''' <summary>
        ''' Gets or sets whether the error is a 403 Forbidden (maintainer-only access).
        ''' </summary>
        Public Property IsMaintainerOnly As Boolean

        ''' <summary>
        ''' Gets or sets the workflow run ID if the dispatch was successful.
        ''' </summary>
        Public Property WorkflowRunId As Long

        ''' <summary>
        ''' Creates a new PublishResult with Idle status.
        ''' </summary>
        Public Sub New()
            Status = PublishStatus.Idle
            ErrorMessage = String.Empty
        End Sub

        ''' <summary>
        ''' Creates a successful PublishResult.
        ''' </summary>
        Public Shared Function CreateSuccess(Optional workflowRunId As Long = 0) As PublishResult
            Return New PublishResult With {
                .Status = PublishStatus.Success,
                .WorkflowRunId = workflowRunId
            }
        End Function

        ''' <summary>
        ''' Creates a failed PublishResult.
        ''' </summary>
        Public Shared Function CreateFailure(errorMessage As String, Optional isMaintainerOnly As Boolean = False) As PublishResult
            Return New PublishResult With {
                .Status = PublishStatus.Failure,
                .ErrorMessage = errorMessage,
                .IsMaintainerOnly = isMaintainerOnly
            }
        End Function

        ''' <summary>
        ''' Creates a PublishResult with Publishing status.
        ''' </summary>
        Public Shared Function CreatePublishing() As PublishResult
            Return New PublishResult With {
                .Status = PublishStatus.Publishing
            }
        End Function
    End Class
End Namespace
