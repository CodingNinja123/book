using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Memento
{
    public class MementSection
    {
        public static void Main()
        {
            ConcreteWorker worker = new ConcreteWorker(String.Empty);
            Manager manager = new Manager(worker);
            manager.SetMessage("Hello!");
            manager.DoWork();
            manager.SetMessage("How are you?");
            manager.DoWork();
            manager.SetMessage("Good bye!");
            manager.DoWork();
            manager.Restore();
            manager.DoWork();
            manager.Restore();
            manager.DoWork();
        }
    }
}
