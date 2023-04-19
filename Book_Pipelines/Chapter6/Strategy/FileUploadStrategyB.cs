namespace Book_Pipelines.Chapter6.Strategy
{
    public class FileUploadStrategyB: IStrategy<IUploadEventData>
    {
        public AbstractPipeline<IUploadEventData> Pipeline { get; set; }
        public FileUploadStrategyB(AbstractPipeline<IUploadEventData> pipeline)
        {
            this.Pipeline = pipeline;
        }
        public void Process(IUploadEventData basicEvent)
        {
            this.Pipeline.Validate(basicEvent);
            this.Pipeline.Preprocess(basicEvent);
            this.Pipeline.Search(basicEvent);
            this.Pipeline.ProcessEvent(basicEvent);
        }
    }
}
