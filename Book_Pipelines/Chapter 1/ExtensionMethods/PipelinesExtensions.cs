using Book_Pipelines.Chapter_1.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.ExtensionMethods
{
    public static class PipelinesExtensions
    {
        public static void BatchProcessing<T>(this GenericAbstractBasicPipeline<T> pipeline, List<T> events) where T: BasicEvent
        {
            events.ForEach(eventObj =>
            {
                pipeline.Process(eventObj);
            });
        }
    }
}
