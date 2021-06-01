using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Articles;
using TechBlog.Interfaces.Dal.Web;
using TechBlog.Interfaces.Domain.Web;

namespace TechBlog.Domain.Services
{
    public class SitemapService : ISitemapService
    {
        public readonly ISitemapRepository sitemapRepo;

        public SitemapService(ISitemapRepository sitemapRepo)
        {
            this.sitemapRepo = sitemapRepo;
        }

        public IEnumerable<ArticleInfo> GetArticles()
        {
            return sitemapRepo.GetArticles();
        }
    }
}
