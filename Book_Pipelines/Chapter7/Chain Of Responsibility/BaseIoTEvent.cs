using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Observer
{
    public class BaseIoTEvent: BasicEvent, IIoTEventData
    {
        public BaseIoTEvent(): base()
        {
        }
        public BaseIoTEvent(Guid id, string type, string source, string action, string value): 
            base(id, type, source)
        {
            this.Action = action;
            this.Value = value; 
        }
        public string Action { get; set; }
        public string Value { get; set; }
    }
}
