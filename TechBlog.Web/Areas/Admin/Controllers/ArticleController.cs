using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechBlog.Contract.Models.Articles;
using TechBlog.Interfaces.Domain.Admin;
using TechBlog.Web.Areas.Admin.Managers.Interfaces;
using TechBlog.Web.Areas.Admin.ViewModels.Article;
using TechBlog.Web.Common.Attributes;

namespace TechBlog.Web.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly Lazy<IArticleManager> articleManager;
        private readonly Lazy<IArticleService> articleService;

        public ArticleController(Lazy<IArticleManager> articleManager, Lazy<IArticleService> articleService)
        {
            this.articleManager = articleManager;
            this.articleService = articleService;
        }

        [HttpGet]
        public ActionResult Index(ArticleFilter filter)
        {
            filter.AuthorId = User.Identity.GetUserId<int>();
            var viewModel = articleManager.Value.GetListViewModel(filter);
            viewModel.Filter = filter;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create(string returnUrl)
        {
            var viewModel = articleManager.Value.GetCreateViewModel();
            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    articleManager.Value.ProcessCreateViewModel(viewModel, User.Identity.GetUserId<int>());
                    return Redirect(viewModel.ReturnUrl);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int articleId, string returnUrl)
        {
            var viewModel = articleManager.Value.GetEditViewModel(articleId);
            viewModel.ReturnUrl = returnUrl;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    articleManager.Value.ProcessEditViewModel(viewModel);
                    return Redirect(viewModel.ReturnUrl);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public JsonResult SetVisibility(int articleId, bool visible, int? page)
        {
            articleService.Value.SetVisibility(articleId, visible);

            return Json(new { success = "1" }, JsonRequestBehavior.AllowGet);
        }
    }
}
