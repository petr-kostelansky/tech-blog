using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechBlog.Contract.Models.Articles;
using TechBlog.Web.Common.Attributes;
using TechBlog.Web.Managers.Interfaces;

namespace TechBlog.Web.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IArticleManager articleManager;

        public ArticleController(IArticleManager articleManager)
        {
            this.articleManager = articleManager;
        }

        [HttpGet]
        public ActionResult GetArticles(ArticleFilter filter)
        {
            int page = filter.Page;
            filter.PageSize = filter.Page * filter.PageSize;
            filter.Page = 1;

            var viewModel = articleManager.GetArticles(filter);

            // set back
            filter.Page = page;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetArticle(string url)
        {
            var viewModel = articleManager.GetArticleViewModel(url);

            ViewBag.MetaDescription = String.IsNullOrEmpty(viewModel.MetaDescription) 
                ? viewModel.Description 
                : viewModel.MetaDescription;
            ViewBag.MetaKeywords = String.IsNullOrEmpty(viewModel.MetaKeywords) 
                ? String.Join(",", viewModel.Tags.Select(s => s.Name)) 
                : viewModel.MetaKeywords;

            return View(viewModel);
        }

        [AjaxOnly]
        [HttpGet]
        public ActionResult GetLastArticles()
        {
            var filter = new ArticleFilter
            {
                Page = 1,
                PageSize = 6
            };
            var viewModel = articleManager.GetArticles(filter);

            return PartialView("_LastArticles", viewModel);
        }

        [AjaxOnly]
        [HttpGet]
        public ActionResult AsyncLoadMore(int page, string tag, int authorId)
        {
            ArticleFilter filter = new ArticleFilter();
            filter.Page = page;
            filter.PageSize = 6;
            filter.Tag = tag;
            filter.AuthorId = authorId;

            var viewModel = articleManager.GetArticles(filter);
            string partialView = RenderRazorViewToString("_MoreArticles", viewModel);

            return Json(new { count = viewModel.Articles.Count(), data = partialView }, JsonRequestBehavior.AllowGet);
        }
    }
}