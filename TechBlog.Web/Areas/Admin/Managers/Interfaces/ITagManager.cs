using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Tags;
using TechBlog.Web.Areas.Admin.ViewModels.Tags;

namespace TechBlog.Web.Areas.Admin.Managers.Interfaces
{
    public interface ITagManager
    {
        ListViewModel ListViewModel();

        IEnumerable<Tag> GetTags();

        CreateViewModel CreateViewModel(string returnUrl);

        void CreateViewModel(CreateViewModel viewModel);

        EditViewModel EditViewModel(string tagCode, string returnUrl);

        void EditViewModel(EditViewModel viewModel);
    }
}
