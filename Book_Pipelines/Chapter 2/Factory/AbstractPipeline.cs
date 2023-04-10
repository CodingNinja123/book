namespace Book_Pipelines.Chapter_2.Factory
{
    public abstract class AbstractPipeline
    {
        protected abstract object Preprocess(BasicEvent basicEvent);
        protected abstract void ProcessEvent(BasicEvent basicEvent, object preprocessingResult);
        protected abstract object Search(BasicEvent basicEvent);
        protected abstract void Store(BasicEvent basicEvent, object existingObject);

        public virtual void Process(BasicEvent basicEvent)
        {
            try
            {
                var metadataObjectGuid = SaveMetadata(basicEvent);
                Notify(basicEvent, "PROCESSING_STARTED");
                Validate(basicEvent);
                var proceprocessingResult = Preprocess(basicEvent);
                ProcessEvent(basicEvent, proceprocessingResult);
                var existingObject = Search(basicEvent);
                Store(basicEvent, existingObject);
                UpdateMetadata(basicEvent, metadataObjectGuid);
                Notify(basicEvent, "PROCESSING_FINISHED");
            }
            catch(Exception ex)
            {
                Notify(basicEvent, "PROCESSING_FAILED");
            }
        }

        protected virtual Guid SaveMetadata(BasicEvent basicEvent)
        {
            Notify(basicEvent, "Saving metadata");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(BasicEvent basicEvent, Guid metadataGuid)
        {
            Notify(basicEvent, "Updating metdata");
        }
        protected virtual void Notify(BasicEvent badicEvent, string message)
        {
            Console.WriteLine($"Processing pipeline: {message}: {badicEvent.EventGuid}");
        }
        protected virtual void Validate(BasicEvent basicEvent)
        {
            if (basicEvent == null)
                throw new ArgumentNullException("Event cannot be null");

            if (basicEvent.Data == null)
                throw new ArgumentException("Data of the event cannot be null");
        }
    }
}
