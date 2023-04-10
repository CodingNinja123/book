using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter3.Prototype
{
    public class FileUploadPipeline : AbstractPipeline
    {
        private string token;

        public bool ShouldSaveMetadata { get; set; }
        public bool ShouldBeFilePreprocessed { get; set; }
        public bool ShouldBeEventStored { get; set; }
        public string TargetSystemUploadUrl { get; set; }
        public string TargetSystemApiUrl { get; set; }

        private BaseUploadEvent data = null;

        protected void Preprocess(BasicEvent basicEvent)
        {
            this.data = basicEvent as BaseUploadEvent;
            Notify(basicEvent, "Preprocessing event");
            Notify(basicEvent, $"Downloading file {this.data.FileName} from {this.data.FileUrl}");
        }

        protected void ProcessEvent(BasicEvent basicEvent)
        {
            if (string.IsNullOrEmpty(TargetSystemUploadUrl))
                return;

            Notify(basicEvent, "Processing event");
            Notify(basicEvent, $"Transfering file {this.data.FileName} downloaded from {this.data.FileUrl} to {this.TargetSystemUploadUrl}");
        }

        protected void Search(BasicEvent basicEvent)
        {
            Notify(basicEvent, "Searching event in the target system");
            Notify(basicEvent, $"Calling {this.TargetSystemApiUrl} to search for {this.data.FileName}");
        }

        protected void Store(BasicEvent basicEvent)
        {
            Notify(basicEvent, "Storing event in the target system");
            Notify(basicEvent, $"Calling {this.TargetSystemApiUrl} api to save the data about transfered file");
        }

        public override void Process(BasicEvent basicEvent)
        {
            RequestToken();
            try
            {
                if (ShouldSaveMetadata)
                    SaveMetadata(basicEvent);
                
                Notify(basicEvent, "PROCESSING_STARTED");
                Validate(basicEvent);
                
                if(ShouldBeFilePreprocessed)
                    Preprocess(basicEvent);

                Search(basicEvent);
                ProcessEvent(basicEvent);

                if (ShouldBeEventStored)
                    Store(basicEvent);
                
                if(ShouldSaveMetadata)
                    UpdateMetadata(basicEvent);
                
                Notify(basicEvent, "PROCESSING_FINISHED");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Notify(basicEvent, ex.ToString());
            }
        }

        protected virtual Guid SaveMetadata(BasicEvent basicEvent)
        {
            Notify(basicEvent, "Saving metadata");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(BasicEvent basicEvent)
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

        public override FileUploadPipeline Copy()
        {
            FileUploadPipeline result = new FileUploadPipeline();
            result.ShouldBeEventStored = this.ShouldBeEventStored;
            result.ShouldBeFilePreprocessed = this.ShouldBeFilePreprocessed;
            result.ShouldSaveMetadata = this.ShouldSaveMetadata;
            result.TargetSystemApiUrl = this.TargetSystemApiUrl;
            result.TargetSystemUploadUrl = this.TargetSystemApiUrl;
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
