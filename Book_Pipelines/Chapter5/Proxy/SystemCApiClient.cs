using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.Proxy
{
    public class SystemCApiClient : ICommunicationClient<IoTData, string>
    {
        private string baseUrl = string.Empty;
        public SystemCApiClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public string ExecuteRequest(IoTData data)
        {
            var token = TokenFactory.GetToken(SystemType.SystemCApi);

            string jsonString = JsonSerializer.Serialize(data);
            Console.WriteLine($"SYSTEM_C_API_CLIENT: Sending message to {baseUrl}. Message: {jsonString}");
            return "Success!";
        }
    }
}
