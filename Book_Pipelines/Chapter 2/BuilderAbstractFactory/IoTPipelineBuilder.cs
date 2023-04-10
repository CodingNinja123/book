using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.BuilderAbstractFactory
{
    public class IoTPipelineBuilder<T> where T: IoTPipeline, new()
    {
        private T pipeline = default;
        public IoTPipelineBuilder()
        {
            this.pipeline = new T();
        }
        
        public IoTPipelineBuilder<T> ShouldSaveMetadata(bool shouldSaveMetadata)
        {
            pipeline.ShouldSaveMetadata = shouldSaveMetadata;
            return this;
        }
        public IoTPipelineBuilder<T> SetApiUrl(string apiUrl)
        {
            pipeline.ApiUrl = apiUrl;
            return this;
        }

        public IoTPipelineBuilder<T> SetTargetApiUrl(string targetApiUrl)
        {
            pipeline.TargetSystemApiUrl = targetApiUrl;
            return this;
        }

        public T Build()
        {
            return this.pipeline;
        }
    }
}
