using Newtonsoft.Json;

public class TokenResponse
{
    [JsonProperty("access_token")]
    public required string AccessToken { get; set; }
}