using Book_Pipelines.Chapter7.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book_Pipelines.Chapter7.Observer.Exceptions;

namespace Book_Pipelines.Chapter7.Chain_Of_Responsibility.Observer
{
    public class ValidateProcessor : Processor
    {
        public ValidateProcessor(Processor nextProcessor) : base(nextProcessor)
        {
        }
        public override void Process(IBasicEvent request)
        {
            // RegisterStep(request, "EVENT_VALIDATION");
            Notify(request, "EVENT_VALIDATION");

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
