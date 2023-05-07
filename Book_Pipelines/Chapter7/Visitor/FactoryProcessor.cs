using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Visitor
{
    public static class FactoryCreator
    {
        private static AbstractFactory<IIoTEventData> iotFactory;
        private static AbstractFactory<IUploadEventData> fileUploadFactory;

        static FactoryCreator()
        {
            iotFactory = new IoTFactory();
            fileUploadFactory = new FileUploadFactory();    
        }

        public static void Execute(BasicEvent basicEvent)
        {
            switch (basicEvent.Source)
            {
                case "IOT":
                    {
                        iotFactory.GetPipeline(basicEvent).Process(basicEvent as BaseIoTEvent);
                        break;
                    }
                case ("FILE"):
                    {
                        fileUploadFactory.GetPipeline(basicEvent).Process(basicEvent as IUploadEventData);   
                        break;
                    }
                case ("REPORT"):
                    {
                        fileUploadFactory.GetPipeline(basicEvent).Process(basicEvent as IUploadEventData);
                        iotFactory.GetPipeline(basicEvent).Process(basicEvent as IIoTEventData);
                        break;
                    }
                default:
                    throw new NotImplementedException($"Scenario for {basicEvent.Source} is not implemented");
            }
        }
    }
}
