

using System.Diagnostics;

namespace Book_Pipelines.Chapter6.ChainOfResponsibility
{
    public class CoRSection
    {
        public static void Main()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var event1 = new BaseUploadEvent(Guid.NewGuid(), "TypeA", "FILE", 
                "TypeAFilename.jpg", "http://typeA.file.url", ".jpg");

            var event2 = new BaseUploadEvent(Guid.NewGuid(), "TypeB", "FILE",
                "TypeBFilename.jpg", "http://typeB.file.url", ".jpg");

            var event3 = new BaseUploadEvent(Guid.NewGuid(), "TypeB", "FILE",
                "TypeB2Filename.jpg", "http://typeB.file.url", ".jpg");

            var event4 = new BaseIoTEvent(Guid.NewGuid(), "TypeC", "IOT", "TEMP_UPDATE", "92.2");
            var event5 = new BaseIoTEvent(Guid.NewGuid(), "TypeC", "IOT", "TEMP_UPDATE", "92.2");
            var event6 = new BaseIoTEvent(Guid.NewGuid(), "TypeC", "IOT", "TEMP_UPDATE", "92.2");
            var reportEvent = new ReportEvent(Guid.NewGuid(), "TypeR", "REPORT", "TypeRFilename.jpg", "http://typeR.file.url",
                ".jpg", "TEMP_UPDATE", "93.2");

            List<BasicEvent> eventList = new List<BasicEvent> { event1, event2, event3, event4, event5, event6, reportEvent };
            eventList.ForEach(eventObj =>
            {
                FactoryCreator.Execute(eventObj);
                Console.WriteLine();
            });

            watch.Stop();
            Console.WriteLine($"MS ellapsed: {watch.ElapsedMilliseconds}");
        }
    }
}
