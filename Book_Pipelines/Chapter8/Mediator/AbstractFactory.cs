using Book_Pipelines.Chapter8.Chain_Of_Responsibility.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Book_Pipelines.Chapter8.Mediator
{
    public abstract class AbstractFactory<T> where T: IBasicEvent
    {
        public abstract Processor GetPipeline(BasicEvent basicEvent);
    }
}
