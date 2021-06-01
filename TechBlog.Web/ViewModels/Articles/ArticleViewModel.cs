using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Web.ViewModels.Articles
{
    public class ArticleViewModel
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastChange { get; set; }

        public string Author { get; set; }

        public int AuthorId { get; set; }

        public int Visited { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}