using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.Factory
{
    public class Chapter2Main
    {
        public static void Main()
        {
            EventTypeAData dataTypeA = new EventTypeAData 
            { 
                FileName = "event_type_a.file", 
                FileType = ".jpg", 
                FileUrl = "http://event.type.a.file.url" 
            };
            EventTypeBData dataTypeB = new EventTypeBData 
            { 
                FileName = "event_type_b.file", 
                FileType = ".png", 
                FileUrl = "http://event.type.b.file.url" 
            };
            BasicEvent basicEventA = new BasicEvent 
            { 
                Data = JsonSerializer.Serialize(dataTypeA), 
                EventGuid = Guid.NewGuid(), 
                Type = Constants.A_EVENT_TYPE 
            };
            BasicEvent basicEventB = new BasicEvent 
            { 
                Data = JsonSerializer.Serialize(dataTypeB), 
                EventGuid = Guid.NewGuid(), 
                Type = Constants.B_EVENT_TYPE
            };
            EventTypeC eventTypeC = new EventTypeC 
            { 
                EventGuid = Guid.NewGuid(), 
                Action = "Update", 
                Type = Constants.C_EVENT_TYPE
            };

            List<BasicEvent> basicEvents = new List<BasicEvent>
            {
                basicEventA, basicEventB, eventTypeC
            };

            basicEvents.ForEach(eventObj =>
            {
                Console.WriteLine("Processing new event!");
                var pipeline = PipelineFactory.CreatePipeline(eventObj);
                pipeline.Process(eventObj);
                Console.WriteLine();
            });
        }
    }
}
