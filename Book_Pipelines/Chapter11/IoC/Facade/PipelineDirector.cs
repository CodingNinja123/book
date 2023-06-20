using Book_Pipelines.Chapter3.Prototype;
using Book_Pipelines.Chapter5.Bridge;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class PipelineDirector: IPipelineDirector
    {
        private ISystemASearchApiClient systemASearchClient;
        private ISystemAStoreApiClient systemAStoreClient;
        private IFileAUploadClient fileUploadAClient;
        private IFileBUploadClient fileUploadBClient;
        private IFileDownloadClient fileDownloadClient;
        private ISystemCAPIClient systemCApiClient;
        private IPipelineCreationFacade pipelineCreationFacade;

        public PipelineDirector(ISystemASearchApiClient systemASearchClient,
            ISystemAStoreApiClient systemAStoreClient, IFileAUploadClient fileUploadAClient,
            IFileBUploadClient fileUploadBClient, IFileDownloadClient fileDownloadClient,
            ISystemCAPIClient systemCApiClient,
            IPipelineCreationFacade pipelineCreationFacade)
        {
            this.systemASearchClient = systemASearchClient;
            this.systemAStoreClient = systemAStoreClient;
            this.fileUploadAClient = fileUploadAClient;
            this.fileUploadBClient = fileUploadBClient;
            this.fileDownloadClient = fileDownloadClient;
            this.systemCApiClient = systemCApiClient;
            this.pipelineCreationFacade = pipelineCreationFacade;
        }

        public AbstractPipeline BuildTypeAPipeline()
        {
            return pipelineCreationFacade.BuildFileUploadPipeline(true, true, true, 
                fileUploadAClient, fileDownloadClient, systemASearchClient, systemAStoreClient);
        }
        public AbstractPipeline BuildTypeBPipeline()
        {
            return pipelineCreationFacade.BuildFileUploadPipeline(true, false, false,
                fileUploadBClient, fileDownloadClient, systemASearchClient, null);
        }
        public AbstractPipeline BuildTypeCPipeline()
        {
            return pipelineCreationFacade.BuildIoTPipeline(true, systemCApiClient);
        }

        public AbstractPipeline BuildReportPipeline()
        {
            return new ReportPipeline(new List<AbstractPipeline> { BuildTypeBPipeline(), BuildTypeCPipeline() });
        }
    }
}
