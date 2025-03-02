namespace WatchIt.Website.Services.Authentication;

public static class AuthenticationTokenProvider
{
    private static Func<CancellationToken, Task<string>>? _getTokenAsyncFunc;
    
    public static void SetTokenGetterFunc(Func<CancellationToken, Task<string>> getTokenAsyncFunc)
    {
        _getTokenAsyncFunc = getTokenAsyncFunc;
    }

    public static Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        if (_getTokenAsyncFunc is null)
        {
            throw new InvalidOperationException("Token getter func must be set before using it");
        }
        return _getTokenAsyncFunc!(cancellationToken);
    }
}