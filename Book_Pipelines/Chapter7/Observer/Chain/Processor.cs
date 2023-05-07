using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book_Pipelines.Chapter7.Observer;
using Book_Pipelines.Chapter7.Observer.Chain;

namespace Book_Pipelines.Chapter7.Chain_Of_Responsibility.Observer
{
    public delegate void RegisterStepExecutionDelegate(IBasicEvent basicEvent, string step);

    public class Processor: IChainEventPublisher
    {
        private Processor nextProcessor;
        private List<IChainEventListener> listeners = new List<IChainEventListener>();
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
                nextProcessor.Process(request);
        }

        public void Subscribe(IChainEventListener subscriber)
        {
            this.listeners.Add(subscriber);
        }

        public void Unsubscribe(IChainEventListener subscriber)
        {
            this.listeners.Remove(subscriber);
        }

        public void Notify(IBasicEvent basicEvent, string message)
        {
            this.listeners.ForEach(x =>
            {
                x.Update(basicEvent, message);
            });
        }
    }
}
