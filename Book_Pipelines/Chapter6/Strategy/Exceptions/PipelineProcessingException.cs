using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter6.Strategy.Exceptions
{
    public class PipelineProcessingException: Exception
    {
        public PipelineProcessingException(string message): base(message)
        {
        }
    }
}
