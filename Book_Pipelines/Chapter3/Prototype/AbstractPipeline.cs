namespace Book_Pipelines.Chapter3.Prototype
{
    public abstract class AbstractPipeline: ICopy<AbstractPipeline>
    {
        public abstract AbstractPipeline Copy();
        public abstract void Process(BasicEvent basicEvent);
    }
}
