using Microsoft.EntityFrameworkCore;
using Net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<IPagedList<T>> ToPageListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (source == null) 
                return new PagedList<T>(new  List<T>(), pageNumber, pageSize);
            pageSize = Math.Max(pageSize, 1);

            var result = new List<T>();
            int count = 0;

            Parallel.Invoke(async () => count = await source.CountAsync(), async () => result = await source.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync());

            return await Task.FromResult(new PagedList<T>(result, pageNumber, pageSize, count));
        }
    }
}
