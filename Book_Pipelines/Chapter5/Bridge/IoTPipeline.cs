using Book_Pipelines.Chapter5.Bridge.Exceptions;

namespace Book_Pipelines.Chapter5.Bridge
{
    public class IoTPipeline : AbstractPipeline
    {
        public bool ShouldSaveMetadata {get;set;}

        public ICommunicationClient<IoTData, string> SystemCApiClient { get; set; }
        
        protected void ProcessEvent(IIoTEventData basicEvent)
        {
            RegisterStep(basicEvent, "EVENT_PROCESSING");
            var data = new IoTData(basicEvent.Source, basicEvent.Action, basicEvent.Value);
            SystemCApiClient.ExecuteRequest(data);
        }

        public override void Process(IBasicEvent basicEvent)
        {
            var data = basicEvent as IIoTEventData;
            SaveMetadata(data);
            Validate(data);
            ProcessEvent(data);
            UpdateMetadata(data);
        }

        protected virtual Guid SaveMetadata(IIoTEventData basicEvent)
        {
            if (!ShouldSaveMetadata)
                return Guid.Empty;
            RegisterStep(basicEvent, "SAVE_METADATA");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(IIoTEventData basicEvent)
        {
            if (!ShouldSaveMetadata)
                return;
            RegisterStep(basicEvent, "UPDATE_METADATA");
        }
        protected virtual void Validate(IIoTEventData basicEvent)
        {   
            if (basicEvent.Action == null)
                throw new PipelineProcessingException("Action of the event cannot be null");
            if (basicEvent.Value == null)
                throw new PipelineProcessingException("Value of the event cannot be null");
        }
    }
}
