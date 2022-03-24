Imports System.Threading, CrashReporterDotNET

''' <summary>
''' CrashReporter.Net Helper Class
''' </summary>
Public Class CrashReporter

    ''' <summary>
    ''' Show CrashReporter.Net for unhandled exceptions.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="unhandledExceptionEventArgs">UnhandledException EventArgs</param>
    Public Shared Sub CurrentDomainOnUnhandledException(sender As Object, unhandledExceptionEventArgs As UnhandledExceptionEventArgs)
        SendReport(DirectCast(unhandledExceptionEventArgs.ExceptionObject, Exception))
        Environment.Exit(0)
    End Sub

    ''' <summary>
    ''' Show CrashReporter.Net for Application Thread Exceptions.
    ''' </summary>
    ''' <param name="sender">Default sender Object</param>
    ''' <param name="e">ThreadException EventArgs</param>
    Public Shared Sub ApplicationThreadException(sender As Object, e As ThreadExceptionEventArgs)
        SendReport(e.Exception)
    End Sub

    ''' <summary>
    ''' Sub that calls CrashReporter.Net to send an error report.
    ''' </summary>
    ''' <param name="exception">Exception to pass to CrashReporter.Net</param>
    Private Shared Sub SendReport(exception As Exception)
        Dim reportCrash = New ReportCrash(Nothing) With {
            .AnalyzeWithDoctorDump = True,
            .CaptureScreen = False,
            .DoctorDumpSettings = New DoctorDumpSettings With {
                .ApplicationID = New Guid("bc23cd2f-ae85-434d-83b0-d98254b1765f")
            },
            .EmailRequired = False,
            .IncludeScreenshot = True,
            .ShowScreenshotTab = True,
            .DeveloperMessage = "Application Settings:" & Environment.NewLine & "Debugger Path: " & My.Settings.DebuggerPath & Environment.NewLine & "Symbol Path: " & My.Settings.SymbolPath
        }
        reportCrash.Send(exception)
    End Sub
End Class
