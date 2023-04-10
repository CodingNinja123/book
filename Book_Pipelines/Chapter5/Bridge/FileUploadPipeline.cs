using Book_Pipelines.Chapter5.Bridge.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.Bridge
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
            if(!ShouldBeFilePreprocessed) return;
            RegisterStep(basicEvent, "EVENT_PREPROCESSING");
            DownloadFileClient.ExecuteRequest(basicEvent.FileUrl);
        }

        protected void ProcessEvent(IUploadEventData basicEvent)
        {
            if (UploadFileClient == null)
                return;

            RegisterStep(basicEvent, "EVENT_PROCESSING");
            this.UploadFileClient.ExecuteRequest(new UploadFileInfo
            {
                FileName = basicEvent.FileName,
                Content = new byte[0]
            });
        }

        protected void Search(IUploadEventData basicEvent)
        {
            RegisterStep(basicEvent, "EVENT_SEARCH");
            TargetSystemSearchApiClient.ExecuteRequest(basicEvent.FileName);
        }

        protected void Store(IUploadEventData basicEvent)
        {
            if (!ShouldBeEventStored) return;
            RegisterStep(basicEvent, "EVENT_STORE");
            TargetSystemStoreApiClient.ExecuteRequest(basicEvent.FileName);
        }

        public override void Process(IBasicEvent basicEvent)
        {
            var data = basicEvent as IUploadEventData;
            SaveMetadata(data);
            Validate(data);
            Preprocess(data);
            Search(data);
            ProcessEvent(data);
            Store(data);
            UpdateMetadata(data);
        }

        protected virtual Guid SaveMetadata(IUploadEventData basicEvent)
        {
            if(!ShouldSaveMetadata) return Guid.Empty; 
            RegisterStep(basicEvent, "SAVE_METADATA");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(IUploadEventData basicEvent)
        {
            if(!ShouldSaveMetadata) return;
            RegisterStep(basicEvent, "UPDATE_PROCESSING");
        }
        
        protected virtual void Validate(IUploadEventData basicEvent)
        {
            if (basicEvent.FileName == null)
                throw new PipelineProcessingException("Filename of the event cannot be null");
            if (basicEvent.FileType == null)
                throw new PipelineProcessingException("File Type of the event cannot be null");
            if (basicEvent.FileUrl == null)
                throw new PipelineProcessingException("File Url of the event cannot be null");
        }
    }
}
