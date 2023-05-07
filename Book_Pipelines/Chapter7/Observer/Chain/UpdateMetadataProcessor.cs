using Book_Pipelines.Chapter7.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Chain_Of_Responsibility.Observer
{
    public class UpdateMetadataProcessor : Processor
    {
        public UpdateMetadataProcessor(Processor nextProcessor) : base(nextProcessor)
        {
        }

        public override void Process(IBasicEvent request)
        {
            // RegisterStep(request, "UPDATE_METADATA");
            Notify(request, "UPDATE_METADATA");
            // Do nothing
            base.Process(request);
        }
    }
}
