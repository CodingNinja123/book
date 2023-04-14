namespace Book_Pipelines.Chapter5.TemplateMethod
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
