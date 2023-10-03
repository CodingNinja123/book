using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.Basic
{
    public interface IPostProcessingPipeline: IPipeline, IPostProcessing
    {
    }
}
