using System.Text.Json.Serialization;

namespace WatchIt.Common.Query;

public interface IQueryOrderable<T>
{
    [JsonIgnore]
    public static abstract IDictionary<string, Func<T, IComparable>> OrderableProperties { get; }
}