using Book_Pipelines.Chapter_2.AbstractFactoryNM;
using Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.ChainOfResponsibility
{
    public class IoTFactory: AbstractFactory<IIoTEventData>
    {
        public override Processor GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                Constants.C_EVENT_TYPE => PipelineDirector.BuildTypeCPipeline(),
                Constants.R_EVENT_TYPE => PipelineDirector.BuildTypeCPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
