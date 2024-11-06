using System.Text.Json;
using System.Text.Json.Serialization;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Configuration;
using WatchIt.Website.Services.Tokens;
using WatchIt.Website.Services.Client.Accounts;
using WatchIt.Website.Services.Client.Genders;
using WatchIt.Website.Services.Client.Media;
using WatchIt.Website.Services.Client.Movies;
using WatchIt.Website.Services.Client.Persons;
using WatchIt.Website.Services.Client.Photos;
using WatchIt.Website.Services.Client.Roles;
using WatchIt.Website.Services.Client.Series;

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
        builder.Services.AddScoped<IAccountsClientService, AccountsClientService>();
        builder.Services.AddSingleton<IGendersClientService, GendersClientService>();
        builder.Services.AddSingleton<IMediaClientService, MediaClientService>();
        builder.Services.AddSingleton<IMoviesClientService, MoviesClientService>();
        builder.Services.AddSingleton<ISeriesClientService, SeriesClientService>();
        builder.Services.AddSingleton<IPhotosClientService, PhotosClientService>();
        builder.Services.AddSingleton<IPersonsClientService, PersonsClientService>();
        builder.Services.AddSingleton<IRolesClientService, RolesClientService>();
        
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