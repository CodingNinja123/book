using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.Basic
{
    public class ExtendedPipeline: BasicPipeline, IPipeline, IPostProcessing
    {
        #region Pre/Post processing
        public void PreProcessing(ExtendedEvent extendedEvent)
        {
            WriteLog($"Preprocessing event: {extendedEvent.Id} from {extendedEvent.Source}");
        }

        public void PostProcessing(ExtendedEvent extendedEvent)
        {
            WriteLog($"Postprocessing event: {extendedEvent.Id} from {extendedEvent.Source}");
        }

        #endregion
        protected sealed override void Validate(BasicEvent basicEvent)
        {
            var extendedEvent = basicEvent as ExtendedEvent;
            if (extendedEvent == null)
                throw new ArgumentException("This pipeline can process only extended events");

            if (string.IsNullOrWhiteSpace(extendedEvent.Source))
            {
                throw new ArgumentException($"Event {basicEvent.Id} is invalid. Source cannot be null.");
            }

            base.Validate(basicEvent);
        }

        public void PostProcessing(BasicEvent basicEvent)
        {
            Console.WriteLine($"Postprocessing of event: {basicEvent.Id}");
        }
    }
}
