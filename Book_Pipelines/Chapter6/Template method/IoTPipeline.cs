﻿using Book_Pipelines.Chapter5.TemplateMethod.Exceptions;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class IoTPipeline<T> : AbstractPipeline<T> where T: IIoTEventData
    {
        public ICommunicationClient<IoTData, string> SystemCApiClient { get; set; }

        public IoTPipeline()
        {
            ShouldBeEventStored = false;
            ShouldBePreprocessed = false;
        }

        protected override void ProcessEvent(T basicEvent)
        {
            RegisterStep(basicEvent, "EVENT_PROCESSING");
            var data = new IoTData(basicEvent.Source, basicEvent.Action, basicEvent.Value);
            SystemCApiClient.ExecuteRequest(data);
        }

        protected virtual Guid SaveMetadata(T basicEvent)
        {
            if (!ShouldSaveMetadata)
                return Guid.Empty;
            RegisterStep(basicEvent, "SAVE_METADATA");
            return Guid.NewGuid();
        }
        protected virtual void UpdateMetadata(T basicEvent)
        {
            if (!ShouldSaveMetadata)
                return;
            RegisterStep(basicEvent, "UPDATE_METADATA");
        }
        protected virtual void Validate(T basicEvent)
        {   
            if (basicEvent.Action == null)
                throw new PipelineProcessingException("Action of the event cannot be null");
            if (basicEvent.Value == null)
                throw new PipelineProcessingException("Value of the event cannot be null");
        }
    }
}
