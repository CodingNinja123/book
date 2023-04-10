using Book_Pipelines.Chapter3.Prototype;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Book_Pipelines.Chapter5.Proxy
{
    public static class PipelineDirector
    {
        private static Configuration config = Configuration.Instance;
        private static SystemAApiClient systemASearchClient = new (config.ASystemSearchApi);
        private static SystemAApiClient systemAStoreClient = new (config.ASystemStoreApi);
        private static FileUploadClient fileUploadAClient = new (config.ASystemUploadUrl);
        private static FileUploadClient fileUploadBClient = new (config.BSystemUploadUrl);
        private static FileDownloadClient fileDownloadClient = new ();
        private static SystemCApiClient systemCApiClient = new (config.CSystemApi);
        private static DashboardNotificationClient dashboardClient = new(config.DashboardLoggingUrl);

        public static AbstractPipeline BuildTypeAPipeline()
        {
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            return BuildExceptionHandlingPipeline(typeAPipelineBuilder.
                ShouldBeFilePreprocessed(true).
                ShouldBeEventStored(true).
                ShouldSaveMetadata(true).
                SetUploadClient(fileUploadAClient).
                SetDownloadClient(fileDownloadClient).
                SetSearchApiClient(systemASearchClient).
                SetStoreApiClient(systemAStoreClient).
                Build());
        }
        public static AbstractPipeline BuildTypeBPipeline()
        {
            var typeBPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            return BuildExceptionHandlingPipeline(typeBPipelineBuilder.
                ShouldBeFilePreprocessed(true).
                ShouldBeEventStored(false).
                ShouldSaveMetadata(false).
                SetUploadClient(fileUploadBClient).
                SetDownloadClient(fileDownloadClient).
                SetSearchApiClient(systemASearchClient).
                Build());
        }
        public static AbstractPipeline BuildTypeCPipeline()
        {
            var typeCPipelineBuilder = new IoTPipelineBuilder<IoTPipeline>();
            return BuildExceptionHandlingPipeline(typeCPipelineBuilder.
                ShouldSaveMetadata(true).
                SetTargetApiClient(systemCApiClient).
                Build());
        }

        private static AbstractPipeline BuildExceptionHandlingPipeline(AbstractPipeline internalPipeline)
        {
            var exceptionPipelineBuilder = new ExceptionHandlingPipelineBuilder<ExceptionHandlingPipeline>();
            return exceptionPipelineBuilder.
                SetLoggingClient(dashboardClient).
                SetInternalPipeline(internalPipeline).
                Build();
        }

        public static AbstractPipeline BuildReportPipeline()
        {
            return new ReportPipeline(new List<AbstractPipeline> { BuildTypeBPipeline(), BuildTypeCPipeline() });
        }
    }
}
