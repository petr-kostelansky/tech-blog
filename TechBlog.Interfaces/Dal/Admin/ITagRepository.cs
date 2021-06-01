using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Interfaces.Dal.Admin
{
    public interface ITagRepository
    {
        int CreateArticleTag(int articleId, string tagCode);


        Tag GetTag(string tagCode);

        IEnumerable<Tag> GetTags();

        int CreateTag(NewTag tag);

        void EditTag(EditTag tag);
    }
}
