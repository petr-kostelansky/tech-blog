using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Articles;
using TechBlog.DbContext;

namespace TechBlog.Dal.Extensions
{
    public static class ArticleExtensions
    {
        public static IQueryable<ViewArticle> WhereByQuerySetting(this IQueryable<ViewArticle> source, ArticleFilter filter)
        {
            source = source.Where(s => s.Visible == true);

            if (filter.AuthorId != null)
            {
                source = source.Where(s => s.AuthorId == filter.AuthorId.Value);
            }

            // Ordering ...
            if (!String.IsNullOrEmpty(filter.OrderBy))
            {
                var orderBy = filter.OrderBy;
                if (filter.Descending)
                {
                    orderBy = String.Format("{0} descending", orderBy);
                }

                source = source.OrderBy(orderBy);
            }

            return source;
        }
    }
}
