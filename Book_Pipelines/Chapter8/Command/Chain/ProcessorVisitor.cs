using Book_Pipelines.Chapter8.Chain_Of_Responsibility.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Command.Chain
{
    public class ProcessorVisitor
    {
        public ProcessorVisitor()
        {
            this.Data = new List<string>();
        }

        public List<string> Data { get; set; }

        public void Visit(ExceptionHandlingProcessor processor)
        {
            this.Data.Add(processor.ExceptionHandlingMessage);
        }

        public void Visit(IoTProcessEventProcessor processor)
        {
            this.Data.Add(processor.IoTProcessEventMessage);
        }
        public void Visit(IoTValidateProcessor processor)
        {
            this.Data.Add(processor.IoTValidateMessage);
        }

        public void Visit(PreProcessProcessor processor)
        {
            this.Data.Add(processor.PreProcessMessage);
        }

        public void Visit(ProcessEventProcessor processor)
        {
            this.Data.Add(processor.ProcessEventMessage);
        }

        public void Visit(SaveMetadataProcessor processor)
        {
            this.Data.Add(processor.SaveMetadataMessage);
        }

        public void Visit(SearchProcessor processor)
        {
            this.Data.Add(processor.SearchMessage);
        }
        public void Visit(StoreProcessor processor)
        {
            this.Data.Add(processor.StoreMessage);
        }

        public void Visit(UpdateMetadataProcessor processor)
        {
            this.Data.Add(processor.UpdateMetadataMessage);
        }

        public void Visit(ValidateProcessor processor)
        {
            this.Data.Add(processor.ValidateProcessorMessage);
        }
    }
}
