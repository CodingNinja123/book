﻿namespace Book_Pipelines.Chapter8.Mediator
{
    public static class FactoryCreator
    {
        static ProcessorMediator mediator = new ProcessorMediator();

        static FactoryCreator()
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
