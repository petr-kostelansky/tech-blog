using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TechBlog.DbContext;
using TechBlog.Interfaces.Dal.Web;
using Model = TechBlog.Contract.Models.Tags;

namespace TechBlog.Dal.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        private readonly IMapper mapper;

        public TagRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Model.Tag GetTag(string tagCode)
        {
            var tag = Context.GetTag(tagCode).FirstOrDefault();
            var result = mapper.Map<GetTagResult, Model.Tag>(tag);

            return result;
        }

        public IEnumerable<Model.Tag> GetTags(int articleId)
        {
            var res = Context.GetArticleTags(articleId).ToList();
            var result = mapper.Map<IEnumerable<GetArticleTagsResult>, IEnumerable<Model.Tag>>(res);

            return result;
        }
    }
}
