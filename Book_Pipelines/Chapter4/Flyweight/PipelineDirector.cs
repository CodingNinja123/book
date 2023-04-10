using Book_Pipelines.Chapter3.Prototype;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Book_Pipelines.Chapter4.Flyweight
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

        public static AbstractPipeline BuildTypeAPipeline()
        {
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            return typeAPipelineBuilder.
                ShouldBeFilePreprocessed(true).
                ShouldBeEventStored(true).
                ShouldSaveMetadata(true).
                SetUploadClient(fileUploadAClient).
                SetDownloadClient(fileDownloadClient).
                SetSearchApiClient(systemASearchClient).
                SetStoreApiClient(systemAStoreClient).
                Build();
        }
        public static AbstractPipeline BuildTypeBPipeline()
        {
            var typeBPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            return typeBPipelineBuilder.
                ShouldBeFilePreprocessed(true).
                ShouldBeEventStored(false).
                ShouldSaveMetadata(false).
                SetUploadClient(fileUploadBClient).
                SetDownloadClient(fileDownloadClient).
                SetSearchApiClient(systemASearchClient).
                Build();
        }
        public static AbstractPipeline BuildTypeCPipeline()
        {
            var typeCPipelineBuilder = new IoTPipelineBuilder<IoTPipeline>();
            return typeCPipelineBuilder.
                ShouldSaveMetadata(true).
                SetTargetApiClient(systemCApiClient).
                Build();
        }
        public static AbstractPipeline BuildReportPipeline()
        {
            return new ReportPipeline(new List<AbstractPipeline> { BuildTypeBPipeline(), BuildTypeCPipeline() });
        }
    }
}
