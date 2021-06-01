using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Articles;
using TechBlog.Interfaces.Dal.Admin;
using TechBlog.Validator;

namespace TechBlog.Domain.Admin.Validations
{
    public class UpdateArticleValidator<T> : CustomValidator<T>
        where T : EditArticle
    {
        private readonly IArticleRepository articleRepo;

        public UpdateArticleValidator(IArticleRepository articleRepo)
        {
            this.articleRepo = articleRepo;
        }

        protected override void Validations(T obj, ValidationErrorCollection errors)
        {
            if (String.IsNullOrEmpty(obj.Url))
                errors.Add(() => obj.Url, "Can not be empty.");
            else
            {
                if (articleRepo.ExistArticle(obj.Url, obj.Id))
                    errors.Add(() => obj.Url, "Article with same url already exists. Try another url.");
            }

            if (String.IsNullOrEmpty(obj.Title))
                errors.Add(() => obj.Title, "Can not be empty.");

            if (String.IsNullOrEmpty(obj.Content))
                errors.Add(() => obj.Content, "Can not be empty.");
        }
    }
}
