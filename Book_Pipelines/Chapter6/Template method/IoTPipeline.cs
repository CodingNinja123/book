using Book_Pipelines.Chapter5.TemplateMethod.Exceptions;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class IoTPipeline : AbstractPipeline<IIoTEventData>
    {
        public ICommunicationClient<IoTData, string> SystemCApiClient { get; set; }
        public IoTPipeline()
        {
            ShouldBeEventStored = false;
            ShouldBePreprocessed = false;
        }
        protected override void ProcessEvent(IIoTEventData basicEvent)
        {
            RegisterStep(basicEvent, "EVENT_PROCESSING");
            var data = new IoTData(basicEvent.Source, basicEvent.Action, basicEvent.Value);
            SystemCApiClient.ExecuteRequest(data);
        }
        protected override void Validate(IIoTEventData basicEvent)
        {   
            if (basicEvent.Action == null)
                throw new PipelineProcessingException("Action of the event cannot be null");
            if (basicEvent.Value == null)
                throw new PipelineProcessingException("Value of the event cannot be null");
        }
    }
}
