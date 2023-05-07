using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Observer.Chain
{
    public interface IChainEventPublisher
    {
        void Subscribe (IChainEventListener subscriber);
        void Unsubscribe (IChainEventListener subscriber);
        void Notify(IBasicEvent basicEvent, string message);
    }
}
