using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.ChainOfResponsibility.Logging
{
    public class FileLogger : ILoggingDestination
    {
        private string basePath = "C:\\Users\\Timur\\source\\repos\\Book_Pipelines\\Logs";
        private List<string> messages = new List<string>();
        private Guid sessiongGuid = Guid.Empty;

        public void Flush()
        {
            File.AppendAllLines($"{basePath}\\{sessiongGuid}.txt", messages);
            messages.Clear();
        }

        public void Initialize(Guid sessionsGuid)
        {
            this.sessiongGuid = sessionsGuid;
        }

        public bool IsSessionsStarted()
        {
            return sessiongGuid != Guid.Empty;
        }

        public void Log(string message)
        {
            messages.Add(message);
        }

        public void Reset()
        {
            messages.Clear();
            sessiongGuid = Guid.Empty;
        }
    }
}
