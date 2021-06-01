using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBlog.Contract.Exceptions;
using TechBlog.Contract.Models.Tags;
using TechBlog.Interfaces.Dal.Admin;
using TechBlog.Interfaces.Domain.Admin;

namespace TechBlog.Domain.Admin.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public Tag GetTag(string tagCode)
        {
            Tag tag = tagRepository.GetTag(tagCode);
            if (tag == null)
            {
                throw new ServiceException(String.Format("Tag {0} can not be found.", tagCode));
            }

            return tag;
        }

        public IEnumerable<Tag> GetTags()
        {
            IEnumerable<Tag> tags = tagRepository.GetTags();
            if (tags == null)
            {
                throw new ServiceException("Tags can not be found.");
            }

            return tags;
        }

        public int CreateTag(NewTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException("tag");
            }

            return tagRepository.CreateTag(tag);
        }

        public void EditTag(EditTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException("tag");
            }

            tagRepository.EditTag(tag);
        }
    }
}
