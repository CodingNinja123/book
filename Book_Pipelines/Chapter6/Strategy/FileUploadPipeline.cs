using Book_Pipelines.Chapter6.Strategy.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Strategy
{
    public class FileUploadPipeline : AbstractPipeline<IUploadEventData>
    {
        public ICommunicationClient<UploadFileInfo, int> UploadFileClient { get; set; }
        public ICommunicationClient<string, string> TargetSystemSearchApiClient { get; set; }
        public ICommunicationClient<string, string> TargetSystemStoreApiClient { get; set; }
        public ICommunicationClient<string, byte[]> DownloadFileClient { get; set; }

        public override void Preprocess(IUploadEventData basicEvent)
        {
            RegisterStep(basicEvent, "EVENT_PREPROCESSING");
            DownloadFileClient.ExecuteRequest(basicEvent.FileUrl);
        }
        public override void ProcessEvent(IUploadEventData basicEvent)
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
        public override void Search(IUploadEventData basicEvent)
        {
            RegisterStep(basicEvent, "EVENT_SEARCH");
            TargetSystemSearchApiClient.ExecuteRequest(basicEvent.FileName);
        }
        public override void Store(IUploadEventData basicEvent)
        {
            RegisterStep(basicEvent, "EVENT_STORE");
            TargetSystemStoreApiClient.ExecuteRequest(basicEvent.FileName);
        }
        public override Guid SaveMetadata(IUploadEventData basicEvent)
        {
            RegisterStep(basicEvent, "SAVE_METADATA");
            return Guid.NewGuid();
        }
        public override void UpdateMetadata(IUploadEventData basicEvent)
        {
            RegisterStep(basicEvent, "UPDATE_PROCESSING");
        }
        public override void Validate(IUploadEventData basicEvent)
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
