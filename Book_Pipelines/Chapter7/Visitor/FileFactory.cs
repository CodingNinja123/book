
using Book_Pipelines.Chapter7.Chain_Of_Responsibility.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Visitor
{
    public class FileUploadFactory: AbstractFactory<IUploadEventData>
    {
        public override Processor GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeA" => PipelineDirector.BuildTypeAPipeline(),
                "TypeB" => PipelineDirector.BuildTypeBPipeline(),
                "TypeR" => PipelineDirector.BuildTypeBPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
