using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Interfaces.Dal.Web
{
    public interface ITagRepository
    {
        Tag GetTag(string tagCode);

        IEnumerable<Tag> GetTags(int articleId);
    }
}
