namespace Book_Pipelines.Chapter6.Strategy
{
    public class FileUploadStrategyA: IStrategy<IUploadEventData>
    {
        public AbstractPipeline<IUploadEventData> Pipeline { get; set; }
        public FileUploadStrategyA(AbstractPipeline<IUploadEventData> pipeline)
        {
            this.Pipeline = pipeline;
        }
        public  void Process(IUploadEventData basicEvent)
        {
            this.Pipeline.SaveMetadata(basicEvent);
            this.Pipeline.Validate(basicEvent);
            this.Pipeline.Preprocess(basicEvent);
            this.Pipeline.Search(basicEvent);
            this.Pipeline.ProcessEvent(basicEvent);
            this.Pipeline.Store(basicEvent);
            this.Pipeline.UpdateMetadata(basicEvent);
        }
    }
}
