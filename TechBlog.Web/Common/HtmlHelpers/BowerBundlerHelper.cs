using B4BCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace TechBlog.Web.Common.HtmlHelpers
{
    /// <summary>
    /// This class contains extention methods to provide a the inclusings of CSS and JavaScript files in your views
    /// 1) @Html.HtmlCssCached("bundleName") is equivalent to @Styles.Render("~/Content/bundleName")
    /// 2) @Html.HtmlScriptsCached("bundleName") is equivalent to @Scripts.Render("~/bundles/bundleName")
    /// Whether it delivers individual files or the single minfied file that Grunt produced is defined by
    /// whether the code was compiled in DEBUG mode or not. This can be overridden on each method by the 
    /// optional 'forceState' parameter.
    /// 
    /// Both these methods have some small limitations:
    /// 1. These methods can do searches for files, e.g. ~/Scripts/*.js, or ~/Scripts/*/*.js 
    ///    However it does not support Grunt/Gulp's /**/ search all directories and subdirectories feature.
    ///    It is something that could be implemented, but I left it out for now. It throws an NotImplementedException if found
    /// 2. I suspect that the .NET search and the Grunt/Gulp search might have slightly different rules. 
    ///    see the Note under Remarks about a search like *.js https://msdn.microsoft.com/en-us/library/wz42302f%28v=vs.110%29.aspx
    /// 3. BundlerHelper has the same problem (feature) as BundleConfig. If you change or add files to the settings.json file 
    ///    then you need to rebuild your application because HtmlCssCached and HtmlCssCached deliver from a static cache. 
    ///    You can turn off caching by setting the forceState parameter in each methods to true.
    /// Finally it is very tempting to add comments to the settings.json file. While Json.Net handles comments Grunt doesn't, and fails silently!
    /// </summary>
    public static class BowerBundlerHelper
    {
        private static BundlerForBower _bundler;
        private static readonly ConcurrentDictionary<string, MvcHtmlString> IncludeCache = new ConcurrentDictionary<string, MvcHtmlString>();
        private static readonly ConcurrentDictionary<string, string> StaticCachebusterCache = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// This returns the CSS links using caching if forceState is null
        /// Note: this assumes that the CSS minified file is in the directory "~/css/"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="bundleName">The name of the setting.json property containing the list of Css file to include. 
        /// defaults to main Css file</param>
        /// <param name="forceState">if not null then true forces into debug state and false forces production state</param>
        /// <returns></returns>
        public static MvcHtmlString HtmlCssCached(this HtmlHelper helper, string bundleName, bool? forceState = null)
        {
            return (forceState == null)
                ? IncludeCache.GetOrAdd(bundleName, setup => CreateHtmlIncludes(helper, bundleName, CssOrJs.Css, forceState))
                : CreateHtmlIncludes(helper, bundleName, CssOrJs.Css, forceState);
        }

        /// <summary>
        /// This returns the script includes for a specific group using caching if forceState is null
        /// Note: this assumes that the JavaScript minified file is in the directory "~/js/"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="bundleName">The name of the setting.json property containing the list of JavaScript file to include. 
        /// defaults to main js file</param>
        /// <param name="forceState">if not null then true forces into debug state and false forces production state</param>
        /// <returns></returns>
        public static MvcHtmlString HtmlScriptsCached(this HtmlHelper helper, string bundleName, bool? forceState = null)
        {
            return (forceState == null)
                ? IncludeCache.GetOrAdd(bundleName, setup => CreateHtmlIncludes(helper, bundleName, CssOrJs.Js, forceState))
                : CreateHtmlIncludes(helper, bundleName, CssOrJs.Js, forceState);
        }

        /// <summary>
        /// This can be applied to any static file, e.g. an image, and it adds a cachebuster value to the file access.
        /// The format of how the cacheBuster value is added is controlled by the 'StaticFileCaching' in the BunderForBower.json file.
        /// If a checksum is calculated for the file it is cached locally, as it can take some time on large files.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="relFilePath">The relative path to the file inside the web application</param>
        /// <param name="precalculatedCacheBuster">if null then a cachebusting value is calculated based on the file content (cached locally for performance)
        /// If you provide a precalculated a cachebuster value, say created at build time, then that will be used instead</param>
        /// <returns></returns>
        public static string AddCacheBusterCached(this HtmlHelper helper, string relFilePath,
            string precalculatedCacheBuster = null)
        {
            if (precalculatedCacheBuster == null)
                precalculatedCacheBuster = StaticCachebusterCache.GetOrAdd(relFilePath, setup => GetChecksumFromRelPath(relFilePath));

            var bundler = GetBundlerForBowerCached(helper);
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            return bundler.FormStaticFileWithCacheBuster(urlHelper.Content(relFilePath), precalculatedCacheBuster);
        }

        /// <summary>
        /// This calculate a checksum based on the content of the file. It actually uses a SHA256 Hash.
        /// This is heplful as it allows you to use the general Grunt 'build'command, which rebuilds everything, 
        /// and the cache buster won't change unless the content changes.
        /// </summary>
        /// <param name="absFilePath"></param>
        /// <returns>a checksum string which is http safe</returns>
        public static string GetChecksumBasedOnFileContent(string absFilePath)
        {
            using (var stream = File.OpenRead(absFilePath))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                var base64 = Convert.ToBase64String(checksum);
                return base64.Replace("/", "_").Replace("+", "-").Substring(0, base64.Length - 1);    //make valid HTTP parameter string
            }
        }

        //---------------------------------------
        //private methods

        /// <summary>
        /// This returns the html to include either CSS or JavaScript files
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="bundleName">The name of the bundle property and the name of the minified file</param>
        /// <param name="cssOrJs">This says if its css or javascript. NOTE: the enum string is used as the dir and the file type</param>
        /// <param name="forceState">if not null then true forces into debug state and false forces production state</param>
        /// <returns></returns>
        private static MvcHtmlString CreateHtmlIncludes(this HtmlHelper helper, string bundleName, CssOrJs cssOrJs, bool? forceState = null)
        {
            var isDebug = false;
#if DEBUG
            isDebug = true;
#endif
            if (forceState != null)
                isDebug = (bool)forceState;
            //isDebug = false; // for testing purpose
            var bundler = GetBundlerForBowerCached(helper);
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            return new MvcHtmlString(bundler.CalculateHtmlIncludes(bundleName, cssOrJs, isDebug, urlHelper.Content));
        }

        private static string GetChecksumFromRelPath(string fileRelPath)
        {
            return GetChecksumBasedOnFileContent(System.Web.Hosting.HostingEnvironment.MapPath(fileRelPath));
        }

        private static BundlerForBower GetBundlerForBowerCached(HtmlHelper helper)
        {
            return _bundler ??
                   (_bundler = new BundlerForBower(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(),
                       HostingEnvironment.MapPath, GetChecksumFromRelPath));
        }
    }
}