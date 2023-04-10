using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter3.Prototype
{
    public interface ICopy<T>
    {
        public T Copy();
    }
}
