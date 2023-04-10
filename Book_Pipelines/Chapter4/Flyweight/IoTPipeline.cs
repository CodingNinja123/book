namespace Book_Pipelines.Chapter4.Flyweight
{
    public class IoTPipeline : AbstractPipeline
    {
        public bool ShouldSaveMetadata {get;set;}

        public ICommunicationClient<IoTData, string> SystemCApiClient { get; set; }

        protected void ProcessEvent(IIoTEventData basicEvent)
        {
            var iotEvent = basicEvent as IIoTEventData;
            Notify(basicEvent, "Processing event");
            var data = new IoTData(iotEvent.Source, iotEvent.Action, iotEvent.Value);
            SystemCApiClient.ExecuteRequest(data);
        }

        public override void Process(IBasicEvent basicEvent)
        {
            var data = basicEvent as IIoTEventData;
            if (ShouldSaveMetadata) SaveMetadata(data);
            Notify(data, "PROCESSING_STARTED");
            Validate(data);
            ProcessEvent(data);
            if (ShouldSaveMetadata) UpdateMetadata(data);
            Notify(data, "PROCESSING_FINISHED");
        }

        protected virtual Guid SaveMetadata(IIoTEventData basicEvent)
        {
            Notify(basicEvent, "Saving metadata");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(IIoTEventData basicEvent)
        {
            Notify(basicEvent, "Updating metdata");
        }
        protected virtual void Validate(IIoTEventData basicEvent)
        {
            if (basicEvent == null)
                throw new ArgumentNullException("Event cannot be null");
            
            if (basicEvent.Action == null)
                throw new ArgumentException("Filename of the event cannot be null");
            if (basicEvent.Value == null)
                throw new ArgumentException("Filename of the event cannot be null");
        }
    }
}
