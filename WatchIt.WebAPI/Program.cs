using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Delta;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WatchIt.Database;
using WatchIt.DTO;
using WatchIt.DTO.Converters;
using WatchIt.WebAPI.BusinessLogic.Accounts;
using WatchIt.WebAPI.BusinessLogic.Authentication;
using WatchIt.WebAPI.BusinessLogic.Genders;
using WatchIt.WebAPI.BusinessLogic.Genres;
using WatchIt.WebAPI.BusinessLogic.Media;
using WatchIt.WebAPI.BusinessLogic.People;
using WatchIt.WebAPI.BusinessLogic.Photos;
using WatchIt.WebAPI.BusinessLogic.Roles;
using WatchIt.WebAPI.Constants;
using WatchIt.WebAPI.Repositories.Accounts;
using WatchIt.WebAPI.Repositories.Genders;
using WatchIt.WebAPI.Repositories.Genres;
using WatchIt.WebAPI.Repositories.Media;
using WatchIt.WebAPI.Repositories.People;
using WatchIt.WebAPI.Repositories.Photos;
using WatchIt.WebAPI.Repositories.Roles;
using WatchIt.WebAPI.Services.Tokens;
using WatchIt.WebAPI.Services.User;

namespace WatchIt.WebAPI;

public static class Program
{
    #region PUBLIC METHODS
    
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.SetupAuthentication();
        builder.SetupDatabases();
        builder.Services.AddRepositories();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddServices();
        builder.Services.AddBusinessLogic();
        builder.Services.AddWorkers();
        builder.Services.AddFluentValidation();
        builder.Services.AddControllers()
                        .AddJsonOptions(x =>
                        {
                            x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                            x.JsonSerializerOptions.Converters.Add(new ColorJsonConverter());
                        });
        builder.Services.AddOpenApi();
        builder.Services.AddHttpLogging(o => { o.LoggingFields |= HttpLoggingFields.RequestQuery; });

        
        WebApplication app = builder.Build();
        
        app.UseHttpLogging();
        
        app.UseDelta<DatabaseContext>();
        
        app.InitializeDatabase();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
    
    #endregion



    #region PRIVATE METHODS

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IGendersRepository, GendersRepository>();
        services.AddTransient<IGenresRepository, GenresRepository>();
        services.AddTransient<IAccountsRepository, AccountsRepository>();
        services.AddTransient<IMediaRepository, MediaRepository>();
        services.AddTransient<IPeopleRepository, PeopleRepository>();
        services.AddTransient<IPhotosRepository, PhotosRepository>();
        services.AddTransient<IRolesRepository, RolesRepository>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<ITokensService, TokensService>();
        services.AddTransient<IUserService, UserService>();
    }

    private static void AddBusinessLogic(this IServiceCollection services)
    {
        services.AddTransient<IGendersBusinessLogic, GendersBusinessLogic>();
        services.AddTransient<IGenresBusinessLogic, GenresBusinessLogic>();
        services.AddTransient<IAuthenticationBusinessLogic, AuthenticationBusinessLogic>();
        services.AddTransient<IAccountsBusinessLogic, AccountsBusinessLogic>();
        services.AddTransient<IMediaBusinessLogic, MediaBusinessLogic>();
        services.AddTransient<IPeopleBusinessLogic, PeopleBusinessLogic>();
        services.AddTransient<IPhotosBusinessLogic, PhotosBusinessLogic>();
        services.AddTransient<IRolesBusinessLogic, RolesBusinessLogic>();
    }

    private static void AddWorkers(this IServiceCollection services)
    {
        
    }

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(CustomValidators)));
        services.AddFluentValidationAutoValidation();
    }

    private static WebApplicationBuilder SetupAuthentication(this WebApplicationBuilder builder)
    {
        string issuer = builder.Configuration.GetValue<string>("Authentication:JWT:Issuer")!;
        string audience = builder.Configuration.GetValue<string>("Authentication:JWT:Audience")!;
        string key = builder.Configuration.GetValue<string>("Authentication:JWT:Key")!;
        byte[] encodedKey = Encoding.UTF8.GetBytes(key);
        
        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        builder.Services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(x =>
               {
                   x.RequireHttpsMetadata = false;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidIssuer = issuer,
                       ValidAudience = audience,
                       IssuerSigningKey = new SymmetricSecurityKey(encodedKey),
                       ClockSkew = TimeSpan.FromMinutes(1),
                   };
                   x.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                           {
                               context.Response.Headers.Append("Token-Expired", "true");
                           }
                           return Task.CompletedTask;
                       }
                   };
               });
        builder.Services
               .AddAuthorization(options =>
               {
                   options.AddPolicy(Policies.Admin, policy => policy.RequireAuthenticatedUser()
                                                                     .RequireClaim(AdditionalClaimNames.Admin, bool.TrueString));
               });
        
        return builder;
    }

    private static WebApplicationBuilder SetupDatabases(this WebApplicationBuilder builder)
    {
        builder.Services
               .AddDbContext<DatabaseContext>(x => x.UseNpgsql(builder.Configuration
                                                                      .GetConnectionString("Database")),
                                                    ServiceLifetime.Transient);
        return builder;
    }

    private static void InitializeDatabase(this WebApplication app)
    {
        using (IServiceScope scope = app.Services.CreateScope())
        {
            DatabaseContext database = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            while (!database.Database.CanConnect())
            {
                app.Logger.LogInformation("Waiting for database...");
                Thread.Sleep(1000);
            }
            
            database.Database.Migrate();
        }
    }

    #endregion
}