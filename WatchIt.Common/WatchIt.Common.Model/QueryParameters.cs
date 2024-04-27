using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace WatchIt.Common.Model;

public abstract class QueryParameters<T> where T : class
{
    #region PROPERTIES

    [FromQuery(Name = "order_by")]
    public string? OrderBy { get; set; }

    [FromQuery(Name = "order")]
    public string? Order { get; set; }

    [FromQuery(Name = "first")]
    public int? First { get; set; }

    [FromQuery(Name = "after")]
    public int? After { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public abstract bool IsMeetingConditions(T item);

    public IEnumerable<T> PrepareData(IEnumerable<T> data)
    {
        data = data.Where(IsMeetingConditions);
        
        if (OrderBy is not null)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                JsonPropertyNameAttribute? attribute = property.GetCustomAttributes<JsonPropertyNameAttribute>(true).FirstOrDefault();
                if (attribute is not null && attribute.Name == OrderBy)
                {
                    if (Order == "asc")
                    {
                        data = data.OrderBy(property.GetValue);
                    }
                    else
                    {
                        data = data.OrderByDescending(property.GetValue);
                    }
                    break;
                }
            }
        }
        if (After is not null)
        {
            data = data.Skip(After.Value);
        }
        if (First is not null)
        {
            data = data.Take(First.Value);
        }
        return data;
    }

    #endregion



    #region PRIVATE METHODS

    protected bool TestString(string? property, string? regexQuery) => 
    (
        string.IsNullOrEmpty(regexQuery) 
        || 
        (
            !string.IsNullOrEmpty(property) 
            && 
            new Regex(regexQuery).IsMatch(property)
        )
    );

    protected bool TestComparable(IComparable? property, IComparable? exact, IComparable? from, IComparable? to) =>
    (
        (
            exact is null
            ||
            (
                property is not null
                &&
                property.CompareTo(exact) == 0
            )
        )
        &&
        (
            from is null
            ||
            (
                property is not null
                &&
                property.CompareTo(from) > 0
            )
        )
        &&
        (
            to is null
            ||
            (
                property is not null
                &&
                property.CompareTo(to) < 0
            )
        )
    );

    #endregion
}