namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class FactoryCreator: IFactoryCreator
    {
        private IIoTFactory iotFactory;
        private IFileUploadFactory fileUploadFactory;
        private IReportFactory reportFactory;

        public FactoryCreator(IIoTFactory iotFactory, IFileUploadFactory fileUploadFactory, IReportFactory reportFactory)
        {
            this.iotFactory = iotFactory;
            this.fileUploadFactory = fileUploadFactory;
            this.reportFactory = reportFactory;  
        }
        public IAbstractFactory GetPipelineFactory(BasicEvent basicEvent)
        {
            return basicEvent.Source switch
            {
                "IOT" => iotFactory,
                "FILE" => fileUploadFactory,
                "REPORT" => reportFactory,
                _ => throw new NotImplementedException("Cannot create a factory for non known source"),
            };
        }
    }
}
