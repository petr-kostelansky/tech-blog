using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model = TechBlog.Contract.Models.Articles;

namespace TechBlog.Web.Areas.Admin.ViewModels.Article
{
    public class ListViewModel
    {
        public IPagedList<Model.ArticleInfo> Articles { get; set; }

        public Model.ArticleFilter Filter { get; set; }
    }
}