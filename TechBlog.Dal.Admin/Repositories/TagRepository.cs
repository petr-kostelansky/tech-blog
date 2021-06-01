using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TechBlog.DbContext;
using TechBlog.Interfaces.Dal.Admin;
using Model = TechBlog.Contract.Models.Tags;

namespace TechBlog.Dal.Admin.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        private readonly IMapper mapper;

        public TagRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public  int CreateArticleTag(int articleId, string tagCode)
        {
            // Testing opening context and transaction
            //BeginTransaction();
            var num = Context.InsArticleTag(articleId, tagCode);
            //RollbackTransaction();
            //CommitTransaction();

            return num;
        }

        public Model.Tag GetTag(string tagCode)
        {
            var tag = Context.GetTag(tagCode).FirstOrDefault();
            var result = mapper.Map<GetTagResult, Model.Tag>(tag);

            return result;
        }

        public IEnumerable<Model.Tag> GetTags()
        {
            var res = Context.ViewTags.OrderBy(s => s.Name).ToList();
            var result = mapper.Map<IEnumerable<ViewTag>, IEnumerable<Model.Tag>>(res);

            return result;
        }

        public int CreateTag(Model.NewTag tag)
        {
            var tagId = Context.InsTag(tag.Code, tag.Name);
            //CacheManager<ImageCacheType>.Clear();

            return tagId;
        }

        public void EditTag(Model.EditTag tag)
        {
            Context.UpdTag(tag.OriginalCode, tag.Code, tag.Name);
            //CacheManager<ImageCacheType>.Clear();
        }
    }
}
