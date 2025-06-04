Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

Public Class ErrorHandler
    'Ruta del archivo donde se aloja el registro de las excepciones capturadas en el proceso de envío
    Private Shared logPath As String = "C:\Logs\WhatsAppService.log"

    Public Shared Sub Manejar(ex As Exception)
        Dim mensajeUsuario As String = "Se produjo un error al procesar la solicitud."

        If TypeOf ex Is TimeoutException Then
            mensajeUsuario = "El servidor tardó demasiado en responder. Intenta de nuevo más tarde."
        ElseIf TypeOf ex Is WebException Then
            mensajeUsuario = "No se pudo establecer conexión con el servidor. Revisa tu red o intenta más tarde."
        ElseIf TypeOf ex Is IOException Then
            mensajeUsuario = "Hubo un problema al acceder a los archivos o rutas locales."
        ElseIf TypeOf ex Is UnauthorizedAccessException Then
            mensajeUsuario = "El sistema no tiene permisos suficientes para ejecutar esta acción."
        ElseIf TypeOf ex Is SqlException Then
            mensajeUsuario = "Error al conectarse o consultar la base de datos."
        ElseIf TypeOf ex Is FormatException Then
            mensajeUsuario = "Uno de los datos ingresados tiene un formato inválido."
        ElseIf TypeOf ex Is InvalidOperationException Then
            mensajeUsuario = "El sistema encontró un estado no válido en tiempo de ejecución."

            ' Detección personalizada... errores relacionados con Git o ejecución de comandos
        ElseIf ex.Message.ToLower().Contains("git") OrElse ex.Message.ToLower().Contains("cmd.exe") Then
            mensajeUsuario = "No se pudo ejecutar el proceso de subida a Git. Verifica las rutas de archivos locales y permisos del sistema."
        End If

        ' Registrar en log
        Try
            Dim logDir As String = Path.GetDirectoryName(logPath)
            If Not Directory.Exists(logDir) Then
                Directory.CreateDirectory(logDir)
            End If

            Dim logEntry As String = String.Format(
                "[{0}] {1}{2}Excepción: {3}{2}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                mensajeUsuario,
                Environment.NewLine,
                ex.Message)

            File.AppendAllText(logPath, logEntry & Environment.NewLine)
        Catch
            ' Ignorar errores de log
        End Try

        MessageBox.Show(mensajeUsuario, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

End Class
