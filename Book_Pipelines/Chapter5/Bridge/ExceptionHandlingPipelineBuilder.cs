using Book_Pipelines.Chapter5.Bridge.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.Bridge
{
    public class ExceptionHandlingPipelineBuilder<T> where T : ExceptionHandlingPipeline, new()
    {
        private T pipeline = default;
        public ExceptionHandlingPipelineBuilder()
        {
            this.pipeline = new T();
        }

        public ExceptionHandlingPipelineBuilder<T> SetLoggingClient(ILoggingDestination loggingDestination)
        {
            pipeline.LoggingClient = new Logger(loggingDestination);
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
