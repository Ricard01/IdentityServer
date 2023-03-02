using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityServer6.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer6.Infrastructure.Persistence;

public class IdentityServerDbContextInitialiser
{
    private readonly ILogger<IdentityServerDbContextInitialiser> _logger;
    private readonly PersistedGrantDbContext _persistedGrantDbContext;
    private readonly ConfigurationDbContext _configurationDbContext;

    public IdentityServerDbContextInitialiser(ILogger<IdentityServerDbContextInitialiser> logger, ConfigurationDbContext configurationDbContext, PersistedGrantDbContext persistedGrantDbContext)
    {
        _logger = logger;
        _configurationDbContext = configurationDbContext;
        _persistedGrantDbContext = persistedGrantDbContext;
    }

    public async Task InitialiseConfigurationDb()
    {
        try
        {
            await _configurationDbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrio un error al inicializar ConfigurationDbContext");
            throw;
        }
    }

    public async Task InitialisePersistedGrantDb()
    {
        try
        {
            await _persistedGrantDbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrion un error al inicializar PersistedGrantDbContext");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedConfigurationDbIdentityServer();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrio un error al agregar la configuracion de Identity Server");
            throw;
        }
    }

    private async Task TrySeedConfigurationDbIdentityServer()
    {

        if (!await _configurationDbContext.Clients.AnyAsync())
        {
            foreach (var client in ConfigIDS.Clients)
            {
              await  _configurationDbContext.Clients.AddAsync(client.ToEntity());

            }
            await _configurationDbContext.SaveChangesAsync();
        }

        if (!await _configurationDbContext.ApiScopes.AnyAsync())
        {
            foreach (var scope in ConfigIDS.ApiScopes)
            {
             await   _configurationDbContext.ApiScopes.AddAsync(scope.ToEntity());

            }

            await _configurationDbContext.SaveChangesAsync();
        }

        if (!await _configurationDbContext.IdentityResources.AnyAsync())
        {
            foreach (var resource in ConfigIDS.IdentityResources)
            {
              await  _configurationDbContext.IdentityResources.AddAsync(resource.ToEntity());
            }
            await _configurationDbContext.SaveChangesAsync();
        }

        if (!await _configurationDbContext.ApiResources.AnyAsync())
        {
            foreach (var resource in ConfigIDS.ApiResources)
            {
              await  _configurationDbContext.ApiResources.AddAsync(resource.ToEntity());
            }

            await _configurationDbContext.SaveChangesAsync();
        }


    }
}
