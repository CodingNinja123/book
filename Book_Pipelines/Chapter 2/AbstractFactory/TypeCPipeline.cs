using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.AbstractFactoryNM
{
    public class TypeCPipeline : IoTPipeline
    {
        private string targetSystemApiUrl = "http://systemC.test/api";
        private string targetSystemProcessingApiUel = "http://systemC.processing.test/api";

        protected override void ProcessEvent(BasicEvent basicEvent)
        {
            var iotEvent = basicEvent as BaseIoTEvent;
            Notify(basicEvent, "Processing event");
            Notify(basicEvent, $"Calling {this.targetSystemProcessingApiUel} to process a values of event: {iotEvent.Action} {iotEvent.Value}");
        }

        protected void Store(BasicEvent basicEvent)
        {
            Notify(basicEvent, "Storing event in the target system");
            Notify(basicEvent, $"Calling {this.targetSystemApiUrl} api to save the data about received values");
        }
    }
}
