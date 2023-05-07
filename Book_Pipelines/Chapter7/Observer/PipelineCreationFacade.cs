
using Book_Pipelines.Chapter7.Chain_Of_Responsibility.Observer;
using Book_Pipelines.Chapter7.Observer.Logging;
namespace Book_Pipelines.Chapter7.Observer
{
    public static class PipelineCreationFacade
    {
        public static Processor BuildFileUploadPipelineA(ICommunicationClient<UploadFileInfo, int> fileUploadClient,
            ICommunicationClient<string, byte[]> fileDownloadClient, ICommunicationClient<string, string> searchApiClient,
            ICommunicationClient<string, string> storeApiClient
            )
        {
            Processor proc = new ExceptionHandlingProcessor(new SaveMetadataProcessor(
                new ValidateProcessor(
                    new PreProcessProcessor(
                        new SearchProcessor(
                            new ProcessEventProcessor(
                                new StoreProcessor(
                                    new UpdateMetadataProcessor(null),
                                    storeApiClient),
                                fileUploadClient),
                            searchApiClient),
                        fileDownloadClient))), GetFileLogger());
            return proc;
        }
        public static Processor BuildFileUploadPipelineB(ICommunicationClient<UploadFileInfo, int> fileUploadClient,
            ICommunicationClient<string, byte[]> fileDownloadClient, ICommunicationClient<string, string> searchApiClient,
            ICommunicationClient<string, string> storeApiClient
            )
        {
            var theLast = new ProcessEventProcessor(
                            null, fileUploadClient);
            Processor proc = new ExceptionHandlingProcessor(new ValidateProcessor(
                new PreProcessProcessor(
                    new SearchProcessor(
                        new ProcessEventProcessor(
                            null, fileUploadClient),
                        searchApiClient),
                    fileDownloadClient)), GetFileLogger());

            return proc;
        }


        public static Processor BuildIoTPipeline(ICommunicationClient<IoTData, string> apiClient)
        {
            Processor processor = new ExceptionHandlingProcessor(new SaveMetadataProcessor(
                new IoTValidateProcessor(
                    new IoTProcessEventProcessor(
                        new UpdateMetadataProcessor(null),
                apiClient))), GetFileLogger());

            return processor;
        }

        private static Logger GetFileLogger()
        {
            var fileLogger = new FileLogger();
            return new Logger(
                        new DateLoggingDecorator(
                            new LoggingDestinationDecorator(fileLogger)));
        }
    }
}
