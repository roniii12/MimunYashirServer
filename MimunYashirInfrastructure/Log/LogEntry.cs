﻿using MimunYashirInfrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirInfrastructure.Log
{
    public class LogEntry
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public AppModule Module { get; set; }
        public AppLayer Layer { get; set; }
        public object Parameters { get; set; }
        public string LoggerAppVersion { get; set; }
        public string LoggerAppCompoment { get; set; }
    }
}
