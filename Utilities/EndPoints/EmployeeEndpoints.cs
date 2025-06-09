using DatacomQA.Alpha.Core.Utilities;
using RestSharp;
using System.Net;
using WebAutomation.Config;
using APIAutomation.Models.EmployeeModel;
using APIAutomation.Models;
using System.Net.Http;
using APIAutomation.Models.CompanyModel;

namespace APIAutomation.Utilities.EndPoints
{
    public sealed class EmployeeEndpoints : Utils
    {
        private readonly EndpointConfig _config;
        private readonly HttpClient _httpClient;

        public EmployeeEndpoints(EndpointConfig config)
        {
            _config = config;
            _httpClient = new HttpClient();
        }

        public GetAllEmployeesResponse GetAllEmployees()
        {
            var client = new RestClient(_config.EmployeeAPIBaseUrl);
            var request = new RestRequest("employees", Method.Get);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = SendRequest(client, request);

            return DeserializeJson<GetAllEmployeesResponse>(response?.Content ?? throw new Exception("Response content is null"));
        }

        public CreateEmployeeResponse CreateEmployee(Data data)
        {
            var body = SerializeJson(data);

            var client = new RestClient(_config.EmployeeAPIBaseUrl);
            var request = new RestRequest("create", Method.Post);

            request.AddHeader("Content-Type", "application/json,text/plain");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("undefined", body, ParameterType.RequestBody);

            var response = SendRequest(client, request);

            return DeserializeJson<CreateEmployeeResponse>(response?.Content ?? throw new Exception("Response content is null"));
        }

        public GetEmployeeResponse GetEmployee(int id)
        {
            var client = new RestClient(_config.EmployeeAPIBaseUrl);
            var request = new RestRequest($"employee/{id}", Method.Get);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var response = SendRequest(client, request);

            return DeserializeJson<GetEmployeeResponse>(response?.Content ?? throw new Exception("Response content is null"));
        }

        private RestResponse? SendRequest(RestClient client, RestRequest request)
        {
            try
            {
                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response;
                }
                else
                {
                    new Exception($"[ERROR] The request returned an error code '{response.StatusCode}' and error message '{response.ErrorMessage}'.");
                }
            }
            catch (Exception e)
            {
                new Exception("[FATAL] Failed to send request. Exception: " + e.StackTrace);
            }

            return null;
        }
    }
}
