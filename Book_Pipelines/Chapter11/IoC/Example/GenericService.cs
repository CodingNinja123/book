using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Example
{
    public class GenericService
    {

        private readonly IGenericDependency _genericDependency;
        public GenericService(IGenericDependency genericDependency)
        {
            _genericDependency = genericDependency;
        }

        public void DoSomeCoolStuff()
        {
            _genericDependency.DoWork();
        }
    }
}
