using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.Proxy
{
    public class ReportEvent : BasicEvent, IUploadEventData, IIoTEventData
    {
        public string FileName { get ; set ; }
        public string FileUrl { get ; set ; }
        public string FileType { get; set; }
        public string Action { get; set; }
        public string Value { get; set; }
    }

    public class CopyOfReportEvent : BasicEvent, IUploadEventData, IIoTEventData
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public string Action { get; set; }
        public string Value { get; set; }
    }
}
