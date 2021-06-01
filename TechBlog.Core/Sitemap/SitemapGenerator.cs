using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using TechBlog.Contract.Models.Sitemaps;

namespace TechBlog.Core.Sitemap
{
    public abstract class SitemapGenerator
    {
        private const string sitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private const string imageNamespace = "http://www.google.com/schemas/sitemap-image/1.1";
        private const int maxUrlInSitemapFile = 50000;

        private string serverName;
        private string urlPrefix;

        public abstract string SitemapName { get; }

        public SitemapGenerator()
        {
        }

        private string Generate()
        {
            using (Utf8StringWriter stringWriter = new Utf8StringWriter())
            {
                using (var xw = new XmlTextWriter(stringWriter))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement("urlset", sitemapNamespace);
                    xw.WriteAttributeString("xmlns", "image", null, imageNamespace);

                    GenerateUrls(xw);

                    xw.WriteEndElement();
                    xw.WriteEndDocument();
                }

                return stringWriter.ToString();
            }
        }

        protected void WriteUrl(XmlTextWriter xw, SitemapItem sitemap, float? priority = null, ChangeFreq changeFreq = ChangeFreq.weekly)
        {
            xw.WriteStartElement("url", sitemapNamespace);
            xw.WriteElementString("loc", sitemapNamespace, urlPrefix + sitemap.Url);
            xw.WriteElementString("changefreq", sitemapNamespace, changeFreq.ToString());

            if (priority.HasValue)
            {
                xw.WriteElementString("priority", sitemapNamespace, priority.Value.ToString(CultureInfo.InvariantCulture));
            }

            //if (sitemap.Image != null)
            //{
            //    WriteImage(xw, sitemap.Image);
            //}

            xw.WriteEndElement(); // url
        }

        //private void WriteImage(XmlTextWriter xw, SitemapImage sitemapImage)
        //{
        //    xw.WriteStartElement("image", imageNamespace);
        //    xw.WriteElementString("loc", imageNamespace, urlPrefix + sitemapImage.ImageUrl);
        //    xw.WriteElementString("title", imageNamespace, sitemapImage.ImageTitle);
        //    xw.WriteEndElement(); // image
        //}

        protected int ParseNumber(string fileName)
        {
            string[] numbers = Regex.Split(fileName, @"\D+");

            if (numbers == null)
            {
                return 0;
            }

            if (numbers.Length > 1)
            {
                throw new ApplicationException("Incorect sitemap file name format.");
            }

            if (!String.IsNullOrEmpty(numbers[0]))
            {
                return int.Parse(numbers[0]);
            }

            return 0;
        }

        public string Generate(string serverName)
        {
            this.serverName = serverName;
            this.urlPrefix = "http://" + serverName + "/";

            return Generate();
        }

        protected abstract void GenerateUrls(XmlTextWriter xw);

        protected enum ChangeFreq
        {
            always,
            hourly,
            daily,
            weekly,
            monthly,
            yearly,
            never
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
