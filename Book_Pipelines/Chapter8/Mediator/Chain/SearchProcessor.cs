using Book_Pipelines.Chapter8.Mediator;
using Book_Pipelines.Chapter8.Mediator.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Chain_Of_Responsibility.Mediator
{
    public class SearchProcessor : Processor
    {
        public ICommunicationClient<string, string> TargetSystemSearchApiClient { get; set; }

        public string SearchMessage { get { return "some special SearchProccessor message"; } }

        public SearchProcessor(Processor nextProcessor, ICommunicationClient<string, string> targetSystemSearchApiClient) : base(nextProcessor)
        {
            TargetSystemSearchApiClient = targetSystemSearchApiClient;
        }

        public override void Accept(ProcessorVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }

        public override void Process(IBasicEvent request)
        {
            var uploadData = request as IUploadEventData;
            if (uploadData == null)
                throw new ArgumentException(nameof(request));
            RegisterStep(request, "EVENT_SEARCH");
            
            TargetSystemSearchApiClient.ExecuteRequest(uploadData.FileName);

            base.Process(request);
        }
    }
}
