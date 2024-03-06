using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core
{
    public interface IPagedList<T> : IList<T>
    {
        int PageNumber { get; }

        int TotalCount { get; }

        int PageSize { get; }

        int TotalPages { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}
