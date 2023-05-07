using Book_Pipelines.Chapter7.Visitor;
using Book_Pipelines.Chapter7.Visitor.Chain;
using Book_Pipelines.Chapter7.Visitor.Exceptions;
using Book_Pipelines.Chapter7.Visitor.Logging;

namespace Book_Pipelines.Chapter7.Chain_Of_Responsibility.Visitor
{
    public class ExceptionHandlingProcessor : Processor
    {
        public string ExceptionHandlingMessage { get { return "some special ExceptionHandlingProccessor message"; } }

        public Logger LogginClient { get; set; }

        public ExceptionHandlingProcessor(Processor nextProcessor, Logger loggingClient) : base(nextProcessor)
        {
            this.RegisterStepExecution += RegisterStepExecutionHandler;
            var tmpProcess = nextProcessor;
            do
            {
                tmpProcess.RegisterStepExecution += RegisterStepExecutionHandler;
                tmpProcess = tmpProcess.NextProcessor;
            } while (tmpProcess.NextProcessor != null);
            
            this.LogginClient = loggingClient;
            this.LogginClient.StartSession(Guid.NewGuid());
        }

        protected void RegisterStepExecutionHandler(IBasicEvent basicEvent, string step)
        {
            var message = $"Executing step: {step} for event: {basicEvent.Id}-{basicEvent.Source}-{basicEvent.Type}";
            LogginClient.Log(message);
        }

        public override void Accept(ProcessorVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }

        public override void Process(IBasicEvent basicEvent)
        {
            try
            {
                RegisterStep(basicEvent, "PROCESSING_STARTED");
                base.Process(basicEvent);
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
                LogginClient.EndSession();
            }
        }
    }
}
