using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TechBlog.Contract.Models.Articles;
using TechBlog.Contract.Models.Sitemaps;
using TechBlog.Interfaces.Domain.Web;

namespace TechBlog.Core.Sitemap.Generators
{
    public class ArticleGenerator : SitemapGenerator
    {
        private readonly ISitemapService sitemapService;

        public override string SitemapName => "article";

        public ArticleGenerator(ISitemapService sitemapService)
        {
            this.sitemapService = sitemapService;
        }

        protected override void GenerateUrls(XmlTextWriter xw)
        {
            var articles = sitemapService.GetArticles();

            var sitemaps = Map(articles);

            foreach (var item in sitemaps)
            {
                item.Url = "article/" + item.Url;

                WriteUrl(xw, item, 1.0f);
            }
        }

        private IEnumerable<SitemapItem> Map(IEnumerable<ArticleInfo> articles)
        {
            List<SitemapItem> sitemaps = new List<SitemapItem>();

            SitemapItem sitemap;
            foreach (var article in articles)
            {
                sitemap = new SitemapItem();
                sitemap.Url = article.Url;

                sitemaps.Add(sitemap);
            }

            return sitemaps;
        }
    }
}
