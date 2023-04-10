using Book_Pipelines.Chapter5.TemplateMethod.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class FileUploadPipeline<T> : AbstractPipeline<T> where T : IUploadEventData
    {
        public bool ShouldSaveMetadata { get; set; }
        public bool ShouldBeFilePreprocessed { get; set; }
        public bool ShouldBeEventStored { get; set; }
        public ICommunicationClient<UploadFileInfo, int> UploadFileClient { get; set; }
        public ICommunicationClient<string, string> TargetSystemSearchApiClient { get; set; }
        public ICommunicationClient<string, string> TargetSystemStoreApiClient { get; set; }
        public ICommunicationClient<string, byte[]> DownloadFileClient { get; set; }

        protected override void Preprocess(T basicEvent)
        {
            if(!ShouldBeFilePreprocessed) return;
            RegisterStep(basicEvent, "EVENT_PREPROCESSING");
            DownloadFileClient.ExecuteRequest(basicEvent.FileUrl);
        }

        protected override void ProcessEvent(T basicEvent)
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

        protected override void Search(T basicEvent)
        {
            RegisterStep(basicEvent, "EVENT_SEARCH");
            TargetSystemSearchApiClient.ExecuteRequest(basicEvent.FileName);
        }

        protected override void Store(T basicEvent)
        {
            if (!ShouldBeEventStored) return;
            RegisterStep(basicEvent, "EVENT_STORE");
            TargetSystemStoreApiClient.ExecuteRequest(basicEvent.FileName);
        }

        protected override Guid SaveMetadata(T basicEvent)
        {
            if(!ShouldSaveMetadata) return Guid.Empty; 
            RegisterStep(basicEvent, "SAVE_METADATA");
            return Guid.NewGuid();
        }
        protected override void UpdateMetadata(T basicEvent)
        {
            if(!ShouldSaveMetadata) return;
            RegisterStep(basicEvent, "UPDATE_PROCESSING");
        }
        
        protected override void Validate(T basicEvent)
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
