using Book_Pipelines.Chapter_1.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.Abstract
{
    public class BasicPipeline : AbstractBasicPipeline
    {
        public BasicPipeline()
        {
            IsPostProcessingEnabled = false;
            IsPreProcessingEnabled = false;
        }
        protected override void ProcessEvent(BasicEvent basicEvent)
        {
            WriteLog($"Processing basic event: {basicEvent.Id}");
        }
    }
}
