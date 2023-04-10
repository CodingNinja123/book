using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter4.Adapter
{
    public static class FactoryCreator
    {
        private static AbstractFactory iotFactory;
        private static AbstractFactory fileUploadFactory;

        static FactoryCreator()
        {
            iotFactory = new IoTFactory();
            fileUploadFactory = new FileUploadFactory();    
        }
        public static AbstractFactory GetPipelineFactory(BasicEvent basicEvent)
        {
            return basicEvent.Source switch
            {
                "IOT" => iotFactory,
                "FILE" => fileUploadFactory,
                _ => throw new NotImplementedException("Cannot create a factory for non known source"),
            };
        }
    }
}
