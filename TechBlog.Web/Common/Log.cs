using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechBlog.Web.Common
{
    public sealed class Log
    {
        private static Logger instance = LogManager.GetLogger("Errors");

        public static Logger Instance
        {
            get { return instance; }
        }

        private Log()
        {
        }
    }

    public sealed class NotFoundLog
    {
        private static Logger instance = LogManager.GetLogger("NotFound");

        public static Logger Instance
        {
            get { return instance; }
        }

        private NotFoundLog()
        {
        }
    }
}