using Book_Pipelines.Chapter7.Visitor.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Visitor.Logging
{
    public class LoggingDestinationDecorator : ILoggingDestination
    {
        private ILoggingDestination loggingDestination;

        public LoggingDestinationDecorator(ILoggingDestination loggingDestination)
        {
            this.loggingDestination = loggingDestination;   
        }

        public void Flush()
        {
            loggingDestination.Flush();
        }

        public void Initialize(Guid sessionsGuid)
        {
            loggingDestination.Initialize(sessionsGuid);
        }

        public bool IsSessionsStarted()
        {
            return loggingDestination.IsSessionsStarted();
        }

        public virtual void Log(string message)
        {
            loggingDestination.Log(message);
        }

        public void Reset()
        {
            loggingDestination.Reset();
        }
    }
}
