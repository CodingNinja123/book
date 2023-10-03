using Book_Pipelines.Chapter6.ChainOfResponsibility;
using Book_Pipelines.Chapter6.ChainOfResponsibility.Exceptions;
using Book_Pipelines.Chapter6.ChainOfResponsibility.Logging;

namespace Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain
{
    public class ExceptionHandlingProcessor : Processor
    {
        public Logger LogginClient { get; set; }

        public ExceptionHandlingProcessor(Processor nextProcessor, Logger loggingClient) : base(nextProcessor)
        {
            this.RegisterStepExecution += RegisterStepExecutionHandler;
            nextProcessor.RegisterStepExecution += RegisterStepExecutionHandler;
            this.LogginClient = loggingClient;
            this.LogginClient.StartSession(Guid.NewGuid());
        }

        public override void Process(IBasicEvent basicEvent)
        {
            try
            {
                this.RegisterStep(basicEvent, "PROCESSING_STARTED");
                base.Process(basicEvent);
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
                this.LogginClient.EndSession();
            }
        }
        private void RegisterStepExecutionHandler(IBasicEvent basicEvent, string step)
        {
            var message = $"Executing step: {step} for event: {basicEvent.Id}-{basicEvent.Source}-{basicEvent.Type}";
            this.LogginClient.Log(message);
        }
    }
}
