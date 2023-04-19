namespace Book_Pipelines.Chapter6.Strategy
{
    public class FilePipelineBuilder<T> where T : FileUploadPipeline, new()
    {
        private T pipeline = default;
        public FilePipelineBuilder()
        {
            this.pipeline = new T();
        }

        public FilePipelineBuilder<T> SetSearchApiClient(ICommunicationClient<string, string> targetSystemApiClient)
        {
            pipeline.TargetSystemSearchApiClient = targetSystemApiClient;
            return this;
        }

        public FilePipelineBuilder<T> SetStoreApiClient(ICommunicationClient<string, string> targetSystemApiClient)
        {
            pipeline.TargetSystemStoreApiClient = targetSystemApiClient;
            return this;
        }

        public FilePipelineBuilder<T> SetDownloadClient(ICommunicationClient<string, byte[]> downloadFileClient)
        {
            pipeline.DownloadFileClient = downloadFileClient;
            return this;
        }

        public FilePipelineBuilder<T> SetUploadClient(ICommunicationClient<UploadFileInfo, int> uploadFileClient)
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
