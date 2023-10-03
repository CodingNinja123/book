using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book_Pipelines.Chapter6.ChainOfResponsibility;

namespace Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain
{
    public delegate void RegisterStepExecutionDelegate(IBasicEvent basicEvent, string step);

    public class Processor
    {
        private Processor nextProcessor;
        public event RegisterStepExecutionDelegate RegisterStepExecution;

        public Processor(Processor nextProcessor)
        {
            this.nextProcessor = nextProcessor;
        }

        public virtual void RegisterStep(IBasicEvent basicEvent, string step)
        {
            if (this.RegisterStepExecution != null)
                this.RegisterStepExecution(basicEvent, step);
        }

        public virtual void Process(IBasicEvent request)
        {
            if (this.nextProcessor != null)
                this.nextProcessor.Process(request);
        }
    }
}
