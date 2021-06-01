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
    public class CreateViewModel : ReturnViewModel
    {
        public string Url { get; set; }

        public string Title { get; set; }

        [StringLength(500, MinimumLength = 50, ErrorMessage = "field must be atleast 50 and max 500 characters")]
        public string Description { get; set; }


        [AllowHtml]
        public string Content { get; set; }

        public bool Visible { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>();

        public string SelectedTags { get; set; }
    }
}