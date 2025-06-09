using APIAutomation.Models.CompanyModel;
using APIAutomation.Models.EmployeeModel;
using DatacomQA.Alpha.Core.Models;

namespace APIAutomation.Models.Contexts
{
    public class EmployeeDataContexts : ReportContexts
    {
        public required GetAllEmployeesResponse GetAllEmployeesResponse;
        public required CreateEmployeeResponse CreateEmployeeResponse;
        public required Data ExpectedData;
        public required Datum ExpectedDatum;
        public required GetEmployeeResponse GetEmployeeResponse;
    }
}
