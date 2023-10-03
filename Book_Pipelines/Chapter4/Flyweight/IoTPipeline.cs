namespace Book_Pipelines.Chapter4.Flyweight
{
    public class IoTPipeline : AbstractPipeline
    {
        public bool ShouldSaveMetadata {get;set;}
        public ICommunicationClient<IoTData, string> SystemCApiClient { get; set; }

        public override void Process(IBasicEvent basicEvent)
        {
            var data = basicEvent as IIoTEventData;
            if (this.ShouldSaveMetadata) this.SaveMetadata(data);
            this.Notify(data, "PROCESSING_STARTED");
            this.Validate(data);
            this.ProcessEvent(data);
            if (this.ShouldSaveMetadata) this.UpdateMetadata(data);
            this.Notify(data, "PROCESSING_FINISHED");
        }
        protected void ProcessEvent(IIoTEventData basicEvent)
        {
            var iotEvent = basicEvent as IIoTEventData;
            this.Notify(basicEvent, "Processing event");
            var data = new IoTData(iotEvent.Source, iotEvent.Action, iotEvent.Value);
            this.SystemCApiClient.ExecuteRequest(data);
        }

        protected virtual Guid SaveMetadata(IIoTEventData basicEvent)
        {
            this.Notify(basicEvent, "Saving metadata");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(IIoTEventData basicEvent)
        {
            this.Notify(basicEvent, "Updating metdata");
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
