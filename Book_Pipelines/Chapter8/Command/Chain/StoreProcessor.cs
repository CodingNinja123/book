using Book_Pipelines.Chapter8.Command;
using Book_Pipelines.Chapter8.Command.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Chain_Of_Responsibility.Command
{
    public class StoreProcessor : Processor
    {
        public ICommunicationClient<string, string> TargetSystemStoreApiClient { get; set; }

        public string StoreMessage { get { return "some special StoreProccessor message"; } }

        public StoreProcessor(Processor nextProcessor, ICommunicationClient<string, string> targetSystemStoreApiClient) : base(nextProcessor)
        {
            TargetSystemStoreApiClient = targetSystemStoreApiClient;
        }

        public override void Accept(ProcessorVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }
        public override void Process(IBasicEvent request)
        {
            var uploadRequest = request as IUploadEventData;
            if (uploadRequest == null)
                throw new ArgumentException(nameof(request));
            RegisterStep(request, "EVENT_STORE");
            
            TargetSystemStoreApiClient.ExecuteRequest(uploadRequest.FileName);
            base.Process(request);
        }
    }
}
