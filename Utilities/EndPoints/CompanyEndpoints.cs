using DatacomQA.Alpha.Core.Utilities;
using RestSharp;
using System.Net;
using WebAutomation.Config;
using APIAutomation.Models.EmployeeModel;
using APIAutomation.Models;
using System.Net.Http;
using APIAutomation.Models.CompanyModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace APIAutomation.Utilities.EndPoints
{
    public sealed class CompanyEndpoints : BaseAPI
    {

        public CompanyEndpoints(EndpointConfig config) : base(config)
        {
        }

        public HttpResponseMessage GetResponse(string id, string authToken,string baseUrl, string endpoint)
        {
            string url = $"{baseUrl}{endpoint}";
            var response = GetResponseWithId(id, url, authToken);
            return response;
        }

        public HttpResponseMessage CompanyGetResponseWithoutToken(string id,string baseUrl, string endpoint)
        {
            string url = $"{baseUrl}{endpoint}";
            var response = GetResponseWithoutToken(id, url);
            return response;
        }
    }
}
