using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Contract.Models.Common
{
    public class PagingSetting
    {
        public PagingSetting()
        {
            PageSize = 6;
            Page = 1;
        }

        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
