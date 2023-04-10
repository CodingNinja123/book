using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.Proxy
{
    public class DashboardNotificationClient : ICommunicationClient<string, string>
    {
        private string baseUrl;

        public DashboardNotificationClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public string ExecuteRequest(string request)
        {
            var token = TokenFactory.GetToken(SystemType.SystemDownload);

            Console.WriteLine($"DASHBOARD_CLIENT: {request}");
            return string.Empty;
        }
    }
}
