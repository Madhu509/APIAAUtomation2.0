namespace APIAutomation.Models.EmployeeModel
{
    public class GetEmployeeResponse
    {
        public string status { get; set; } = string.Empty;
        public required Datum data { get; set; }
        public string message { get; set; } = string.Empty;
    }
}
