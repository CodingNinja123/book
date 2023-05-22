namespace Book_Pipelines.Chapter8.Command
{
    public static class FactoryProcessor
    {
        static ProcessorMediator mediator = new ProcessorMediator();

        static FactoryProcessor()
        {
            mediator.AddProcessor("TypeA", PipelineDirector.BuildTypeAPipeline());
            mediator.AddProcessor("TypeB", PipelineDirector.BuildTypeBPipeline());
            mediator.AddProcessor("TypeC", PipelineDirector.BuildTypeCPipeline());
            mediator.AddProcessor("TypeR", PipelineDirector.BuildTypeRPipeline(mediator));
        }

        public static void Execute(BasicEvent basicEvent)
        {
            mediator.ProcessEvent(basicEvent);
        }
    }
}
