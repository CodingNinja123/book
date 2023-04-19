using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Strategy
{
    public interface IStrategy<T>
    {
        public void Process(T basicEvent);
    }
}
