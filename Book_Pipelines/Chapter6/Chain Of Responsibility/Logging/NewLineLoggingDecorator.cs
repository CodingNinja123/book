using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.ChainOfResponsibility.Logging
{
    public class NewLineLoggingDecorator : LoggingDestinationDecorator
    {
        public NewLineLoggingDecorator(ILoggingDestination loggingDestination) : base(loggingDestination)
        {
        }

        public override void Log(string message)
        {
            string dateTimeMessage = $"{message}{Environment.NewLine}";
            base.Log(dateTimeMessage);
        }
    }
}
