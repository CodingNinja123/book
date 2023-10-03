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
            this.RegisterStep(basicEvent, "EVENT_PREPROCESSING");
            this.DownloadFileClient.ExecuteRequest(basicEvent.FileUrl);
        }
        public override void ProcessEvent(IUploadEventData basicEvent)
        {
            if (this.UploadFileClient == null)
                return;

            this.RegisterStep(basicEvent, "EVENT_PROCESSING");
            this.UploadFileClient.ExecuteRequest(new UploadFileInfo
            {
                FileName = basicEvent.FileName,
                Content = new byte[0]
            });
        }
        public override void Search(IUploadEventData basicEvent)
        {
            this.RegisterStep(basicEvent, "EVENT_SEARCH");
            this.TargetSystemSearchApiClient.ExecuteRequest(basicEvent.FileName);
        }
        public override void Store(IUploadEventData basicEvent)
        {
            this.RegisterStep(basicEvent, "EVENT_STORE");
            this.TargetSystemStoreApiClient.ExecuteRequest(basicEvent.FileName);
        }
        public override Guid SaveMetadata(IUploadEventData basicEvent)
        {
            this.RegisterStep(basicEvent, "SAVE_METADATA");
            return Guid.NewGuid();
        }
        public override void UpdateMetadata(IUploadEventData basicEvent)
        {
            this.RegisterStep(basicEvent, "UPDATE_PROCESSING");
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
