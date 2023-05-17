using Book_Pipelines.Chapter8.Chain_Of_Responsibility.Mediator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Mediator.Chain
{
    public class EmitterProcessor : Processor
    {
        private ProcessorMediator processorMediator;
        public EmitterProcessor(Processor nextProcessor, ProcessorMediator mediator) : base(nextProcessor)
        {
            this.processorMediator = mediator;
        }
        public override void Process(IBasicEvent request)
        {
            RegisterStep(request, "EMITTER_EMIT_EVENT");
            var reportEvent = request as ReportEvent;
            var iotEvent = new ReportEvent(reportEvent);
            iotEvent.Type = "TypeC";
            var fileEvent = new ReportEvent(reportEvent);
            fileEvent.Type = "TypeB";
            
            processorMediator.ProcessEvent(fileEvent);
            processorMediator.ProcessEvent(iotEvent);
        }
    }
}
