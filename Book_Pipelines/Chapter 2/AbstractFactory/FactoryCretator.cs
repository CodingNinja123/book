using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.AbstractFactoryNM
{
    public static class FactoryCreator
    {
        public static AbstractFactory GetPipelineFactory(BasicEvent basicEvent)
        {
            return basicEvent.Source switch
            {
                Constants.IOT_EVENT_SOURCE => new IoTFactory(),
                Constants.FILE_EVENT_SOURCE => new FileUploadFactory(),
                _ => throw new NotImplementedException("Cannot create a factory for non known source"),
            };
        }
    }
}
