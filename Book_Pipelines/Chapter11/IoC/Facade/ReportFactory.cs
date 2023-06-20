using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class ReportFactory : IReportFactory
    {
        private readonly IPipelineDirector director;

        public ReportFactory(IPipelineDirector director)
        {
            this.director = director;
        }
        public AbstractPipeline GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeR" => director.BuildReportPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
