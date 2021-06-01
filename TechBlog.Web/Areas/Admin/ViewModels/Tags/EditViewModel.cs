using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Web.Areas.Admin.ViewModels.Tags
{
    public class EditViewModel : ReturnViewModel
    {
        public EditTag Tag { get; set; }

        public EditViewModel() { }

        public EditViewModel(EditTag tag, string returnUrl)
        {
            Tag = tag;
            ReturnUrl = returnUrl;
        }
    }
}