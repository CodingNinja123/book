namespace Book_Pipelines.Chapter3.Prototype
{
    public class BaseUploadEvent: BasicEvent
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
    }
}
