using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Memento
{
    public interface IWorker
    {
        public IMemento Save();

        public object GetState();

        void SetState(Object state);
    }
}
