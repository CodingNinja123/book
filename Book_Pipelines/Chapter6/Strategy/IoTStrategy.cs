using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Strategy
{
    public class IoTStrategy: IStrategy<IIoTEventData>
    {
        public AbstractPipeline<IIoTEventData> Pipeline { get; set; }
        public IoTStrategy(AbstractPipeline<IIoTEventData> pipeline)
        {
            this.Pipeline = pipeline;
        }
        public void Process(IIoTEventData basicEvent)
        {
            this.Pipeline.SaveMetadata(basicEvent);
            this.Pipeline.Validate(basicEvent);
            this.Pipeline.Search(basicEvent);
            this.Pipeline.ProcessEvent(basicEvent);
            this.Pipeline.UpdateMetadata(basicEvent);
        }
    }
}
