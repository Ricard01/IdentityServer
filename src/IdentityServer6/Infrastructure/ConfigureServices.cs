using IdentityServer6.Infrastructure.Identity;
using IdentityServer6.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        // Registra el contexto especificado como servicio
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Identity"), SqlOptionsAction);
        });

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IdentityServerDbContextInitialiser>();


        // 8.7
        services.AddIdentity<ApplicationUser, IdentityRole>()
    //.AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>() // si no queremos esos claims personalizados no seria necesario 
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


        services.AddIdentityServer( options =>
        {
            //options.Authentication.CookieLifetime = TimeSpan.FromHours(1);
            //options.Authentication.CookieSlidingExpiration = false;
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;

            options.EmitStaticAudienceClaim = true;

        })
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(configurationStoreOptions =>
            {
                configurationStoreOptions.ConfigureDbContext = opt => ConfigOptions(configuration, opt);

            }).
            AddOperationalStore(operationalStoreOptions =>
            {
                operationalStoreOptions.ConfigureDbContext = opt => ConfigOptions(configuration, opt);
            });

        // Configurations for password, user  or lockout settings 
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        });

        services.AddRazorPages();

        return services;


    }


    private static DbContextOptionsBuilder ConfigOptions(IConfiguration configuration, DbContextOptionsBuilder opt)
    {
        return opt.UseSqlServer(configuration.GetConnectionString("Identity"), SqlOptionsAction);
    }


    private static void SqlOptionsAction(SqlServerDbContextOptionsBuilder sqlServerDbContextOptionsBuilder)
    {
        sqlServerDbContextOptionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
    }
}
