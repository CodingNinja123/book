using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter4.Composite
{
    public class FileUploadPipeline : AbstractPipeline
    {
        private string token;

        public bool ShouldSaveMetadata { get; set; }
        public bool ShouldBeFilePreprocessed { get; set; }
        public bool ShouldBeEventStored { get; set; }
        public ICommunicationClient<UploadFileInfo, int> UploadFileClient { get; set; }
        public ICommunicationClient<string, string> TargetSystemSearchApiClient { get; set; }
        public ICommunicationClient<string, string> TargetSystemStoreApiClient { get; set; }
        public ICommunicationClient<string, byte[]> DownloadFileClient { get; set; }

        public override void Process(IBasicEvent basicEvent)
        {
            var data = basicEvent as IUploadEventData;
            this.RequestToken();
            try
            {
                if (this.ShouldSaveMetadata)
                    SaveMetadata(data);

                this.Notify(data, "PROCESSING_STARTED");
                this.Validate(data);

                if (this.ShouldBeFilePreprocessed)
                    this.Preprocess(data);

                this.Search(data);
                this.ProcessEvent(data);

                if (this.ShouldBeEventStored)
                    this.Store(data);

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

        protected void Preprocess(IUploadEventData basicEvent)
        {
            this.Notify(basicEvent, "Preprocessing event");
            this.DownloadFileClient.ExecuteRequest(basicEvent.FileUrl);
        }

        protected void ProcessEvent(IUploadEventData basicEvent)
        {
            if (this.UploadFileClient == null)
                return;

            this.Notify(basicEvent, "Processing event");
            this.UploadFileClient.ExecuteRequest(new UploadFileInfo
            {
                FileName = basicEvent.FileName,
                Content = new byte[0]
            });
        }

        protected void Search(IUploadEventData basicEvent)
        {
            this.Notify(basicEvent, "Searching event in the target system");
            this.TargetSystemSearchApiClient.ExecuteRequest(basicEvent.FileName);
        }

        protected void Store(IUploadEventData basicEvent)
        {
            this.Notify(basicEvent, "Storing event in the target system");
            this.TargetSystemStoreApiClient.ExecuteRequest(basicEvent.FileName);
        }

        protected virtual Guid SaveMetadata(IUploadEventData basicEvent)
        {
            this.Notify(basicEvent, "Saving metadata");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(IUploadEventData basicEvent)
        {
            this.Notify(basicEvent, "Updating metdata");
        }
        protected virtual void Notify(IUploadEventData badicEvent, string message)
        {
            Console.WriteLine($"Processing pipeline: {message}: {badicEvent.Id}");
        }
        protected virtual void Validate(IUploadEventData basicEvent)
        {
            if (basicEvent == null)
                throw new ArgumentNullException("Event cannot be null");
            if (basicEvent.FileName == null)
                throw new ArgumentException("Filename of the event cannot be null");
            if (basicEvent.FileType == null)
                throw new ArgumentException("Filename of the event cannot be null");
            if (basicEvent.FileUrl == null)
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
