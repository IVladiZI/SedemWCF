using System.ServiceModel;

namespace SedemWCF
{
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IServiceSedem
    {
        [OperationContract]
        string SedemNotificationQr(string xmlRequest);
    }
}
