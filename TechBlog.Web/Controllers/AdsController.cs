using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechBlog.Web.Common.Attributes;

namespace TechBlog.Web.Controllers
{
    public class AdsController : Controller
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult UnderArticle()
        {
            return PartialView("_AdsUnderArticle");
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult LeftPanel()
        {
            return PartialView("_AdsUnderArticle");
        }
    }
}