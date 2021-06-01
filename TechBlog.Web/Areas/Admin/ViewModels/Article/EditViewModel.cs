using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechBlog.Contract.Models.Articles;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Web.Areas.Admin.ViewModels.Article
{
    public class EditViewModel : CreateViewModel
    {
        public int Id { get; set; }

        public bool CanEditUrl { get; set; }
    }
}