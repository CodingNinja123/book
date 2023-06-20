using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.Facade
{
    public static class TokenFactory
    {
        private static Dictionary<SystemType,Token> tokens = new Dictionary<SystemType,Token>();
        public static Token GetToken(SystemType system)
        {
            if(tokens.ContainsKey(system) && tokens[system].ExpiresAt > DateTime.Now.AddMinutes(1))
            {
                return tokens[system];
            }

            // Imitating token request
            Thread.Sleep(200);

            tokens[system] = new Token
            {
                ExpiresAt = DateTime.Now.AddHours(2),
                TokenValue = Guid.NewGuid().ToString(),
                Type = system
            };

            return tokens[system];
        }
    }

    public class Token
    {   
        public SystemType Type { get; init; }
        public DateTime ExpiresAt { get; init; }
        public string TokenValue { get; init; }
    }

    public enum SystemType
    {
        None  = 0,
        SystemAApi = 1,
        SystemUpload = 2,
        SystemCApi = 3,
        SystemDownload = 4,
        DashboardApi = 5,
    }
}
