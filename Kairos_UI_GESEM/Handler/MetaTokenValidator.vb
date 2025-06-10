Imports System.Runtime.Serialization.Json
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Text

Public Class MetaTokenValidator
    Public Shared Function ValidateToken(token As String, apiVersion As String) As Boolean
        Dim url As String = $"https://graph.facebook.com/{apiVersion}/me?access_token={token}"

        Try
            Dim request As Net.HttpWebRequest = Net.HttpWebRequest.Create(url)
            request.Method = "GET"
            request.Timeout = 5000

            Using response As Net.HttpWebResponse = CType(request.GetResponse(), Net.HttpWebResponse)
                Using stream = response.GetResponseStream()
                    Dim serializer As New DataContractJsonSerializer(GetType(MetaResponse))
                    Dim metaResponse = CType(serializer.ReadObject(stream), MetaResponse)
                    Return Not String.IsNullOrEmpty(metaResponse.id)
                End Using
            End Using

        Catch ex As Exception
            ' Lanza tu excepción personalizada con información de error
            Throw New TokenErrorHandler("Token inválido o error en la validación.", ex)
        End Try
    End Function

    ' Clase decorada para deserializar correctamente la respuesta JSON de Meta
    <DataContract>
    Private Class MetaResponse
        <DataMember(Name:="id")>
        Public Property id As String

        <DataMember(Name:="name")>
        Public Property name As String
    End Class
End Class
