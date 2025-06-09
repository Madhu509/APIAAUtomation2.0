using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

public class OAuthUtility
{
    private readonly HttpClient _client;

    public OAuthUtility()
    {
        _client = new HttpClient();
    }

    public string GetAccessTokenFromPasswordGrant(string clientID, string clientSecret, string username, string password, string scope, string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        // Set the body content
        var requestBody = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", clientID),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>("scope", scope)
        };

        request.Content = new FormUrlEncodedContent(requestBody);

        // Set content type explicitly (although FormUrlEncodedContent sets it for you)
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        // Send the request
        HttpResponseMessage response = _client.SendAsync(request).Result;

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Request failed with status code: {response.StatusCode}. Reason: {response.ReasonPhrase}. Content: {response.Content.ReadAsStringAsync().Result}");
        }

        var responseContent = response.Content.ReadAsStringAsync().Result;

        // Check if response content is empty or null
        if (string.IsNullOrEmpty(responseContent))
        {
            throw new Exception("Response content is blank.");
        }

        // Deserialize the response content to TokenResponse object
        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

        // Check if accessToken is null or empty
        if (string.IsNullOrEmpty(tokenResponse?.AccessToken))
        {
            throw new Exception("Access token not found in the response.");
        }

        return tokenResponse.AccessToken;
    }
}
