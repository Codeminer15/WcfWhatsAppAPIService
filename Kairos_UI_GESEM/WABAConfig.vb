
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Text

Public Class WABAConfig
    ' IMPORTANTE: Nunca poner InitializeComponent() dentro del evento Load.
    ' El inicializador debe estar en el constructor para asegurar que los controles existan
    ' antes de manipularlos. Error básico, pero ya quedó.
    Public Sub New()
        ' Esto se ejecuta antes de que el formulario dibuje su interfaz
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen ' Centrado en pantalla
        Me.ControlBox = False

        InitializeComponent()
    End Sub
    Private Sub WhatsAppConfig_(sender As Object, e As EventArgs) Handles MyBase.Load
        UIHelpers.ApplyFormLayout(Me, "Configuración WhatsApp Business API")
        UIHelpers.StyleButtonWithImage(btnSave, UIHelpers.PrimaryColor, "guardar.png", 24, "Guardar")
        UIHelpers.StyleButtonWithImage(BtnCancel, UIHelpers.DangerColor, "cancelar.png", 24, "Cancelar")
        UIHelpers.StyleComboBox(cmbVersion)
        UIHelpers.StyleTextBox(txtUserAccessToken)
        UIHelpers.StyleTextBox(txtPhoneNumberId)
        UIHelpers.StyleTextBox(TextUrl)
        LoadComboBoxData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Deshabilitar botón para evitar múltiples clics
        btnSave.Enabled = False
        Cursor.Current = Cursors.WaitCursor

        ' Validación de campos (sincrónica, es rápida)
        If Not ValidateFields() Then
            btnSave.Enabled = True
            Cursor.Current = Cursors.Default
            Return
        End If

        ' Mostrar mensaje de progreso que se cerrará automáticamente
        Dim progressForm As New Form()
        progressForm.FormBorderStyle = FormBorderStyle.None
        progressForm.StartPosition = FormStartPosition.CenterScreen
        progressForm.Size = New Size(300, 100)
        progressForm.BackColor = ColorTranslator.FromHtml("#07418C")
        progressForm.ControlBox = False

        Dim lblMessage As New Label()
        lblMessage.Text = "Guardando configuración..."
        lblMessage.Dock = DockStyle.Fill
        lblMessage.TextAlign = ContentAlignment.MiddleCenter
        lblMessage.ForeColor = Color.White
        lblMessage.Font = New Font("Segoe UI", 10)

        progressForm.Controls.Add(lblMessage)
        progressForm.Show()

        ' Ejecutar guardado en segundo plano
        Dim worker As New BackgroundWorker()

        AddHandler worker.DoWork, Sub(senderBG, eBG)
                                      Dim args = DirectCast(eBG.Argument, Object)
                                      Dim success As Boolean = False
                                      Dim errorMessage As String = ""

                                      Try
                                          Using conexion As SqlConnection = GetConnection()
                                              conexion.Open()

                                              Dim querySave As String = "IF EXISTS (SELECT 1 FROM UrlConfig) " &
                                                                  "UPDATE UrlConfig SET UserAccessToken = @token, " &
                                                                  "Version = @version, PhoneNumberId = @phone, ApiUrl = @url,LastUpdate = GETDATE()" &
                                                                  "ELSE " &
                                                                  "INSERT INTO UrlConfig (UserAccessToken, Version, PhoneNumberId, ApiUrl, LastUpdate) " &
                                                                  "VALUES (@token, @version, @phone, @url, GETDATE())"

                                              Using cmdSave As New SqlCommand(querySave, conexion)
                                                  cmdSave.Parameters.AddWithValue("@token", args.Token)
                                                  cmdSave.Parameters.AddWithValue("@version", args.Version)
                                                  cmdSave.Parameters.AddWithValue("@phone", args.Phone)
                                                  cmdSave.Parameters.AddWithValue("@url", args.Url)

                                                  cmdSave.ExecuteNonQuery()
                                                  success = True
                                              End Using
                                          End Using
                                      Catch ex As Exception
                                          errorMessage = ex.Message
                                          success = False
                                      End Try

                                      eBG.Result = New With {.Success = success, .Message = errorMessage}
                                  End Sub

        AddHandler worker.RunWorkerCompleted, Sub(senderBG, eBG)
                                                  If Me.InvokeRequired Then
                                                      Me.Invoke(Sub() RunCompletionHandler(eBG, progressForm))
                                                  Else
                                                      RunCompletionHandler(eBG, progressForm)
                                                  End If
                                              End Sub
        Dim versionSeleccionada As String = cmbVersion.SelectedItem.ToString()
        Dim token As String = txtUserAccessToken.Text
        Dim phoneId As String = txtPhoneNumberId.Text
        Dim apiUrl As String = TextUrl.Text

        Dim parametros = New With {
            .Token = token,
            .Version = versionSeleccionada,
            .Phone = phoneId,
            .Url = apiUrl
        }

        worker.RunWorkerAsync(parametros)

    End Sub

    Private Sub RunCompletionHandler(eBG As RunWorkerCompletedEventArgs, progressForm As Form)
        progressForm.Close()
        btnSave.Enabled = True
        Cursor.Current = Cursors.Default

        If eBG.Error IsNot Nothing Then
            MessageBox.Show($"Error inesperado: {eBG.Error.Message}", "Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim result = DirectCast(eBG.Result, Object)

        If result.Success Then
            MessageBox.Show("Configuración guardada con éxito", "Éxito",
                         MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show($"Error al guardar: {result.Message}", "Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function ValidateFields() As Boolean
        Dim camposIncompletos As Boolean = False

        ' Restaurar colores por defecto
        txtUserAccessToken.BackColor = SystemColors.Window
        txtPhoneNumberId.BackColor = SystemColors.Window
        TextUrl.BackColor = SystemColors.Window
        cmbVersion.BackColor = SystemColors.Window

        ' Validaciones individuales
        If txtUserAccessToken.Text Is Nothing OrElse txtUserAccessToken.Text.Trim() = "" Then
            txtUserAccessToken.BackColor = Color.LightSalmon

            camposIncompletos = True
        End If

        If txtPhoneNumberId.Text Is Nothing OrElse txtPhoneNumberId.Text.Trim() = "" Then
            txtPhoneNumberId.BackColor = Color.LightSalmon
            camposIncompletos = True
        End If

        If TextUrl.Text Is Nothing OrElse TextUrl.Text.Trim() = "" Then
            TextUrl.BackColor = Color.LightSalmon
            camposIncompletos = True
        End If

        If cmbVersion.SelectedItem Is Nothing Then
            cmbVersion.BackColor = Color.LightSalmon
            camposIncompletos = True
        End If

        If cmbVersion.SelectedItem Is Nothing Then
            cmbVersion.BackColor = Color.LightSalmon
            camposIncompletos = True
        End If


        If camposIncompletos Then
            MessageBox.Show("Por favor, completa todos los campos antes de guardar.", "Validación",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Sub LoadComboBoxData()
        cmbVersion.Items.Clear()
        ' Agregar versiones disponibles en el ComboBox
        cmbVersion.Items.Add("v24.0")
        cmbVersion.Items.Add("v23.0")
        cmbVersion.Items.Add("v22.0")
        cmbVersion.Items.Add("v21.0")
        cmbVersion.Items.Add("v20.0")
        cmbVersion.Items.Add("v19.0")
        cmbVersion.Items.Add("v18.0")

        ' Seleccionar por defecto la primera versión
        If cmbVersion.Items.Count > 0 Then
            cmbVersion.SelectedIndex = 1
        End If
    End Sub

    Private Sub txtUserAccessToken_TextChanged_1(sender As Object, e As EventArgs) Handles txtUserAccessToken.TextChanged
        txtUserAccessToken.Multiline = True
        txtUserAccessToken.ScrollBars = ScrollBars.Vertical ' o ScrollBars.Both
        txtUserAccessToken.WordWrap = True ' Para evitar saltos de línea automáticos
        txtUserAccessToken.AcceptsReturn = False ' Para no permitir retornos de carro   
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ClearError(ctrl As Control)
        ' Restaurar color original
        If TypeOf ctrl Is TextBox Then
            ctrl.BackColor = Color.White
        ElseIf TypeOf ctrl Is ComboBox Then
            ctrl.BackColor = Color.White
        End If

        ' Remover manejador si existe
        RemoveHandler ctrl.Enter, AddressOf ClearErrorHandler
    End Sub

    Private Sub ClearErrorHandler(sender As Object, e As EventArgs)
        ' Limpiar el error cuando el control recibe foco
        ClearError(DirectCast(sender, Control))
    End Sub

    '
    'Estilos
    '
End Class

