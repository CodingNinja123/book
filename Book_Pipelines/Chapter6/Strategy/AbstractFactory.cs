using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Strategy
{
    public abstract class AbstractFactory<T> where T: IBasicEvent
    {
        public abstract AbstractPipeline<T> GetPipeline(BasicEvent basicEvent);
    }
}
