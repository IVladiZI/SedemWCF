using Connector.WebApiSedem;
using Model;
using Model.Entities;
using System;

namespace SedemWCF
{
    public class ServiceSedem : IServiceSedem
    {
        public string SedemNotificationQr(string xmlRequest)
        {
            try
            {
                Manager manager = new Manager();
                Response<string> response = manager.LoginUser().Result;
                if (!response.Succeeded)
                    return response.Message;

                XmlObject xmlObject = new XmlObject()
                {
                    XmlRequest = xmlRequest,
                    Jwt = response.Data
                };

                response = manager.GetNotificationQr(xmlObject).Result;
                if (!response.Succeeded)
                    return response.Message;

                return response.Data;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
