using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace WatchIt.Common.Query;

public abstract class QueryParameters
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

    public override string ToString()
    {
        List<string> queries = new List<string>();
        PropertyInfo[] properties = this.GetType().GetProperties();
        foreach (PropertyInfo property in properties)
        {
            object? value = property.GetValue(this);
            FromQueryAttribute? attribute = property.GetCustomAttributes<FromQueryAttribute>(true).FirstOrDefault();
            if (value is not null && attribute is not null)
            {
                string query = $"{attribute.Name}={value}";
                queries.Add(query);
            }
        }

        return $"?{string.Join('&', queries)}";
    }

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected static bool TestBoolean(bool property, bool? query) =>
    (
        query is null
        ||
        property == query
    );

    protected static bool TestString(string? property, string? regexQuery) => 
    (
        string.IsNullOrEmpty(regexQuery) 
        || 
        (
            !string.IsNullOrEmpty(property) 
            && 
            new Regex(regexQuery).IsMatch(property)
        )
    );

    protected static bool TestComparable(IComparable? property, IComparable? exact, IComparable? from, IComparable? to) =>
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



public abstract class QueryParameters<T> : QueryParameters where T : class 
{
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
}