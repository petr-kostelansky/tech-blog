using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Articles;

namespace TechBlog.Interfaces.Dal.Web
{
    public interface ISitemapRepository
    {
        IEnumerable<ArticleInfo> GetArticles();
    }
}
