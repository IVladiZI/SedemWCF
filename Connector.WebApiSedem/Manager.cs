using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Configuration;
using Model;
using Model.Identity;
using Model.Entities;
using Newtonsoft.Json;
using System.Text;

namespace Connector.WebApiSedem
{
    public class Manager
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string ApiSeg { get; set; }
        public string Api { get; set; }
        public string Autenticate { get; set; }
        public string NotificationQr { get; set; }

        public Manager()
        {
            Password = ConfigurationManager.AppSettings["Password"];
            UserName = ConfigurationManager.AppSettings["UserName"];
            ApiSeg = ConfigurationManager.AppSettings["ApiSeg"];
            Api = ConfigurationManager.AppSettings["Api"];
            Autenticate = ConfigurationManager.AppSettings["Autenticate"];
            NotificationQr = ConfigurationManager.AppSettings["NotificationQr"];
        }

        public async Task<Response<string>> LoginUser()
        {
            Response<string> responseEntity = new Response<string>();
            try
            {
                AuthenticationRequest authenticatioRequest = new AuthenticationRequest()
                {
                    UserName = UserName,
                    Password = Password
                };
                var request = JsonConvert.SerializeObject(authenticatioRequest);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiSeg);
                    var stringContent = new StringContent(request, Encoding.UTF8);
                    stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    using (var response = await client.PostAsync(Autenticate, stringContent))
                    {
                        using (var content = response.Content)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var result = await content.ReadAsStringAsync();
                                var segResponse = JsonConvert.DeserializeObject<Response<SegUsuarioDto>>(result);
                                if (segResponse == null)
                                {
                                    responseEntity.Message = "0003|Login";
                                    responseEntity.Succeeded = false;
                                }
                                {
                                    responseEntity.Data = segResponse.Data.JwToken;
                                    responseEntity.Succeeded = true;
                                }
                            }
                            else
                            {
                                responseEntity.Message = $"0004|LoginUser|{request}|||{JsonConvert.SerializeObject(response)}";
                                responseEntity.Succeeded = false;
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                responseEntity.Message = $"0003|CatchLogin|{ApiSeg}{Autenticate}|{JsonConvert.SerializeObject(e)}";
                responseEntity.Succeeded = false;
            }
            return responseEntity;
        }
        public async Task<Response<string>> GetNotificationQr(XmlObject XmlObject)
        {
            Response<string> responseEntity = new Response<string>();
            try
            {

                var request = JsonConvert.SerializeObject(XmlObject);
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Api);
                    var stringContent = new StringContent(request, Encoding.UTF8);
                    stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", XmlObject.Jwt);

                    using (var response = await client.PostAsync(NotificationQr, stringContent))
                    {
                        using (var content = response.Content)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var result = await content.ReadAsStringAsync();
                                var notificationQrResponse = JsonConvert.DeserializeObject<Response<string>>(result);
                                if (notificationQrResponse == null)
                                {
                                    responseEntity.Message = "0003|GetNotificationQr";
                                    responseEntity.Succeeded = false;
                                }
                                {
                                    responseEntity.Data = notificationQrResponse.Data;
                                    responseEntity.Succeeded = true;
                                }
                            }
                            else
                            {
                                responseEntity.Message = $"0004|GetNotificationQr|{request}|||{JsonConvert.SerializeObject(response)}";
                                responseEntity.Succeeded = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                responseEntity.Message = $"0003|CatchGetNotificationQr|{Api}{NotificationQr}|{JsonConvert.SerializeObject(e)}";
                responseEntity.Succeeded = false;
            }
            return responseEntity;
        }
    }
}
