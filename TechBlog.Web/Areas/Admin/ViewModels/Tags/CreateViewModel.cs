using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Web.Areas.Admin.ViewModels.Tags
{
    public class CreateViewModel : ReturnViewModel
    {
        public NewTag Tag { get; set; }
    }
}