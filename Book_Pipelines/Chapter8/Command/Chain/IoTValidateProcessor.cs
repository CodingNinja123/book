using Book_Pipelines.Chapter8.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book_Pipelines.Chapter8.Command.Exceptions;
using Book_Pipelines.Chapter8.Command.Chain;

namespace Book_Pipelines.Chapter8.Chain_Of_Responsibility.Command
{
    public class IoTValidateProcessor : Processor
    {
        public string IoTValidateMessage { get { return "some special IoTValidateProccessor message"; } }
        public IoTValidateProcessor(Processor nextProcessor) : base(nextProcessor)
        {
        }
        public override void Accept(ProcessorVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }
        public override void Process(IBasicEvent request)
        {
            RegisterStep(request, "EVENT_VALIDATION");
            var basicEvent = request as IIoTEventData;

            if (basicEvent.Action == null)
                throw new PipelineProcessingException("Action of the event cannot be null");
            if (basicEvent.Value == null)
                throw new PipelineProcessingException("Value of the event cannot be null");

            base.Process(request);
        }
    }
}
