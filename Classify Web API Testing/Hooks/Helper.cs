using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Classify_Web_API_Testing.Hooks
{
    [Binding]
    internal class Helper
    {
        public static HttpClient HttpClient;
        public static string BaseURL;
        [BeforeTestRun]
        public static void TestSetup()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string BaseUrl = configuration.GetValue<string>("BaseUrl");
            BaseURL = BaseUrl;

            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(BaseUrl);

        }
    }
}
