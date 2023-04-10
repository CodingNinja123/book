using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.Decorator.Exceptions
{
    public class PipelineProcessingException: Exception
    {
        public PipelineProcessingException(string message): base(message)
        {
        }
    }
}
