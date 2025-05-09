Imports System.Data.SqlClient
Imports System.Threading

Public Class DataBaseChecker
    Private Const CheckInterval As Integer = 5000 ' 5 segundos
    Private _isChecking As Boolean = False
    Private _lastStatus As Boolean = False

    ' Evento para notificar cambios en el estado de la conexión
    Public Event ConnectionStatusChanged(ByVal isConnected As Boolean)

    ' Propiedad para obtener el último estado conocido
    Public ReadOnly Property IsConnected As Boolean
        Get
            Return _lastStatus
        End Get
    End Property

    ' Método para iniciar la verificación periódica
    Public Sub StartChecking()
        If _isChecking Then Return

        _isChecking = True
        Dim thread As New Thread(AddressOf CheckDatabaseLoop)
        thread.IsBackground = True
        thread.Start()
    End Sub

    ' Método para detener la verificación periódica
    Public Sub StopChecking()
        _isChecking = False
    End Sub

    ' Método para verificar la conexión una vez
    Public Function CheckDatabaseConnection() As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                _lastStatus = (conn.State = ConnectionState.Open)
                RaiseEvent ConnectionStatusChanged(_lastStatus)
                Return _lastStatus
            End Using
        Catch
            _lastStatus = False
            RaiseEvent ConnectionStatusChanged(False)
            Return False
        End Try
    End Function

    ' Bucle de verificación periódica
    Private Sub CheckDatabaseLoop()
        While _isChecking
            CheckDatabaseConnection()
            Thread.Sleep(CheckInterval)
        End While
    End Sub
End Class
