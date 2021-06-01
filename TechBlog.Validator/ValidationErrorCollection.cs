using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Validator
{
    public class ValidationErrorCollection : List<ValidationError>
    {
        public void Add(string errorMessage)
        {
            this.Add(new ValidationError((string)null, errorMessage));
        }

        public void Add(string field, string errorMessage)
        {
            this.Add(new ValidationError(field, errorMessage));
        }

        public void Add(Expression<Func<object>> field, string errorMessage)
        {
            this.Add(new ValidationError(field, errorMessage));
        }
    }
}
