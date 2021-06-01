using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Articles;
using TechBlog.Web.ViewModels.Articles;

namespace TechBlog.Web.Managers.Interfaces
{
    public interface IArticleManager
    {
        ArticlesViewModel GetArticles(ArticleFilter filter);

        ArticleViewModel GetArticleViewModel(string url);
    }
}
