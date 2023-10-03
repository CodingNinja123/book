using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter4.Adapter
{
    public class FileUploadPipeline : AbstractPipeline
    {
        private string token;
        private BaseUploadEvent data = null;
        public bool ShouldSaveMetadata { get; set; }
        public bool ShouldBeFilePreprocessed { get; set; }
        public bool ShouldBeEventStored { get; set; }
        public ICommunicationClient<UploadFileInfo, int> UploadFileClient { get; set; }
        public ICommunicationClient<string, string> TargetSystemSearchApiClient { get; set; }
        public ICommunicationClient<string, string> TargetSystemStoreApiClient { get; set; }
        public ICommunicationClient<string, byte[]> DownloadFileClient { get; set; }

        public override void Process(BasicEvent basicEvent)
        {
            this.RequestToken();
            try
            {
                if (this.ShouldSaveMetadata)
                    this.SaveMetadata(basicEvent);

                this.Notify(basicEvent, "PROCESSING_STARTED");
                this.Validate(basicEvent);

                if (this.ShouldBeFilePreprocessed)
                    this.Preprocess(basicEvent);

                this.Search(basicEvent);
                this.ProcessEvent(basicEvent);

                if (this.ShouldBeEventStored)
                    this.Store(basicEvent);

                if (this.ShouldSaveMetadata)
                    this.UpdateMetadata(basicEvent);

                this.Notify(basicEvent, "PROCESSING_FINISHED");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                this.Notify(basicEvent, ex.ToString());
            }
        }

        protected void Preprocess(BasicEvent basicEvent)
        {
            this.data = basicEvent as BaseUploadEvent;
            this.Notify(basicEvent, "Preprocessing event");
            this.DownloadFileClient.ExecuteRequest(this.data.FileUrl);
        }

        protected void ProcessEvent(BasicEvent basicEvent)
        {
            if (this.UploadFileClient == null)
                return;

            this.Notify(basicEvent, "Processing event");
            this.UploadFileClient.ExecuteRequest(new UploadFileInfo
            {
                FileName = this.data.FileName,
                Content = new byte[0]
            });
        }

        protected void Search(BasicEvent basicEvent)
        {
            this.Notify(basicEvent, "Searching event in the target system");
            this.TargetSystemSearchApiClient.ExecuteRequest(this.data.FileName);
        }

        protected void Store(BasicEvent basicEvent)
        {
            this.Notify(basicEvent, "Storing event in the target system");
            this.TargetSystemStoreApiClient.ExecuteRequest(this.data.FileName);
        }

        protected virtual Guid SaveMetadata(BasicEvent basicEvent)
        {
            this.Notify(basicEvent, "Saving metadata");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(BasicEvent basicEvent)
        {
            this.Notify(basicEvent, "Updating metdata");
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
            result.DownloadFileClient = this.DownloadFileClient;
            result.TargetSystemSearchApiClient = this.TargetSystemSearchApiClient;
            result.TargetSystemStoreApiClient = this.TargetSystemStoreApiClient;
            result.UploadFileClient = this.UploadFileClient;
            result.token = this.token;
            return result;
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
