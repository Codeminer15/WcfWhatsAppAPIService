Imports System.ComponentModel
Imports System.Data.SqlClient

Module DBConnection

    ' Cadena de conexión
    Private Const ConnectionString As String =
        "Data Source=DESKTOP-JMH\SQLEXPRESS;Initial Catalog=ApiWhatsAppConfig;Integrated Security=True;Connect Timeout=3;"

    ' Retorna un objeto de conexión
    Public Function GetConnection() As SqlConnection
        Return New SqlConnection(ConnectionString)
    End Function

    ' Verifica la conexión de manera asíncrona
    Public Sub TestConnectionAsync(callback As Action(Of Boolean))
        Dim worker As New BackgroundWorker()

        AddHandler worker.DoWork,
        Sub(sender, e)
            Using conn As New SqlConnection(ConnectionString)
                Try
                    conn.Open()
                    e.Result = True
                Catch
                    e.Result = False
                End Try
            End Using
        End Sub

        AddHandler worker.RunWorkerCompleted,
        Sub(sender, e)
            callback(If(e.Error Is Nothing, CBool(e.Result), False))
        End Sub

        worker.RunWorkerAsync()
    End Sub

End Module

