using System.Configuration;

namespace WebAutomation.Config
{
    public sealed class EndpointConfig
    {
        public required string EmployeeAPIBaseUrl { get; set; }
        public required string BaseUrl { get; set; }
    }
}
