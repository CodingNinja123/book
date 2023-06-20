using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public interface IAbstractFactory
    {
        AbstractPipeline GetPipeline(BasicEvent basicEvent);
    }
    public interface IFileUploadFactory : IAbstractFactory
    {
    }
    public interface IIoTFactory : IAbstractFactory
    {
    }
    public interface IReportFactory : IAbstractFactory
    {
    }
    public interface ICommunicationClient<TRequest, TResponse>
    {
        TResponse ExecuteRequest(TRequest request);
    }
    public interface IDashboardNotificationClient : ICommunicationClient<string, string>
    {
    }
    public interface IFileDownloadClient : ICommunicationClient<string, byte[]>
    {
    }
    public interface IFileUploadClient : ICommunicationClient<UploadFileInfo, int>
    {
    }
    public interface IFileAUploadClient : IFileUploadClient
    {
    }
    public interface IFileBUploadClient : IFileUploadClient
    {
    }
    public interface ISystemASearchApiClient : ISystemAApiClient
    {
    }
    public interface ISystemAApiClient : ICommunicationClient<string, string>
    {
    }
    public interface ISystemAStoreApiClient : ISystemAApiClient
    {
    }

    public interface ISystemCAPIClient : ICommunicationClient<IoTData, string>
    {
    }
    public interface ITokenFactory
    {
        Token GetToken(SystemType system);
    }
    public interface IBasicEvent
    {
        Guid Id { get; set; }
        string Type { get; set; }
        string Source { get; set; }
    }

    public interface IIoTEventData : IBasicEvent
    {
        string Action { get; set; }
        string Value { get; set; }
    }
    public interface IFactoryCreator
    {
        IAbstractFactory GetPipelineFactory(BasicEvent basicEvent);
    }
    public interface IPipelineCreationFacade
    {
        AbstractPipeline BuildFileUploadPipeline(bool shouldBeFileProcessed, bool shouldEventBeStored, bool shouldSaveMetadata,
            IFileUploadClient fileUploadClient, IFileDownloadClient fileDownloadClient,
            ISystemASearchApiClient searchApiClient, ISystemAStoreApiClient storeApiClient);
        AbstractPipeline BuildIoTPipeline(bool shouldSaveMetadata, ISystemCAPIClient apiClient);

        AbstractPipeline BuildReportPipeline(bool shouldBeFileProcessed, bool shouldEventBeStored, bool shouldSaveMetadata,
            IFileUploadClient fileUploadClient, IFileDownloadClient fileDownloadClient,
            ISystemASearchApiClient searchApiClient, ISystemAStoreApiClient storeApiClient,
            bool shouldSaveIoTMetadata, ISystemCAPIClient apiClient);
    }

    public interface IPipelineDirector
    {
        AbstractPipeline BuildTypeAPipeline();
        AbstractPipeline BuildTypeBPipeline();
        AbstractPipeline BuildTypeCPipeline();
        AbstractPipeline BuildReportPipeline();
    }
}
