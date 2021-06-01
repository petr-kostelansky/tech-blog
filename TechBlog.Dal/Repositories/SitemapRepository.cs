using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Model = TechBlog.Contract.Models.Articles;
using TechBlog.Contract.Models.Articles;
using TechBlog.DbContext;
using TechBlog.Interfaces.Dal.Web;

namespace TechBlog.Dal.Repositories
{
    public class SitemapRepository : BaseRepository, ISitemapRepository
    {
        private readonly IMapper mapper;

        public SitemapRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<ArticleInfo> GetArticles()
        {
            var res =  Context.ViewArticles.ToList();

            return mapper.Map<IEnumerable<ViewArticle>, IEnumerable<Model.ArticleInfo>>(res);
        }
    }
}
