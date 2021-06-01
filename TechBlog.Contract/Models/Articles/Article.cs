using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Contract.Models.Articles
{
    public class Article : ArticleInfo
    {
        public string Content { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
