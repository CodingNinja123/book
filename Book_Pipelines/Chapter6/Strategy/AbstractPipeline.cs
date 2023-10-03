namespace Book_Pipelines.Chapter6.Strategy
{
    public delegate void RegisterStepExecutionDelegate(IBasicEvent basicEvent, string step);
    public abstract class AbstractPipeline<T> where T : IBasicEvent
    {
        public event RegisterStepExecutionDelegate RegisterStepExecution;
        public virtual void RegisterStep(T basicEvent, string step)
        {
            if (this.RegisterStepExecution != null)
                this.RegisterStepExecution(basicEvent, step);
        }
        public virtual void Preprocess(T basicEvent)
        {
        }
        public virtual void ProcessEvent(T basicEvent)
        {
        }
        public virtual void Search(T basicEvent)
        {
        }
        public virtual void Store(T basicEvent)
        {
        }
        public virtual Guid SaveMetadata(T basicEvent)
        {
            return Guid.Empty;
        }
        public virtual void UpdateMetadata(T basicEvent)
        {
        }
        public virtual void Validate(T basicEvent)
        {
        }
    }
}
