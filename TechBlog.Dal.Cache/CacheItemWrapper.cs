using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Dal.Cache
{
    public class CacheItemWrapper
    {
        public object Value { get; set; }

        public string Key { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
