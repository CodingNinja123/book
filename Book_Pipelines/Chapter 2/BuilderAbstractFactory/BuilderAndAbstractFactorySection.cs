using Book_Pipelines.Chapter_1.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.BuilderAbstractFactory
{
    public class BuilderAndAbstractFactorySection
    {
        public static void Main()
        {
            var event1 = new BaseUploadEvent 
            { 
                Source = "FILE", 
                Type = "TypeA", 
                FileName = "TypeAFilename.jpg", 
                FileType = ".jpg", 
                FileUrl = "http://typeA.file.url", 
                Id = Guid.NewGuid() 
            };

            var event2 = new BaseUploadEvent
            {
                Source = "FILE",
                Type = "TypeB",
                FileName = "TypeBFilename.jpg",
                FileType = ".jpg",
                FileUrl = "http://typeB.file.url",
                Id = Guid.NewGuid()
            };
            var event3 = new BaseIoTEvent
            {
                Source = "IOT",
                Type = "TypeC",
                Action = "TEMP_UPDATE",
                Value = 92.2.ToString(),
                Id = Guid.NewGuid()
            };

            List<BasicEvent> eventList = new List<BasicEvent> { event1, event2, event3 };

            eventList.ForEach(eventObj =>
            {
                FactoryCreator.GetPipelineFactory(eventObj).GetPipeline(eventObj).Process(eventObj);
            });
        }
    }
}
