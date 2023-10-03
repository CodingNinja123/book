namespace Book_Pipelines.Chapter4.Composite
{
    public class IoTPipeline : AbstractPipeline
    {
        private string token;
        public bool ShouldSaveMetadata {get;set;}
        public ICommunicationClient<IoTData, string> SystemCProcessingApiClient { get; set; }

        public override void Process(IBasicEvent basicEvent)
        {
            this.RequestToken();
            var data = basicEvent as IIoTEventData;

            try
            {
                if (this.ShouldSaveMetadata)
                    this.SaveMetadata(data);

                this.Notify(data, "PROCESSING_STARTED");
                this.Validate(data);
                this.ProcessEvent(data);

                if (this.ShouldSaveMetadata)
                    this.UpdateMetadata(data);

                this.Notify(data, "PROCESSING_FINISHED");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                this.Notify(data, ex.ToString());
            }
        }

        public override IoTPipeline Copy()
        {
            IoTPipeline result = new IoTPipeline();

            result.ShouldSaveMetadata = this.ShouldSaveMetadata;
            result.SystemCProcessingApiClient = this.SystemCProcessingApiClient;
            result.token = this.token;
            return result;
        }

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

        protected virtual Guid SaveMetadata(IIoTEventData basicEvent)
        {
            this.Notify(basicEvent, "Saving metadata");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(IIoTEventData basicEvent)
        {
            this.Notify(basicEvent, "Updating metdata");
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

        private void RequestToken()
        {
            if (!string.IsNullOrWhiteSpace(this.token))
                return;

            Thread.Sleep(200);
            this.token = $"Token: {Guid.NewGuid()}";
        }
    }
}
