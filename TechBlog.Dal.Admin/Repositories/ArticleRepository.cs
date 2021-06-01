using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model = TechBlog.Contract.Models.Articles;
using ModelTag = TechBlog.Contract.Models.Tags;
using TechBlog.DbContext;
using TechBlog.Interfaces.Dal.Admin;
using TechBlog.Contract.Models.Common;
using TechBlog.Dal.Admin.Extensions;
using AutoMapper.QueryableExtensions;
using PagedList;
using TechBlog.Utils.Extensions;
using TechBlog.Dal.Cache;

namespace TechBlog.Dal.Admin.Repositories
{
    public class ArticleRepository : BaseRepository, IArticleRepository
    {
        private readonly IMapper mapper;
        private readonly ITagRepository tagRepo;
        private readonly ICacheManager cache;

        public ArticleRepository(IMapper mapper, ICacheManager cache, ITagRepository tagRepo)
        {
            this.mapper = mapper;
            this.cache = cache;
            this.tagRepo = tagRepo;
        }

        public IPagedList<Model.ArticleInfo> GetArticlesByFilter(Model.ArticleFilter articleFilter)
        {
            return Context.ViewArticles
                                .WhereByQuerySetting(articleFilter)
                                .UseAsDataSource(mapper).For<Model.ArticleInfo>()
                                .ToPagedList<Model.ArticleInfo>(articleFilter.Page, articleFilter.PageSize);
        }

        public Model.Article GetArticleById(int id)
        {
            var res = Context.ViewArticles.FirstOrDefault(s => s.Id == id);
            var article = mapper.Map<ViewArticle, Model.Article>(res);
            article.Tags = GetArticleTags(article.Id);

            return article;
        }

        public bool ExistArticle(string url, int articleId = 0)
        {


            return Context.ViewArticles.Any(s => s.Url == url && s.Id != articleId);
        }

        public void UpdateArticle(Model.EditArticle article)
        {
            var imageTags = GetArticleTags(article.Id).Select(s => new ModelTag.AssignTag { Code = s.Code });

            List<ModelTag.AssignTag> addTags = article.Tags.Except(article.Tags).ToList();
            List<ModelTag.AssignTag> deleteTags = imageTags.Except(article.Tags).ToList();

            BeginTransaction();
            try
            {
                Context.UpdArticle(
                    article.Id,
                    article.Url,
                    article.Title,
                    article.Description,
                    article.Content,
                    article.MetaDescription,
                    article.MetaKeywords
                    );

                deleteTags.ForEach(s => Context.DelArticleTag(article.Id, s.Code));
                addTags.ForEach(s => Context.InsArticleTag(article.Id, s.Code));

                CommitTransaction();

                cache.RemoveByTags(CacheEntity.Article, article.Id);
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
        }

        public int CreateArticle(Model.NewArticle article)
        {
            BeginTransaction();
            try
            {
                var articleId = Context.InsArticle(
                    article.Url,
                    article.Title,
                    article.Description,
                    article.Content,
                    article.CreatedByUserId,
                    article.MetaDescription,
                    article.MetaKeywords
                );

                if (article.Tags != null)
                {
                    article.Tags.ForEach<ModelTag.AssignTag>(s => CreateArticleTag(articleId, s.Code));
                }

                CommitTransaction();

                return articleId;
            }
            catch (Exception)
            {
                RollbackTransaction();
                throw;
            }
        }

        public bool SetVisibility(int articleId, bool visible)
        {
            return Context.UpdArticleVisibility(articleId, visible) == 1 ? true : false;
        }


        public IEnumerable<ModelTag.Tag> GetArticleTags(int articleId)
        {
            //return CacheManager<ImageCacheType>.GetValue<IEnumerable<ModelTag.Tag>>("ImageTagsForId_" + imageId, () =>
            //{
                var articleTags = Context.GetArticleTags(articleId).ToList();

                return mapper.Map<IEnumerable<GetArticleTagsResult>, IEnumerable<ModelTag.Tag>>(articleTags);
            //});
        }

        private int CreateArticleTag(int articleId, string tagCode)
        {
            return Context.InsArticleTag(articleId, tagCode);
        }

        private void DeleteArticleTag(int articleId, string tagCode)
        {
            Context.DelArticleTag(articleId, tagCode);
        }
    }
}
