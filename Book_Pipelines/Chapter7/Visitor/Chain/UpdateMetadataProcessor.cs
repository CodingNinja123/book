using Book_Pipelines.Chapter7.Visitor;
using Book_Pipelines.Chapter7.Visitor.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Chain_Of_Responsibility.Visitor
{
    public class UpdateMetadataProcessor : Processor
    {
        public string UpdateMetadataMessage { get { return "some special UpdateMetadataProccessor message"; } }

        public UpdateMetadataProcessor(Processor nextProcessor) : base(nextProcessor)
        {
        }

        public override void Accept(ProcessorVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }

        public override void Process(IBasicEvent request)
        {
            RegisterStep(request, "UPDATE_METADATA");
            
            // Do nothing
            base.Process(request);
        }
    }
}
