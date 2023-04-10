using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter3.Prototype
{
    public class IoTPipeline : AbstractPipeline
    {
        private string token;
        public bool ShouldSaveMetadata {get;set;}
        public string ApiUrl { get; set; }
        public string TargetSystemApiUrl { get; set; }

        protected void ProcessEvent(BasicEvent basicEvent)
        {
            var iotEvent = basicEvent as BaseIoTEvent;
            Notify(basicEvent, "Processing event");
            Notify(basicEvent, $"Calling {this.TargetSystemApiUrl} to process a values of event: {iotEvent.Action} {iotEvent.Value}");
        }

        public override void Process(BasicEvent basicEvent)
        {
            RequestToken();
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
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Notify(basicEvent, ex.ToString());
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

        public override IoTPipeline Copy()
        {
            IoTPipeline result = new IoTPipeline();
          
            result.ShouldSaveMetadata = this.ShouldSaveMetadata;
            result.TargetSystemApiUrl = this.TargetSystemApiUrl;
            result.ApiUrl = this.ApiUrl;
            result.token = this.token;
            return result;
        }

        private void RequestToken()
        {
            if (!string.IsNullOrWhiteSpace(token))
                return;

            Thread.Sleep(200);
            this.token = $"Token: {Guid.NewGuid()}";
        }
    }
}
