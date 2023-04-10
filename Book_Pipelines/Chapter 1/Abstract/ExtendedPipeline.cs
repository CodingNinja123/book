using Book_Pipelines.Chapter_1.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.Abstract
{
    public class ExtendedPipeline : AbstractBasicPipeline
    {
        protected override void ProcessEvent(BasicEvent basicEvent)
        {
            WriteLog($"Processing extended event: {basicEvent.Id}");
        }

        protected override void Validate(BasicEvent basicEvent)
        {
            base.Validate(basicEvent);

            var extendedEvent = basicEvent as ExtendedEvent;
            if (extendedEvent != null)
            {
                if (string.IsNullOrWhiteSpace(extendedEvent.Source))
                {
                    throw new ArgumentException($"Event {basicEvent.Id} is invalid. Source cannot be null.");
                }
            }
        }

        protected override void PostProcess(BasicEvent basicEvent)
        {
            WriteLog($"Executing post processing functionality for event: {basicEvent.Id}");
        }

        protected override void PreProcess(BasicEvent basicEvent)
        {
            WriteLog($"Executing pre processing functionality for event: {basicEvent.Id}");
        }
    }
}
