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

        protected void Preprocess(IUploadEventData basicEvent)
        {
            Notify(basicEvent, "Preprocessing event");
            DownloadFileClient.ExecuteRequest(basicEvent.FileUrl);
        }

        protected void ProcessEvent(IUploadEventData basicEvent)
        {
            if (UploadFileClient == null)
                return;

            Notify(basicEvent, "Processing event");
            this.UploadFileClient.ExecuteRequest(new UploadFileInfo
            {
                FileName = basicEvent.FileName,
                Content = new byte[0]
            });
        }

        protected void Search(IUploadEventData basicEvent)
        {
            Notify(basicEvent, "Searching event in the target system");
            TargetSystemSearchApiClient.ExecuteRequest(basicEvent.FileName);
        }

        protected void Store(IUploadEventData basicEvent)
        {
            Notify(basicEvent, "Storing event in the target system");
            TargetSystemStoreApiClient.ExecuteRequest(basicEvent.FileName);
        }

        public override void Process(IBasicEvent basicEvent)
        {
            var data = basicEvent as IUploadEventData;
            RequestToken();
            try
            {
                if (ShouldSaveMetadata)
                    SaveMetadata(data);
                
                Notify(data, "PROCESSING_STARTED");
                Validate(data);
                
                if(ShouldBeFilePreprocessed)
                    Preprocess(data);

                Search(data);
                ProcessEvent(data);

                if (ShouldBeEventStored)
                    Store(data);
                
                if(ShouldSaveMetadata)
                    UpdateMetadata(data);
                
                Notify(data, "PROCESSING_FINISHED");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Notify(data, ex.ToString());
            }
        }

        protected virtual Guid SaveMetadata(IUploadEventData basicEvent)
        {
            Notify(basicEvent, "Saving metadata");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(IUploadEventData basicEvent)
        {
            Notify(basicEvent, "Updating metdata");
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
            if (!string.IsNullOrWhiteSpace(token))
                return;

            Thread.Sleep(200);
            this.token = $"Token: {Guid.NewGuid()}";
        }
    }
}
