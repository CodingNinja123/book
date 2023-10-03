namespace Book_Pipelines.Chapter5.TemplateMethod
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

        public bool ShouldSaveMetadata { get; set; }
        public bool ShouldBePreprocessed { get; set; }
        public bool ShouldBeEventStored { get; set; }

        public virtual void Process(T basicEvent)
        {
            if (this.ShouldSaveMetadata) this.SaveMetadata(basicEvent);
            this.Validate(basicEvent);
            if (this.ShouldBePreprocessed) this.Preprocess(basicEvent);
            this.Search(basicEvent);
            this.ProcessEvent(basicEvent);
            if (this.ShouldBeEventStored) this.Store(basicEvent);
            if (this.ShouldSaveMetadata) this.UpdateMetadata(basicEvent);
        }

        protected virtual void Preprocess(T basicEvent)
        {
        }
        protected virtual void ProcessEvent(T basicEvent)
        {
        }
        protected virtual void Search(T basicEvent)
        {
        }
        protected virtual void Store(T basicEvent)
        {
        }
        protected virtual Guid SaveMetadata(T basicEvent)
        {
            return Guid.Empty;
        }
        protected virtual void UpdateMetadata(T basicEvent)
        {
        }
        protected virtual void Validate(T basicEvent)
        {
        }
    }
}
