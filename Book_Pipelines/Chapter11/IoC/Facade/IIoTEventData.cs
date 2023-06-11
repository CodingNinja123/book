using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public interface IIoTEventData: IBasicEvent
    {
        string Action { get; set; }
        string Value { get; set; }
    }
}
