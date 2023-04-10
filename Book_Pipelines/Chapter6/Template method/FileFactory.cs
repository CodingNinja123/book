using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class FileUploadFactory: AbstractFactory<IUploadEventData>
    {
        public override AbstractPipeline<IUploadEventData> GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeA" => PipelineDirector.BuildTypeAPipeline(),
                "TypeB" => PipelineDirector.BuildTypeBPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
