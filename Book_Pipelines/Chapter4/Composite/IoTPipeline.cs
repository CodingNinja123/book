namespace Book_Pipelines.Chapter4.Composite
{
    public class IoTPipeline : AbstractPipeline
    {
        private string token;
        public bool ShouldSaveMetadata {get;set;}

        public ICommunicationClient<IoTData, string> SystemCProcessingApiClient { get; set; }

        protected void ProcessEvent(IIoTEventData basicEvent)
        {
            var iotEvent = basicEvent as IIoTEventData;
            Notify(basicEvent, "Processing event");
            var data = new IoTData
            {
                Action = iotEvent.Action,
                Source = iotEvent.Source,
                Value = iotEvent.Value
            };
            SystemCProcessingApiClient.ExecuteRequest(data);
        }

        public override void Process(IBasicEvent basicEvent)
        {
            RequestToken();
            var data = basicEvent as IIoTEventData;

            try
            {
                if(ShouldSaveMetadata)
                    SaveMetadata(data);
               
                Notify(data, "PROCESSING_STARTED");
                Validate(data);
                ProcessEvent(data);
                
                if (ShouldSaveMetadata)
                    UpdateMetadata(data);
                
                Notify(data, "PROCESSING_FINISHED");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Notify(data, ex.ToString());
            }
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
        protected virtual void Notify(IIoTEventData badicEvent, string message)
        {
            Console.WriteLine($"Processing pipeline: {message}: {badicEvent.Id}");
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

        public override IoTPipeline Copy()
        {
            IoTPipeline result = new IoTPipeline();
          
            result.ShouldSaveMetadata = this.ShouldSaveMetadata;
            result.SystemCProcessingApiClient = this.SystemCProcessingApiClient;
            result.token = this.token;
            return result;
        }

        private void RequestToken()
        {
            if (!string.IsNullOrWhiteSpace(token))
                return;

            Thread.Sleep(200);
            this.token = $"Token: {Guid.NewGuid()}";
        }
    }
}
