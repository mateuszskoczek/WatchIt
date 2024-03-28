using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using WatchIt.Database;
using WatchIt.Shared.Models.Accounts.Register;
using WatchIt.WebAPI.Services.Controllers;
using WatchIt.WebAPI.Services.Utility.Configuration;
using WatchIt.WebAPI.Services.Utility.JWT;
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
            ConfigureWebsite();

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
            _builder.Services.AddValidatorsFromAssembly(Assembly.Load("WatchIt.Shared.Models"));
            
            _builder.Services.AddAuthentication(x =>
                             {
                                 x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                 x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                             })
                             .AddJwtBearer(x =>
                             {
                                 x.RequireHttpsMetadata = false;
                                 x.SaveToken = true;
                                 x.TokenValidationParameters = new TokenValidationParameters
                                 {
                                     ValidateIssuerSigningKey = true,
                                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_builder.Configuration.GetValue<string>("WebAPI:Authentication:Key"))),
                                     ValidateAudience = true,
                                     ValidAudience = "access",
                                     ValidIssuer = _builder.Configuration.GetValue<string>("WebAPI:Authentication:Issuer"),
                                     ValidateLifetime = true,
                                     ClockSkew = TimeSpan.FromMinutes(1),
                                 };
                                 x.Events = new JwtBearerEvents
                                 {
                                     OnAuthenticationFailed = context =>
                                     {
                                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                         {
                                             context.Response.Headers.Add("Token-Expired", "true");
                                         }
                                         return Task.CompletedTask;
                                     }
                                 };
                             })
                             .AddJwtBearer("refresh", x =>
                             {
                                 x.RequireHttpsMetadata = false;
                                 x.SaveToken = true;
                                 x.TokenValidationParameters = new TokenValidationParameters
                                 {
                                     ValidateIssuerSigningKey = true,
                                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_builder.Configuration.GetValue<string>("WebAPI:Authentication:Key"))),
                                     ValidateAudience = true,
                                     ValidIssuer = _builder.Configuration.GetValue<string>("WebAPI:Authentication:Issuer"),
                                     ValidAudience = "refresh",
                                     ValidateLifetime = true,
                                     ClockSkew = TimeSpan.FromMinutes(1)
                                 };
                                 x.Events = new JwtBearerEvents
                                 {
                                     OnAuthenticationFailed = context =>
                                     {
                                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                         {
                                             context.Response.Headers.Add("Token-Expired", "true");
                                         }
                                         return Task.CompletedTask;
                                     }
                                 };
                             });
            _builder.Services.AddAuthorization();

            _builder.Services.AddHttpContextAccessor();
            
            _builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            _builder.Services.AddSingleton<IJWTService, JWTService>();

            _builder.Services.AddSingleton<IAccountsControllerService, AccountsControllerService>();

            _builder.Services.AddFluentValidationAutoValidation();
            _builder.Services.AddEndpointsApiExplorer();
            _builder.Services.AddControllers();
            _builder.Services.AddSwaggerGen();
        }

        protected static void ConfigureWebsite()
        {
            _builder.Services.AddRazorComponents()
                             .AddInteractiveServerComponents();
        }

        #endregion
    }
}
