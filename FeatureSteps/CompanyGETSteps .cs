using DatacomQA.Alpha.Core.Utilities;
using APIAutomation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;
using Reqnroll.Assist.Dynamic;
using APIAutomation.Models.Contexts;
using APIAutomation.Config;
using APIAutomation.FeatureSteps;
using APIAutomation.Models.CompanyModel;
using WebAutomation.Config;
using APIAutomation.Utilities.EndPoints;
using Newtonsoft.Json.Linq;
using Reqnroll.Assist;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Net.Http;
using APIAutomation.Utilities;
using System.Reflection;

namespace APIAutomation.FeatureSteps
{
    [Binding]
    public class CompanyGETSteps : Utils
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CompanyEndpoints _companyEndpoints;
        private readonly EndpointConfig _endpointConfig;

        public CompanyGETSteps(ScenarioContext scenarioContext, EndpointConfig endpointConfig)
        {
            _scenarioContext = scenarioContext;
            _endpointConfig = endpointConfig;
            _companyEndpoints = new CompanyEndpoints(endpointConfig);
        }

        [When(@"I send a GET request to Get company Id endpoint to an invalid company code '([^']*)'")]
        [When(@"I send a GET request to Get company Id endpoint to a valid company code '([^']*)'")]
        public void WhenISendAGETRequestToGetCompanyIdEndpointWithAValidCompanyCode(string companyCode)
        {
            var response = _companyEndpoints.GetResponse(companyCode, (string)_scenarioContext["AccessToken"], _endpointConfig.BaseUrl, Constants.GET_COMPANY_ID);
            _scenarioContext["Response"] = response;
        }

        [When(@"I send a GET request to Get company Id endpoint without access token to a valid company code '([^']*)'")]
        public void WhenISendAGETRequestToEndpointWithoutAccessToken(string companyCode)
        {
            var response = _companyEndpoints.CompanyGetResponseWithoutToken(companyCode, _endpointConfig.BaseUrl, Constants.GET_COMPANY_ID);
            _scenarioContext["Response"] = response;
        }

        [Then(@"I verify the company data is correct")]
        public void ThenIVerifyTheCompanyDataIsCorrect(Table table)
        {
            IEnumerable<dynamic> data = table.CreateDynamicSet();

            HttpResponseMessage response = (HttpResponseMessage)_scenarioContext["Response"];
            var content = response.Content.ReadAsStringAsync().Result;
            GetCompanyDataResponse listCompanyData = DeserializeJson<GetCompanyDataResponse>(content ?? throw new Exception("Response content is null"));

            foreach (dynamic row in data)
            {
                string companyName = row.CompanyName;
                string companyCode = row.CompanyCode.ToString();
                string terminationDate = row.TerminationDate;

                Assert.AreEqual(companyName, listCompanyData.BusinessName);
                Assert.AreEqual(companyCode, listCompanyData.CompanyCode.ToString());
                Assert.AreEqual(terminationDate, listCompanyData.TerminationDate.ToString());
            }
        }    
    }
}


