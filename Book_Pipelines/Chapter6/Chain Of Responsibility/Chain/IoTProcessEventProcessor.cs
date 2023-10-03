using Book_Pipelines.Chapter6.ChainOfResponsibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain
{
    public class IoTProcessEventProcessor : Processor
    {
        public ICommunicationClient<IoTData, string> SystemCApiClient { get; set; }

        public IoTProcessEventProcessor(Processor nextProcessor, ICommunicationClient<IoTData, string> client) : base(nextProcessor)
        {
            this.SystemCApiClient = client;
        }

        public override void Process(IBasicEvent request)
        {
            if (this.SystemCApiClient == null)
                return;

            var uploadData = request as IIoTEventData;
            if (uploadData == null)
                throw new ArgumentException(nameof(request));

            this.RegisterStep(request, "EVENT_PROCESSING");
            var data = new IoTData(uploadData.Source, uploadData.Action, uploadData.Value);
            this.SystemCApiClient.ExecuteRequest(data);

            base.Process(request);
        }
    }
}
