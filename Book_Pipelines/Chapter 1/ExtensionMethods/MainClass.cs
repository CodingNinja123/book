using Book_Pipelines.Chapter_1.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.ExtensionMethods
{
    public static class MainClass
    {
        public static void Main()
        {
            var extendedPipeline = new ExtendedPipeline();

            ExtendedEvent basicEvent = new ExtendedEvent
            {
                Id = Guid.NewGuid(),
                Data = "Some data",
                Type = "Ver3",
                Source = "Console App"
            };

            ExtendedEvent extendedEvent = new ExtendedEvent
            {
                Data = "Some data",
                Id = Guid.NewGuid(),
                Source = "Console App",
                Type = "Ver3"
            };

            List<ExtendedEvent> events = new List<ExtendedEvent> { basicEvent, extendedEvent };
            extendedPipeline.BatchProcessing(events);
        }
    }
}
