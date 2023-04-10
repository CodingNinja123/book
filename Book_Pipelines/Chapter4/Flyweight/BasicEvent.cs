namespace Book_Pipelines.Chapter4.Flyweight
{
    public class BasicEvent: IBasicEvent
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
    }
}
