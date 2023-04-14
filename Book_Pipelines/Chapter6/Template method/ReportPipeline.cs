namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public class ReportPipeline : AbstractPipeline<ReportEvent> 
    {
        public AbstractPipeline<IUploadEventData> UploadPipeline { get; set; }
        public AbstractPipeline<IIoTEventData> IoTTPipeline { get; set; }

        public override void Process(ReportEvent basicEvent)
        {
            this.UploadPipeline.Process(basicEvent);
            this.IoTTPipeline.Process(basicEvent);
        }
    }
}
