using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Example
{
    public class GenericDependency: IGenericDependency
    {
        public void DoWork()
        {
            Console.WriteLine("Doing some work...");
        }
    }
}
