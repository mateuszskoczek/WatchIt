using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.Database.Model.Media;

namespace WatchIt.DTO.Query;

public class OrderQuery
{
    #region PROPERTIES
    
    [FromQuery(Name = "order_asc")]
    [AliasAs("order_asc")]
    public bool OrderAscending { get; set; }
    
    [FromQuery(Name = "order_by")]
    [AliasAs("order_by")]
    public string? OrderBy { get; set; }

    #endregion
}

public static class OrderQueryExtensions
{
    public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> data, OrderQuery orderQuery, params IDictionary<string, Expression<Func<T, object?>>>[] orderKeys)
    {
        if (orderQuery.OrderBy is not null)
        {
            Dictionary<string, Expression<Func<T, object?>>> orderByKeys = orderKeys.SelectMany(x => x)
                                                                                         .ToDictionary(x => x.Key, x => x.Value);
            if (orderByKeys.TryGetValue(orderQuery.OrderBy, out Expression<Func<T, object?>>? orderFunc))
            {
                data = orderQuery.OrderAscending ? data.OrderBy(orderFunc) : data.OrderByDescending(orderFunc);
            }
        }
        return data;
    }
}