﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter4.Adapter
{
    public class FilePipelineBuilder<T> where T : FileUploadPipeline, new()
    {
        private T pipeline = default;
        public FilePipelineBuilder()
        {
            this.pipeline = new T();
        }

        public FilePipelineBuilder<T> ShouldSaveMetadata(bool shouldSaveMetadata)
        {
            pipeline.ShouldSaveMetadata = shouldSaveMetadata;
            return this;
        }

        public FilePipelineBuilder<T> ShouldBeFilePreprocessed(bool shouldBeFilePreprocessed)
        {
            pipeline.ShouldBeFilePreprocessed = shouldBeFilePreprocessed;
            return this;
        }

        public FilePipelineBuilder<T> ShouldBeEventStored(bool shouldBeEventStored)
        {
            pipeline.ShouldBeEventStored = shouldBeEventStored;
            return this;
        }

        public FilePipelineBuilder<T> SetTargetSystemSearchApiClient(ICommunicationClient<string, string> targetSystemApiClient)
        {
            pipeline.TargetSystemSearchApiClient = targetSystemApiClient;
            return this;
        }

        public FilePipelineBuilder<T> SetTargetSystemStoreApiClient(ICommunicationClient<string, string> targetSystemApiClient)
        {
            pipeline.TargetSystemStoreApiClient = targetSystemApiClient;
            return this;
        }

        public FilePipelineBuilder<T> SetTargetSystemDownloadClient(ICommunicationClient<string, byte[]> downloadFileClient)
        {
            pipeline.DownloadFileClient = downloadFileClient;
            return this;
        }

        public FilePipelineBuilder<T> SetTargetSystemUploadClient(ICommunicationClient<UploadFileInfo, int> uploadFileClient)
        {
            pipeline.UploadFileClient = uploadFileClient;
            return this;
        }
        
        public T Build()
        {
            return pipeline;
        }
    }
}
