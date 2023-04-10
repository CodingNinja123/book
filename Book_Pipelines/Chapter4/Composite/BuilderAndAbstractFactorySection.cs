

using System.Diagnostics;

namespace Book_Pipelines.Chapter4.Composite
{
    public class CompositeSection
    {
        public static void Main()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var event1 = new ReportEvent 
            { 
                Source = "REPORT", 
                Type = "TypeR", 
                FileName = "TypeBFilename.jpg", 
                FileType = ".jpg", 
                FileUrl = "http://typeA.file.url", 
                Id = Guid.NewGuid() ,
                Action = "TEMP_UPDATE",
                Value = 92.2.ToString()
            };

            FactoryCreator.GetPipelineFactory(event1).GetPipeline(event1).Process(event1);

            watch.Stop();
            Console.WriteLine($"MS ellapsed: {watch.ElapsedMilliseconds}");
        }
    }
}
