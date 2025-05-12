Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class ButtonsForm

    Private Sub ButtonsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyButtonStyles()
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub BtnSendMessageWindow_Click(sender As Object, e As EventArgs) Handles BtnSendMessageWindow.Click
        BtnSendMessageWindow.Enabled = False
        Cursor.Current = Cursors.WaitCursor

        Dim loadingMsg As Form = CreateLoadingForm("Verificando conexión...")
        loadingMsg.Show()

        DBConnection.TestConnectionAsync(Sub(isConnected As Boolean)
                                             Me.Invoke(Sub()
                                                           loadingMsg.Close()
                                                           BtnSendMessageWindow.Enabled = True
                                                           Cursor.Current = Cursors.Default

                                                           If Not isConnected Then
                                                               MessageBox.Show("No se puede conectar a la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                               Return
                                                           End If

                                                           ProcessClientData()
                                                       End Sub)
                                         End Sub)
    End Sub

    Private Sub ProcessClientData()
        Dim idCliente As Integer

        If Not Integer.TryParse(txtIdCliente.Text, idCliente) Then
            MessageBox.Show("Ingrese un ID de cliente válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim queryMsg As Form = CreateLoadingForm("Consultando datos del cliente...")
        queryMsg.Show()

        Dim worker As New BackgroundWorker()

        AddHandler worker.DoWork, Sub(sender, e)
                                      Dim result As New Dictionary(Of String, String)

                                      Try
                                          Using conexion As SqlConnection = GetConnection()
                                              conexion.Open()

                                              Dim query As String =
                                                  "SELECT c.Nombre, c.Telefono, f.RutaXML, f.RutaPDF " &
                                                  "FROM Clientes c INNER JOIN Factura f ON c.Id = f.IdCliente " &
                                                  "WHERE c.Id = @IdCliente"

                                              Using cmd As New SqlCommand(query, conexion)
                                                  cmd.Parameters.AddWithValue("@IdCliente", idCliente)

                                                  Using reader As SqlDataReader = cmd.ExecuteReader()
                                                      If reader.Read() Then
                                                          result("Nombre") = reader("Nombre").ToString()
                                                          result("Telefono") = reader("Telefono").ToString()
                                                          result("RutaXML") = reader("RutaXML").ToString()
                                                          result("RutaPDF") = reader("RutaPDF").ToString()
                                                          result("Success") = "True"
                                                      Else
                                                          result("Error") = "No se encontraron datos para este cliente."
                                                      End If
                                                  End Using
                                              End Using
                                          End Using
                                      Catch ex As Exception
                                          result("Error") = "Error al obtener los datos: " & ex.Message
                                      End Try

                                      e.Result = result
                                  End Sub

        AddHandler worker.RunWorkerCompleted, Sub(sender, e)
                                                  queryMsg.Close()

                                                  If e.Error IsNot Nothing Then
                                                      MessageBox.Show("Error inesperado: " & e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                      Return
                                                  End If

                                                  Dim result As Dictionary(Of String, String) = DirectCast(e.Result, Dictionary(Of String, String))

                                                  If result.ContainsKey("Error") Then
                                                      MessageBox.Show(result("Error"), "Error", MessageBoxButtons.OK,
                                                                      If(result("Error").Contains("No se encontraron"), MessageBoxIcon.Warning, MessageBoxIcon.Error))
                                                      Return
                                                  End If

                                                  Dim xmlExiste As Boolean = IO.File.Exists(result("RutaXML"))
                                                  Dim pdfExiste As Boolean = IO.File.Exists(result("RutaPDF"))
                                                  Dim estado As String = If(xmlExiste AndAlso pdfExiste, "Estado: Archivos disponibles", "Estado: Archivos no encontrados")

                                                  Dim form As New WhatsAppForm(result("Nombre"), result("Telefono"), result("RutaXML"), result("RutaPDF"), estado)
                                                  form.Show()
                                              End Sub

        worker.RunWorkerAsync()
    End Sub

    Private Function CreateLoadingForm(message As String) As Form
        Dim form As New Form With {
            .FormBorderStyle = FormBorderStyle.None,
            .StartPosition = FormStartPosition.CenterScreen,
            .Size = New Size(300, 100),
            .BackColor = UIHelpers.WhatsAppGreen,
            .ControlBox = False
        }

        Dim label As New Label With {
            .Text = message,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold)
        }

        form.Controls.Add(label)
        Return form
    End Function

    Private Sub BtnSetting_Click(sender As Object, e As EventArgs) Handles BtnSetting.Click
        Dim configForm As New WABAConfig()
        configForm.StartPosition = FormStartPosition.CenterParent
        configForm.Show()
    End Sub

    ' ============================================
    ' = Aplicar diseño visual al formulario
    ' ============================================
    Private Sub ApplyButtonStyles()
        ' Estilo general
        Me.ClientSize = New Size(400, 300)
        Me.BackColor = Color.FromArgb(240, 240, 240)

        ' Panel principal
        Dim mainPanel As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(40)}
        Me.Controls.Add(mainPanel)

        ' Tabla para centrado vertical
        Dim layout As New TableLayoutPanel With {.Dock = DockStyle.Fill, .ColumnCount = 1, .RowCount = 4}
        layout.RowStyles.Add(New RowStyle(SizeType.Percent, 20))
        layout.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        layout.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        layout.RowStyles.Add(New RowStyle(SizeType.Percent, 20))
        mainPanel.Controls.Add(layout)

        ' Entrada de cliente
        Dim inputPanel As New Panel() With {.AutoSize = True, .Anchor = AnchorStyles.None}
        Label1.Text = "ID Cliente:"
        Label1.Font = New Font("Segoe UI", 10)
        Label1.AutoSize = True
        Label1.TextAlign = ContentAlignment.MiddleRight

        txtIdCliente.Font = New Font("Segoe UI", 10)
        txtIdCliente.Size = New Size(150, 26)

        Label1.Location = New Point(0, 6)
        txtIdCliente.Location = New Point(Label1.Right + 10, 3)
        inputPanel.Size = New Size(Label1.Width + txtIdCliente.Width + 10, Math.Max(Label1.Height, txtIdCliente.Height))

        inputPanel.Controls.Add(Label1)
        inputPanel.Controls.Add(txtIdCliente)
        layout.Controls.Add(inputPanel, 0, 1)

        ' Botones
        Dim buttonsPanel As New FlowLayoutPanel With {
            .FlowDirection = FlowDirection.TopDown,
            .AutoSize = True,
            .Anchor = AnchorStyles.None,
            .WrapContents = False
        }
        layout.Controls.Add(buttonsPanel, 0, 2)

        Dim toolTip As New ToolTip With {
            .AutoPopDelay = 5000,
            .InitialDelay = 1000,
            .ReshowDelay = 500,
            .ShowAlways = True
        }

        ConfigureWhatsAppButton(buttonsPanel, toolTip)
        ConfigureSettingsButton(buttonsPanel, toolTip)
    End Sub

    Private Sub ConfigureWhatsAppButton(container As FlowLayoutPanel, toolTip As ToolTip)
        BtnSendMessageWindow.AutoSize = False
        BtnSendMessageWindow.Size = New Size(160, 50)
        UIHelpers.StyleButtonWithImage(BtnSendMessageWindow, UIHelpers.WhatsAppGreen, "logo_whatsapp.png", 28, "WhatsApp")
        BtnSendMessageWindow.TextAlign = ContentAlignment.MiddleRight
        BtnSendMessageWindow.Padding = New Padding(10, 0, 15, 0)
        toolTip.SetToolTip(BtnSendMessageWindow, "Enviar mensaje por WhatsApp")
        BtnSendMessageWindow.Margin = New Padding(0, 0, 0, 20)
        container.Controls.Add(BtnSendMessageWindow)
    End Sub

    Private Sub ConfigureSettingsButton(container As FlowLayoutPanel, toolTip As ToolTip)
        BtnSetting.AutoSize = False
        BtnSetting.Size = New Size(160, 50)
        UIHelpers.StyleButtonWithImage(BtnSetting, UIHelpers.PrimaryColor, "engranaje3.png", 24, "Configuración")
        BtnSetting.TextAlign = ContentAlignment.MiddleRight
        BtnSetting.Padding = New Padding(10, 0, 15, 0)
        toolTip.SetToolTip(BtnSetting, "Abrir configuración de los mensajes de WhatsApp")
        container.Controls.Add(BtnSetting)
    End Sub

End Class
