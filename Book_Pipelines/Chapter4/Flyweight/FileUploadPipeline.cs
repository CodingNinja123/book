using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter4.Flyweight
{
    public class FileUploadPipeline : AbstractPipeline
    {
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
    }
}
