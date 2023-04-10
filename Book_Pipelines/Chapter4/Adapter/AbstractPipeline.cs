namespace Book_Pipelines.Chapter4.Adapter
{
    public abstract class AbstractPipeline: ICopy<AbstractPipeline>
    {
        public abstract AbstractPipeline Copy();
        public abstract void Process(BasicEvent basicEvent);
    }
}
