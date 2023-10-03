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
