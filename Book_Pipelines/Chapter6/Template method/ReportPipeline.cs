namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class ReportPipeline<T> : AbstractPipeline<T> where T : IBasicEvent
    {
        private List<AbstractPipeline<T>> pipelines;

        public ReportPipeline(List<AbstractPipeline<T>> pipelines)
        {
            this.pipelines = pipelines;
        }

        public override void Process(T basicEvent)
        {
            pipelines.ForEach(x => x.Process(basicEvent));
        }
    }
}
