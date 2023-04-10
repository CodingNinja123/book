

using System.Diagnostics;

namespace Book_Pipelines.Chapter4.Adapter
{
    public class PrototypeSection
    {
        public static void Main()
        {
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

            List<BasicEvent> eventList = new List<BasicEvent> { event1, event2, event3, event4, event5, event6 };
            eventList.ForEach(eventObj => FactoryCreator.GetPipelineFactory(eventObj).GetPipeline(eventObj).Process(eventObj));

            watch.Stop();
            Console.WriteLine($"MS ellapsed: {watch.ElapsedMilliseconds}");
        }
    }
}
