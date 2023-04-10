using Book_Pipelines.Chapter3.Prototype;
using System.Reflection.Metadata.Ecma335;

namespace Book_Pipelines.Chapter3.Prototype
{
    public static class PipelineDirector
    {
        private static Configuration configurationInstance;
        private static FileUploadPipeline typeAPipeline;
        private static FileUploadPipeline typeBPipeline;
        private static IoTPipeline typeCPipeline;

        static PipelineDirector()
        {
            configurationInstance = Configuration.Instance;
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            typeAPipeline = typeAPipelineBuilder.
                ShouldBeFilePreprocessed(true).
                ShouldBeEventStored(true).
                ShouldSaveMetadata(true).
                SetTargetSystemApiUrl(configurationInstance.TargetASystemApiUrl).
                SetTargetSystemUploadUrl(configurationInstance.TargetASystemUploadUrl).
                Build();
            var typeBPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            typeBPipeline = typeBPipelineBuilder.
                ShouldBeFilePreprocessed(true).
                ShouldBeEventStored(false).
                ShouldSaveMetadata(false).
                SetTargetSystemApiUrl(configurationInstance.TargetBSystemApiUrl).
                SetTargetSystemUploadUrl(configurationInstance.TargetBSystemUploadUrl).
                Build();
            var typeCPipelineBuilder = new IoTPipelineBuilder<IoTPipeline>();
            typeCPipeline = typeCPipelineBuilder.
                ShouldSaveMetadata(true).
                SetApiUrl(configurationInstance.TargetCSystemApiUrl).
                SetTargetApiUrl(configurationInstance.TargetCSystemProcessingApiUrl).
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
