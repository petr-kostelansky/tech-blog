using AutoMapper;
using Model = TechBlog.Contract.Models.Articles;
using ModelTag = TechBlog.Contract.Models.Tags;
using TechBlog.DbContext;

namespace TechBlog.Dal.Admin.Mappings
{
    public class DalAdminMapperProfile : Profile
    {
        public DalAdminMapperProfile()
        {
            CreateMap<ViewArticle, Model.Article>();

            CreateMap<ViewArticle, Model.ArticleInfo>();

            CreateMap<GetArticleTagsResult, ModelTag.Tag>();
            CreateMap<ViewTag, ModelTag.Tag>();
            CreateMap<GetTagResult, ModelTag.Tag>();
        }
    }
}
