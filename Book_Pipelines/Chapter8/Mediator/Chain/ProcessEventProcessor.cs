using Book_Pipelines.Chapter8.Mediator;
using Book_Pipelines.Chapter8.Mediator.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Chain_Of_Responsibility.Mediator
{
    public class ProcessEventProcessor : Processor
    {
        public ICommunicationClient<UploadFileInfo, int> UploadFileClient { get; set; }

        public string ProcessEventMessage { get { return "some special ProcessEventProccessor message"; } }

        public ProcessEventProcessor(Processor nextProcessor, ICommunicationClient<UploadFileInfo, int> client) : base(nextProcessor)
        {
            this.UploadFileClient = client;
        }

        public override void Accept(ProcessorVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }

        public override void Process(IBasicEvent request)
        {
            if (UploadFileClient == null)
                return;

            var uploadData = request as IUploadEventData;
            if (uploadData == null)
                throw new ArgumentException(nameof(request));

            RegisterStep(request, "EVENT_PROCESSING");
            this.UploadFileClient.ExecuteRequest(new UploadFileInfo
            {
                FileName = uploadData.FileName,
                Content = new byte[0]
            });

            base.Process(request);
        }
    }
}
