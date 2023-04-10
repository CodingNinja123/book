namespace Book_Pipelines.Chapter5.Facade
{
    public class BasicEvent: IBasicEvent
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
    }
}
