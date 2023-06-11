﻿

using Book_Pipelines.Chapter11.IoC.Example;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class FacadeSection
    {
        public static void Main()
        {
            var container = new ServiceCollection();
            container.AddScoped<IGenericDependency, GenericDependency>();
            container.AddScoped<GenericService>();

            // Build the IoC and get a provider
            var provider = container.BuildServiceProvider();
            var service = provider.GetService<GenericService>();
            service.DoSomeCoolStuff();




            Stopwatch watch = new Stopwatch();
            watch.Start();

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

            var event3 = new BaseUploadEvent
            {
                Source = "FILE",
                Type = "TypeB",
                FileName = "TypeB2Filename.jpg",
                FileType = ".jpg",
                FileUrl = "http://typeB.file.url",
                Id = Guid.NewGuid()
            };

            var event4 = new BaseIoTEvent
            {
                Source = "IOT",
                Type = "TypeC",
                Action = "TEMP_UPDATE",
                Value = 92.2.ToString(),
                Id = Guid.NewGuid()
            };

            var event5 = new BaseIoTEvent
            {
                Source = "IOT",
                Type = "TypeC",
                Action = "TEMP_UPDATE",
                Value = 92.2.ToString(),
                Id = Guid.NewGuid()
            };

            var event6 = new BaseIoTEvent
            {
                Source = "IOT",
                Type = "TypeC",
                Action = "TEMP_UPDATE",
                Value = 92.2.ToString(),
                Id = Guid.NewGuid()
            };

            var reportEvent = new ReportEvent
            {
                Source = "REPORT",
                Type = "TypeR",
                FileName = "TypeBFilename.jpg",
                FileType = ".jpg",
                FileUrl = "http://typeA.file.url",
                Id = Guid.NewGuid(),
                Action = "TEMP_UPDATE",
                Value = 92.2.ToString()
            };

            List<BasicEvent> eventList = new List<BasicEvent> { event1, event2, event3, event4, event5, event6, reportEvent };
            eventList.ForEach(eventObj =>
            {
                FactoryCreator.GetPipelineFactory(eventObj).GetPipeline(eventObj).Process(eventObj);
                Console.WriteLine();
            });

            watch.Stop();
            Console.WriteLine($"MS ellapsed: {watch.ElapsedMilliseconds}");
        }
    }
}
