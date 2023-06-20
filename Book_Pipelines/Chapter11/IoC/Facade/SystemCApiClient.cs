using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class SystemCApiClient : ISystemCAPIClient
    {
        private string baseUrl = string.Empty;
        private readonly ITokenFactory tokenFactory;

        public SystemCApiClient(string baseUrl, ITokenFactory tokenFactory)
        {
            this.baseUrl = baseUrl;
            this.tokenFactory = tokenFactory;
        }

        public string ExecuteRequest(IoTData data)
        {
            var token = tokenFactory.GetToken(SystemType.SystemCApi);

            string jsonString = JsonSerializer.Serialize(data);
            Console.WriteLine($"SYSTM_C_API_CLIENT: Sending message to {baseUrl}. Message: {jsonString}");
            return "Success!";
        }
    }
}
