using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.Generics
{
    public abstract class GenericAbstractBasicPipeline<TEvent> where TEvent: BasicEvent
    {
        public bool IsPostProcessingEnabled { get; set; }
        public bool IsPreProcessingEnabled { get; set; }

        public virtual void Process(TEvent basicEvent)
        {
            try
            {
                WriteLog($"Starting processing of event {basicEvent.Id}");
                if (IsPreProcessingEnabled)
                    PreProcess(basicEvent);

                Validate(basicEvent);
                WriteLog($"Processing {basicEvent.Id}");
                ProcessEvent(basicEvent);

                if (IsPostProcessingEnabled)
                    PostProcess(basicEvent);

                WriteLog($"Processing successfully completed for event {basicEvent.Id}");
            }
            catch (Exception ex)
            {
                WriteLog($"Processing has failed for event: {basicEvent.Id}");
            }
        }
        protected abstract void ProcessEvent(TEvent basicEvent);
        protected virtual void WriteLog(string message)
        {
            Console.WriteLine($"[{DateTime.Now}]: {message}");
        }
        protected virtual void Validate(TEvent basicEvent)
        {
            if (basicEvent == null)
            {
                throw new ArgumentNullException("Event object cannot be null");
            }
            if (string.IsNullOrWhiteSpace(basicEvent.Data))
            {
                throw new ArgumentException($"Event {basicEvent.Id} is invalid");
            }
        }
        protected virtual void PostProcess(TEvent basicEvent)
        {
            throw new NotImplementedException("PostProcessing functionality is not implemented");
        }
        protected virtual void PreProcess(TEvent basicEvent)
        {
            throw new NotImplementedException("PreProcessing functionality is not implemented");
        }
    }
}
