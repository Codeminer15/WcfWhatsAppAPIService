Imports System.IO

Public Class GitHandler
    'Manejador que lleva la administración de la carga de los archivos a GitHub,
    'se puede modificar para manejar la carga de los archivo al Servidor
    Public Shared Function VerificarArchivosEnGitHub(nombreXML As String, nombrePDF As String, rutaLocalGit As String) As Boolean
        Try
            Dim rutaXML = Path.Combine(rutaLocalGit, nombreXML)
            Dim rutaPDF = Path.Combine(rutaLocalGit, nombrePDF)
            Return File.Exists(rutaXML) AndAlso File.Exists(rutaPDF)
        Catch ex As Exception
            ErrorHandler.Manejar(ex)
            Return False
        End Try
    End Function

    Public Shared Function SubirArchivosAGitHub(rutaXML As String, rutaPDF As String, rutaLocalGit As String) As Boolean
        Try
            Dim nombreXML = Path.GetFileName(rutaXML)
            Dim nombrePDF = Path.GetFileName(rutaPDF)

            File.Copy(rutaXML, Path.Combine(rutaLocalGit, nombreXML), True)
            File.Copy(rutaPDF, Path.Combine(rutaLocalGit, nombrePDF), True)

            EjecutarComandoGit("git pull --rebase", rutaLocalGit)
            EjecutarComandoGit("git add .", rutaLocalGit)
            EjecutarComandoGit($"git commit -m ""Subida automática de factura {nombrePDF}""", rutaLocalGit)
            EjecutarComandoGit("git push origin main", rutaLocalGit)

            Return True
        Catch ex As Exception
            ErrorHandler.Manejar(ex)
            Return False
        End Try
    End Function

    Private Shared Sub EjecutarComandoGit(comando As String, rutalocal As String)
        Dim proceso As New Process()
        proceso.StartInfo.FileName = "cmd.exe"
        proceso.StartInfo.WorkingDirectory = rutalocal
        proceso.StartInfo.Arguments = "/c " & comando
        proceso.StartInfo.RedirectStandardOutput = True
        proceso.StartInfo.UseShellExecute = False
        proceso.StartInfo.CreateNoWindow = True
        proceso.Start()
        proceso.WaitForExit()
    End Sub
End Class
