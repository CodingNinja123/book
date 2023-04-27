﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Observer
{
    public interface IUploadEventData: IBasicEvent
    {
        string FileName { get; set; }
        string FileUrl { get; set; }
        string FileType { get; set; }
    }
}
