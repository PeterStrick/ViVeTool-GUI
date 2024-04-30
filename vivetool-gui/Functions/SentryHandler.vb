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
Imports Sentry, Sentry.Protocol, Sentry.Infrastructure, System.Diagnostics
''' <summary>
''' A Handler class for Sentry.NET, handling UI and sending to Sentry
''' </summary>
Partial Public Class SentryHandler
    Private Shared _sentry As IDisposable
    Private Shared _ex As Exception

    ''' <summary>
    ''' Initializes the Sentry SDK
    ''' </summary>
    Public Shared Sub Init()
        Dim Options As New SentryOptions With {
            .Dsn = _DSN,
            .AutoSessionTracking = True,
            .IsGlobalModeEnabled = True,
            .EnableTracing = True,
            .TracesSampleRate = 0.1,
            .Environment = "production"
        }

        ' Enable Debug Logs if a Debugger is attached
#If DEBUG Then
        If Diagnostics.Debugger.IsAttached Then
            Options.Debug = True
            Options.DiagnosticLogger = New FileDiagnosticLogger(Application.StartupPath & "\sentry.log")
            Options.Environment = "debug"
        End If
#End If

        _sentry = SentrySdk.Init(Options)
    End Sub

    ''' <summary>
    ''' Dispose the Sentry SDK to ensure events are flushed and sent to Sentry
    ''' </summary>
    Public Shared Sub Dispose()
        _sentry.Dispose()
    End Sub

    ''' <summary>
    ''' Shows a Dialog asking if the User wants to send the Exception to Sentry or not
    ''' </summary>
    ''' <param name="ex">Unhandled Exception to Report and Display</param>
    Public Shared Sub ShowSentry(ex As Exception)
        ' Store the Exception in _ex for use in SendSentry()
        _ex = ex

        ' Command Link Buttons
        Dim RTDCLB_SendAndRestart As New RadTaskDialogCommandLinkButton With {
            .Text = My.Resources.Error_Sentry_SendAndReportBtn_Text,
            .AllowCloseDialog = False
        }

        Dim RTDCLB_Restart As New RadTaskDialogCommandLinkButton With {
            .Text = My.Resources.Error_Sentry_RestartWithoutReportingBtn_Text,
            .AllowCloseDialog = False
        }

        AddHandler RTDCLB_SendAndRestart.Click, AddressOf SendSentry
        AddHandler RTDCLB_Restart.Click, AddressOf Restart

        ' Rad TaskDialog Sentry Page
        Dim RTD_Sentry As New RadTaskDialogPage With {
            .Caption = $" {My.Resources.Error_AnUnknownExceptionOccurred}",
            .Heading = My.Resources.Error_AnUnknownExceptionOccurred,
            .Text = My.Resources.Error_Sentry_Text,
            .Icon = RadTaskDialogIcon.ShieldErrorRedBar,
            .ContentAreaButtons = New RadItemOwnerGenericCollection(Of RadTaskDialogCommandLinkButton) From {
                RTDCLB_SendAndRestart, RTDCLB_Restart
            }
        }

        ' Add Exception Message to Expander
        RTD_Sentry.Expander.Text = ex.Message
        RTD_Sentry.Expander.ExpandedButtonText = My.Resources.Error_CollapseException
        RTD_Sentry.Expander.CollapsedButtonText = My.Resources.Error_ShowException
        RTD_Sentry.Expander.Position = RadTaskDialogExpanderPosition.AfterFootnote

        ' Show Dialog
        RadTaskDialog.ShowDialog(RTD_Sentry)
    End Sub

    ''' <summary>
    ''' Sends the Exception to Sentry
    ''' </summary>
    Private Shared Sub SendSentry()
        ' Set some additional data on the exception for Sentry to recognize this exception as unhandled
        _ex.Data(Mechanism.HandledKey) = False
        _ex.Data(Mechanism.MechanismKey) = "WindowsFormsApplicationBase.UnhandledException"

        ' Configure Custom Scope, used to show the Values within My.Settings in Sentry
        SentrySdk.ConfigureScope(
            Sub(Scope)
                Scope.Contexts("settings") = New With {
                    .AutoLoad = My.Settings.AutoLoad,
                    .DarkMode = My.Settings.DarkMode,
                    .UseSystemTheme = My.Settings.UseSystemTheme,
                    .UserLanguage = My.Settings.TwoCharLanguageCode
                }
            End Sub)

        ' Capture the exception with Sentry
        SentrySdk.CaptureException(_ex)

        ' Flush and Dispose the Sentry SDK
        Dispose()

        ' Restart the Application
        Restart()
    End Sub

    ''' <summary>
    ''' Sub that starts a New Process with the current Executable Path, after which it exits the current process
    ''' </summary>
    Private Shared Sub Restart()
        Process.Start(Application.ExecutablePath)
        Application.Exit()
    End Sub
End Class
