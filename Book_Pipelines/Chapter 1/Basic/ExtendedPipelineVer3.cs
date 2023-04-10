using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.Basic
{
    public class ExtendedPipelineVer3: BasicPipeline
    {
        public new void Process(BasicEvent basicEvent)
        {
            if(basicEvent is ExtendedEvent)
            {
                ProcessExtendedEvent(basicEvent as ExtendedEvent);
            }
            else
            {
                base.Process(basicEvent);
            }
        }

        private void ProcessExtendedEvent(ExtendedEvent extendedEvent)
        {
            Console.WriteLine("Processing extended event");
        }

        protected sealed override void Validate(BasicEvent basicEvent)
        {
            var extendedEvent = basicEvent as ExtendedEvent;
            if (extendedEvent != null)
            {
                if (string.IsNullOrWhiteSpace(extendedEvent.Source))
                {
                    throw new ArgumentException($"Event {basicEvent.Id} is invalid. Source cannot be null.");
                }
            }

            base.Validate(basicEvent);
        }
    }
}
