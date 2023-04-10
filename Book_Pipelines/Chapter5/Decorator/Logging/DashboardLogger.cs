using Book_Pipelines.Chapter5.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.Decorator.Logging
{
    public class DashboardLogger : ILoggingDestination
    {
        private DashboardNotificationClient dashboardNotificationClient = new DashboardNotificationClient("");
        private Guid sessionGuid = Guid.Empty;

        public void Flush()
        {
            // do nothing
        }

        public void Initialize(Guid sessionsGuid)
        {
            this.sessionGuid = sessionsGuid;
        }

        public bool IsSessionsStarted()
        {
            return sessionGuid != Guid.Empty;
        }

        public void Log(string message)
        {
            dashboardNotificationClient.ExecuteRequest(message);
        }

        public void Reset()
        {
            sessionGuid = Guid.Empty;
        }
    }
}
