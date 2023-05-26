using Book_Pipelines.Chapter9.Iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Memento
{
    public class ConcreteWorker : IWorker
    {
        private string _state;

        public ConcreteWorker(string state)
        {
            this._state = state;
        }

        public void DisplayState()
        {
            Console.WriteLine(this._state);
        }

        public IMemento Save()
        {
            return new ConcreteMemento(this);
        }

        public object GetState()
        {
            return _state;
        }

        public void SetState(Object state)
        {
            this._state = state.ToString();
        }
    }
}
