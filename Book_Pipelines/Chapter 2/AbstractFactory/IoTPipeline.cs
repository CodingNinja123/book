using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.AbstractFactoryNM
{
    public abstract class IoTPipeline: AbstractPipeline
    {
        protected abstract void ProcessEvent(BasicEvent basicEvent);

        public override void Process(BasicEvent basicEvent)
        {
            try
            {
                var metadataObjectGuid = SaveMetadata(basicEvent);
                Notify(basicEvent, "PROCESSING_STARTED");
                Validate(basicEvent);
                ProcessEvent(basicEvent);
                UpdateMetadata(basicEvent, metadataObjectGuid);
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
        protected virtual void UpdateMetadata(BasicEvent basicEvent, Guid metadataGuid)
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
