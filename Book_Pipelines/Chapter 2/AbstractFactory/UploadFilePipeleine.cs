using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.AbstractFactoryNM
{
    public abstract class UploadFilePipeleine: AbstractPipeline
    {
        protected abstract object Preprocess(BasicEvent basicEvent);
        protected abstract void ProcessEvent(BasicEvent basicEvent, object preprocessingResult);
        protected abstract object Search(BasicEvent basicEvent);
        protected abstract void Store(BasicEvent basicEvent, object existingObject);

        public override void Process(BasicEvent basicEvent)
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
            catch (Exception ex)
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
            Console.WriteLine($"Processing pipeline: {message}: {badicEvent.Id}");
        }
        protected virtual void Validate(BasicEvent basicEvent)
        {
            if (basicEvent == null)
                throw new ArgumentNullException("Event cannot be null");
            var baseUploadEvent = basicEvent as BaseUploadEvent;
            if (baseUploadEvent.FileName == null)
                throw new ArgumentException("Filename of the event cannot be null");
            if (baseUploadEvent.FileType == null)
                throw new ArgumentException("Filename of the event cannot be null");
            if (baseUploadEvent.FileUrl == null)
                throw new ArgumentException("Filename of the event cannot be null");
        }
    }
}
