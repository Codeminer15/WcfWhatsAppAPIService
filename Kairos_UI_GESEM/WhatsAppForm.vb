Imports System.IO
Imports System.Net
Imports System.ServiceModel
Imports System.Text
Imports Kairos_UI_GESEM.WhatsAppAPI


Public Class WhatsAppForm

    Private Const Tls12 As SecurityProtocolType = CType(&HC00, SecurityProtocolType)
    Private ReadOnly logPath As String = "C:\Logs\WhatsAppService.log"
    Private ReadOnly rutaLocalGit As String = "D:\Projects\Facturas_Prueba"
    Private ReadOnly urlGitHub As String = "https://github.com/codeminer15/Facturas_Prueba/"
    Private WithEvents _networkChecker As New NetworkChecker()


    Public Sub New(nombre As String, telefono As String, xml As String, pdf As String, state As String)
        Me.FormBorderStyle = FormBorderStyle.None
        Me.ControlBox = False
        Me.StartPosition = FormStartPosition.CenterScreen

        InitializeComponent()

        txtName.Text = nombre
        txtPhoneNumber.Text = telefono
        lblXml.Text = xml
        lblPdf.Text = pdf
        lblState.Text = state
    End Sub

    Private Sub WhatsAppForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        UIHelpers.ApplyFormLayout(Me, "Envío de Factura por WhatsApp")
        ApplyStyles()
        _networkChecker.StartChecking()
        btnSendWhatsApp.Enabled = _networkChecker.IsConnected
    End Sub

    Private Sub ApplyStyles()
        UIHelpers.StyleTextBox(txtName)
        UIHelpers.StyleTextBox(txtPhoneNumber)

        UIHelpers.StyleButtonWithImage(btnSendWhatsApp, UIHelpers.PrimaryColor, "enviar.png", 24, "Enviar")
        UIHelpers.StyleButtonWithImage(BtnCancel, UIHelpers.DangerColor, "cancelar.png", 24, "Cancelar")
        UIHelpers.StyleButton(btnTestConnection, ColorTranslator.FromHtml("#07418C"))
        UIHelpers.StyleButton(btnViewLogs, ColorTranslator.FromHtml("#07418C"))

        UIHelpers.AddToolTip(btnTestConnection, "Verificar conexión con el servicio")
        UIHelpers.AddToolTip(btnViewLogs, "Ver registro de actividades")
        UIHelpers.AddToolTip(BtnCancel, "Cancelar y cerrar esta ventana")
        UIHelpers.AddToolTip(btnSendWhatsApp, "Enviar mensaje por WhatsApp con los archivos adjuntos")

        lblServiceStatus.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        lblServiceStatus.ForeColor = Color.Gray

        btnSendWhatsApp.TextAlign = ContentAlignment.MiddleCenter
        btnSendWhatsApp.ImageAlign = ContentAlignment.MiddleLeft
        BtnCancel.TextAlign = ContentAlignment.MiddleCenter
        BtnCancel.ImageAlign = ContentAlignment.MiddleLeft

        btnTestConnection.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        btnViewLogs.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        btnSendWhatsApp.Padding = New Padding(10, 0, 10, 0)
        BtnCancel.Padding = New Padding(10, 0, 10, 0)
    End Sub

    Private Sub BtnSendWhatsApp_Click(sender As Object, e As EventArgs) Handles btnSendWhatsApp.Click
        Dim phonenumber As String = New String(txtPhoneNumber.Text.Where(AddressOf Char.IsDigit).ToArray())

        If txtName.Text Is Nothing OrElse txtName.Text.Trim() = "" Then
            txtName.BackColor = Color.LightSalmon
            MessageBox.Show("Por favor ingrese el nombre del cliente", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If txtPhoneNumber.Text Is Nothing OrElse txtPhoneNumber.Text.Trim() = "" Then
            txtPhoneNumber.BackColor = Color.LightSalmon
            MessageBox.Show("Por favor ingrese el número del cliente", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If phonenumber.Length <> 12 Then
            txtPhoneNumber.BackColor = Color.LightSalmon
            MessageBox.Show("El número de teléfono debe tener 12 dígitos.", "Número inválido", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim rutaXML As String = lblXml.Text
        Dim rutaPDF As String = lblPdf.Text

        If Not File.Exists(rutaXML) OrElse Not File.Exists(rutaPDF) Then
            MessageBox.Show("No se encontraron los archivos en las rutas especificadas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim nombreXML As String = Path.GetFileName(rutaXML)
        Dim nombrePDF As String = Path.GetFileName(rutaPDF)

        If Not GitHandler.VerificarArchivosEnGitHub(nombreXML, nombrePDF, rutaLocalGit) Then
            If Not GitHandler.SubirArchivosAGitHub(rutaXML, rutaPDF, rutaLocalGit) Then

                CheckBoxPdf.Checked = False
                CheckBoxXml.Checked = False
                Return
            End If
        End If

        CheckBoxPdf.Checked = True
        CheckBoxXml.Checked = True

        If Not CheckBoxPdf.Checked OrElse Not CheckBoxXml.Checked Then
            MessageBox.Show("Por favor verifica que ambos documentos han sido subidos a GitHub marcando las casillas correspondientes.",
                            "Documentos no verificados", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        lblXGH.Text = urlGitHub & nombreXML
        lblPGH.Text = urlGitHub & nombrePDF

        EnviarMensajeWhatsApp(txtPhoneNumber.Text.Trim(), txtName.Text.Trim(), nombrePDF, nombreXML)
    End Sub

    Private Sub EnviarMensajeWhatsApp(phoneNumber As String, customerName As String, fileNamePdf As String, fileNameXml As String)
        Try
            Dim proxy As New WebProxy("127.0.0.1", 8888)

            Dim request As HttpWebRequest = DirectCast(WebRequest.Create("http://localhost/svcWebService/Service1.svc/SendTemplateBillingMessage"), HttpWebRequest)
            request.Proxy = proxy
            request.Method = "POST"
            request.ContentType = "application/json"

            ServicePointManager.SecurityProtocol = CType(48 Or 192 Or 768 Or 3072, SecurityProtocolType)
            ServicePointManager.ServerCertificateValidationCallback = Function() True

            Dim jsonPayload As String = String.Format(
                "{{""phoneNumber"":""{0}"",""customerName"":""{1}"",""fileNamePdf"":""{2}"",""fileNameXml"":""{3}""}}",
                phoneNumber, customerName, fileNamePdf, fileNameXml)

            Dim data As Byte() = Encoding.UTF8.GetBytes(jsonPayload)
            request.ContentLength = data.Length

            Using stream As Stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()

                    'Guardar el log de envío
                    MessageLogger.GuardarLogDeEnvio(responseText)

                    MessageBox.Show("Mensaje enviado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using

        Catch ex As Exception
            ErrorHandler.Manejar(ex)
        End Try
    End Sub

    Private Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
        Dim endpoints As New List(Of String) From {
            "http://192.168.1.8/API_WA/WhatsAppAPIs.svc",
            "https://192.168.1.8:80/API_WA/WhatsAppAPIs.svc"
        }

        lblServiceStatus.Text = "Probando conexión..."
        Application.DoEvents()

        Dim conectado As Boolean = False
        Dim endpointActivo As String = ""

        For Each endpoint As String In endpoints
            If TestServiceConnection(endpoint) Then
                conectado = True
                endpointActivo = endpoint
                Exit For
            End If
        Next

        lblServiceStatus.Text = If(conectado, $"Conectado a: {endpointActivo}", "Servicio no disponible")
        lblServiceStatus.ForeColor = If(conectado, Color.Green, Color.Red)
    End Sub

    Private Function TestServiceConnection(endpoint As String) As Boolean
        Try
            Dim binding As BasicHttpBinding

            If endpoint.StartsWith("https") Then
                binding = New BasicHttpBinding(BasicHttpSecurityMode.Transport)
                ServicePointManager.SecurityProtocol = CType(&H30 Or &HC0 Or &H300 Or &HC00, SecurityProtocolType)
                ServicePointManager.ServerCertificateValidationCallback = Function() True
            Else
                binding = New BasicHttpBinding(BasicHttpSecurityMode.None)
            End If

            binding.OpenTimeout = TimeSpan.FromSeconds(5)
            binding.ReceiveTimeout = TimeSpan.FromSeconds(5)
            binding.SendTimeout = TimeSpan.FromSeconds(5)

            Using client As New WhatsAppAPIsClient(binding, New EndpointAddress(endpoint))
                client.Open()
                Return client.State = CommunicationState.Opened
            End Using
        Catch
            Return False
        End Try
    End Function

    'Solo aplicar para casos de prueba
    Private Sub btnViewLogs_Click(sender As Object, e As EventArgs) Handles btnViewLogs.Click
        Try
            If File.Exists(logPath) Then
                Process.Start("notepad.exe", logPath)
            Else
                MessageBox.Show("El archivo de log no existe aún", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error al abrir logs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub NetworkStatusChanged(ByVal isConnected As Boolean) Handles _networkChecker.ConnectionStatusChanged
        If Me.InvokeRequired Then
            Me.Invoke(New Action(Of Boolean)(AddressOf UpdateNetworkStatus), isConnected)
        Else
            UpdateNetworkStatus(isConnected)
        End If
    End Sub

    Private Sub UpdateNetworkStatus(isConnected As Boolean)
        btnSendWhatsApp.Enabled = isConnected
        lblNetworkStatus.Text = If(isConnected, "Estado de la red: Conectado a Internet", "Estado de la red: Sin conexión a Internet")
        lblNetworkStatus.ForeColor = If(isConnected, Color.Green, Color.Red)
    End Sub

    Private Sub WhatsAppForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        _networkChecker.StopChecking()
    End Sub

End Class
