using Book_Pipelines.Chapter5.TemplateMethod.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class ExceptionHandlingPipelineBuilder<T,T1> where T : ExceptionHandlingPipeline<T1>, new()
        where T1 : IBasicEvent
    {
        private T pipeline = default;
        public ExceptionHandlingPipelineBuilder()
        {
            this.pipeline = new T();
        }

        public ExceptionHandlingPipelineBuilder<T, T1> SetLoggingClient(ILoggingDestination loggingDestination)
        {
            pipeline.LoggingClient = new Logger(loggingDestination);
            return this;
        }

        public ExceptionHandlingPipelineBuilder<T, T1> SetInternalPipeline(AbstractPipeline<T1> internalPipeline)
        {
            pipeline.Pipeline = internalPipeline;
            return this;
        }

        public ExceptionHandlingPipeline<T1> Build()
        {
            return pipeline;
        }
    }
}
