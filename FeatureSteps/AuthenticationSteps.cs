using APIAutomation.Config;
using APIAutomation.Models.CompanyModel;
using APIAutomation.Utilities;
using System;
using Reqnroll;
using WebAutomation.Config;
using WebAutomation.Hooks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace APIAutomation.FeatureSteps
{
    [Binding]
    public class AuthenticationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly EnvironmentConfig _environmentConfig;
        private readonly IdentityServerConfig _identityServerConfig;

        public AuthenticationSteps(ScenarioContext scenarioContext,
            EnvironmentConfig environmentConfig,
            IdentityServerConfig identityServerConfig)
        {
            _scenarioContext = scenarioContext;
            _environmentConfig = environmentConfig;
            _identityServerConfig = identityServerConfig;
        }

        [Given(@"I obtained the access token using '([^']*)'")]
        public void GivenIObtainedTheAccessTokenUsing(string user)
        {
            var loadAuthCredentials = new LoadAuthenticationCredentials(_environmentConfig);
            var loadedAuthCredentials = loadAuthCredentials.LoadAuthCredentials(user);
            _scenarioContext["UserId"] = loadedAuthCredentials.ClientID;
            _scenarioContext["FirstName"] = loadedAuthCredentials.FirstName;
            _scenarioContext["LastName"] = loadedAuthCredentials.LastName;
            _scenarioContext["CompanyCode"] = loadedAuthCredentials.CompanyCode;
            _scenarioContext["CompanyName"] = loadedAuthCredentials.CompanyName;
            _scenarioContext["Description"] = loadedAuthCredentials.Description;
            _scenarioContext["EmployeeId"] = loadedAuthCredentials.EmployeeId;
            _scenarioContext["EmployeeCode"] = loadedAuthCredentials.EmployeeCode;
            _scenarioContext["Username"] = loadedAuthCredentials.Username;

            try
            {
                var oAuth2Utility = new OAuthUtility();

                string accessToken = oAuth2Utility.GetAccessTokenFromPasswordGrant
                    (loadedAuthCredentials.ClientID,
                    loadedAuthCredentials.ClientSecret,
                    loadedAuthCredentials.Username,
                    loadedAuthCredentials.Password,
                    loadedAuthCredentials.Scope,
                    _identityServerConfig.TokenUrl);

                _scenarioContext["AccessToken"] = accessToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
