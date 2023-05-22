using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book_Pipelines.Chapter8.Command;
using Book_Pipelines.Chapter8.Command.Chain;

namespace Book_Pipelines.Chapter8.Chain_Of_Responsibility.Command
{
    public delegate void RegisterStepExecutionDelegate(IBasicEvent basicEvent, string step);

    public class Processor
    {
        private Processor nextProcessor;
        public Processor NextProcessor { get => nextProcessor; }

        public event RegisterStepExecutionDelegate RegisterStepExecution;

        public Processor(Processor nextProcessor)
        {
            this.nextProcessor = nextProcessor;
        }

        public virtual void RegisterStep(IBasicEvent basicEvent, string step)
        {
            if (RegisterStepExecution != null)
                RegisterStepExecution(basicEvent, step);
        }

        public virtual void Process(IBasicEvent request)
        {
            if (nextProcessor != null)
            {
                nextProcessor.Process(request);
            }
        }

        public virtual void Accept(ProcessorVisitor visitor)
        {
            if(nextProcessor != null)
            {
                nextProcessor.Accept(visitor);
            }
        }
    }
}
