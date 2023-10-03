using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.Builder
{
    public static class PipelineDirector
    {
        private static string targetASystemUploadUrl = "http://file.storage.test/systemA/upload";
        private static string targetASystemApiUrl = "http://systemA.test/api";

        private static string targetBSystemUploadUrl = "http://file.storage.test/systemB/upload";
        private static string targetBSystemApiUrl = "http://systemB.test/api";

        private static string targetCSystemApiUrl = "http://systemC.test/api";
        private static string targetCSystemProcessingApiUrl = "http://systemC.processing.test/api";

        public static AbstractPipeline BuildTypeAPipeline()
        {
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            return typeAPipelineBuilder.
                ShouldBeFilePreprocessed(false).
                ShouldBeEventStored(true).
                ShouldSaveMetadata(true).
                SetTargetSystemApiUrl(targetASystemUploadUrl).
                SetTargetSystemUploadUrl(targetASystemApiUrl).
                Build();
        }

        public static AbstractPipeline BuildTypeBPipeline()
        {
            var typeAPipelineBuilder = new FilePipelineBuilder<FileUploadPipeline>();
            return typeAPipelineBuilder.
                ShouldBeFilePreprocessed(false).
                ShouldBeEventStored(true).
                ShouldSaveMetadata(true).
                SetTargetSystemApiUrl(targetBSystemUploadUrl).
                SetTargetSystemUploadUrl(targetBSystemApiUrl).
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
