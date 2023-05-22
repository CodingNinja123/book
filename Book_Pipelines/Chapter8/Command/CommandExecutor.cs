using Book_Pipelines.Chapter5.TemplateMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Command
{
    public class CommandExecutor
    {
        public void Execute(IBasicEvent basicEvent)
        {
            ICommand command = basicEvent.Type switch
            {
                "TypeA" => new FileUploadCommand(PipelineDirector.BuildTypeAPipeline(), basicEvent as IUploadEventData),
                "TypeB" => new FileUploadCommand(PipelineDirector.BuildTypeBPipeline(), basicEvent as IUploadEventData),
                "TypeC" => new IoTCommand(PipelineDirector.BuildTypeCPipeline(), basicEvent as IIoTEventData),
                "TypeR" => new ReportCommand(PipelineDirector.BuildTypeBPipeline(), PipelineDirector.BuildTypeCPipeline(),  basicEvent as ReportEvent),
                _ => throw new InvalidOperationException("Not supported type")
            };
            CommandInvoker commandInvoker = new CommandInvoker(command);
            commandInvoker.ExecuteCommand();
        }
    }
}
