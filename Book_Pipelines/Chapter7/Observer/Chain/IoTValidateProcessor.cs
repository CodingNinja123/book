using Book_Pipelines.Chapter7.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book_Pipelines.Chapter7.Observer.Exceptions;

namespace Book_Pipelines.Chapter7.Chain_Of_Responsibility.Observer
{
    public class IoTValidateProcessor : Processor
    {
        public IoTValidateProcessor(Processor nextProcessor) : base(nextProcessor)
        {
        }

        public override void Process(IBasicEvent request)
        {
            // RegisterStep(request, "EVENT_VALIDATION");
            Notify(request, "EVENT_VALIDATION");
            var basicEvent = request as IIoTEventData;

            if (basicEvent.Action == null)
                throw new PipelineProcessingException("Action of the event cannot be null");
            if (basicEvent.Value == null)
                throw new PipelineProcessingException("Value of the event cannot be null");

            base.Process(request);
        }
    }
}
