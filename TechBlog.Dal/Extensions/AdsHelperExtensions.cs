using System;

namespace TechBlog.Dal.Extensions
{
    public static class AdsHelperExtensions
    {
        private const string replacePattern = "{{article-advertisement}}";

        private const string googleArticleAds = @"<script async src=""//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js""></script>
<ins class=""adsbygoogle""
     style=""display:block; text-align:center;""
     data-ad-layout=""in-article""
     data-ad-format=""fluid""
     data-ad-client=""ca-pub-3322311392930139""
     data-ad-slot=""6477726449""></ins>
<script>
     (adsbygoogle = window.adsbygoogle || []).push({ });
</script>";

        public static string FillAdvertisement(this string articleContent)
        {
            return articleContent.Replace(replacePattern, googleArticleAds);
        }

        public static string RemoveAdvertisement(this string articleContent)
        {
            return articleContent.Replace(replacePattern, String.Empty);
        }
    }
}
