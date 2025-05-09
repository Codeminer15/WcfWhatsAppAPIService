Imports System.Net
Imports System.Threading

Public Class NetworkChecker
    Private Const TestUrl As String = "http://www.google.com"
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
        Dim thread As New Thread(AddressOf CheckConnectionLoop)
        thread.IsBackground = True
        thread.Start()
    End Sub

    ' Método para detener la verificación periódica
    Public Sub StopChecking()
        _isChecking = False
    End Sub

    ' Método para verificar la conexión una vez
    Public Function CheckConnection() As Boolean
        Try
            ' Configuración para compatibilidad TLS en .NET 3.5
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Ssl3
            ServicePointManager.ServerCertificateValidationCallback = Function() True

            Dim request As HttpWebRequest = CType(WebRequest.Create(TestUrl), HttpWebRequest)
            request.Timeout = 2000 ' 3 segundos de timeout
            request.Method = "HEAD"

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                _lastStatus = (response.StatusCode = HttpStatusCode.OK)
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
    Private Sub CheckConnectionLoop()
        While _isChecking
            CheckConnection()
            Thread.Sleep(CheckInterval)
        End While
    End Sub
End Class

