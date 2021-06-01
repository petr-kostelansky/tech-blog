using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlog.Contract.Models.Articles;
using TechBlog.Contract.Models.Users;
using TechBlog.Domain.Share.Services;
using TechBlog.Interfaces.Domain.Web;
using TechBlog.Web.Managers.Interfaces;
using TechBlog.Web.ViewModels.Articles;

namespace TechBlog.Web.Managers
{
    public class ArticleManager : IArticleManager
    {
        private readonly IArticleService articleService;
        private readonly ITagService tagService;
        private readonly IUserStore<User, int> userService;

        public ArticleManager(IArticleService articleService, ITagService tagService, IUserStore<User, int> userService)
        {
            this.articleService = articleService;
            this.tagService = tagService;
            this.userService = userService;
        }

        public ArticlesViewModel GetArticles(ArticleFilter filter)
        {
            var viewModel = new ArticlesViewModel();
            viewModel.Filter = filter;
            viewModel.Articles = articleService.GetArticlesByFilter(filter);

            if (!String.IsNullOrEmpty(filter.Tag))
            {
                var tag = tagService.GetTag(filter.Tag);
                if (tag != null)
                    viewModel.Tag = tag.Name;
            }

            if (filter.AuthorId != null)
            {
                var author = userService.FindByIdAsync(filter.AuthorId.Value).Result;
                if (author != null)
                    viewModel.Author = author.UserName;
            }

            return viewModel;
        }

        public ArticleViewModel GetArticleViewModel(string url)
        {
            var article = articleService.GetArticle(url);

            var viewModel = new ArticleViewModel
            {
                Url = article.Url,
                Title = article.Title,
                Description = article.Description,
                Content = article.Content,
                CreatedAt = article.Created,
                LastChange = article.LastChange,
                Author = article.Author,
                AuthorId = article.AuthorId,
                Visited = article.Visited,
                MetaDescription = article.MetaDescription,
                MetaKeywords = article.MetaKeywords,
                Tags = article.Tags
            };

            return viewModel;
        }
    }
}