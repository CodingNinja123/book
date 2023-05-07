using Book_Pipelines.Chapter7.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Chain_Of_Responsibility.Observer
{
    public class SaveMetadataProcessor : Processor
    {
        public SaveMetadataProcessor(Processor nextProcessor) : base(nextProcessor)
        {
        }

        public override void Process(IBasicEvent request)
        {
            // RegisterStep(request, "SAVE_METADATA");
            Notify(request, "SAVE_METADATA");
            base.Process(request);
        }
    }
}
