namespace Book_Pipelines.Chapter6.Strategy
{
    public class IoTPipelineBuilder<T> where T: IoTPipeline, new()
    {
        private T pipeline = default;

        public IoTPipelineBuilder()
        {
            this.pipeline = new T();
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
