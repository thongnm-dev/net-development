using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        #region Properties
        public int PageNumber { get; }

        public int TotalCount { get; }

        public int PageSize { get; }

        public int TotalPages { get; }

        public bool HasPreviousPage { get; }

        public bool HasNextPage { get; }

        #endregion

        #region Ctor
        public PagedList(IList<T> source, int pageNumber, int pageSize, int? totalCount = null)
        {
            pageSize = Math.Max(pageSize, 1);

            TotalCount = totalCount ?? source.Count;

            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0) TotalPages++;

            PageSize = pageSize;

            PageNumber = pageNumber;

            AddRange(totalCount != null ? source : source.Skip(PageNumber * PageSize ).Take(PageSize));
        }
        #endregion
    }
}
