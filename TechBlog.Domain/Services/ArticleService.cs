using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Exceptions;
using TechBlog.Contract.Models.Articles;
using TechBlog.Interfaces.Dal.Web;
using TechBlog.Interfaces.Domain.Web;

namespace TechBlog.Domain.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepo;

        public ArticleService(IArticleRepository articleRepo)
        {
            this.articleRepo = articleRepo;
        }

        public Article GetArticle(string url)
        {
            if (String.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            var article = articleRepo.GetArticle(url);
            if (article == null)
                throw new ServiceException("Article not found");

            return article;
        }

        public IPagedList<ArticleInfo> GetArticlesByFilter(ArticleFilter articleFilter)
        {
            return articleRepo.GetArticlesByFilter(articleFilter);
        }
    }
}
