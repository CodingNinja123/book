using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.Factory
{
    public static class PipelineFactory
    {
        public static AbstractPipeline CreatePipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                Constants.A_EVENT_TYPE => new TypeAProcessingPipeline(),
                Constants.B_EVENT_TYPE => new TypeBProcessingPipeline(),
                Constants.C_EVENT_TYPE => new TypeCProcessingPipeline(),
                _ => throw new NotImplementedException("There is no such pipeline to process passed event") 
            };
        }
    }
}
