using PagedList;
using Model = TechBlog.Contract.Models.Articles;

namespace TechBlog.Web.ViewModels.Articles
{
    public class ArticlesViewModel
    {
        public IPagedList<Model.ArticleInfo> Articles { get; set; }

        public Model.ArticleFilter Filter { get; set; }

        public string Author { get; set; }

        public string Tag { get; set; }
    }
}