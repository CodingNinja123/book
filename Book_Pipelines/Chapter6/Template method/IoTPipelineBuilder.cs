using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class IoTPipelineBuilder<T, T1> where T: IoTPipeline<T1>, new()
        where T1: IIoTEventData
    {
        private T pipeline = default;

        public IoTPipelineBuilder()
        {
            this.pipeline = new T();
        }

        public IoTPipelineBuilder<T,T1> ShouldSaveMetadata(bool shouldSaveMetadata)
        {
            pipeline.ShouldSaveMetadata = shouldSaveMetadata;
            return this;
        }

        public IoTPipelineBuilder<T, T1> SetTargetApiClient(ICommunicationClient<IoTData,string> targetCProcessingClient)
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
