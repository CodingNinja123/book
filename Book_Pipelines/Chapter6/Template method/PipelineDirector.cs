using Book_Pipelines.Chapter3.Prototype;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Book_Pipelines.Chapter5.TemplateMethod
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

        public static AbstractPipeline<IUploadEventData> BuildTypeAPipeline()
        {
            return PipelineCreationFacade.BuildFileUploadPipeline(true, true, true, 
                fileUploadAClient, fileDownloadClient, systemASearchClient, systemAStoreClient);
        }
        public static AbstractPipeline<IUploadEventData> BuildTypeBPipeline()
        {
            return PipelineCreationFacade.BuildFileUploadPipeline(true, false, false,
                fileUploadBClient, fileDownloadClient, systemASearchClient, null);
        }
        public static AbstractPipeline<T> BuildTypeCPipeline<T>() where T: IIoTEventData
        {
            return PipelineCreationFacade.BuildIoTPipeline<T>(true, systemCApiClient);
        }

        public static AbstractPipeline<IBasicEvent> BuildReportPipeline()
        {
            throw new NotImplementedException("Implement Report Pipeline");
            // return PipelineCreationFacade.BuildReportPipeline(new List<AbstractPipeline<IBasicEvent>> { BuildTypeBPipeline(), BuildTypeCPipeline() });
        }
    }
}
