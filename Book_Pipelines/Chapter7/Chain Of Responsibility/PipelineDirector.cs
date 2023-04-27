﻿using Book_Pipelines.Chapter3.Prototype;
using Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Book_Pipelines.Chapter7.Observer
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
    }
}
