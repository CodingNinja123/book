using Book_Pipelines.Chapter6.Strategy.Logging;
namespace Book_Pipelines.Chapter6.Strategy
{
    public static class PipelineCreationFacade
    {
        public static AbstractPipeline<IUploadEventData> BuildFileUploadPipelineA(ICommunicationClient<UploadFileInfo, int> fileUploadClient,
            ICommunicationClient<string, byte[]> fileDownloadClient, ICommunicationClient<string, string> searchApiClient,
            ICommunicationClient<string, string> storeApiClient
            )
        {
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            var pipeline = typeAPipelineBuilder.
                SetUploadClient(fileUploadClient).
                SetDownloadClient(fileDownloadClient).
                SetSearchApiClient(searchApiClient).
                SetStoreApiClient(storeApiClient).
                Build();
            var strategy = new FileUploadStrategyA(pipeline);
            return BuildExceptionHandlingPipeline(pipeline, strategy);
        }
        public static AbstractPipeline<IUploadEventData> BuildFileUploadPipelineB(ICommunicationClient<UploadFileInfo, int> fileUploadClient,
            ICommunicationClient<string, byte[]> fileDownloadClient, ICommunicationClient<string, string> searchApiClient,
            ICommunicationClient<string, string> storeApiClient
            )
        {
            
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            var pipeline = typeAPipelineBuilder.
                SetUploadClient(fileUploadClient).
                SetDownloadClient(fileDownloadClient).
                SetSearchApiClient(searchApiClient).
                SetStoreApiClient(storeApiClient).
                Build();
            FileUploadStrategyB strategy = new FileUploadStrategyB(pipeline);
            return BuildExceptionHandlingPipeline(pipeline, strategy);
        }


        public static AbstractPipeline<IIoTEventData> BuildIoTPipeline(ICommunicationClient<IoTData, string> apiClient)
        {
            var typeCPipelineBuilder = new IoTPipelineBuilder<IoTPipeline>();
            var pipeline = typeCPipelineBuilder.
                SetTargetApiClient(apiClient).
                Build();
            IoTStrategy strategy = new IoTStrategy(pipeline);
            return BuildExceptionHandlingPipeline(pipeline, strategy);
        }

        private static AbstractPipeline<T> BuildExceptionHandlingPipeline<T>(AbstractPipeline<T> internalPipeline, 
            IStrategy<T> processingStrategy) where T : IBasicEvent
        {
            var exceptionPipelineBuilder = new ExceptionHandlingPipelineBuilder<ExceptionHandlingPipeline<T>, T>();
            return exceptionPipelineBuilder.
                SetLoggingClient(GetFileLogger()).
                SetInternalPipeline(internalPipeline).
                SetProcessingStrategy(processingStrategy).
                Build();
        }

        private static ILoggingDestination GetFileLogger()
        {
            var fileLogger = new DashboardLogger();
            return new NewLineLoggingDecorator(
                        new DateLoggingDecorator(
                            new LoggingDestinationDecorator(fileLogger)));
        }
    }
}
