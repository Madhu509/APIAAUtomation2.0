using System;
using System.IO;

namespace APIAutomation.Utilities
{
    internal static class Constants
    {
        public static string ProjectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

        public static string TestDataLocation = "\\Resources\\TestData";

        public const string GET_COMPANY_ID = "/api/company/{Id}";
    }
}
