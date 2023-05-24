using Book_Pipelines.Chapter_1.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Iterator
{
    public class IteratorSection
    {
        public static void Main()
        {
            FibonacciContainer fibonacy = new FibonacciContainer();

            for (var iter = fibonacy.GetIterator(); iter.HasMore();)
            {
                var number = iter.GetNext();
                Console.WriteLine(number);
            }
        }
    }
}
