using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class ReportFactory : AbstractFactory<IBasicEvent>
    {
        public override AbstractPipeline<IBasicEvent> GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeR" => PipelineDirector.BuildReportPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
