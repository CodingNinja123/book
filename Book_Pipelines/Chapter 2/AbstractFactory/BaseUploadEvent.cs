namespace Book_Pipelines.Chapter_2.AbstractFactoryNM
{
    public class BaseUploadEvent: BasicEvent
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
    }
}
