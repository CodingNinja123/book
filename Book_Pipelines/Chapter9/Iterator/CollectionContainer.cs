using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter9.Iterator
{
    public interface ICollectionContainer<T>
    {
        public IIterator<T> GetIterator();
    }
}
