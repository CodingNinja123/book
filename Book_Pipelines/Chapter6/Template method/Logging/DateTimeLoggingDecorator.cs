using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod.Logging
{
    public class DateLoggingDecorator: LoggingDestinationDecorator
    {
        public DateLoggingDecorator(ILoggingDestination loggingDestination) : base(loggingDestination)
        {
        }
        
        public override void Log(string message)
        {
            string dateTimeMessage = $"{DateTime.UtcNow.ToShortDateString()}: {message}";
            base.Log(dateTimeMessage);
        }
    }
}
