using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Memento
{
    public class ConcreteMemento : IMemento
    {
        private object state;
        public ConcreteMemento(IWorker worker)
        {
            this.state = worker.GetState();
        }
        public void Restore(IWorker worker)
        {
            worker.SetState(this.state);
        }
    }
}
