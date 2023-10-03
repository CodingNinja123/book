namespace Book_Pipelines.Chapter4.Composite
{
    public class ReportPipeline : AbstractPipeline
    {
        private List<AbstractPipeline> pipelines;

        public ReportPipeline(List<AbstractPipeline> pipelines)
        {
            this.pipelines = pipelines;
        }

        public override AbstractPipeline Copy()
        {
            ReportPipeline pipeline = new ReportPipeline(this.pipelines.Select(x=> x.Copy()).ToList());
            return pipeline;
        }

        public override void Process(IBasicEvent basicEvent)
        {
            this.pipelines.ForEach(x => x.Process(basicEvent));
        }
    }
}
