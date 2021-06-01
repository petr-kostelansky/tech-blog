using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Exceptions;
using TechBlog.Validator;

namespace TechBlog.Domain.Admin.Validations
{
    public abstract class CustomValidator<T> : BaseValidator<T> 
        where T: class
    {
        protected override void ThrowExceptionOnError(ValidationErrorCollection errors)
        {
            IEnumerable<string> errorMesages = errors.Select(s =>
                String.IsNullOrEmpty(s.Field)
                ? s.ErrorMessage
                : String.Format("{0} - {1}", s.Field, s.ErrorMessage));

            throw new ValidationException() { ValidationErrors = errorMesages };
        }
    }
}
