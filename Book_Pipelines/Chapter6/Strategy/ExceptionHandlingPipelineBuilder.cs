using Book_Pipelines.Chapter6.Strategy.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Strategy
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
            this.pipeline.LoggingClient = new Logger(loggingDestination);
            return this;
        }
        public ExceptionHandlingPipelineBuilder<T, T1> SetProcessingStrategy(IStrategy<T1> strategy)
        {
            this.pipeline.Strategy = strategy;
            return this;
        }
        public ExceptionHandlingPipelineBuilder<T, T1> SetInternalPipeline(AbstractPipeline<T1> internalPipeline)
        {
            this.pipeline.Pipeline = internalPipeline;
            return this;
        }
        public ExceptionHandlingPipeline<T1> Build()
        {
            return this.pipeline;
        }
    }
}
