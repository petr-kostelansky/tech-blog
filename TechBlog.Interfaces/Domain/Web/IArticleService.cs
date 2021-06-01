using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Articles;

namespace TechBlog.Interfaces.Domain.Web
{
    public interface IArticleService
    {
        Article GetArticle(string url);

        IPagedList<ArticleInfo> GetArticlesByFilter(ArticleFilter articleFilter);
    }
}
