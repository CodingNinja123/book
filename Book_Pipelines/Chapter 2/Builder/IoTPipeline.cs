using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.Builder
{
    public class IoTPipeline : AbstractPipeline
    {
        public bool ShouldSaveMetadata {get;set;}
        public string ApiUrl { get; set; }
        public string TargetSystemApiUrl { get; set; }
        private Guid metadataGuid = Guid.Empty;
        protected void ProcessEvent(BasicEvent basicEvent)
        {
            var iotEvent = basicEvent as BaseIoTEvent;
            Notify(basicEvent, "Processing event");
            Notify(basicEvent, $"Calling {this.TargetSystemApiUrl} to process a values of event: {iotEvent.Action} {iotEvent.Value}");
        }

        public override void Process(BasicEvent basicEvent)
        {
            try
            {
                if(ShouldSaveMetadata)
                    SaveMetadata(basicEvent);
               
                Notify(basicEvent, "PROCESSING_STARTED");
                Validate(basicEvent);
                ProcessEvent(basicEvent);
                
                if (ShouldSaveMetadata)
                    UpdateMetadata(basicEvent);
                
                Notify(basicEvent, "PROCESSING_FINISHED");
            }
            catch (Exception ex)
            {
                Notify(basicEvent, "PROCESSING_FAILED");
            }
        }

        protected virtual Guid SaveMetadata(BasicEvent basicEvent)
        {
            Notify(basicEvent, "Saving metadata");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(BasicEvent basicEvent)
        {
            Notify(basicEvent, "Updating metdata");
        }
        protected virtual void Notify(BasicEvent badicEvent, string message)
        {
            Console.WriteLine($"Processing pipeline: {message}: {badicEvent.Id}");
        }
        protected virtual void Validate(BasicEvent basicEvent)
        {
            if (basicEvent == null)
                throw new ArgumentNullException("Event cannot be null");
            
            var iotEvent = basicEvent as BaseIoTEvent;
            
            if (iotEvent.Action == null)
                throw new ArgumentException("Filename of the event cannot be null");
            if (iotEvent.Value == null)
                throw new ArgumentException("Filename of the event cannot be null");
        }
    }
}
