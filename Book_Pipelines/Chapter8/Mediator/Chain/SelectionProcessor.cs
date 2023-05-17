using Book_Pipelines.Chapter8.Chain_Of_Responsibility.Mediator;

namespace Book_Pipelines.Chapter8.Mediator.Chain
{
    public class SelectionProcessor : Processor
    {
        private readonly string eventType;

        public SelectionProcessor(Processor nextProcessor, string eventType) : base(nextProcessor)
        {
            this.eventType = eventType;
        }

        public override void Process(IBasicEvent request)
        {
            if(request.Type == this.eventType)
                base.Process(request);
        }
    }
}
