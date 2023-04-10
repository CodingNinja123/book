using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class FilePipelineBuilder<T, T1> where T : FileUploadPipeline<T1>, new()
        where T1: IUploadEventData
    {
        private T pipeline = default;
        public FilePipelineBuilder()
        {
            this.pipeline = new T();
        }

        public FilePipelineBuilder<T, T1> ShouldSaveMetadata(bool shouldSaveMetadata)
        {
            pipeline.ShouldSaveMetadata = shouldSaveMetadata;
            return this;
        }

        public FilePipelineBuilder<T, T1> ShouldBeFilePreprocessed(bool shouldBeFilePreprocessed)
        {
            pipeline.ShouldBeFilePreprocessed = shouldBeFilePreprocessed;
            return this;
        }

        public FilePipelineBuilder<T, T1> ShouldBeEventStored(bool shouldBeEventStored)
        {
            pipeline.ShouldBeEventStored = shouldBeEventStored;
            return this;
        }

        public FilePipelineBuilder<T, T1> SetSearchApiClient(ICommunicationClient<string, string> targetSystemApiClient)
        {
            pipeline.TargetSystemSearchApiClient = targetSystemApiClient;
            return this;
        }

        public FilePipelineBuilder<T, T1> SetStoreApiClient(ICommunicationClient<string, string> targetSystemApiClient)
        {
            pipeline.TargetSystemStoreApiClient = targetSystemApiClient;
            return this;
        }

        public FilePipelineBuilder<T, T1> SetDownloadClient(ICommunicationClient<string, byte[]> downloadFileClient)
        {
            pipeline.DownloadFileClient = downloadFileClient;
            return this;
        }

        public FilePipelineBuilder<T, T1> SetUploadClient(ICommunicationClient<UploadFileInfo, int> uploadFileClient)
        {
            pipeline.UploadFileClient = uploadFileClient;
            return this;
        }
        
        public T Build()
        {
            return this.pipeline;
        }
    }
}
