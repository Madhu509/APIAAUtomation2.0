using DatacomQA.Alpha.Core.Utilities;
using APIAutomation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;
using APIAutomation.Models.Contexts;
using APIAutomation.Config;
using APIAutomation.FeatureSteps;
using APIAutomation.Models.CompanyModel;
using WebAutomation.Config;
using APIAutomation.Utilities.EndPoints;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Net.Http;
using APIAutomation.Utilities;
using System.Reflection;

namespace APIAutomation.FeatureSteps
{
    [Binding]
    public class CommonSteps : Utils
    {
        private readonly ScenarioContext _scenarioContext;

        public CommonSteps(ScenarioContext scenarioContext, EndpointConfig endpointConfig)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"I verify (.*) HTTP response code")]
        public void ThenIVerifyHTTPResponseCode(int expectedStatusCode)
        {
            HttpResponseMessage response = (HttpResponseMessage)_scenarioContext["Response"];
            Assert.AreEqual(expectedStatusCode, (int)response.StatusCode, "HTTP response code should match.");
        }

        [Then(@"I verify '([^']*)' response message")]
        public void ThenIVerifyResponseMessage(string expectedErrorMessage)
        {
            HttpResponseMessage response = (HttpResponseMessage)_scenarioContext["Response"];
            Assert.AreEqual(expectedErrorMessage, response.StatusCode.ToString(), "HTTP response code should match.");
        }

        [Then(@"I verify the error message '([^']*)'")]
        public void ThenIVerifyTheErrorMessage(string message)
        {
            HttpResponseMessage response = (HttpResponseMessage)_scenarioContext["Response"];
            var content = response.Content.ReadAsStringAsync().Result; ;
            var jsonResponse = JObject.Parse(content);
            var detail = jsonResponse["detail"]?.ToString();
            Assert.AreEqual(message, detail);
        }
    }
}


