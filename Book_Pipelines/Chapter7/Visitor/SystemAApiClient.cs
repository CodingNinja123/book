using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Visitor
{
    public class SystemAApiClient : ICommunicationClient<string, string>
    {
        private string baseUrl = string.Empty;
        public SystemAApiClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }
        public string ExecuteRequest(string data)
        {
            var token = TokenFactory.GetToken(SystemType.SystemAApi);

            Console.WriteLine($"SYSTEM_A_API_CLIENT: Sending message to {baseUrl}. Message: {data}");
            return string.Empty;
        }
    }
}
