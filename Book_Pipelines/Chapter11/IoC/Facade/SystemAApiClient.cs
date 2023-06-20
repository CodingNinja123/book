using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class SystemAApiClient : ISystemAApiClient
    {
        private string baseUrl = string.Empty;
        private readonly ITokenFactory tokenFactory;

        public SystemAApiClient(string baseUrl, ITokenFactory tokenFactory)
        {
            this.baseUrl = baseUrl;
            this.tokenFactory = tokenFactory;
        }
        public string ExecuteRequest(string data)
        {
            var token = tokenFactory.GetToken(SystemType.SystemAApi);

            Console.WriteLine($"SYSTEM_A_API_CLIENT: Sending message to {baseUrl}. Message: {data}");
            return string.Empty;
        }
    }

    public class SystemASearchApiClient : SystemAApiClient, ISystemASearchApiClient
    {
        public SystemASearchApiClient(string baseUrl, ITokenFactory tokenFactory) : base(baseUrl, tokenFactory)
        {

        }
    }

    public class SystemAStoreApiClient: SystemAApiClient, ISystemAStoreApiClient
    {
        public SystemAStoreApiClient(string baseUrl, ITokenFactory tokenFactory): base(baseUrl, tokenFactory)
        {

        }
    }
}
