using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfWhatsAppAPIService
{
    [ServiceContract] // Contrato de servicio WCF
    public interface IWhatsAppAPIs
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "TemplateBillingMessage",
            RequestFormat = WebMessageFormat.Json,// El cliente debe enviar los datos en formato JSON
            ResponseFormat = WebMessageFormat.Json,// La respuesta de los datos en formato JSON
            BodyStyle = WebMessageBodyStyle.Wrapped)]// Los parámetros irán envueltos en un solo objeto JSON
        string TemplateBillingMessage(string phoneNumber, string customerName, string fileNamePdf, string fileNameXml);
    }
}
