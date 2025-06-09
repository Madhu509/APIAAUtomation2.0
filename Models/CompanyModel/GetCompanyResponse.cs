using Newtonsoft.Json;
using System.Collections.Generic;

namespace APIAutomation.Models.CompanyModel
{
    public class GetCompanyDataResponse
    {
        [JsonProperty("companyCode")]
        public int CompanyCode { get; set; }

        [JsonProperty("businessName")]
        public required string BusinessName { get; set; }

        [JsonProperty("terminationDate")]
        public DateTime? TerminationDate { get; set; }
    }
}
