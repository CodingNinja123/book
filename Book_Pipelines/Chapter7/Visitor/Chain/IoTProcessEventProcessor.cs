using Book_Pipelines.Chapter7.Visitor;
using Book_Pipelines.Chapter7.Visitor.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Chain_Of_Responsibility.Visitor
{
    public class IoTProcessEventProcessor : Processor
    {
        public string IoTProcessEventMessage { get { return "some special IoTProcessEventProccessor message"; } }

        public ICommunicationClient<IoTData, string> SystemCApiClient { get; set; }

        public IoTProcessEventProcessor(Processor nextProcessor, ICommunicationClient<IoTData, string> client) : base(nextProcessor)
        {
            this.SystemCApiClient = client;
        }

        public override void Accept(ProcessorVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }

        public override void Process(IBasicEvent request)
        {
            if (SystemCApiClient == null)
                return;

            var uploadData = request as IIoTEventData;
            if (uploadData == null)
                throw new ArgumentException(nameof(request));

            RegisterStep(request, "EVENT_PROCESSING");
            var data = new IoTData(uploadData.Source, uploadData.Action, uploadData.Value);
            SystemCApiClient.ExecuteRequest(data);

            base.Process(request);
        }
    }
}
