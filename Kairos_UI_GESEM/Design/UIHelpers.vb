Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class UIHelpers

    ' =========================
    ' == Colores Corporativos
    ' =========================
    Public Shared ReadOnly PrimaryColor As Color = ColorTranslator.FromHtml("#07418C")
    Public Shared ReadOnly SecondaryColor As Color = ColorTranslator.FromHtml("#0A58CA")
    Public Shared ReadOnly WhatsAppGreen As Color = Color.FromArgb(37, 211, 102)
    Public Shared ReadOnly DangerColor As Color = Color.FromArgb(220, 53, 69)
    Public Shared ReadOnly BackgroundColor As Color = Color.FromArgb(239, 248, 255)
    Public Shared ReadOnly TitleBarColor As Color = PrimaryColor
    Public Shared ReadOnly TitleTextColor As Color = Color.White

    ' =========================
    ' == Formulario Base
    ' =========================
    Public Shared Sub ApplyFormLayout(form As Form, Optional title As String = "")
        form.BackColor = BackgroundColor
        form.Font = New Font("Segoe UI", 9)
        form.FormBorderStyle = FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        form.StartPosition = FormStartPosition.CenterScreen
        form.Text = ""

        If Not String.IsNullOrEmpty(title) Then
            AddCustomTitleBar(form, title)
        End If
    End Sub

    ' =========================
    ' == Título personalizado
    ' =========================
    <DllImport("user32.dll")> Private Shared Function ReleaseCapture() As Boolean : End Function
    <DllImport("user32.dll")> Private Shared Function SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer) As IntPtr : End Function
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = &H2

    Private Shared Sub AddCustomTitleBar(form As Form, titleText As String)
        Dim titleBar As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 40,
            .BackColor = TitleBarColor,
            .Padding = New Padding(5)
        }

        Dim titleLabel As New Label With {
            .Text = titleText,
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = TitleTextColor,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter
        }

        Dim closeButton As New Button With {
            .Text = "X",
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = TitleBarColor,
            .ForeColor = TitleTextColor,
            .Dock = DockStyle.Right,
            .Width = 40,
            .Cursor = Cursors.Hand
        }

        closeButton.FlatAppearance.BorderSize = 0

        AddHandler closeButton.Click, Sub() form.Close()
        AddHandler titleBar.MouseDown, Sub(s, e) If e.Button = MouseButtons.Left Then ReleaseCapture() : SendMessage(form.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
        AddHandler titleLabel.MouseDown, Sub(s, e) If e.Button = MouseButtons.Left Then ReleaseCapture() : SendMessage(form.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
        AddHandler closeButton.MouseEnter, AddressOf CloseButton_MouseEnter
        AddHandler closeButton.MouseLeave, AddressOf CloseButton_MouseLeave

        titleBar.Controls.Add(titleLabel)
        titleBar.Controls.Add(closeButton)
        form.Controls.Add(titleBar)

        For Each ctrl As Control In form.Controls
            If ctrl IsNot titleBar Then ctrl.Top += titleBar.Height
        Next
    End Sub
    Private Shared Sub CloseButton_MouseEnter(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.BackColor = Color.FromArgb(200, 0, 0)
        btn.ForeColor = Color.White
    End Sub

    Private Shared Sub CloseButton_MouseLeave(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.BackColor = TitleBarColor
        btn.ForeColor = TitleTextColor
    End Sub


    ' =========================
    ' == Botones Base
    ' =========================
    Public Shared Sub StylePrimaryButton(btn As Button)
        StyleButton(btn, PrimaryColor)
    End Sub

    Public Shared Sub StyleDangerButton(btn As Button)
        StyleButton(btn, DangerColor)
    End Sub

    Public Shared Sub StyleSecondaryButton(btn As Button)
        StyleButton(btn, SecondaryColor)
    End Sub

    Private Shared Sub StyleButton(btn As Button, backColor As Color)
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.BackColor = backColor
        btn.ForeColor = Color.White
        btn.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btn.Cursor = Cursors.Hand
        btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor, 0.2)
        btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor, 0.2)
        btn.Padding = New Padding(5)
    End Sub

    ' =========================
    ' == Botones con Imagen
    ' =========================
    Public Shared Sub StyleButtonWithImage(btn As Button, backColor As Color, imageName As String, iconSize As Integer, Optional buttonText As String = "")
        StyleButton(btn, backColor)

        btn.Text = buttonText
        btn.TextAlign = ContentAlignment.MiddleRight
        btn.ImageAlign = ContentAlignment.MiddleLeft
        btn.TextImageRelation = TextImageRelation.ImageBeforeText
        btn.Padding = New Padding(15, 0, 15, 0)

        Try
            Dim imagePath As String = Path.Combine(Application.StartupPath, "Pictures")
            imagePath = Path.Combine(imagePath, imageName)

            If File.Exists(imagePath) Then
                Using originalImage As Image = Image.FromFile(imagePath)
                    Dim scale As Single = Math.Min(iconSize / originalImage.Width, iconSize / originalImage.Height)
                    Dim newWidth As Integer = CInt(originalImage.Width * scale)
                    Dim newHeight As Integer = CInt(originalImage.Height * scale)

                    Dim resizedImage As New Bitmap(newWidth, newHeight)
                    Using g As Graphics = Graphics.FromImage(resizedImage)
                        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                        g.DrawImage(originalImage, 0, 0, newWidth, newHeight)
                    End Using

                    btn.Image = resizedImage
                End Using
            End If
        Catch ex As Exception
            Debug.WriteLine("Error cargando imagen del botón: " & ex.Message)
        End Try
    End Sub

    ' =========================
    ' == TextBox
    ' =========================
    Public Shared Sub StyleTextBox(txt As TextBox)
        txt.BorderStyle = BorderStyle.FixedSingle
        txt.BackColor = Color.White
        txt.ForeColor = Color.FromArgb(64, 64, 64)

        AddHandler txt.Enter, Sub() txt.BackColor = Color.FromArgb(240, 248, 255)
        AddHandler txt.Leave, Sub() txt.BackColor = Color.White
    End Sub

    ' =========================
    ' == ComboBox
    ' =========================
    Public Shared Sub StyleComboBox(cbo As ComboBox)
        Dim selectedItem = If(cbo.SelectedIndex >= 0, cbo.SelectedItem, Nothing)
        Dim items As New List(Of Object)(cbo.Items.Cast(Of Object))

        cbo.FlatStyle = FlatStyle.Flat
        cbo.BackColor = Color.White
        cbo.ForeColor = Color.FromArgb(64, 64, 64)
        cbo.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        cbo.DropDownStyle = ComboBoxStyle.DropDownList

        cbo.BeginUpdate()
        cbo.Items.Clear()
        cbo.Items.AddRange(items.ToArray())
        If selectedItem IsNot Nothing AndAlso cbo.Items.Contains(selectedItem) Then
            cbo.SelectedItem = selectedItem
        ElseIf cbo.Items.Count > 0 Then
            cbo.SelectedIndex = 0
        End If
        cbo.EndUpdate()

        AddHandler cbo.Enter, AddressOf ComboBox_Enter
        AddHandler cbo.Leave, AddressOf ComboBox_Leave
    End Sub

    Private Shared Sub ComboBox_Enter(sender As Object, e As EventArgs)
        Dim cbo As ComboBox = DirectCast(sender, ComboBox)
        cbo.BackColor = Color.FromArgb(240, 248, 255)
        cbo.ForeColor = Color.Black
    End Sub

    Private Shared Sub ComboBox_Leave(sender As Object, e As EventArgs)
        Dim cbo As ComboBox = DirectCast(sender, ComboBox)
        cbo.BackColor = Color.White
        cbo.ForeColor = Color.FromArgb(64, 64, 64)
    End Sub

    ' =========================
    ' == ToolTip
    ' =========================
    Public Shared Sub AddToolTip(control As Control, text As String)
        Dim tip As New ToolTip With {
            .ToolTipTitle = "Información",
            .IsBalloon = True
        }
        tip.SetToolTip(control, text)
    End Sub

End Class

