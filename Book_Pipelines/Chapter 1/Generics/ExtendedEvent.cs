using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.Generics
{
    public class ExtendedEvent : BasicEvent
    {
        private string source;

        public ExtendedEvent()
        {
            this.source = "EMAIL";
        }

        public string Source
        {
            get
            {
                return this.source;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (value == "EMAIL" || value == "IOT")
                    {
                        this.source = value;
                    }
                }
            }
        }
    }
}
