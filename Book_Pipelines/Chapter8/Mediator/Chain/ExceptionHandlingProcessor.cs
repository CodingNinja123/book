using Book_Pipelines.Chapter8.Mediator;
using Book_Pipelines.Chapter8.Mediator.Chain;
using Book_Pipelines.Chapter8.Mediator.Exceptions;
using Book_Pipelines.Chapter8.Mediator.Logging;
using Microsoft.VisualBasic;

namespace Book_Pipelines.Chapter8.Chain_Of_Responsibility.Mediator
{
    public class ExceptionHandlingProcessor : Processor
    {
        private ProcessorVisitor processorVisitorInstance;
        public string ExceptionHandlingMessage { get { return "some special ExceptionHandlingProccessor message"; } }

        public Logger LogginClient { get; set; }
        
        public ExceptionHandlingProcessor(Processor nextProcessor, Logger loggingClient, ProcessorVisitor processorVisitorInstance) : base(nextProcessor)
        {
            this.RegisterStepExecution += RegisterStepExecutionHandler;
            var tmpProcess = nextProcessor;
            do
            {
                tmpProcess.RegisterStepExecution += RegisterStepExecutionHandler;
                tmpProcess = tmpProcess.NextProcessor;
            } while (tmpProcess != null);
            
            this.LogginClient = loggingClient;
            this.processorVisitorInstance = processorVisitorInstance;
        }

        protected void RegisterStepExecutionHandler(IBasicEvent basicEvent, string step)
        {
            var message = $"Executing step: {step} for event: {basicEvent.Id}-{basicEvent.Source}-{basicEvent.Type}";
            LogginClient.Log(message);
        }
        private void LogStatistic()
        {
            string message = string.Join(Environment.NewLine, processorVisitorInstance.Data);
            LogginClient.Log(message);
        }

        public override void Accept(ProcessorVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }

        public override void Process(IBasicEvent basicEvent)
        {
            this.LogginClient.StartSession(Guid.NewGuid());

            try
            {
                RegisterStep(basicEvent, "PROCESSING_STARTED");
                base.Process(basicEvent);
                Accept(processorVisitorInstance);
                LogStatistic();
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
