using System;
using System.Collections.Generic;
using System.Linq;

namespace PaginationExtensions
{
    public static class PaginationExtensions
    {
        public static PaginationResult<T> ToPaginableEnumerable<T>(this IQueryable<T> query,
                                                            int page = 1, int pageSize = 20)
        {
            var result = new PaginationResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip)
                                .Take(pageSize)
                                .ToArray();

            return result;
        }

        public static PaginationResult<T> ToPaginableEnumerable<T>(this IEnumerable<T> list,
                                                            int page = 1, int pageSize = 20)
        {
            var query = list.AsEnumerable();
            return query.ToPaginableEnumerable<T>(page, pageSize);
        }
    }
}
