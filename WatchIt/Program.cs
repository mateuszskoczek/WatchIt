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

            ConfigureLogging();
            ConfigureDatabase();
            ConfigureWebAPI();

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
            else
            {
                app.UseSwagger(x =>
                {
                    x.RouteTemplate = "api/swagger/{documentname}/swagger.json";
                });
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/api/swagger/v1/swagger.json", "WatchIt API");
                    x.RoutePrefix = "api/swagger";
                });
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapRazorComponents<App>()
               .AddInteractiveServerRenderMode();

            app.Run();
        }

        #endregion



        #region PRIVATE METHODS

        protected static void ConfigureLogging()
        {
            _builder.Logging.ClearProviders();
            _builder.Logging.AddConsole();
        }

        protected static void ConfigureDatabase()
        {
            _builder.Services.AddDbContext<DatabaseContext>(x => x.UseNpgsql(_builder.Configuration.GetConnectionString("Default")), ServiceLifetime.Singleton);
        }

        protected static void ConfigureWebAPI()
        {
            _builder.Services.AddControllers();
            _builder.Services.AddEndpointsApiExplorer();
            _builder.Services.AddSwaggerGen();
        }

        #endregion
    }
}
