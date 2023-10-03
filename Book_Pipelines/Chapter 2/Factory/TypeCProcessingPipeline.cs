namespace Book_Pipelines.Chapter_2.Factory
{
    public class TypeCProcessingPipeline : AbstractPipeline
    {
        private EventTypeC data = null;
        private string targetSystemApiUrl = "http://systemC.test/api";
        private string targetSystemProcessingApiUel = "http://systemC.processing.test/api";
        protected override object Preprocess(BasicEvent basicEvent)
        {
            this.data = (basicEvent as EventTypeC) ?? new EventTypeC();
            Notify(basicEvent, "Preprocessing completed");
            return this.data;
        }

        protected override void ProcessEvent(BasicEvent basicEvent, object preprocessingResult)
        {
            Notify(basicEvent, "Processing event");
            Notify(basicEvent, $"Calling {this.targetSystemProcessingApiUel} to process a values of event: {this.data.Action} {this.data.Value}");
        }

        protected override object Search(BasicEvent basicEvent)
        {
            return null;
        }

        protected override void Store(BasicEvent basicEvent, object existingObject)
        {
            Notify(basicEvent, "Storing event in the target system");
            Notify(basicEvent, $"Calling {this.targetSystemApiUrl} api to save the data about received values");
        }

        protected override void Validate(BasicEvent basicEvent)
        {
            if (basicEvent == null)
                throw new ArgumentNullException("Event cannot be null");
        }
    }
}
