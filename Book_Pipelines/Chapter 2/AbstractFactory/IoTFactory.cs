using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.AbstractFactoryNM
{
    public class IoTFactory: AbstractFactory
    {
        public override AbstractPipeline GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                Constants.C_EVENT_TYPE => new TypeCPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
