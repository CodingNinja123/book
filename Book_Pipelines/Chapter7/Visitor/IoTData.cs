using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Visitor
{
    public class IoTData
    {
        public IoTData(string source, string action, string value)
        {
            Source = source;
            Action = action;
            Value = value;
        }

        public string Source { get; set; }
        public string Action { get; set; }  
        public string Value { get; set; }
    }
}
