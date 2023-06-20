using Book_Pipelines.Chapter_2.AbstractFactoryNM;
using Book_Pipelines.Chapter_2.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class IoTFactory: IIoTFactory
    {
        private readonly IPipelineDirector director;

        public IoTFactory(IPipelineDirector director)
        {
            this.director = director;
        }
        public AbstractPipeline GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeC" => director.BuildTypeCPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
