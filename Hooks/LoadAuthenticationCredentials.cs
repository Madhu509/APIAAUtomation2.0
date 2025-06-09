using APIAutomation.Models.TestDataModels;
using APIAutomation.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reqnroll;
using Constants = APIAutomation.Utilities.Constants;

namespace WebAutomation.Hooks
{
    [Binding]
    public sealed class LoadAuthenticationCredentials
    {
        private readonly EnvironmentConfig _environmentConfig;
        private readonly string UserCredentialsDirectory = "user_credentials";

        public LoadAuthenticationCredentials(EnvironmentConfig environmentConfig)
        {
            _environmentConfig = environmentConfig;
        }

        public AuthenticationCredentials LoadAuthCredentials(string fileAndUser)
        {
            string[] parts = fileAndUser.Split(':');
            string fileName;
            string user;
            AuthenticationCredentials authenticationCredentials;

            if (parts.Length == 2)
            {
                fileName = parts[0].Trim();
                user = parts[1].Trim();
                string filePath = FileSearchUtility.SearchFile($"{fileName}.json", Path.Combine($"{Constants.ProjectRoot}\\{Path.Combine(Constants.TestDataLocation, _environmentConfig.Name)}", UserCredentialsDirectory));

                var json = File.ReadAllText(filePath);
                var jObject = JObject.Parse(json);
                var userCredentials = jObject[user];

                if (userCredentials == null)
                {
                    throw new Exception($"User '{user}' not found in the credentials file.");
                }

                authenticationCredentials = userCredentials.ToObject<AuthenticationCredentials>()
                                            ?? throw new Exception("Failed to deserialize authentication credentials.");
            }
            else if (parts.Length == 1)
            {
                fileName = parts[0].Trim();
                string filePath = FileSearchUtility.SearchFile($"{fileName}.json", Path.Combine($"{Constants.ProjectRoot}\\{Path.Combine(Constants.TestDataLocation, _environmentConfig.Name)}", UserCredentialsDirectory));

                var json = File.ReadAllText(filePath);
                authenticationCredentials = JsonConvert.DeserializeObject<AuthenticationCredentials>(json)
                                            ?? throw new Exception("Failed to deserialize authentication credentials.");
            }
            else
            {
                throw new Exception("Test Data Format Error: No colon found in the string or no second part or there are multiple colons.");
            }

            return authenticationCredentials;
        }
    }
}
