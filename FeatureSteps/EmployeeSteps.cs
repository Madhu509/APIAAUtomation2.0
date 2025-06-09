using DatacomQA.Alpha.Core.Utilities;
using APIAutomation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;
using APIAutomation.Models.Contexts;
using APIAutomation.Utilities.EndPoints;

namespace APIAutomation.FeatureSteps
{
    [Binding]
    public class EmployeeSteps : Utils
    {
        private readonly EmployeeDataContexts _contexts;
        private readonly EmployeeEndpoints _endpoints;

        public EmployeeSteps(EmployeeDataContexts contexts, EmployeeEndpoints endpoints) : base(contexts)
        {
            _contexts = contexts;
            _endpoints = endpoints;
        }

        [Given(@"an employee data exists in the database")]
        public void GivenAnEmployeeDataExistsInTheDatabase()
        {
            _contexts.GetAllEmployeesResponse = _endpoints.GetAllEmployees();

            Assert.IsTrue(_contexts.GetAllEmployeesResponse.data.Count > 0, "[ERROR] No employee data exists in the database.");

            _contexts.ExpectedDatum = _contexts.GetAllEmployeesResponse.data[RandomInt(_contexts.GetAllEmployeesResponse.data.Count)];

            Log(_contexts.ExpectedDatum, "Existing employee data");
        }

        [When(@"I send a request to '(.*)' employee data")]
        public void WhenISendARequestToEmployeeData(string request)
        {
            switch (request)
            {
                case "get the list of all":
                    {
                        _contexts.GetAllEmployeesResponse = _endpoints.GetAllEmployees();

                        break;
                    }
                case "create a new":
                    {
                        _contexts.ExpectedData = new Data
                        {
                            name = "User " + Guid.NewGuid(),
                            salary = RandomInt(100000).ToString(),
                            age = RandomInt(100).ToString()
                        };

                        Log(_contexts.ExpectedData, "Randomized employee data");

                        _contexts.CreateEmployeeResponse = _endpoints.CreateEmployee(_contexts.ExpectedData);

                        Log(_contexts.CreateEmployeeResponse.data, "Actual employee data");

                        break;
                    }
                case "get details of a single":
                    {
                        _contexts.GetEmployeeResponse = _endpoints.GetEmployee(_contexts.ExpectedDatum.id);

                        Log(_contexts.GetEmployeeResponse.data, "Actual employee data");

                        break;

                    }
                default:
                    break;
            }
        }

        [Then(@"the '(.*)' response should be correct")]
        public void ThenTheResponseShouldBeCorrect(string request)
        {
            switch (request)
            {
                case "get the list of all employee data":
                    {
                        Assert.AreEqual(_contexts.GetAllEmployeesResponse.status, "success", "[ERROR] The request is not successful.");
                        Assert.AreEqual(_contexts.GetAllEmployeesResponse.message, "Successfully! All records has been fetched.", "[ERROR] Received a wrong success message.");
                        Assert.IsTrue(_contexts.GetAllEmployeesResponse.data.Count > 0, "[ERROR] The request did not return any employee record.");

                        break;
                    }
                case "create employee data":
                    {

                        Assert.AreEqual(_contexts.CreateEmployeeResponse.status, "success", "[ERROR] The request is not successful.");
                        Assert.AreEqual(_contexts.CreateEmployeeResponse.message, "Successfully! Record has been added.", "[ERROR] Received a wrong success message.");

                        Assert.IsNotNull(_contexts.CreateEmployeeResponse.data.id, "[ERROR] Newly created employee did not have an id.");

                        Assert.AreEqual(_contexts.CreateEmployeeResponse.data.name, _contexts.ExpectedData.name, "[ERROR] Employee name miscompare.");
                        Assert.AreEqual(_contexts.CreateEmployeeResponse.data.salary, _contexts.ExpectedData.salary, "[ERROR] Employee salary miscompare.");
                        Assert.AreEqual(_contexts.CreateEmployeeResponse.data.age, _contexts.ExpectedData.age, "[ERROR] Employee age miscompare.");

                        break;
                    }
                case "get employee data":
                    {
                        Assert.AreEqual(_contexts.GetEmployeeResponse.data.id, _contexts.ExpectedDatum.id, "[ERROR] Employee id miscompare.");
                        Assert.AreEqual(_contexts.GetEmployeeResponse.data.employee_name, _contexts.ExpectedDatum.employee_name, "[ERROR] Employee name miscompare.");
                        Assert.AreEqual(_contexts.GetEmployeeResponse.data.employee_salary, _contexts.ExpectedDatum.employee_salary, "[ERROR] Employee salary miscompare.");
                        Assert.AreEqual(_contexts.GetEmployeeResponse.data.employee_age, _contexts.ExpectedDatum.employee_age, "[ERROR] Employee age miscompare.");

                        break;
                    }
                default:
                    break;
            }
        }
    }
}
