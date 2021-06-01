using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Contract.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> ValidationErrors { get; set; }

        public ValidationException()
            : base()
        { }

        public ValidationException(string message)
            : base(message)
        { }

        public ValidationException(string message, Exception innerEx)
            : base(message, innerEx)
        { }
    }
}
