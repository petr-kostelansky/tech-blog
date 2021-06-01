using System;
using System.Collections.Generic;
using Model = TechBlog.Contract.Models.Tags;

namespace TechBlog.Web.Areas.Admin.ViewModels.Tags
{
    public class ListViewModel
    {
        public IEnumerable<Model.Tag> Tags { get; set; }
    }
}