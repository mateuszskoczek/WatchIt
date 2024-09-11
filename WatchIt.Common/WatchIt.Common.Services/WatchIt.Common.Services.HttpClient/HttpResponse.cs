using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WatchIt.Common.Services.HttpClient;

public class HttpResponse
{
    #region FIELDS

    private readonly HttpResponseMessage _message;
    
    private Action? _2XXAction;
    private Action? _400Action;
    private Action? _401Action;
    private Action? _403Action;
    private Action? _404Action;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    internal HttpResponse(HttpResponseMessage message)
    {
        _message = message;
    }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public HttpResponse RegisterActionFor2XXSuccess(Action? action)
    {
        _2XXAction = action;
        return this;
    }
    
    public HttpResponse RegisterActionFor2XXSuccess<T>(Action<T>? action)
    {
        _2XXAction = () => Invoke(action);
        return this;
    }

    public HttpResponse RegisterActionFor400BadRequest(Action<IDictionary<string, string[]>>? action)
    {
        _400Action = () => Invoke(action);
        return this;
    }

    public HttpResponse RegisterActionFor401Unauthorized(Action? action)
    {
        _401Action = action;
        return this;
    }

    public HttpResponse RegisterActionFor403Forbidden(Action? action)
    {
        _403Action = action;
        return this;
    }
    
    public HttpResponse RegisterActionFor404NotFound(Action? action)
    {
        _404Action = action;
        return this;
    }

    public void ExecuteAction()
    {
        switch ((int)_message.StatusCode)
        {
            case >= 200 and <= 299: _2XXAction?.Invoke(); break;
            case 400: _400Action?.Invoke(); break;
            case 401: _401Action?.Invoke(); break;
            case 403: _403Action?.Invoke(); break;
            case 404: _404Action?.Invoke(); break;
        }
    }
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    private async void Invoke<T>(Action<T>? action)
    {
        Stream streamData = await _message.Content.ReadAsStreamAsync();
        T? data = await JsonSerializer.DeserializeAsync<T>(streamData);
        action?.Invoke(data!);
    }

    private async void Invoke(Action<IDictionary<string, string[]>>? action)
    {
        Stream streamData = await _message.Content.ReadAsStreamAsync();
        ValidationProblemDetails? data = await JsonSerializer.DeserializeAsync<ValidationProblemDetails>(streamData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        action?.Invoke(data!.Errors);
    }
    
    #endregion
}