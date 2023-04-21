using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book_Pipelines.Chapter6.ChainOfResponsibility;

namespace Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain
{
    public class Processor
    {
        private Processor nextProcessor;
        public Processor(Processor nextProcessor)
        {
            this.nextProcessor = nextProcessor;
        }
        public virtual void Process(IBasicEvent request)
        {
            if (nextProcessor != null)
                nextProcessor.Process(request);
        }
    }
}
