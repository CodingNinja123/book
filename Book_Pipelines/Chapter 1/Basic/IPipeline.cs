namespace Book_Pipelines.Chapter_1.Basic
{
    public interface IPipeline
    {
        string Type { get; set; }
        void Process(BasicEvent basicEvent);
    }
}
