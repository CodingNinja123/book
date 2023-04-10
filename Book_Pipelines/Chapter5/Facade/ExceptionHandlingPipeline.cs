using Book_Pipelines.Chapter5.Facade.Exceptions;

namespace Book_Pipelines.Chapter5.Facade
{
    public class ExceptionHandlingPipeline : AbstractPipeline
    {
        private AbstractPipeline internalPipeline;

        public ICommunicationClient<string, string> DashboardLoggingClient { get; set; }

        public ExceptionHandlingPipeline()
        {
            this.RegisterStepExecution += RegisterStepExecutionHandler;
        }

        public AbstractPipeline Pipeline 
        {    
            set
            {
                if(this.internalPipeline != null)
                    this.internalPipeline.RegisterStepExecution -= RegisterStepExecutionHandler;

                this.internalPipeline = value;
                this.internalPipeline.RegisterStepExecution += RegisterStepExecutionHandler;
            }
        }

        private void RegisterStepExecutionHandler(IBasicEvent basicEvent, string step)
        {
            var message = $"Executing step: {step} for event: {basicEvent.Id}-{basicEvent.Source}-{basicEvent.Type}";
            DashboardLoggingClient.ExecuteRequest(message);
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
        }
    }
}
