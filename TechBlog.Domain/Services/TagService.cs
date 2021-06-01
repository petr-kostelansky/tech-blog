using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Exceptions;
using TechBlog.Contract.Models.Tags;
using TechBlog.Interfaces.Dal.Web;
using TechBlog.Interfaces.Domain.Web;

namespace TechBlog.Domain.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepo;

        public TagService(ITagRepository tagRepo)
        {
            this.tagRepo = tagRepo;
        }

        public Tag GetTag(string tagCode)
        {
            Tag tag = tagRepo.GetTag(tagCode);
            if (tag == null)
            {
                throw new ServiceException(String.Format("Tag {0} can not be found.", tagCode));
            }

            return tag;
        }
    }
}
