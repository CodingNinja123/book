using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.Builder
{
    public class BaseIoTEvent: BasicEvent
    {
        public string Action { get; set; }
        public string Value { get; set; }
    }
}
