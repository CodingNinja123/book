﻿namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class BaseUploadEvent: BasicEvent, IUploadEventData
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
    }
}
