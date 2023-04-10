namespace Book_Pipelines.Chapter5.Proxy
{
    public class BaseUploadEvent: BasicEvent, IUploadEventData
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
    }

    public class CopyOfBaseUploadEvent : BasicEvent, IUploadEventData
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
    }
}
