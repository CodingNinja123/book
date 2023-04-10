using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.Decorator
{
    public class ReportFactory : AbstractFactory
    {
        public override AbstractPipeline GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeR" => PipelineDirector.BuildReportPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
