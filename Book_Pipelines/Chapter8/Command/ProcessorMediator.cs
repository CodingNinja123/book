using Book_Pipelines.Chapter8.Chain_Of_Responsibility.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter8.Command
{
    public class ProcessorMediator
    {
        private Dictionary<string, List<Processor>> Processors { get; set; } = new Dictionary<string, List<Processor>>();

        public void ProcessEvent(IBasicEvent basicEvent)
        {
            this.Processors[basicEvent.Type].ForEach(processor => processor.Process(basicEvent));
        }

        public void AddProcessor(string source, Processor processor)
        {
            if (this.Processors.ContainsKey(source))
            {
                this.Processors[source].Add(processor);
            }
            else
            {
                this.Processors.Add(source, new List<Processor> { processor });
            }
        }
    }
}
