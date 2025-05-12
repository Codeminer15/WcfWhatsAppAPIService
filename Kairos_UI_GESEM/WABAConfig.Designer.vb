<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WABAConfig
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
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextUrl = New System.Windows.Forms.TextBox()
        Me.LblMsg = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.cmbVersion = New System.Windows.Forms.ComboBox()
        Me.txtPhoneNumberId = New System.Windows.Forms.TextBox()
        Me.txtUserAccessToken = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnCancel
        '
        Me.BtnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCancel.Location = New System.Drawing.Point(314, 267)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(109, 40)
        Me.BtnCancel.TabIndex = 30
        Me.BtnCancel.Text = "Cancelar"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(88, 169)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 21)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "URL Meta:"
        '
        'TextUrl
        '
        Me.TextUrl.Location = New System.Drawing.Point(181, 169)
        Me.TextUrl.Name = "TextUrl"
        Me.TextUrl.Size = New System.Drawing.Size(228, 20)
        Me.TextUrl.TabIndex = 28
        Me.TextUrl.Text = "https://graph.facebook.com"
        '
        'LblMsg
        '
        Me.LblMsg.AutoSize = True
        Me.LblMsg.Location = New System.Drawing.Point(132, 247)
        Me.LblMsg.Name = "LblMsg"
        Me.LblMsg.Size = New System.Drawing.Size(55, 13)
        Me.LblMsg.TabIndex = 27
        Me.LblMsg.Text = "Estado: ---"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Location = New System.Drawing.Point(466, 267)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(109, 40)
        Me.btnSave.TabIndex = 26
        Me.btnSave.Text = "Guardar"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'cmbVersion
        '
        Me.cmbVersion.FormattingEnabled = True
        Me.cmbVersion.Location = New System.Drawing.Point(181, 204)
        Me.cmbVersion.Name = "cmbVersion"
        Me.cmbVersion.Size = New System.Drawing.Size(121, 21)
        Me.cmbVersion.TabIndex = 25
        '
        'txtPhoneNumberId
        '
        Me.txtPhoneNumberId.Location = New System.Drawing.Point(181, 132)
        Me.txtPhoneNumberId.Name = "txtPhoneNumberId"
        Me.txtPhoneNumberId.Size = New System.Drawing.Size(317, 20)
        Me.txtPhoneNumberId.TabIndex = 24
        Me.txtPhoneNumberId.Text = "410461155481036"
        '
        'txtUserAccessToken
        '
        Me.txtUserAccessToken.Location = New System.Drawing.Point(181, 32)
        Me.txtUserAccessToken.Multiline = True
        Me.txtUserAccessToken.Name = "txtUserAccessToken"
        Me.txtUserAccessToken.Size = New System.Drawing.Size(317, 60)
        Me.txtUserAccessToken.TabIndex = 23
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(104, 204)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 21)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Versión:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(54, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(121, 42)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "ID de Número " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "de Teléfono:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(35, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 21)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Token de Acceso:"
        '
        'WABAConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(610, 392)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextUrl)
        Me.Controls.Add(Me.LblMsg)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.cmbVersion)
        Me.Controls.Add(Me.txtPhoneNumberId)
        Me.Controls.Add(Me.txtUserAccessToken)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "WABAConfig"
        Me.Text = "Configuración de API Cloud"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnCancel As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents TextUrl As TextBox
    Friend WithEvents LblMsg As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents cmbVersion As ComboBox
    Friend WithEvents txtPhoneNumberId As TextBox
    Friend WithEvents txtUserAccessToken As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
