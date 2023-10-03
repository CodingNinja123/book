using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Strategy
{
    public class FileUploadFactory: AbstractFactory<IUploadEventData>
    {
        public override AbstractPipeline<IUploadEventData> GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                Constants.A_EVENT_TYPE => PipelineDirector.BuildTypeAPipeline(),
                Constants.B_EVENT_TYPE => PipelineDirector.BuildTypeBPipeline(),
                Constants.R_EVENT_TYPE => PipelineDirector.BuildTypeBPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
