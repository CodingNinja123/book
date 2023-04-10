using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.Bridge
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

        public IoTPipelineBuilder<T> SetTargetApiClient(ICommunicationClient<IoTData,string> targetCProcessingClient)
        {
            pipeline.SystemCApiClient = targetCProcessingClient;
            return this;
        }

        public T Build()
        {
            return this.pipeline;
        }
    }
}
