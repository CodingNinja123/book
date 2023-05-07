using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book_Pipelines.Chapter7.Chain_Of_Responsibility.Observer;

namespace Book_Pipelines.Chapter7.Observer
{
    public abstract class AbstractFactory<T> where T: IBasicEvent
    {
        public abstract Processor GetPipeline(BasicEvent basicEvent);
    }
}
