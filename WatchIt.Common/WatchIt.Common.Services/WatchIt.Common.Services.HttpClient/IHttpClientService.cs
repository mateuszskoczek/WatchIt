namespace WatchIt.Common.Services.HttpClient;

public interface IHttpClientService
{
    Task<HttpResponse> SendRequestAsync(HttpRequest request);
}