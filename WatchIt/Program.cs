using Microsoft.EntityFrameworkCore;
using WatchIt.Database;
using WatchIt.Website;

namespace WatchIt
{
    public class Program
    {
        #region FIELDS

        protected static WebApplicationBuilder _builder;

        #endregion



        #region PUBLIC METHODS

        public static void Main(string[] args)
        {
            _builder = WebApplication.CreateBuilder(args);

            // Logging
            _builder.Logging.ClearProviders();
            _builder.Logging.AddConsole();

            // Database
            _builder.Services.AddDbContext<DatabaseContext>(x => x.UseNpgsql(_builder.Configuration.GetConnectionString("Default")), ServiceLifetime.Singleton);

            // Add services to the container.
            _builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = _builder.Build();

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
    }
}
