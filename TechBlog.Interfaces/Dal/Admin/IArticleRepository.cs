using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Articles;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Interfaces.Dal.Admin
{
    public interface IArticleRepository
    {
        IPagedList<ArticleInfo> GetArticlesByFilter(ArticleFilter articleFilter);

        Article GetArticleById(int id);

        bool ExistArticle(string url, int articleId = 0);

        void UpdateArticle(EditArticle article);

        int CreateArticle(NewArticle article);

        bool SetVisibility(int articleId, bool disable);

        IEnumerable<Tag> GetArticleTags(int articleId);
    }
}
