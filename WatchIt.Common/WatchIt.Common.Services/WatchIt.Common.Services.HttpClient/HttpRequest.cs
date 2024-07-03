using System.Diagnostics.CodeAnalysis;
using WatchIt.Common.Query;

namespace WatchIt.Common.Services.HttpClient;

public class HttpRequest
{
    #region PROPERTIES
    
    public required HttpMethodType MethodType { get; set; }
    public required string Url { get; set; }
    public QueryParameters? Query { get; set; }
    public object? Body { get; set; }
    public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

    public string FullUrl => $"{Url}{(Query is not null ? Query.ToString() : string.Empty)}";
    
    #endregion



    #region CONSTRUCTORS
    
    [SetsRequiredMembers]
    public HttpRequest(HttpMethodType methodType, string url)
    {
        MethodType = methodType;
        Url = url;
    }

    #endregion
}