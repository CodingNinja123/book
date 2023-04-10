using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.BuilderAbstractFactory
{
    public static class FactoryCreator
    {
        public static AbstractFactory GetPipelineFactory(BasicEvent basicEvent)
        {
            return basicEvent.Source switch
            {
                "IOT" => new IoTFactory(),
                "FILE" => new FileUploadFactory(),
                _ => throw new NotImplementedException("Cannot create a factory for non known source"),
            };
        }
    }
}
