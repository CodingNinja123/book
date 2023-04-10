namespace Book_Pipelines.Chapter5.Facade
{
    public delegate void RegisterStepExecutionDelegate(IBasicEvent basicEvent, string step);

    public abstract class AbstractPipeline
    {
        public event RegisterStepExecutionDelegate RegisterStepExecution;
        public abstract void Process(IBasicEvent basicEvent);
        public virtual void RegisterStep(IBasicEvent basicEvent, string step)
        {
            if (RegisterStepExecution != null)
                RegisterStepExecution(basicEvent, step);
        }
    }
}
