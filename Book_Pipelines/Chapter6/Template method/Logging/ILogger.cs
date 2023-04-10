using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod.Logging
{
    public class Logger
    {
        public ILoggingDestination Destination { get; set; }

        public Logger(ILoggingDestination destination)
        {
            Destination = destination;
        }
        
        public void StartSession(Guid logGuid)
        {
            this.Destination.Initialize(logGuid);
        }
        public void EndSession()
        {
            this.Destination.Flush();
            this.Destination.Reset();
        }
        public void Log(string message)
        {
            if (!Destination.IsSessionsStarted())
                throw new InvalidOperationException("Logging sessions is not initialized");

            this.Destination.Log(message);
        }
    }
}
