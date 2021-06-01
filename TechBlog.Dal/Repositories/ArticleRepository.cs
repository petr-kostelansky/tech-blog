using AutoMapper;
using System.Linq;
using Model = TechBlog.Contract.Models.Articles;
using TechBlog.DbContext;
using TechBlog.Interfaces.Dal.Web;
using TechBlog.Dal.Extensions;
using PagedList;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using TechBlog.Contract.Models;

namespace TechBlog.Dal.Repositories
{
    public class ArticleRepository : BaseRepository, IArticleRepository
    {
        private readonly IMapper mapper;
        private readonly ITagRepository tagRepo;

        public ArticleRepository(IMapper mapper, ITagRepository tagRepo)
        {
            this.mapper = mapper;
            this.tagRepo = tagRepo;
        }

        public Model.Article GetArticle(string url)
        {
            var res = Context.ViewArticles.FirstOrDefault(s => s.Url == url && s.Visible == true);

            if (res == null)
                return null;

            var article = mapper.Map<ViewArticle, Model.Article>(res);
            article.Content = article.Content.FillAdvertisement();
            article.Tags = tagRepo.GetTags(article.Id);

            IncrementVisited(article.Id);

            return article;
        }

        public IPagedList<Model.ArticleInfo> GetArticlesByFilter(Model.ArticleFilter filter)
        {
            IPagedList<Model.ArticleInfo> pagedList = null;
            if (string.IsNullOrEmpty(filter.Tag))
            {
                pagedList = Context.ViewArticles
                                    .WhereByQuerySetting(filter)
                                    .UseAsDataSource(mapper).For<Model.ArticleInfo>()
                                    .ToPagedList<Model.ArticleInfo>(filter.Page, filter.PageSize);
            }
            else
            {
                List<Model.ArticleInfo> articles = new List<Model.ArticleInfo>();
                int totalItemsCount = 0;

                using (SqlConnection connection = new SqlConnection(Context.Connection.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SearchArticles", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //string tag = filter.Tag.RemoveChars();
                        command.Parameters.Add("@TotalItemsCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@Tags", SqlDbType.Structured).Value = filter.Tag.ToSearchWords();
                        command.Parameters.Add("@SearchText", SqlDbType.VarChar, 100).Value = null;
                        command.Parameters.Add("@OrderBy", SqlDbType.Bit).Value = 2;
                        command.Parameters.Add("@OnlyVisible", SqlDbType.Bit).Value = filter.OnlyVisible;
                        command.Parameters.Add("@AuthorId", SqlDbType.Int).Value = filter.AuthorId == null ? DBNull.Value : (object)filter.AuthorId.Value;
                        command.Parameters.Add("@Page", SqlDbType.Int).Value = filter.Page;
                        command.Parameters.Add("@PageSize", SqlDbType.Int).Value = filter.PageSize;
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Model.ArticleInfo article = new Model.ArticleInfo();
                                article.Id = (int)(reader["Id"]);
                                article.Url = (reader["Url"].ToString());
                                article.Title = (reader["Title"].ToString());
                                article.Description = (reader["Description"].ToString());
                                article.Author = (reader["Author"].ToString());
                                article.Created = (DateTime)reader["Created"];

                                articles.Add(article);
                            }
                        }
                        totalItemsCount = (Int32.Parse(command.Parameters["@TotalItemsCount"].Value.ToString()));

                        //logRepository.Value.LogSearch(searchSettings.Page, searchSettings.SearchText, searchText, images.Select(s => s.Id).ToList());
                    }
                }

                pagedList = new MyPagedList<Model.ArticleInfo>(articles,
                    filter.Page, filter.PageSize, totalItemsCount);
            }

            return pagedList;
        }

        public void IncrementVisited(int articleId)
        {
            Context.UpdArticleVisited(articleId);
        }
    }
}
