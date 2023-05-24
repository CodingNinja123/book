using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Iterator
{
    public class FibonacciContainer : ICollectionContainer<int>
    {
        public IIterator<int> GetIterator()
        {
            return new FibonacciIterator(25);
        }
    }

    public class FibonacciIterator : IIterator<int>
    {
        private readonly int length;
        private int counter = 0, a = 0, b = 1, c = 0;


        public FibonacciIterator(int length)
        {
            this.length = length;
        }

        public int GetNext()
        {
            if(!HasMore()) throw new InvalidOperationException();

            int result = 0;
            if (counter > 0)
            {
                c = a + b;
                a = b;
                b = c;
                result = c;
            }
            counter++;
            return result;
        }

        public bool HasMore() => counter < length;
    }
}
