using System.Text.Json;
using System.Text.Json.Serialization;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Utility.Authentication;
using WatchIt.Website.Services.Utility.Configuration;
using WatchIt.Website.Services.Utility.Tokens;
using WatchIt.Website.Services.WebAPI.Accounts;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Movies;
using WatchIt.Website.Services.WebAPI.Photos;
using WatchIt.Website.Services.WebAPI.Series;

namespace WatchIt.Website;

public static class Program
{
    #region PUBLIC METHODS
    
    public static void Main(string[] args)
    {
        WebApplication app = WebApplication.CreateBuilder(args)
                                           .SetupServices()
                                           .SetupAuthentication()
                                           .SetupApplication()
                                           .Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
           .AddInteractiveServerRenderMode();

        app.Run();
    }
    
    #endregion
    
    
    
    #region PRIVATE METHODS
    
    private static WebApplicationBuilder SetupServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddBlazorise(options =>
                         {
                             options.Immediate = true;
                         })
                         .AddBootstrap5Providers()
                         .AddFontAwesomeIcons();
        
        // Utility
        builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
        builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
        builder.Services.AddScoped<ITokensService, TokensService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        // WebAPI
        builder.Services.AddScoped<IAccountsWebAPIService, AccountsWebAPIService>();
        builder.Services.AddSingleton<IMediaWebAPIService, MediaWebAPIService>();
        builder.Services.AddSingleton<IMoviesWebAPIService, MoviesWebAPIService>();
        builder.Services.AddSingleton<ISeriesWebAPIService, SeriesWebAPIService>();
        builder.Services.AddSingleton<IPhotosWebAPIService, PhotosWebAPIService>();
        
        return builder;
    }
    
    private static WebApplicationBuilder SetupAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>();
        
        return builder;
    }
    
    private static WebApplicationBuilder SetupApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorComponents()
                        .AddInteractiveServerComponents();
        return builder;
    }
    
    #endregion
}