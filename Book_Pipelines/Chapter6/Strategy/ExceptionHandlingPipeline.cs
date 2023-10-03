using Book_Pipelines.Chapter6.Strategy.Exceptions;
using Book_Pipelines.Chapter6.Strategy.Logging;

namespace Book_Pipelines.Chapter6.Strategy
{
    public class ExceptionHandlingPipeline<T> : AbstractPipeline<T> where T : IBasicEvent
    {
        private AbstractPipeline<T> internalPipeline;
        private Logger loggingClient;
        public IStrategy<T> Strategy { get; set; }
        public ExceptionHandlingPipeline()
        {
            this.RegisterStepExecution += RegisterStepExecutionHandler;
        }
        public Logger LoggingClient
        {
            set
            {
                this.loggingClient = value;
                this.loggingClient.StartSession(Guid.NewGuid());
            }
        }
        public AbstractPipeline<T> Pipeline
        {
            set
            {
                if (this.internalPipeline != null)
                    this.internalPipeline.RegisterStepExecution -= RegisterStepExecutionHandler;

                this.internalPipeline = value;
                this.internalPipeline.RegisterStepExecution += RegisterStepExecutionHandler;
            }
        }
        private void RegisterStepExecutionHandler(IBasicEvent basicEvent, string step)
        {
            var message = $"Executing step: {step} for event: {basicEvent.Id}-{basicEvent.Source}-{basicEvent.Type}";
            this.loggingClient.Log(message);
        }
        public override void ProcessEvent(T basicEvent)
        {
            try
            {
                this.RegisterStep(basicEvent, "PROCESSING_STARTED");
                this.Strategy.Process(basicEvent);
                this.RegisterStep(basicEvent, "PROCESSING_FINISHED");
            }
            catch (gRpcCommunicationException)
            {
                this.RegisterStep(basicEvent, "PROCESSING_FAILED");
                Console.WriteLine("gRpc communication error received");
            }
            catch (HttpCommunicationException)
            {
                this.RegisterStep(basicEvent, "PROCESSING_FAILED");
                Console.WriteLine("Http communication error received");
            }
            catch (FileCommunicationException)
            {
                this.RegisterStep(basicEvent, "PROCESSING_FAILED");
                Console.WriteLine("File communication error received");
            }
            catch (PipelineProcessingException)
            {
                this.RegisterStep(basicEvent, "PROCESSING_FAILED");
                Console.WriteLine("Pipeline business error received");
            }
            finally
            {
                this.loggingClient.EndSession();
            }
        }
    }
}
