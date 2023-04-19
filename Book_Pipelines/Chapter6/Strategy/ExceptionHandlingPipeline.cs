using Book_Pipelines.Chapter6.Strategy.Exceptions;
using Book_Pipelines.Chapter6.Strategy.Logging;

namespace Book_Pipelines.Chapter6.Strategy
{
    public class ExceptionHandlingPipeline<T> : AbstractPipeline<T> where T : IBasicEvent
    {
        private AbstractPipeline<T> internalPipeline;
        private Logger loggingClient;
        public IStrategy<T> Strategy { get; set; }
        public Logger LoggingClient
        {
            set
            {
                loggingClient = value;
                loggingClient.StartSession(Guid.NewGuid());
            }
        }
        public ExceptionHandlingPipeline()
        {
            this.RegisterStepExecution += RegisterStepExecutionHandler;
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
            loggingClient.Log(message);
        }
        public override void ProcessEvent(T basicEvent)
        {
            try
            {
                RegisterStep(basicEvent, "PROCESSING_STARTED");
                Strategy.Process(basicEvent);
                RegisterStep(basicEvent, "PROCESSING_FINISHED");
            }
            catch (gRpcCommunicationException)
            {
                RegisterStep(basicEvent, "PROCESSING_FAILED");
                Console.WriteLine("gRpc communication error received");
            }
            catch (HttpCommunicationException)
            {
                RegisterStep(basicEvent, "PROCESSING_FAILED");
                Console.WriteLine("Http communication error received");
            }
            catch (FileCommunicationException)
            {
                RegisterStep(basicEvent, "PROCESSING_FAILED");
                Console.WriteLine("File communication error received");
            }
            catch (PipelineProcessingException)
            {
                RegisterStep(basicEvent, "PROCESSING_FAILED");
                Console.WriteLine("Pipeline business error received");
            }
            finally
            {
                loggingClient.EndSession();
            }
        }
    }
}
