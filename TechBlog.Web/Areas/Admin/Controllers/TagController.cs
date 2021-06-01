using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechBlog.Interfaces.Domain.Admin;
using TechBlog.Web.Areas.Admin.Managers;
using TechBlog.Web.Areas.Admin.Managers.Interfaces;
using TechBlog.Web.Areas.Admin.ViewModels.Tags;
using TechBlog.Web.Common.Attributes;

namespace TechBlog.Web.Areas.Admin.Controllers
{
    public class TagController : BaseController
    {
        private readonly ITagManager tagManager;

        public TagController(ITagManager tagManager)
        {
            this.tagManager = tagManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = tagManager.ListViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create(string returnUrl)
        {
            var viewModel = tagManager.CreateViewModel(returnUrl);

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CreateViewModel viewModel)
        {
            try
            {
                tagManager.CreateViewModel(viewModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(string tagCode, string returnUrl)
        {
            var viewModel = tagManager.EditViewModel(tagCode, returnUrl);

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(EditViewModel viewModel)
        {
            try
            {
                tagManager.EditViewModel(viewModel);
                return String.IsNullOrEmpty(viewModel.ReturnUrl)
                    ? RedirectToAction("Index")
                    : MyRedirect(viewModel.ReturnUrl);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return View(viewModel);
                //return RedirectToAction("Edit", new { tagCode = viewModel.Tag.OriginalCode, returnUrl = viewModel.ReturnUrl });
            }
        }

        [AllowAnonymous]
        [AjaxOnly]
        [HttpGet]
        public ActionResult GetTags()
        {
            var tags = tagManager.GetTags().Select(s => new { value = s.Code, label = s.Name });

            return Json(tags, JsonRequestBehavior.AllowGet);
        }
    }
}