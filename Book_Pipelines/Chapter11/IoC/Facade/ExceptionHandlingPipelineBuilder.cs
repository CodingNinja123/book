using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class ExceptionHandlingPipelineBuilder<T> where T : ExceptionHandlingPipeline, new()
    {
        private T pipeline = default;
        public ExceptionHandlingPipelineBuilder()
        {
            this.pipeline = new T();
        }

        public ExceptionHandlingPipelineBuilder<T> SetLoggingClient(ICommunicationClient<string, string> dashboardLoggingClient)
        {
            pipeline.DashboardLoggingClient = dashboardLoggingClient;
            return this;
        }

        public ExceptionHandlingPipelineBuilder<T> SetInternalPipeline(AbstractPipeline internalPipeline)
        {
            pipeline.Pipeline = internalPipeline;
            return this;
        }

        public ExceptionHandlingPipeline Build()
        {
            return pipeline;
        }
    }
}
