namespace Book_Pipelines.Chapter4.Flyweight
{
    public class BaseUploadEvent: BasicEvent, IUploadEventData
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
    }
}
