﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public interface IBasicEvent
    {
        Guid Id { get; set; }
        string Type { get; set; }
        string Source { get; set; }
    }
}
