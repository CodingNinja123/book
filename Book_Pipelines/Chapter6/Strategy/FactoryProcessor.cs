using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Strategy
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
                case Constants.IOT_EVENT_SOURCE:
                    {
                        iotFactory.GetPipeline(basicEvent).ProcessEvent(basicEvent as BaseIoTEvent);
                        break;
                    }
                case Constants.FILE_EVENT_SOURCE:
                    {
                        fileUploadFactory.GetPipeline(basicEvent).ProcessEvent(basicEvent as IUploadEventData);   
                        break;
                    }
                case Constants.REPORT_EVENT_SOURCE:
                    {
                        fileUploadFactory.GetPipeline(basicEvent).ProcessEvent(basicEvent as IUploadEventData);
                        iotFactory.GetPipeline(basicEvent).ProcessEvent(basicEvent as IIoTEventData);
                        break;
                    }
                default:
                    throw new NotImplementedException($"Scenario for {basicEvent.Source} is not implemented");
            }
        }
    }
}
