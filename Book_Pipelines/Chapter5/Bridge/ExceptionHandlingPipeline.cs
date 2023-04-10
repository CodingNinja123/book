using Book_Pipelines.Chapter5.Bridge.Exceptions;
using Book_Pipelines.Chapter5.Bridge.Logging;

namespace Book_Pipelines.Chapter5.Bridge
{
    public class ExceptionHandlingPipeline : AbstractPipeline
    {
        private AbstractPipeline internalPipeline;
        private Logger loggingClient;

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

        public AbstractPipeline Pipeline
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

        public override void Process(IBasicEvent basicEvent)
        {
            try
            {
                RegisterStep(basicEvent, "PROCESSING_STARTED");
                internalPipeline.Process(basicEvent);
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
