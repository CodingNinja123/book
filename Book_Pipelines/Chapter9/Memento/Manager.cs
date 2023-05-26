using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Memento
{
    public class Manager
    {
        public List<IMemento> History { get; set; } = new List<IMemento>();
        public ConcreteWorker Worker { get; set; }
        public Manager(ConcreteWorker worker)
        {
            Worker = worker;
        }
        public void SetMessage(string message)
        {
            History.Add(Worker.Save());
            Worker.SetState(message);
        }
        public void DoWork()
        {
            Worker.DisplayState();
        }
        public void Restore()
        {
            var lastMemento = History.Last();
            History.Remove(lastMemento);
            lastMemento.Restore(Worker);
        }
    }
}
