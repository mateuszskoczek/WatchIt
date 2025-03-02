using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;

namespace WatchIt.WebAPI.Repositories;

public static class QueryableExtensions
{
    internal static IQueryable<T> Include<T>(this IQueryable<T> queryable, Func<IQueryable<T>, IQueryable<T>>? additionalIncludes = null)
    {
        if (additionalIncludes is not null)
        {
            queryable = additionalIncludes(queryable);
        }
        return queryable;
    }
}