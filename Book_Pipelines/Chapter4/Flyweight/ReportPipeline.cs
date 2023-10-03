namespace Book_Pipelines.Chapter4.Flyweight
{
    public class ReportPipeline : AbstractPipeline
    {
        private List<AbstractPipeline> pipelines;

        public ReportPipeline(List<AbstractPipeline> pipelines)
        {
            this.pipelines = pipelines;
        }

        public override void Process(IBasicEvent basicEvent)
        {
            this.pipelines.ForEach(x => x.Process(basicEvent));
        }
    }
}
