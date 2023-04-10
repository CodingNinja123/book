namespace Book_Pipelines.Chapter5.Decorator
{
    public class BasicEvent: IBasicEvent
    {
        public BasicEvent()
        {
        }
        public BasicEvent(Guid id, string type, string source)
        {
            this.Id = id;
            this.Type = type;
            this.Source = source;
        }
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
    }
}
