using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.Inheritance
{
    public class BasicPipeline
    {
        public void Process(BasicEvent basicEvent)
        {
            WriteLog($"Starting processing of event {basicEvent.Id}");
            Validate(basicEvent);
            WriteLog($"Processing {basicEvent.Id}");
            ProcessEvent(basicEvent);
            WriteLog($"Processing successfully completed for event {basicEvent.Id}");
        }

        private void ProcessEvent(BasicEvent basicEvent)
        {
            // Do processing stuff ..
        }

        protected void WriteLog(string message)
        {
            Console.WriteLine(message);
        }

        protected void Validate(BasicEvent basicEvent)
        {
            if (basicEvent == null)
            {
                throw new ArgumentNullException("Event object cannot be null");
            }
            if (string.IsNullOrWhiteSpace(basicEvent.Data))
            {
                throw new ArgumentException($"Event {basicEvent.Id} is invalid");
            }
        }
    }
}
