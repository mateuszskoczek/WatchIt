using Microsoft.AspNetCore.Mvc;

namespace WatchIt.DTO.Query;

public sealed record PagingQuery
{
    [FromQuery(Name = "first")]
    public int? First { get; set; }

    [FromQuery(Name = "after")]
    public int? After { get; set; }
}

public static class PagingQueryExtensions
{
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> data, PagingQuery pagingQuery)
    {
        if (pagingQuery.After is not null)
        {
            data = data.Skip(pagingQuery.After.Value);
        }
        if (pagingQuery.First is not null)
        {
            data = data.Take(pagingQuery.First.Value);
        }
        return data;
    }
}