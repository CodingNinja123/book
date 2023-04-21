﻿using Book_Pipelines.Chapter6.ChainOfResponsibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book_Pipelines.Chapter6.ChainOfResponsibility.Exceptions;

namespace Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain
{
    public class ValidateProcessor : Processor
    {
        public ValidateProcessor(Processor nextProcessor) : base(nextProcessor)
        {
        }
        public override void Process(IBasicEvent request)
        {
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
