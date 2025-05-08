using System;
using System.IO;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using WcfWhatsAppAPIService.Utilities;

namespace WcfWhatsAppAPIService
{
    public class WhatsAppAPIs : IWhatsAppAPIs
    {
        public WhatsAppAPIs()
        {
            // Forzar TLS 1.2 para conexión segura con la API de Meta
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            try
            {
                // Inicializa parámetros desde base de datos
                DataBaseConfigChecker.EnsureInitialized();

                // Prueba de conexión
                using (var conn = new SqlConnection(DataBaseConfigChecker.ConnectionString))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar servicio: " + ex.Message);
            }
        }

        public string TemplateBillingMessage(string phoneNumber, string customerName, string fileNamePdf, string fileNameXml)
        {
            try
            {
                DataBaseConfigChecker.EnsureInitialized();

                if (string.IsNullOrEmpty(DataBaseConfigChecker.AccessToken))
                    throw new ApplicationException("Token de acceso no disponible.");

                return SendMessage(
                    DataBaseConfigChecker.AccessToken,
                    phoneNumber,
                    "efact_test", // Nombre de la plantilla
                    customerName,
                    fileNamePdf,
                    fileNameXml
                );
            }
            catch (Exception ex)
            {
                return $"Error en TemplateBillingMessage: {ex.Message}";
            }
        }

        private string SendMessage(string accessToken, string phoneNumber, string templateName, string recipientName, string fileNamePdf, string fileNameXml)
        {
            string apiUrl = $"{DataBaseConfigChecker.ApiUrl}/{DataBaseConfigChecker.Version}/{DataBaseConfigChecker.PhoneNumberId}/messages";

            var payload = new
            {
                messaging_product = "whatsapp",
                to = phoneNumber,
                type = "template",
                template = new
                {
                    name = templateName,
                    language = new { code = "es" },
                    components = new object[]
                    {
                        new
                        {
                            type = "body",
                            parameters = new object[]
                            {
                                new { type = "text", text = recipientName }
                            }
                        },
                        new
                        {
                            type = "button",
                            sub_type = "url",
                            index = "0",
                            parameters = new object[]
                            {
                                new { type = "text", text = fileNamePdf }
                            }
                        },
                        new
                        {
                            type = "button",
                            sub_type = "url",
                            index = "1",
                            parameters = new object[]
                            {
                                new { type = "text", text = fileNameXml }
                            }
                        }
                    }
                }
            };

            string jsonPayload = new JavaScriptSerializer().Serialize(payload);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers["Authorization"] = "Bearer " + accessToken;

            byte[] data = Encoding.UTF8.GetBytes(jsonPayload);
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException webEx)
            {
                using (StreamReader reader = new StreamReader(webEx.Response.GetResponseStream()))
                {
                    string errorResponse = reader.ReadToEnd();
                    LogError($"Error WhatsApp API: {errorResponse}");
                    return $"Error al enviar mensaje: {errorResponse}";
                }
            }

        }
        private void LogError(string message)
        {
            try
            {
                string logPath = @"C:\Logs\WcfService_errors.txt";
                string logMessage = $"[{DateTime.Now}] {message}{Environment.NewLine}";
                File.AppendAllText(logPath, logMessage);
            }
            catch
            {
                // Ignorar errores de logging
            }
        }
    }
}
