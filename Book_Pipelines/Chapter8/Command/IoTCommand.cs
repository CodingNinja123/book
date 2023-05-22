using Book_Pipelines.Chapter8.Chain_Of_Responsibility.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Command
{
    public class IoTCommand : ICommand
    {
        private readonly Processor processor;
        private readonly IIoTEventData eventData;

        public IoTCommand(Processor processor, IIoTEventData eventData)
        {
            this.processor = processor;
            this.eventData = eventData;
        }
        public void Execute()
        {
            this.processor.Process(eventData); 
        }
    }
}
