using Book_Pipelines.Chapter_1.Abstract;
using Book_Pipelines.Chapter_1.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.Generics
{
    public class ExtendedPipeline : GenericAbstractBasicPipeline<ExtendedEvent>
    {
        protected override void ProcessEvent(ExtendedEvent extendedEvent)
        {
            WriteLog($"Processing extended event: {extendedEvent.Id} from {extendedEvent.Source}");
        }

        protected override void Validate(ExtendedEvent extendedEvent)
        {
            base.Validate(extendedEvent);

            if (string.IsNullOrWhiteSpace(extendedEvent.Source))
            {
                throw new ArgumentException($"Event {extendedEvent.Id} is invalid. Source cannot be null.");
            }
        }

        protected override void PostProcess(ExtendedEvent extendedEvent)
        {
            WriteLog($"Executing post processing functionality for event: {extendedEvent.Id} from {extendedEvent.Source}");
        }

        protected override void PreProcess(ExtendedEvent extendedEvent)
        {
            WriteLog($"Executing pre processing functionality for event: {extendedEvent.Id} from {extendedEvent.Source}");
        }
    }
}
