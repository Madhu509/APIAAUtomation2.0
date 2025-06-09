using System.Collections.Generic;

namespace APIAutomation.Models.EmployeeModel
{
    public class GetAllEmployeesResponse
    {
        public string status { get; set; } = string.Empty;
        public required List<Datum> data { get; set; }
        public string message { get; set; } = string.Empty;
    }

    public class Datum
    {
        public int id { get; set; }
        public string employee_name { get; set; } = string.Empty;
        public int employee_salary { get; set; }
        public int employee_age { get; set; }
        public string profile_image { get; set; } = string.Empty;
    }

}
