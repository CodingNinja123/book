
using Book_Pipelines.Chapter8.Chain_Of_Responsibility.Mediator;
using Book_Pipelines.Chapter8.Mediator.Chain;
using Book_Pipelines.Chapter8.Mediator.Logging;
namespace Book_Pipelines.Chapter8.Mediator
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
                        fileDownloadClient))), GetFileLogger(), new Chain.ProcessorVisitor());
            return proc;
        }
        public static Processor BuildFileUploadPipelineB(ICommunicationClient<UploadFileInfo, int> fileUploadClient,
            ICommunicationClient<string, byte[]> fileDownloadClient, ICommunicationClient<string, string> searchApiClient,
            ICommunicationClient<string, string> storeApiClient
            )
        {
            Processor proc = new ExceptionHandlingProcessor(new ValidateProcessor(
                new PreProcessProcessor(
                    new SearchProcessor(
                        new ProcessEventProcessor(
                            null, fileUploadClient),
                        searchApiClient),
                    fileDownloadClient)), GetFileLogger(), new Chain.ProcessorVisitor());

            return proc;
        }


        public static Processor BuildIoTPipeline(ICommunicationClient<IoTData, string> apiClient)
        {
            Processor processor = new ExceptionHandlingProcessor(new SaveMetadataProcessor(
                new IoTValidateProcessor(
                    new IoTProcessEventProcessor(
                        new UpdateMetadataProcessor(null),
                apiClient))), GetFileLogger(), new Chain.ProcessorVisitor());

            return processor;
        }

        public static Processor BuildReportPipeline(ProcessorMediator mediator)
        {
            Processor processor = 
                new ExceptionHandlingProcessor(
                    new EmitterProcessor(null,mediator), 
                GetFileLogger(), new Chain.ProcessorVisitor());

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
