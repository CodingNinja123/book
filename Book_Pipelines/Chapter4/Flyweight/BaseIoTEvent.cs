using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter4.Flyweight
{
    public class BaseIoTEvent: BasicEvent, IIoTEventData
    {
        public string Action { get; set; }
        public string Value { get; set; }
    }
}
