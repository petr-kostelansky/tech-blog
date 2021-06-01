using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TechBlog.Core.Sitemap;
using TechBlog.Core.Sitemap.Generators;
using TechBlog.Interfaces.Domain.Web;

namespace TechBlog.Web.Controllers
{
    public class DataController : BaseController
    {
        private readonly Lazy<ISitemapService> sitemapService;

        public DataController(Lazy<ISitemapService> sitemapService)
        {
            this.sitemapService = sitemapService;
        }

        [HttpGet]
        [Route("robots.txt", Name = "Robots"), OutputCache(Duration = 86400)]
        public ContentResult RobotsText()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("user-agent: *");
            stringBuilder.AppendLine("disallow: /error");
            stringBuilder.Append("sitemap: ");
            stringBuilder.AppendLine(this.Url.RouteUrl("ArticlesSitemap", null, this.Request.Url.Scheme).TrimEnd('/'));

            return this.Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
        }

        [HttpGet]
        [Route("articles.xml", Name = "ArticlesSitemap"), OutputCache(Duration = 3600)]
        public ContentResult Articles()
        {
            return GenerateSitemap(new ArticleGenerator(sitemapService.Value));
        }

        private ContentResult GenerateSitemap(SitemapGenerator generator)
        {
            string sitemap = generator.Generate(HttpContext.Request.Url.Host);

            //Response.ContentType = "text/xml";
            return this.Content(sitemap, "text/xml", Encoding.UTF8);
        }
    }
}