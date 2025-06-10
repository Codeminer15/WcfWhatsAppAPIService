Public Class TokenErrorHandler
    Inherits Exception

    Public Sub New(message As String, ex As Exception)
        MyBase.New(message)
    End Sub

    Public Sub New()
        MyBase.New("El token de acceso proporcionado es inválido o ha expirado.")
    End Sub
End Class
