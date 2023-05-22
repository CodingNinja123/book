using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Command
{
    public interface ICommand
    {
        void Execute();
    }
}
