namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public static class FactoryCreator
    {
        private static AbstractFactory iotFactory;
        private static AbstractFactory fileUploadFactory;
        private static AbstractFactory reportFactory;

        static FactoryCreator()
        {
            iotFactory = new IoTFactory();
            fileUploadFactory = new FileUploadFactory();    
            reportFactory = new ReportFactory();    
        }
        public static AbstractFactory GetPipelineFactory(BasicEvent basicEvent)
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
