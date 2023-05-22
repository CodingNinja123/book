namespace Book_Pipelines.Chapter8.Command
{
    public class BaseUploadEvent: BasicEvent, IUploadEventData
    {
        public BaseUploadEvent(): base()
        {
        }
        public BaseUploadEvent(Guid id, string type, string source, string fileName, string fileUrl, string fileType): 
            base(id, type, source)
        {
            this.FileName = fileName;
            this.FileUrl = fileUrl;
            this.FileType = fileType;
        }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
    }
}
