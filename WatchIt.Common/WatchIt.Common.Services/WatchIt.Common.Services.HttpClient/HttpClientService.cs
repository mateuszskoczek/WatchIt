using System.Text.Json;
using System.Text.Json.Serialization;

namespace WatchIt.Common.Services.HttpClient;

public class HttpClientService(System.Net.Http.HttpClient httpClient) : IHttpClientService
{
    #region PUBLIC METHODS

    public async Task<HttpResponse> SendRequestAsync(HttpRequest request)
    {
        HttpMethod method = request.MethodType switch
        {
            HttpMethodType.Get => HttpMethod.Get,
            HttpMethodType.Post => HttpMethod.Post,
            HttpMethodType.Put => HttpMethod.Put,
            HttpMethodType.Patch => HttpMethod.Patch,
            HttpMethodType.Delete => HttpMethod.Delete,
            _ => throw new ArgumentOutOfRangeException()
        };

        HttpRequestMessage httpRequest = new HttpRequestMessage(method, request.FullUrl);

        if (request.Body is not null)
        {
            string json = JsonSerializer.Serialize(request.Body);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType!.MediaType = "application/json";
            httpRequest.Content = content;
        }
        
        foreach (KeyValuePair<string, string> header in request.Headers)
        {
            httpRequest.Headers.Add(header.Key, header.Value);
        }

        HttpResponseMessage response = await httpClient.SendAsync(httpRequest);

        return new HttpResponse(response);
    }

    #endregion
}