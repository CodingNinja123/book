using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.AbstractFactoryNM
{
    public class FileUploadFactory: AbstractFactory
    {
        public override AbstractPipeline GetPipeline(BasicEvent basicEvent)
        {
            return basicEvent.Type switch
            {
                "TypeA" => new TypeAPipeline(),
                "TypeB" => new TypeBPipeline(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
