using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.BuilderAbstractFactory
{
    public static class PipelineDirector
    {
        private static string targetASystemUploadUrl = "http://file.storage.com/systemA/upload";
        private static string targetASystemApiUrl = "http://systemA.com/api";

        private static string targetBSystemUploadUrl = "http://file.storage.com/systemB/upload";
        private static string targetBSystemApiUrl = "http://systemB.com/api";

        private static string targetCSystemApiUrl = "http://systemC.com/api";
        private static string targetCSystemProcessingApiUrl = "http://systemC.processing.com/api";

        public static AbstractPipeline BuildTypeAPipeline()
        {
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            return typeAPipelineBuilder.
                ShouldBeFilePreprocessed(true).
                ShouldBeEventStored(true).
                ShouldSaveMetadata(true).
                SetTargetSystemApiUrl(targetASystemApiUrl).
                SetTargetSystemUploadUrl(targetASystemUploadUrl).
                Build();
        }

        public static AbstractPipeline BuildTypeBPipeline()
        {
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            return typeAPipelineBuilder.
                ShouldBeFilePreprocessed(true).
                ShouldBeEventStored(false).
                ShouldSaveMetadata(false).
                SetTargetSystemApiUrl(targetBSystemApiUrl).
                SetTargetSystemUploadUrl(targetBSystemUploadUrl).
                Build();
        }

        public static AbstractPipeline BuildTypeCPipeline()
        {
            var typeCPipeline = new IoTPipelineBuilder<IoTPipeline>();

            return typeCPipeline.
                ShouldSaveMetadata(true).
                SetApiUrl(targetCSystemApiUrl).
                SetTargetApiUrl(targetCSystemProcessingApiUrl).
                Build();

        }
    }
}
