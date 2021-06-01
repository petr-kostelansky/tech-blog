using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Interfaces.Domain.Admin
{
    public interface ITagService
    {
        Tag GetTag(string tagCode);

        IEnumerable<Tag> GetTags();

        int CreateTag(NewTag tag);

        void EditTag(EditTag tag);
    }
}
