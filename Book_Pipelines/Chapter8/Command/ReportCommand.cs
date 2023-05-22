using Book_Pipelines.Chapter8.Chain_Of_Responsibility.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Command
{
    public class ReportCommand : ICommand
    {
        private readonly Processor fileUploadProcessor;
        private readonly Processor iotProcessor;
        private readonly ReportEvent reportEvent;

        public ReportCommand(Processor fileUploadProcessor, Processor iotProcessor, ReportEvent reportEvent)
        {
            this.fileUploadProcessor = fileUploadProcessor;
            this.iotProcessor = iotProcessor;
            this.reportEvent = reportEvent;
        }
        public void Execute()
        {
            this.fileUploadProcessor.Process(reportEvent);
            this.iotProcessor.Process(reportEvent);
        }
    }
}
