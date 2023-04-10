using Book_Pipelines.Chapter_2.AbstractFactoryNM;
using Book_Pipelines.Chapter_2.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter4.Adapter
{
    public class IoTFactory: AbstractFactory
    {
        public override AbstractPipeline GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeC" => PipelineDirector.BuildTypeCPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
