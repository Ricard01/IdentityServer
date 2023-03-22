using Identity.Domain;
using Identity.Infrastructure.Common.Interfaces;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories.Roles;
using Identity.Infrastructure.Repositories.Users;
using Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("angularVSUrl", policy =>
            {
                policy.WithOrigins("https://localhost:5002")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Identity"), SqlOptionsAction);
        });        

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            
        // Configurations for password, user  or lockout settings 
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        });

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRolRepository, RolRepository>();
         

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            // En otro proyecto usando exactamente el mismo codigo no fue necesario especificar todo esto solo con JwtBearerDefaults.AuthenticationScheme; fue suficiente ni idea porque
            // me fastidie a probar diferentes cosas y nomas no :/ 
        }).AddJwtBearer( options =>
        {
            options.Authority = configuration["IdentityServer"];

            // Error 401 si esta mal 
            options.Audience = "identityApi";
            // it's recommended to check the type header to avoid "JWT confusion" attacks
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidTypes = new[] { "at+jwt" }
            };


        });

        // aud y policy tienen que estar bien para que se autorize el request. 
        services.AddAuthorization(options =>
        {
            options.AddPolicy("IdentityScope", policy =>
            {
              // socpeName valida mayusculas y minusculas, con un scope este bn es suficiente Genera error 403 
                policy.RequireClaim("scope", "identity.api", "aveOrders.Purchases");               
                policy.RequireAuthenticatedUser();
                //policy.Requirements.Add(new MinimumAgeRequirement());
            }
            );
        });

        services.AddAutoMapper(Assembly.GetAssembly(typeof(ApplicationDbContext)));

        return services; 
    }

    private static void SqlOptionsAction(SqlServerDbContextOptionsBuilder sqlServerDbContextOptionsBuilder)
    {
        sqlServerDbContextOptionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
    }
}
