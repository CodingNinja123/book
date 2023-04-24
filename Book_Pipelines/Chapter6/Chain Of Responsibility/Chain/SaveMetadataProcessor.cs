using Book_Pipelines.Chapter6.ChainOfResponsibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain
{
    public class SaveMetadataProcessor : Processor
    {
        public SaveMetadataProcessor(Processor nextProcessor) : base(nextProcessor)
        {
        }

        public override void Process(IBasicEvent request)
        {
            RegisterStep(request, "SAVE_METADATA");
            base.Process(request);
        }
    }
}
