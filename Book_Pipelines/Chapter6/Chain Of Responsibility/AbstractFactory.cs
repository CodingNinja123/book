using Book_Pipelines.Chapter6.Chain_Of_Responsibility.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.ChainOfResponsibility
{
    public abstract class AbstractFactory<T> where T: IBasicEvent
    {
        public abstract Processor GetPipeline(BasicEvent basicEvent);
    }
}
