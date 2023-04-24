using Book_Pipelines.Chapter6.ChainOfResponsibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain
{
    public class SearchProcessor : Processor
    {
        public ICommunicationClient<string, string> TargetSystemSearchApiClient { get; set; }

        public SearchProcessor(Processor nextProcessor, ICommunicationClient<string, string> targetSystemSearchApiClient) : base(nextProcessor)
        {
            TargetSystemSearchApiClient = targetSystemSearchApiClient;
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
