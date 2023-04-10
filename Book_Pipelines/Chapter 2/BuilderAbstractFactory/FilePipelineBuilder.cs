using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.BuilderAbstractFactory
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

        public FilePipelineBuilder<T> SetTargetSystemApiUrl(string apiUrl)
        {
            pipeline.TargetSystemApiUrl = apiUrl;
            return this;
        }

        public FilePipelineBuilder<T> SetTargetSystemUploadUrl(string targetApiUrl)
        {
            pipeline.TargetSystemUploadUrl = targetApiUrl;
            return this;
        }
        
        public T Build()
        {
            return pipeline;
        }
    }
}
