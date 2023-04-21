using Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain;

namespace Book_Pipelines.Chapter6.ChainOfResponsibility
{
    public class FileUploadStrategyA: IStrategy<IUploadEventData>
    {
        public AbstractPipeline<IUploadEventData> Pipeline { get; set; }
        public Processor Processor { get; set; }
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
