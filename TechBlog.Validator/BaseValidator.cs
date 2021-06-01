using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Validator
{
    public abstract class BaseValidator<T> where T : class
    {
        public void Validate(T obj)
        {
            ValidationErrorCollection errors = new ValidationErrorCollection();

            Validations(obj, errors);

            if (errors.Any())
            {
                ThrowExceptionOnError(errors);
            }
        }

        protected abstract void Validations(T obj, ValidationErrorCollection errors);

        protected abstract void ThrowExceptionOnError(ValidationErrorCollection errors);
    }
}
