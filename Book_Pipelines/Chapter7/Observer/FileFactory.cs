
using Book_Pipelines.Chapter7.Chain_Of_Responsibility.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Observer
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
