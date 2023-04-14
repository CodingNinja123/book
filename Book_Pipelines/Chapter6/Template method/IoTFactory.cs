using Book_Pipelines.Chapter_2.AbstractFactoryNM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class IoTFactory: AbstractFactory<IIoTEventData>
    {
        public override AbstractPipeline<IIoTEventData> GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeC" => PipelineDirector.BuildTypeCPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
