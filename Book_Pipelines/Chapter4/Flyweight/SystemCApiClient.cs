using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter4.Flyweight
{
    public class SystemCApiClient : ICommunicationClient<IoTData, string>
    {
        private string baseUrl = string.Empty;
        public SystemCApiClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<string> ExecuteRequest(IoTData data)
        {
            var token = TokenFactory.GetToken(SystemType.SystemCApi);
            Console.WriteLine($"Token received {token.TokenValue}");


            string jsonString = JsonSerializer.Serialize(data);
            Console.WriteLine($"Sending message to {this.baseUrl}. Message: {jsonString}");
            return await Task.FromResult("Success!");
        }
    }
}
