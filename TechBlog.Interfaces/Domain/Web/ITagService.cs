using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Models.Tags;

namespace TechBlog.Interfaces.Domain.Web
{
    public interface ITagService
    {
        Tag GetTag(string tagCode);
    }
}
