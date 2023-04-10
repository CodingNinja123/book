namespace Book_Pipelines.Chapter4.Composite
{
    public abstract class AbstractPipeline: ICopy<AbstractPipeline>
    {
        public abstract AbstractPipeline Copy();
        public abstract void Process(IBasicEvent basicEvent);
    }
}
