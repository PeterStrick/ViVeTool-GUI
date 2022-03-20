Imports System.Security.Cryptography, Telerik.WinControls.UI

Namespace My
    ' Für MyApplication sind folgende Ereignisse verfügbar:
    ' Startup: Wird beim Starten der Anwendung noch vor dem Erstellen des Startformulars ausgelöst.
    ' Shutdown: Wird nach dem Schließen aller Anwendungsformulare ausgelöst.  Dieses Ereignis wird nicht ausgelöst, wenn die Anwendung mit einem Fehler beendet wird.
    ' UnhandledException: Wird bei einem Ausnahmefehler ausgelöst.
    ' StartupNextInstance: Wird beim Starten einer Einzelinstanzanwendung ausgelöst, wenn die Anwendung bereits aktiv ist. 
    ' NetworkAvailabilityChanged: Wird beim Herstellen oder Trennen der Netzwerkverbindung ausgelöst.
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            'Load the Text Boxes
            ScannerUI.RTB_DbgPath.Text = My.Settings.DebuggerPath
            ScannerUI.RTB_SymbolPath.Text = My.Settings.SymbolPath

            'Check if mach2.exe and msdia140.dll are present
            If IO.File.Exists(Application.Info.DirectoryPath & "\mach2.exe") = False OrElse IO.File.Exists(Application.Info.DirectoryPath & "\msdia140.dll") = False Then
                Dim RTD As New RadTaskDialogPage With {
                       .Caption = " An Error occurred",
                       .Heading = "An Error occurred. Files required for ViVeTool GUI - Feature Scanner to work properly are missing. Please check that the following files are present:" & vbNewLine & vbNewLine & "mach2.exe" & vbNewLine & "msdia140.dll",
                       .Icon = RadTaskDialogIcon.ShieldSuccessGreenBar
                   }
                'Show the Message Box
                RadTaskDialog.ShowDialog(RTD)
                End
            End If
        End Sub
        Private Function CreateSHA256Sum(filename As String) As String
            Using SHA256 As SHA256 = SHA256.Create()
                Using stream = IO.File.OpenRead(filename)
                    Return BitConverter.ToString(SHA256.ComputeHash(stream))
                End Using
            End Using
        End Function
    End Class
End Namespace
