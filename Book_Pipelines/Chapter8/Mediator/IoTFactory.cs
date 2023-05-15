

using Book_Pipelines.Chapter8.Chain_Of_Responsibility.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Mediator
{
    public class IoTFactory: AbstractFactory<IIoTEventData>
    {
        public override Processor GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeC" => PipelineDirector.BuildTypeCPipeline(),
                "TypeR" => PipelineDirector.BuildTypeCPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
