using APIAutomation.Models.CompanyModel;
using DatacomQA.Alpha.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAutomation.Config;

namespace APIAutomation.Utilities
{
    public abstract class BaseAPI:Utils
    {
        private readonly HttpClient _httpClient;

       public BaseAPI(EndpointConfig config)
       {
            _httpClient = new HttpClient();
       }

        protected HttpResponseMessage GetResponseWithId(string id, string baseUrl, string authToken)
        {
            string endPoint = baseUrl.Replace("{Id}", id);
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", $"Bearer {authToken}");
            HttpResponseMessage response = _httpClient.Send(request);
            return response;
        }

        protected HttpResponseMessage GetResponseWithoutToken(string id, string baseUrl)
        {
            string endPoint = baseUrl.Replace("{Id}", id);
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(request);
            return response;
        }
    }
}
