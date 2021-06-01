using PagedList;
using System.Collections.Generic;
using TechBlog.Contract.Models.Articles;
using TechBlog.Contract.Models.Tags;
using TechBlog.Domain.Admin.Validations;
using TechBlog.Interfaces.Dal.Admin;
using TechBlog.Interfaces.Domain.Admin;

namespace TechBlog.Domain.Admin.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public IPagedList<ArticleInfo> GetArticlesByFilter(ArticleFilter articleFilter)
        {
            return articleRepository.GetArticlesByFilter(articleFilter);
        }

        public Article GetArticleById(int id)
        {
            return articleRepository.GetArticleById(id);
        }

        public void UpdateArticle(EditArticle article)
        {
            new UpdateArticleValidator<EditArticle>(articleRepository).Validate(article);

            articleRepository.UpdateArticle(article);
        }

        public void CreateArticle(NewArticle article)
        {
            new CreateArticleValidator<NewArticle>(articleRepository).Validate(article);

            articleRepository.CreateArticle(article);
        }

        public void SetVisibility(int articleId, bool visible)
        {
            articleRepository.SetVisibility(articleId, visible);
        }

        public IEnumerable<Tag> GetArticleTags(int articleId)
        {
            return articleRepository.GetArticleTags(articleId);
        }
    }
}
