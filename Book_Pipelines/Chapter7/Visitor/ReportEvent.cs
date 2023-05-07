using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Visitor
{
    public class ReportEvent : BasicEvent, IUploadEventData, IIoTEventData
    {
        public ReportEvent(): base()
        {
        }
        public ReportEvent(Guid id, string type, string source, string fileName, string fileUrl, string fileType, string action, string value):
            base(id, type, source)
        {
            this.FileName = fileName;
            this.FileUrl = fileUrl;
            this.FileType = fileType;
            this.Action = action;
            this.Value = value;
        }

        public string FileName { get ; set ; }
        public string FileUrl { get ; set ; }
        public string FileType { get; set; }
        public string Action { get; set; }
        public string Value { get; set; }
    }
}
