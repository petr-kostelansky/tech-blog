using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Contract.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException()
            : base()
        { }

        public ServiceException(string message)
            : base(message)
        { }

        public ServiceException(string message, Exception innerEx)
            : base(message, innerEx)
        { }

    }
}
