namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class PipelineCreationFacade: IPipelineCreationFacade
    {
        private IDashboardNotificationClient notificationClient;
        public PipelineCreationFacade(IDashboardNotificationClient notificationClient)
        {
            this.notificationClient = notificationClient;
        }

        public AbstractPipeline BuildFileUploadPipeline(bool shouldBeFileProcessed, bool shouldEventBeStored, bool shouldSaveMetadata,
            IFileUploadClient fileUploadClient, IFileDownloadClient fileDownloadClient,
            ISystemASearchApiClient searchApiClient, ISystemAStoreApiClient storeApiClient
            )
        {
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
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
        public AbstractPipeline BuildIoTPipeline(bool shouldSaveMetadata, ISystemCAPIClient apiClient)
        {
            var typeCPipelineBuilder = new IoTPipelineBuilder<IoTPipeline>();
            return BuildExceptionHandlingPipeline(typeCPipelineBuilder.
                ShouldSaveMetadata(shouldSaveMetadata).
                SetTargetApiClient(apiClient).
                Build());
        }
        public AbstractPipeline BuildReportPipeline(bool shouldBeFileProcessed, bool shouldEventBeStored, bool shouldSaveMetadata,
            IFileUploadClient fileUploadClient, IFileDownloadClient fileDownloadClient, 
            ISystemASearchApiClient searchApiClient, ISystemAStoreApiClient storeApiClient,
            bool shouldSaveIoTMetadata, ISystemCAPIClient apiClient)
        {
            return new ReportPipeline(new List<AbstractPipeline> { 
                BuildFileUploadPipeline(shouldBeFileProcessed, shouldEventBeStored, shouldSaveMetadata, fileUploadClient,
                fileDownloadClient, searchApiClient, storeApiClient), BuildIoTPipeline(shouldSaveIoTMetadata, apiClient) });
        }
        private AbstractPipeline BuildExceptionHandlingPipeline(AbstractPipeline internalPipeline)
        {
            var exceptionPipelineBuilder = new ExceptionHandlingPipelineBuilder<ExceptionHandlingPipeline>();
            return exceptionPipelineBuilder.
                SetLoggingClient(notificationClient).
                SetInternalPipeline(internalPipeline).
                Build();
        }
    }
}
