<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ButtonsForm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIdCliente = New System.Windows.Forms.TextBox()
        Me.BtnSetting = New System.Windows.Forms.Button()
        Me.BtnSendMessageWindow = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(76, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Id Cliente:"
        '
        'txtIdCliente
        '
        Me.txtIdCliente.Location = New System.Drawing.Point(136, 31)
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(100, 20)
        Me.txtIdCliente.TabIndex = 6
        '
        'BtnSetting
        '
        Me.BtnSetting.BackColor = System.Drawing.Color.RoyalBlue
        Me.BtnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSetting.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.BtnSetting.Location = New System.Drawing.Point(123, 121)
        Me.BtnSetting.Name = "BtnSetting"
        Me.BtnSetting.Size = New System.Drawing.Size(115, 47)
        Me.BtnSetting.TabIndex = 5
        Me.BtnSetting.Text = "Configuración"
        Me.BtnSetting.UseVisualStyleBackColor = False
        '
        'BtnSendMessageWindow
        '
        Me.BtnSendMessageWindow.BackColor = System.Drawing.Color.LimeGreen
        Me.BtnSendMessageWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSendMessageWindow.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSendMessageWindow.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.BtnSendMessageWindow.Location = New System.Drawing.Point(123, 68)
        Me.BtnSendMessageWindow.Name = "BtnSendMessageWindow"
        Me.BtnSendMessageWindow.Size = New System.Drawing.Size(115, 47)
        Me.BtnSendMessageWindow.TabIndex = 4
        Me.BtnSendMessageWindow.Text = "WhatsApp"
        Me.BtnSendMessageWindow.UseVisualStyleBackColor = False
        '
        'ButtonsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(383, 268)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtIdCliente)
        Me.Controls.Add(Me.BtnSetting)
        Me.Controls.Add(Me.BtnSendMessageWindow)
        Me.Name = "ButtonsForm"
        Me.Text = "Sistema GESEM"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtIdCliente As TextBox
    Friend WithEvents BtnSetting As Button
    Friend WithEvents BtnSendMessageWindow As Button
End Class
