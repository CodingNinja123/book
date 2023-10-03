using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter4.Flyweight
{
    public class SystemAApiClient : ICommunicationClient<string, string>
    {
        private string baseUrl = string.Empty;
        public SystemAApiClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }
        public async Task<string> ExecuteRequest(string data)
        {
            var token = TokenFactory.GetToken(SystemType.SystemAApi);
            Console.WriteLine($"Token received {token.TokenValue}");

            Console.WriteLine($"Sending message to {this.baseUrl}. Message: {data}");
            return await Task.FromResult(string.Empty);
        }
    }
}
