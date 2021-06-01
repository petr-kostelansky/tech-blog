using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Common;

namespace TechBlog.Contract.Models.Articles
{
    public class ArticleFilter : FilterSetting
    {
        public ArticleFilter()
        {
            OnlyVisible = true;
            OrderBy = "Id";
            Descending = true;
        }

        public string Tag { get; set; }

        public  bool OnlyVisible { get; }

        public int? AuthorId { get; set; }
    }
}
