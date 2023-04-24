using Book_Pipelines.Chapter6.ChainOfResponsibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain
{
    public class PreProcessProcessor : Processor
    {
        public ICommunicationClient<string, byte[]> DownloadFileClient { get; set; }

        public PreProcessProcessor(Processor nextProcessor, ICommunicationClient<string, byte[]> client) : base(nextProcessor)
        {
            this.DownloadFileClient = client;
        }

        public override void Process(IBasicEvent request)
        {
            RegisterStep(request, "EVENT_PREPROCESSING");
            var uploadData = request as IUploadEventData;
            if (uploadData == null)
                throw new ArgumentException(nameof(request));

            if (this.DownloadFileClient != null)
                this.DownloadFileClient.ExecuteRequest(uploadData.FileUrl);
            base.Process(request);
        }
    }
}
