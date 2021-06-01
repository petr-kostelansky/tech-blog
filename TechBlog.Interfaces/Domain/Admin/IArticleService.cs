using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Articles;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Interfaces.Domain.Admin
{
    public interface IArticleService
    {
        IPagedList<ArticleInfo> GetArticlesByFilter(ArticleFilter articleFilter);

        Article GetArticleById(int id);

        void UpdateArticle(EditArticle article);

        void CreateArticle(NewArticle article);

        void SetVisibility(int articleId, bool disable);

        IEnumerable<Tag> GetArticleTags(int articleId);
    }
}
