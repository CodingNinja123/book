namespace Book_Pipelines.Chapter_1.Inheritance
{
    public class ExtendedPipeline : BasicPipeline
    {
        public void PreProcessing(ExtendedEvent extendedEvent)
        {
            WriteLog($"Preprocessing event: {extendedEvent.Id} from {extendedEvent.Source}");
        }

        public void PostProcessing(ExtendedEvent extendedEvent)
        {
            WriteLog($"Postprocessing event: {extendedEvent.Id} from {extendedEvent.Source}");
        }
    }

    public class ExtendedEvent : BasicEvent
    {
        public string Source { get; set; }
    }

}
