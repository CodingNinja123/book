using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public static class FactoryCreator
    {
        private static AbstractFactory<IIoTEventData> iotFactory;
        private static AbstractFactory<IUploadEventData> fileUploadFactory;
        private static AbstractFactory<ReportEvent> reportFactory;

        static FactoryCreator()
        {
            iotFactory = new IoTFactory();
            fileUploadFactory = new FileUploadFactory();    
            reportFactory = new ReportFactory();    
        }

        public static void Execute(BasicEvent basicEvent)
        {
            switch (basicEvent.Source)
            {
                case Constants.IOT_EVENT_SOURCE:
                    {
                        iotFactory.GetPipeline(basicEvent).Process(basicEvent as BaseIoTEvent);
                        break;
                    }
                case Constants.FILE_EVENT_SOURCE:
                    {
                        fileUploadFactory.GetPipeline(basicEvent).Process(basicEvent as BaseUploadEvent);
                        break;
                    }
                case Constants.REPORT_EVENT_SOURCE:
                    {
                        reportFactory.GetPipeline(basicEvent).Process(basicEvent as ReportEvent);
                        break;
                    }
                default:
                    throw new NotImplementedException($"Scenario for {basicEvent.Source} is not implemented");
            }
        }


        //public static AbstractFactory GetPipelineFactory<T>(BasicEvent basicEvent)
        //{
        //    iotFactory.GetPipeline(basicEvent).Process(basicEvent as BaseIoTEvent);

        //    return basicEvent.Source switch
        //    {
        //        "IOT" => iotFactory.GetPipeline(basicEvent).Process(basicEvent as BaseIoTEvent),
        //        "FILE" => fileUploadFactory,
        //        // "REPORT" => reportFactory,
        //        _ => throw new NotImplementedException("Cannot create a factory for non known source"),
        //    };
        //}
    }
}
