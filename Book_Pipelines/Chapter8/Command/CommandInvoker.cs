using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Command
{
    public class CommandInvoker
    {
        private ICommand _command;

        public CommandInvoker(ICommand command)
        {
            _command = command; 
        }

        public void ExecuteCommand()
        {
            _command.Execute();
        }
    }
}
