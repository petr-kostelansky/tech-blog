using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Articles;
using TechBlog.Web.Areas.Admin.ViewModels.Article;

namespace TechBlog.Web.Areas.Admin.Managers.Interfaces
{
    public interface IArticleManager
    {
        ListViewModel GetListViewModel(ArticleFilter filter);

        CreateViewModel GetCreateViewModel();

        void ProcessCreateViewModel(CreateViewModel viewModel, int userId);

        EditViewModel GetEditViewModel(int articleId);

        void ProcessEditViewModel(EditViewModel viewModel);
    }
}
