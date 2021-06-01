using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Contract.Models.Common
{
    public class FilterSetting : PagingSetting
    {
        public string OrderBy { get; set; }

        public bool Descending { get; set; }
    }
}
