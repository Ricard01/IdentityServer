using Identity.Domain;
using Identity.Infrastructure.Common.Interfaces;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories.Users;
using Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

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
 
        //services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication(options =>
        {
            // Not Found si no se espeifica esta informaccion. 
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
              .AddJwtBearer( options =>
              {
                  options.Authority = configuration["IdentityServer"];

                  // Error 401 si esta mal 
                  options.Audience = "identityApi";
                  // it's recommended to check the type header to avoid "JWT confusion" attacks
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      //ValidIssuer = configuration["IdentityServer"],
                      //ValidAudience = "identityApi",    
                      ValidateActor = true,
                      //ValidateAudience = true, por default realiza la validacion al menos q este parametro este en falso.
                      ValidateIssuerSigningKey = true,
                      ValidTypes = new[] { "at+jwt" }
                  };

                  //options.IncludeErrorDetails = true;
                  //options.Events.OnChallenge = "";

                 // options.RequireHttpsMetadata = false;

              });

        // aud y policy tienen que estar bien para que se autorize el request. 
        services.AddAuthorization(options =>
        {
            options.AddPolicy("IdentityScope", policy =>
            {
              // socpeName valida mayusculas y minusculas Genera error 403 
                policy.RequireClaim("scope", "identity.api", "aveOrders.Purchases");
                policy.RequireAuthenticatedUser();
                //policy.Requirements.Add(new MinimumAgeRequirement());
            });
        });
        //services.AddControllers();
        //services.AddHttpContextAccessor();

        return services; 
    }

    private static void SqlOptionsAction(SqlServerDbContextOptionsBuilder sqlServerDbContextOptionsBuilder)
    {
        sqlServerDbContextOptionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
    }
}
