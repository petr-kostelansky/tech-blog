using AspNetIdentity = Microsoft.AspNet.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlog.Contract.Models.Articles;
using TechBlog.Interfaces.Domain.Admin;
using TechBlog.Web.Areas.Admin.Managers.Interfaces;
using TechBlog.Web.Areas.Admin.ViewModels.Article;
using Microsoft.AspNet.Identity;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Web.Areas.Admin.Managers
{
    public class ArticleManager : IArticleManager
    {
        private readonly IArticleService articleService;
        private readonly Lazy<IMapper> mapper;

        public ArticleManager(IArticleService articleService, Lazy<IMapper> mapper)
        {
            this.articleService = articleService;
            this.mapper = mapper;
        }

        public ListViewModel GetListViewModel(ArticleFilter filter)
        {
            var viewModel = new ListViewModel();
            viewModel.Articles = articleService.GetArticlesByFilter(filter);

            return viewModel;
        }

        public CreateViewModel GetCreateViewModel()
        {
            var viewModel = new CreateViewModel();

            return viewModel;
        }

        public void ProcessCreateViewModel(CreateViewModel viewModel, int userId)
        {
            var newArticle = new NewArticle
            {
                Content = viewModel.Content,
                CreatedByUserId = userId,
                Title = viewModel.Title,
                Description = viewModel.Description,
                MetaDescription = viewModel.MetaDescription,
                MetaKeywords = viewModel.MetaKeywords,
                Url = viewModel.Url,
                Tags = String.IsNullOrEmpty(viewModel.SelectedTags)
                    ? Enumerable.Empty<AssignTag>()
                    : viewModel.SelectedTags.Replace(" ", "").Split(',').Select(s => new AssignTag { Code = s })
            };

            articleService.CreateArticle(newArticle);

            return;
        }

        public EditViewModel GetEditViewModel(int articleId)
        {
            var article = articleService.GetArticleById(articleId);

            var viewModel = new EditViewModel
            {
                Content = article.Content,
                Visible = article.Visible,
                Title = article.Title,
                Description = article.Description,
                MetaDescription = article.MetaDescription,
                MetaKeywords = article.MetaKeywords,
                CanEditUrl = CanEditUrl(article.Created),
                Url = article.Url,
                Id = article.Id,
                Tags = article.Tags
            };

            return viewModel;
        }

        public void ProcessEditViewModel(EditViewModel viewModel)
        {
            var editArticle = new EditArticle
            {
                Content = viewModel.Content,
                Title = viewModel.Title,
                Description = viewModel.Description,
                MetaDescription = viewModel.MetaDescription,
                MetaKeywords = viewModel.MetaKeywords,
                Id = viewModel.Id,
                Tags = String.IsNullOrEmpty(viewModel.SelectedTags)
                    ? Enumerable.Empty<AssignTag>()
                    : viewModel.SelectedTags.Replace(" ", "").Split(',').Select(s => new AssignTag { Code = s })
            };

            editArticle.Url = viewModel.Url;

            articleService.UpdateArticle(editArticle);

            return;
        }

        private bool CanEditUrl(DateTime created)
        {
            return (DateTime.Now - created).TotalMinutes < 60;
        }
    }
}