namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class FileUploadFactory: IFileUploadFactory
    {
        private readonly IPipelineDirector director;

        public FileUploadFactory(IPipelineDirector director)
        {
            this.director = director;
        }
        public AbstractPipeline GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeA" => director.BuildTypeAPipeline(),
                "TypeB" => director.BuildTypeBPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
