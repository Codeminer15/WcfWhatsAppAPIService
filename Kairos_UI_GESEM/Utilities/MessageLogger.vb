Imports System.IO

Public Class MessageLogger
    Public Shared Sub GuardarLogDeEnvio(json As String)
        Try
            Dim logPath As String = "C:\Logs\WhatsAppEnvios.log"
            Dim logDir As String = Path.GetDirectoryName(logPath)

            If Not Directory.Exists(logDir) Then
                Directory.CreateDirectory(logDir)
            End If

            Dim logEntry As String = String.Format(
                "[{0}] Respuesta de WhatsApp: {1}{2}{2}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Environment.NewLine,
                json)
            File.AppendAllText(logPath, logEntry)
        Catch ex As Exception
            'Si falla el log, no interrumpir el proceso
        End Try
    End Sub
End Class
