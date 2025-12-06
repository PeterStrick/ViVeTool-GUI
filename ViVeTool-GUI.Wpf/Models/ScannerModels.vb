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
    ''' Represents the result of a feature scanner validation.
    ''' </summary>
    Public Class ScannerValidationResult
        ''' <summary>
        ''' Gets whether the validation passed.
        ''' </summary>
        Public ReadOnly Property IsValid As Boolean

        ''' <summary>
        ''' Gets the error message if validation failed.
        ''' </summary>
        Public ReadOnly Property ErrorMessage As String

        ''' <summary>
        ''' Creates a new ScannerValidationResult.
        ''' </summary>
        ''' <param name="isValid">Whether validation passed.</param>
        ''' <param name="errorMessage">Error message if failed.</param>
        Public Sub New(isValid As Boolean, errorMessage As String)
            Me.IsValid = isValid
            Me.ErrorMessage = If(errorMessage, String.Empty)
        End Sub
    End Class

    ''' <summary>
    ''' Represents the result of a feature scanner operation.
    ''' </summary>
    Public Class ScannerResult
        ''' <summary>
        ''' Gets or sets whether the operation was successful.
        ''' </summary>
        Public Property Success As Boolean

        ''' <summary>
        ''' Gets or sets whether the operation was cancelled.
        ''' </summary>
        Public Property IsCancelled As Boolean

        ''' <summary>
        ''' Gets or sets the path to the generated feature list file.
        ''' </summary>
        Public Property OutputFilePath As String

        ''' <summary>
        ''' Gets or sets the error message if the operation failed.
        ''' </summary>
        Public Property ErrorMessage As String

        ''' <summary>
        ''' Gets or sets the exit code of the process.
        ''' </summary>
        Public Property ExitCode As Integer

        ''' <summary>
        ''' Gets or sets the standard output from the process.
        ''' </summary>
        Public Property Output As String

        ''' <summary>
        ''' Creates a new ScannerResult.
        ''' </summary>
        Public Sub New()
            Success = False
            IsCancelled = False
            OutputFilePath = String.Empty
            ErrorMessage = String.Empty
            Output = String.Empty
        End Sub

        ''' <summary>
        ''' Creates a successful ScannerResult.
        ''' </summary>
        ''' <param name="outputFilePath">Path to the generated feature list file.</param>
        Public Shared Function CreateSuccess(outputFilePath As String) As ScannerResult
            Return New ScannerResult() With {
                .Success = True,
                .OutputFilePath = If(outputFilePath, String.Empty)
            }
        End Function

        ''' <summary>
        ''' Creates a failed ScannerResult.
        ''' </summary>
        ''' <param name="errorMessage">The error message.</param>
        Public Shared Function CreateFailure(errorMessage As String) As ScannerResult
            Return New ScannerResult() With {
                .Success = False,
                .ErrorMessage = If(errorMessage, String.Empty)
            }
        End Function

        ''' <summary>
        ''' Creates a cancelled ScannerResult.
        ''' </summary>
        Public Shared Function CreateCancelled() As ScannerResult
            Return New ScannerResult() With {
                .Success = False,
                .IsCancelled = True,
                .ErrorMessage = "Operation was cancelled."
            }
        End Function
    End Class

    ''' <summary>
    ''' Event arguments for scan progress updates.
    ''' </summary>
    Public Class ScanProgressEventArgs
        Inherits EventArgs

        ''' <summary>
        ''' Gets the progress percentage (0-100).
        ''' </summary>
        Public ReadOnly Property ProgressPercentage As Integer

        ''' <summary>
        ''' Gets the status message.
        ''' </summary>
        Public ReadOnly Property StatusMessage As String

        ''' <summary>
        ''' Creates new ScanProgressEventArgs.
        ''' </summary>
        ''' <param name="progressPercentage">Progress percentage (0-100).</param>
        ''' <param name="statusMessage">Status message.</param>
        Public Sub New(progressPercentage As Integer, statusMessage As String)
            Me.ProgressPercentage = Math.Max(0, Math.Min(100, progressPercentage))
            Me.StatusMessage = If(statusMessage, String.Empty)
        End Sub
    End Class

    ''' <summary>
    ''' Represents the current state of the scanner.
    ''' </summary>
    Public Enum ScannerState
        ''' <summary>
        ''' Scanner is idle, ready to start.
        ''' </summary>
        Idle

        ''' <summary>
        ''' Validating prerequisites.
        ''' </summary>
        Validating

        ''' <summary>
        ''' Downloading symbol files.
        ''' </summary>
        DownloadingSymbols

        ''' <summary>
        ''' Scanning symbol files.
        ''' </summary>
        Scanning

        ''' <summary>
        ''' Scan completed successfully.
        ''' </summary>
        Completed

        ''' <summary>
        ''' Scan failed with an error.
        ''' </summary>
        Failed

        ''' <summary>
        ''' Scan was cancelled.
        ''' </summary>
        Cancelled
    End Enum
End Namespace
