using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechBlog.Contract.Models.Tags;
using TechBlog.Interfaces.Domain.Admin;
using TechBlog.Web.Areas.Admin.Managers.Interfaces;
using TechBlog.Web.Areas.Admin.ViewModels.Tags;

namespace TechBlog.Web.Areas.Admin.Managers
{
    public class TagManager : ITagManager
    {
        private readonly ITagService tagService;

        public TagManager(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public ListViewModel ListViewModel()
        {
            ListViewModel viewModel = new ListViewModel();
            viewModel.Tags = GetTags();

            return viewModel;
        }

        public IEnumerable<Tag> GetTags()
        {
            return tagService.GetTags();
        }

        public CreateViewModel CreateViewModel(string returnUrl)
        {
            return new CreateViewModel { ReturnUrl = returnUrl };
        }

        public void CreateViewModel(CreateViewModel viewModel)
        {
            tagService.CreateTag(viewModel.Tag);
        }

        public EditViewModel EditViewModel(string tagCode, string returnUrl)
        {
            var res = tagService.GetTag(tagCode);
            EditTag tag = new EditTag
            {
                OriginalCode = res.Code,
                Code = res.Code,
                Name = res.Name
            };

            return new EditViewModel(tag, returnUrl);
        }

        public void EditViewModel(EditViewModel viewModel)
        {
            tagService.EditTag(viewModel.Tag);
        }
    }
}