

using Book_Pipelines.Chapter11.IoC.Example;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class FacadeIoCSection
    {
        private static IServiceProvider Configure()
        {
            var container = new ServiceCollection();
            Configuration config = Configuration.Instance;
            container.AddSingleton<ITokenFactory, TokenFactory>();
            container.AddScoped<ISystemASearchApiClient, SystemASearchApiClient>(x => new SystemASearchApiClient(config.ASystemSearchApi, x.GetService<ITokenFactory>()));
            container.AddScoped<ISystemAStoreApiClient, SystemAStoreApiClient>(x => new SystemAStoreApiClient(config.ASystemStoreApi, x.GetService<ITokenFactory>()));
            container.AddScoped<IFileAUploadClient, FileAUploadClient>(x => new FileAUploadClient(config.ASystemUploadUrl, x.GetService<ITokenFactory>()));
            container.AddScoped<IFileBUploadClient, FileBUploadClient>(x => new FileBUploadClient(config.BSystemUploadUrl, x.GetService<ITokenFactory>()));
            container.AddScoped<IFileDownloadClient, FileDownloadClient>();
            container.AddScoped<ISystemCAPIClient, SystemCApiClient>(x => new SystemCApiClient(config.CSystemApi, x.GetService<ITokenFactory>()));

            container.AddScoped<IDashboardNotificationClient, DashboardNotificationClient>(x => new DashboardNotificationClient(
                config.DashboardLoggingUrl,
                x.GetService<ITokenFactory>()));
            container.AddScoped<IPipelineCreationFacade, PipelineCreationFacade>();
            container.AddScoped<IPipelineDirector, PipelineDirector>();
            container.AddScoped<IIoTFactory, IoTFactory>();
            container.AddScoped<IFileUploadFactory, FileUploadFactory>();
            container.AddScoped<IReportFactory, ReportFactory>();
            container.AddScoped<IFactoryCreator, FactoryCreator>();

            // Build the IoC and get a provider
            var provider = container.BuildServiceProvider();
            return provider;
        }

        public static void Main()
        {
            List<BasicEvent> eventList = GenerateEvents();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            var provider = Configure();
            var factory = provider.GetService<IFactoryCreator>();

            eventList.ForEach(eventObj =>
            {
                factory.GetPipelineFactory(eventObj).GetPipeline(eventObj).Process(eventObj);
                Console.WriteLine();
            });

            watch.Stop();
            Console.WriteLine($"MS ellapsed: {watch.ElapsedMilliseconds}");
        }
        
        private static List<BasicEvent> GenerateEvents()
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
            return eventList;
        }

    }
}
