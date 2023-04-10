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
                "TypeA" => new TypeAProcessingPipeline(),
                "TypeB" => new TypeBProcessingPipeline(),
                "TypeC" => new TypeCProcessingPipeline(),
                _ => throw new NotImplementedException("There is no such pipeline to process passed event") 
            };
        }
    }
}
