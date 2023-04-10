using Book_Pipelines.Chapter3.Prototype;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Book_Pipelines.Chapter4.Adapter
{
    public static class PipelineDirector
    {
        private static Configuration configurationInstance = Configuration.Instance;
        private static FileUploadPipeline typeAPipeline;
        private static FileUploadPipeline typeBPipeline;
        private static IoTPipeline typeCPipeline;

        private static SystemAApiClient systemASearchClient = new SystemAApiClient(configurationInstance.TargetASystemSearchApiUrl);
        private static SystemAApiClient systemAStoreClient = new SystemAApiClient(configurationInstance.TargetASystemStoreApiUrl);
        private static FileUploadClient fileUploadAClient = new FileUploadClient(configurationInstance.TargetASystemUploadUrl);
        private static FileUploadClient fileUploadBClient = new FileUploadClient(configurationInstance.TargetBSystemUploadUrl);
        private static FileDownloadClient fileDownloadClient = new FileDownloadClient();
        private static SystemCApiClient systemCApiClient = new SystemCApiClient(configurationInstance.TargetCSystemProcessingApiUrl);


        static PipelineDirector()
        {
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            typeAPipeline = typeAPipelineBuilder.
                ShouldBeFilePreprocessed(true).
                ShouldBeEventStored(true).
                ShouldSaveMetadata(true).
                SetTargetSystemUploadClient(fileUploadAClient).
                SetTargetSystemDownloadClient(fileDownloadClient).
                SetTargetSystemSearchApiClient(systemASearchClient).
                SetTargetSystemStoreApiClient(systemAStoreClient).
                Build();
            var typeBPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            typeBPipeline = typeBPipelineBuilder.
                ShouldBeFilePreprocessed(true).
                ShouldBeEventStored(false).
                ShouldSaveMetadata(false).
                SetTargetSystemUploadClient(fileUploadBClient).
                SetTargetSystemDownloadClient(fileDownloadClient).
                SetTargetSystemSearchApiClient(systemASearchClient).
                Build();
            var typeCPipelineBuilder = new IoTPipelineBuilder<IoTPipeline>();
            typeCPipeline = typeCPipelineBuilder.
                ShouldSaveMetadata(true).
                SetTargetApiClient(systemCApiClient).
                Build();
        }
        public static AbstractPipeline BuildTypeAPipeline()
        {
            typeAPipeline = typeAPipeline.Copy();
            return typeAPipeline;
        }
        public static AbstractPipeline BuildTypeBPipeline()
        {
            typeBPipeline = typeBPipeline.Copy();
            return typeBPipeline;
        }
        public static AbstractPipeline BuildTypeCPipeline()
        {
            typeCPipeline = typeCPipeline.Copy();
            return typeCPipeline;
        }
    }
}
