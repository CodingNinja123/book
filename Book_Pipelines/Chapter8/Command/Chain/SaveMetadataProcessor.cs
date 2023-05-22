using Book_Pipelines.Chapter8.Command;
using Book_Pipelines.Chapter8.Command.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Chain_Of_Responsibility.Command
{
    public class SaveMetadataProcessor : Processor
    {
        public string SaveMetadataMessage { get { return "some special SaveMetadataProccessor message"; } }

        public SaveMetadataProcessor(Processor nextProcessor) : base(nextProcessor)
        {
        }
        public override void Accept(ProcessorVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }

        public override void Process(IBasicEvent request)
        {
            RegisterStep(request, "SAVE_METADATA");
            
            base.Process(request);
        }
    }
}
