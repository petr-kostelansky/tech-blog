using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Validator
{
    public class ValidationError
    {
        public ValidationError(string field, string errorMessage)
        {
            this.field = field;
            this.errorMessage = errorMessage;
        }

        public ValidationError(Expression<Func<object>> field, string errorMessage)
        {
            this.field = GetExpressionName(field);
            this.errorMessage = errorMessage;
        }

        private string field;
        public string Field
        {
            get { return field; }
            set { field = value; }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        private string GetExpressionName(Expression<Func<object>> field)
        {
            MemberExpression body = field.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)field.Body;
                body = ubody.Operand as MemberExpression;
            }

            if (body == null)
                return null;

            return body.Member.Name;
        }
    }
}
