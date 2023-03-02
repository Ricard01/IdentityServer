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


        services.AddIdentityServer()
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(configurationStoreOptions =>
            {
                configurationStoreOptions.ConfigureDbContext = opt => ConfigOptions(configuration, opt);

            }).
            AddOperationalStore(operationalStoreOptions =>
            {
                operationalStoreOptions.ConfigureDbContext = opt => ConfigOptions(configuration, opt);
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
