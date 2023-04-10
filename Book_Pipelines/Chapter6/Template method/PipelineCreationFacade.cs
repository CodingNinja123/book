using Book_Pipelines.Chapter5.TemplateMethod.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public static class PipelineCreationFacade
    {
        public static AbstractPipeline<IUploadEventData> BuildFileUploadPipeline(bool shouldBeFileProcessed, bool shouldEventBeStored, bool shouldSaveMetadata,
            ICommunicationClient<UploadFileInfo, int> fileUploadClient, ICommunicationClient<string, byte[]> fileDownloadClient,
            ICommunicationClient<string, string> searchApiClient, ICommunicationClient<string, string> storeApiClient
            ) 
        {
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline<IUploadEventData>, IUploadEventData>();
            return BuildExceptionHandlingPipeline(typeAPipelineBuilder.
                ShouldBeFilePreprocessed(shouldBeFileProcessed).
                ShouldBeEventStored(shouldEventBeStored).
                ShouldSaveMetadata(shouldSaveMetadata).
                SetUploadClient(fileUploadClient).
                SetDownloadClient(fileDownloadClient).
                SetSearchApiClient(searchApiClient).
                SetStoreApiClient(storeApiClient).
                Build());
        }
        public static AbstractPipeline<T> BuildIoTPipeline<T>(bool shouldSaveMetadata, ICommunicationClient<IoTData, string> apiClient)
            where T : IIoTEventData
        {
            var typeCPipelineBuilder = new IoTPipelineBuilder<IoTPipeline<T>, T>();
            return BuildExceptionHandlingPipeline(typeCPipelineBuilder.
                ShouldSaveMetadata(shouldSaveMetadata).
                SetTargetApiClient(apiClient).
                Build());
        }

        public static AbstractPipeline<T> BuildReportPipeline<T>(List<AbstractPipeline<T>> pipelines) where T : IBasicEvent
        {
            return BuildExceptionHandlingPipeline(new ReportPipeline<T>(pipelines));
        }
        private static AbstractPipeline<T> BuildExceptionHandlingPipeline<T>(AbstractPipeline<T> internalPipeline) where T : IBasicEvent
        {
            var exceptionPipelineBuilder = new ExceptionHandlingPipelineBuilder<ExceptionHandlingPipeline<T>, T>();
            return exceptionPipelineBuilder.
                SetLoggingClient(GetFileLogger()).
                SetInternalPipeline(internalPipeline).
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
