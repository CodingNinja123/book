using Book_Pipelines.Chapter7.Chain_Of_Responsibility.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Visitor.Chain
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

        }

        public void Visit(IoTProcessEventProcessor processor)
        {

        }
        public void Visit(IoTValidateProcessor processor)
        {

        }

        public void Visit(PreProcessProcessor processor)
        {

        }

        public void Visit(ProcessEventProcessor processor)
        {

        }

        public void Visit(SaveMetadataProcessor processor)
        {

        }

        public void Visit(SearchProcessor processor)
        {

        }
        public void Visit(StoreProcessor processor)
        {

        }

        public void Visit(UpdateMetadataProcessor processor)
        {

        }

        public void Visit(ValidateProcessor processor)
        {

        }

    }
}
