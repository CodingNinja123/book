using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class DashboardNotificationClient : IDashboardNotificationClient
    {
        private string baseUrl;
        private readonly ITokenFactory tokenFactory;

        public DashboardNotificationClient(string baseUrl, ITokenFactory tokenFactory)
        {
            this.baseUrl = baseUrl;
            this.tokenFactory = tokenFactory;
        }

        public string ExecuteRequest(string request)
        {
            var token = tokenFactory.GetToken(SystemType.SystemDownload);

            Console.WriteLine($"DASHBOARD_CLIENT: {request}");
            return string.Empty;
        }
    }
}
