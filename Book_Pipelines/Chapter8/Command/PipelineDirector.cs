

using Book_Pipelines.Chapter8.Chain_Of_Responsibility.Command;
using System.Diagnostics;


namespace Book_Pipelines.Chapter8.Command
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

        public static Processor BuildTypeAPipeline()
        {
            return PipelineCreationFacade.BuildFileUploadPipelineA(fileUploadAClient, fileDownloadClient, systemASearchClient, systemAStoreClient);
        }
        public static Processor BuildTypeBPipeline()
        {
            return PipelineCreationFacade.BuildFileUploadPipelineB(fileUploadBClient, fileDownloadClient, systemASearchClient, null);
        }
        public static Processor BuildTypeCPipeline() 
        {
            return PipelineCreationFacade.BuildIoTPipeline(systemCApiClient);
        }
        public static Processor BuildTypeRPipeline(ProcessorMediator mediator)
        {
            return PipelineCreationFacade.BuildReportPipeline(mediator);
        }
    }
}
