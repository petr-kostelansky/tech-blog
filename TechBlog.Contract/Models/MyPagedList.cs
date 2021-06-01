using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBlog.Contract.Models
{
    public class MyPagedList<T> : BasePagedList<T>
    {
        public MyPagedList(IEnumerable<T> subCollection, int pageNumber, int pageSize, int totalItemCount)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", (object)pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", (object)pageSize, "PageSize cannot be less than 1.");
            this.TotalItemCount = subCollection == null ? 0 : totalItemCount;
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.PageCount = this.TotalItemCount > 0 ? (int)Math.Ceiling((double)this.TotalItemCount / (double)this.PageSize) : 0;
            this.HasPreviousPage = this.PageNumber > 1;
            this.HasNextPage = this.PageNumber < this.PageCount;
            this.IsFirstPage = this.PageNumber == 1;
            this.IsLastPage = this.PageNumber >= this.PageCount;
            this.FirstItemOnPage = (this.PageNumber - 1) * this.PageSize + 1;
            int num = this.FirstItemOnPage + this.PageSize - 1;
            this.LastItemOnPage = num > this.TotalItemCount ? this.TotalItemCount : num;
            if (subCollection == null || this.TotalItemCount <= 0)
                return;
            this.Subset.AddRange(subCollection);
        }
    }
}
