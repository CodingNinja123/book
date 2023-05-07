﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter7.Visitor.Logging
{
    public interface ILoggingDestination
    {
        void Initialize(Guid sessionsGuid);
        void Flush();
        void Reset();
        void Log(string message);
        bool IsSessionsStarted();
    }
}
