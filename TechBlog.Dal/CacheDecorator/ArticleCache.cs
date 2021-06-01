using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using TechBlog.Contract.Models.Articles;
using TechBlog.Interfaces.Dal.Web;
using TechBlog.Dal.Cache;

namespace TechBlog.Dal.CacheDecorator
{
    public class ArticleCache : IArticleRepository
    {
        private readonly IArticleRepository articleRepository;
        private readonly ICacheManager cache;

        public ArticleCache(ICacheManager cache, IArticleRepository articleRepository)
        {
            this.cache = cache;
            this.articleRepository = articleRepository;
        }

        public Article GetArticle(string url)
        {
            var cacheKey = "GetArticleByUrl_" + url;
            bool cached;
            Article article = cache.GetValue<Article>(cacheKey, out cached);

            if (article != null)
            {
                IncrementVisited(article.Id);
            }
            else if (!cached)
            {
                article = articleRepository.GetArticle(url);
                cache.SetValue<CacheEntity>(cacheKey, article, 30, CacheEntity.Article, article != null ? new int[] { article.Id } : Enumerable.Empty<int>());
            }

            return article;
        }

        public IPagedList<ArticleInfo> GetArticlesByFilter(ArticleFilter articleFilter)
        {
            return articleRepository.GetArticlesByFilter(articleFilter);
        }

        public void IncrementVisited(int articleId)
        {
            articleRepository.IncrementVisited(articleId);
        }
    }
}
