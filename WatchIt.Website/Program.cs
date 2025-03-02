using System.Net.Http.Headers;
using System.Text.Json;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Refit;
using WatchIt.DTO.Converters;
using WatchIt.Website.Clients;
using WatchIt.Website.Components;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Tokens;
using _Imports = Blazorise._Imports;

namespace WatchIt.Website;

public static class Program
{
    #region PUBLIC METHODS
    
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddResponseCaching();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddRazorComponents()
                        .AddInteractiveServerComponents();
        builder.Services.AddServices();
        builder.Services.AddClients(builder.Configuration);
        builder.Services.AddBlazorise(options =>
                        {
                            options.Immediate = true;
                        })
                        .AddBootstrap5Providers()
                        .AddFontAwesomeIcons();
        
        var app = builder.Build();

        app.UseResponseCaching();
        
        AuthenticationTokenProvider.SetTokenGetterFunc(async _ =>
        {
            using (var scope = app.Services.CreateScope())
            {
                IAuthenticationService service = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
                return await service.GetRawAccessTokenAsync() ?? string.Empty;
            }
        });

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
           .AddInteractiveServerRenderMode();

        app.Run();
    }
    
    #endregion



    #region PRIVATE METHODS

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokensService, TokensService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
    
    private static void AddClients(this IServiceCollection services, IConfigurationManager configuration)
    {
        string baseUri = configuration.GetValue<string>("Clients:BaseAddress")!;
        Uri BaseUriFunc(string controller) => new Uri($"{baseUri}/{controller}");
        
        RefitSettings settings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                Converters =
                {
                    new ColorJsonConverter(),
                }
            }),
            //AuthorizationHeaderValueGetter = (_, token) => AuthenticationTokenProvider.GetTokenAsync(token)
        };
        Action<HttpClient, string> configureHttpClient = (x, prefix) =>
        {
            x.BaseAddress = BaseUriFunc(prefix);
        };

        services.AddRefitClient<IAuthenticationClient>(settings).ConfigureHttpClient(x => configureHttpClient(x, "authentication"));
        services.AddRefitClient<IAccountsClient>(settings).ConfigureHttpClient(x => configureHttpClient(x, "accounts"));
        services.AddRefitClient<IGendersClient>(settings).ConfigureHttpClient(x => configureHttpClient(x, "genders"));
        services.AddRefitClient<IGenresClient>(settings).ConfigureHttpClient(x => configureHttpClient(x, "genres"));
        services.AddRefitClient<IMediaClient>(settings).ConfigureHttpClient(x => configureHttpClient(x, "media"));
        services.AddRefitClient<IPeopleClient>(settings).ConfigureHttpClient(x => configureHttpClient(x, "people"));
        services.AddRefitClient<IPhotosClient>(settings).ConfigureHttpClient(x => configureHttpClient(x, "photos"));
        services.AddRefitClient<IRolesClient>(settings).ConfigureHttpClient(x => configureHttpClient(x, "roles"));
    }

    #endregion
}