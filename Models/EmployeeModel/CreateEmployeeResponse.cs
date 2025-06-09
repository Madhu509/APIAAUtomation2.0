namespace APIAutomation.Models
{
    public class CreateEmployeeResponse
    {
        public string status { get; set; } = string.Empty;
        public required Data data { get; set; }
        public string message { get; set; } = string.Empty;
    }

    public class Data
    {
        public string name { get; set; } = string.Empty;
        public string salary { get; set; } = string.Empty;
        public string age { get; set; } = string.Empty;
        public int id { get; set; }
    }
}
