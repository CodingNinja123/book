namespace Book_Pipelines.Chapter8.Command
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
