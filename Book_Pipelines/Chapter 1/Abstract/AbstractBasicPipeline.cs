using Book_Pipelines.Chapter_1.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_1.Abstract
{
    public abstract class AbstractBasicPipeline
    {
        public bool IsPostProcessingEnabled { get; set; }
        public bool IsPreProcessingEnabled { get; set; }

        public virtual void Process(BasicEvent basicEvent)
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
            catch(Exception ex)
            {
                WriteLog($"Processing has failed for event: {basicEvent.Id}");
            }
        }
        protected abstract void ProcessEvent(BasicEvent basicEvent);
        protected virtual void WriteLog(string message)
        {
            Console.WriteLine($"[{DateTime.Now}]: {message}");
        }
        protected virtual void Validate(BasicEvent basicEvent)
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
        protected virtual void PostProcess(BasicEvent basicEvent)
        {
            throw new NotImplementedException("PostProcessing functionality is not implemented");
        }
        protected virtual void PreProcess(BasicEvent basicEvent)
        {
            throw new NotImplementedException("PreProcessing functionality is not implemented");
        }
    }
}
