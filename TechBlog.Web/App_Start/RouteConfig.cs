using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TechBlog.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

           
            routes.MapRoute(
                name: "ArticlesLoadMore",
                url: "articles/AsyncLoadMore",
                defaults: new { controller = "Article", action = "AsyncLoadMore" },
                namespaces: new[] { "TechBlog.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Articles",
                url: "articles/search",
                defaults: new { controller = "Article", action = "GetArticles" },
                namespaces: new[] { "TechBlog.Web.Controllers" }
            );

            routes.MapRoute(
                name: "LatestArticles",
                url: "articles/GetLastArticles",
                defaults: new { controller = "Article", action = "GetLastArticles" },
                namespaces: new[] { "TechBlog.Web.Controllers" }
            ); 

            routes.MapRoute(
                name: "ArticlePage",
                url: "article/{url}",
                defaults: new { controller = "Article", action = "GetArticle"},
                namespaces: new[] { "TechBlog.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TechBlog.Web.Controllers" }
            );
        }
    }
}
