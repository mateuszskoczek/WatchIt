using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Utility.Configuration;
using WatchIt.Website.Services.WebAPI.Accounts;
using WatchIt.Website.Services.WebAPI.Media;

namespace WatchIt.Website;

public static class Program
{
    #region PUBLIC METHODS
    
    public static void Main(string[] args)
    {
        WebApplication app = WebApplication.CreateBuilder(args)
                                           .SetupServices()
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
        builder.Services.AddHttpClient();
        
        // Utility
        builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
        builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
        
        // WebAPI
        builder.Services.AddSingleton<IAccountsWebAPIService, AccountsWebAPIService>();
        builder.Services.AddSingleton<IMediaWebAPIService, MediaWebAPIService>();
        
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