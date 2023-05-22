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
    public class ValidateProcessor : Processor
    {
        public string ValidateProcessorMessage { get { return "some special ValidateProccessor message"; } }

        public ValidateProcessor(Processor nextProcessor) : base(nextProcessor)
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
            
            var basicEvent = request as IUploadEventData;

            if (basicEvent.FileName == null)
                throw new PipelineProcessingException("Filename of the event cannot be null");
            if (basicEvent.FileType == null)
                throw new PipelineProcessingException("File Type of the event cannot be null");
            if (basicEvent.FileUrl == null)
                throw new PipelineProcessingException("File Url of the event cannot be null");


            base.Process(request);
        }
    }
}
