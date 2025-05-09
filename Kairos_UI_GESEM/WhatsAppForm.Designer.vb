<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WhatsAppForm
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblNetworkStatus = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxXml = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPdf = New System.Windows.Forms.CheckBox()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.btnViewLogs = New System.Windows.Forms.Button()
        Me.btnTestConnection = New System.Windows.Forms.Button()
        Me.lblServiceStatus = New System.Windows.Forms.Label()
        Me.lblXGH = New System.Windows.Forms.Label()
        Me.lblPGH = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblState = New System.Windows.Forms.Label()
        Me.lblXml = New System.Windows.Forms.Label()
        Me.lblPdf = New System.Windows.Forms.Label()
        Me.txtPhoneNumber = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSendWhatsApp = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblNetworkStatus
        '
        Me.lblNetworkStatus.AutoSize = True
        Me.lblNetworkStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetworkStatus.Location = New System.Drawing.Point(4, 12)
        Me.lblNetworkStatus.Name = "lblNetworkStatus"
        Me.lblNetworkStatus.Size = New System.Drawing.Size(108, 13)
        Me.lblNetworkStatus.TabIndex = 42
        Me.lblNetworkStatus.Text = "Estado de la red: ---"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.DarkRed
        Me.Label6.Location = New System.Drawing.Point(185, 145)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(424, 13)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "Debe incluir lada y la cadena de 10 dígitos, evitando símbolos (ejemplo: 52123456" &
    "7890)"
        '
        'CheckBoxXml
        '
        Me.CheckBoxXml.AutoSize = True
        Me.CheckBoxXml.Enabled = False
        Me.CheckBoxXml.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.CheckBoxXml.Location = New System.Drawing.Point(576, 99)
        Me.CheckBoxXml.Name = "CheckBoxXml"
        Me.CheckBoxXml.Size = New System.Drawing.Size(60, 25)
        Me.CheckBoxXml.TabIndex = 40
        Me.CheckBoxXml.Text = "XML"
        Me.CheckBoxXml.UseVisualStyleBackColor = True
        '
        'CheckBoxPdf
        '
        Me.CheckBoxPdf.AutoSize = True
        Me.CheckBoxPdf.Enabled = False
        Me.CheckBoxPdf.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.CheckBoxPdf.Location = New System.Drawing.Point(576, 74)
        Me.CheckBoxPdf.Name = "CheckBoxPdf"
        Me.CheckBoxPdf.Size = New System.Drawing.Size(57, 25)
        Me.CheckBoxPdf.TabIndex = 39
        Me.CheckBoxPdf.Text = "PDF"
        Me.CheckBoxPdf.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCancel.Location = New System.Drawing.Point(500, 354)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(109, 40)
        Me.BtnCancel.TabIndex = 38
        Me.BtnCancel.Text = "Cancelar"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'btnViewLogs
        '
        Me.btnViewLogs.Location = New System.Drawing.Point(387, 354)
        Me.btnViewLogs.Name = "btnViewLogs"
        Me.btnViewLogs.Size = New System.Drawing.Size(75, 40)
        Me.btnViewLogs.TabIndex = 37
        Me.btnViewLogs.Text = "Abrir Logs"
        Me.btnViewLogs.UseVisualStyleBackColor = True
        '
        'btnTestConnection
        '
        Me.btnTestConnection.Location = New System.Drawing.Point(112, 293)
        Me.btnTestConnection.Name = "btnTestConnection"
        Me.btnTestConnection.Size = New System.Drawing.Size(75, 40)
        Me.btnTestConnection.TabIndex = 36
        Me.btnTestConnection.Text = "Conexion"
        Me.btnTestConnection.UseVisualStyleBackColor = True
        '
        'lblServiceStatus
        '
        Me.lblServiceStatus.AutoSize = True
        Me.lblServiceStatus.Location = New System.Drawing.Point(198, 303)
        Me.lblServiceStatus.Name = "lblServiceStatus"
        Me.lblServiceStatus.Size = New System.Drawing.Size(46, 13)
        Me.lblServiceStatus.TabIndex = 35
        Me.lblServiceStatus.Text = "Estado--"
        '
        'lblXGH
        '
        Me.lblXGH.AutoSize = True
        Me.lblXGH.Location = New System.Drawing.Point(198, 263)
        Me.lblXGH.Name = "lblXGH"
        Me.lblXGH.Size = New System.Drawing.Size(24, 13)
        Me.lblXGH.TabIndex = 34
        Me.lblXGH.Text = "Xml"
        '
        'lblPGH
        '
        Me.lblPGH.AutoSize = True
        Me.lblPGH.Location = New System.Drawing.Point(198, 238)
        Me.lblPGH.Name = "lblPGH"
        Me.lblPGH.Size = New System.Drawing.Size(23, 13)
        Me.lblPGH.TabIndex = 33
        Me.lblPGH.Text = "Pdf"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(52, 246)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(130, 21)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Ruta en GitHub:"
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblState.Location = New System.Drawing.Point(4, 25)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(124, 13)
        Me.lblState.TabIndex = 31
        Me.lblState.Text = "Estado de Archivos: ---"
        '
        'lblXml
        '
        Me.lblXml.AutoSize = True
        Me.lblXml.Location = New System.Drawing.Point(198, 212)
        Me.lblXml.Name = "lblXml"
        Me.lblXml.Size = New System.Drawing.Size(24, 13)
        Me.lblXml.TabIndex = 30
        Me.lblXml.Text = "Xml"
        '
        'lblPdf
        '
        Me.lblPdf.AutoSize = True
        Me.lblPdf.Location = New System.Drawing.Point(198, 185)
        Me.lblPdf.Name = "lblPdf"
        Me.lblPdf.Size = New System.Drawing.Size(23, 13)
        Me.lblPdf.TabIndex = 29
        Me.lblPdf.Text = "Pdf"
        '
        'txtPhoneNumber
        '
        Me.txtPhoneNumber.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhoneNumber.Location = New System.Drawing.Point(188, 113)
        Me.txtPhoneNumber.Name = "txtPhoneNumber"
        Me.txtPhoneNumber.Size = New System.Drawing.Size(207, 29)
        Me.txtPhoneNumber.TabIndex = 28
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(573, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(145, 21)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Archivos  a Enviar"
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(188, 70)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(207, 29)
        Me.txtName.TabIndex = 26
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(71, 185)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 21)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Documentos:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(171, 21)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Número de Teléfono:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(19, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(163, 21)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Nombre del Cliente:"
        '
        'btnSendWhatsApp
        '
        Me.btnSendWhatsApp.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnSendWhatsApp.Location = New System.Drawing.Point(631, 354)
        Me.btnSendWhatsApp.Name = "btnSendWhatsApp"
        Me.btnSendWhatsApp.Size = New System.Drawing.Size(109, 40)
        Me.btnSendWhatsApp.TabIndex = 22
        Me.btnSendWhatsApp.Text = "Enviar"
        Me.btnSendWhatsApp.UseVisualStyleBackColor = True
        '
        'WhatsAppForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 404)
        Me.Controls.Add(Me.lblNetworkStatus)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CheckBoxXml)
        Me.Controls.Add(Me.CheckBoxPdf)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.btnViewLogs)
        Me.Controls.Add(Me.btnTestConnection)
        Me.Controls.Add(Me.lblServiceStatus)
        Me.Controls.Add(Me.lblXGH)
        Me.Controls.Add(Me.lblPGH)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblState)
        Me.Controls.Add(Me.lblXml)
        Me.Controls.Add(Me.lblPdf)
        Me.Controls.Add(Me.txtPhoneNumber)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSendWhatsApp)
        Me.Name = "WhatsAppForm"
        Me.Text = "WhatsAppForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblNetworkStatus As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents CheckBoxXml As CheckBox
    Friend WithEvents CheckBoxPdf As CheckBox
    Friend WithEvents BtnCancel As Button
    Friend WithEvents btnViewLogs As Button
    Friend WithEvents btnTestConnection As Button
    Friend WithEvents lblServiceStatus As Label
    Friend WithEvents lblXGH As Label
    Friend WithEvents lblPGH As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblState As Label
    Friend WithEvents lblXml As Label
    Friend WithEvents lblPdf As Label
    Friend WithEvents txtPhoneNumber As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnSendWhatsApp As Button
End Class
