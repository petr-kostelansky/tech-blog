using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Users;

namespace TechBlog.Contract.Models.Articles
{
    public class ArticleInfo
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Visible { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastChange { get; set; }

        public int Visited { get; set; }

        public string Author { get; set; }

        public int AuthorId { get; set; }
    }
}
