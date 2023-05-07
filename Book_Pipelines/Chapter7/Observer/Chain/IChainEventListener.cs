using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Observer.Chain
{
    public interface IChainEventListener
    {
        void Update(IBasicEvent basicEvennt, string message);
    }
}
