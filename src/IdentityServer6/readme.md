# Orden de Creación y Modificación desde Ceros 

1. En Properties/LaunchSettings.json modificar el puerto 
	> " profiles>IdentityServer6>applicationUrl: <b>https://localhost:5000</b> "
2. Agregar la libreria IdentityServer.
	> " Duende.IdentityServer "

3. En Program agregar el servicio de IdentityServer <see cref="builder.Services.AddIdentityServer"/>
   ```C# 
   builder.Services.AddIdentityServer(options =>
    { 
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    }).AddTestUsers(Config.Users)
        .AddInMemoryClients(Config.Clients)
        .AddInMemoryApiResources(Config.ApiResources)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryIdentityResources(Config.IdentityResources); 
      ```
4. Crear Config File.

5. En Powershell ir a la ruta del folder IdentityServer6 y ejecutar el cmd... (After Tag IdentityServer&Api)
    ```
    iex ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/DuendeSoftware/IdentityServer.Quickstart.UI/main/getmain.ps1'))
    ```

 6. Agregar servicios. 

 7. Crear cliente de Angular

 8. Crear Folder Infrastructure

    1. Modificar appsettings.dev para configurar la cadena de conexion a la bd. 
    2. Agregar los paquetes tools. design. y sql 
    3. Crear la clase ApplicationDbContext 
    4. Crear la clase ConfigureServices donde se registrar los servicios y agregar y configurar el contexto recien creado. 
    5. Registrar el servicio  AddInfrastrucure en Program. 
    6. Agregar la migracion de AspNet Identity 
    7 Agregar   services.AddIdentity<ApplicationUser, IdentityRole>() ..
    8 Agregar migraciones IdentityServer
    9 Agregar ApplicationDbContextInitialiser 
    10 Agregar IdentityDbContextInitialiser
    11 Modificar Program para que inicialise las clases anteriores. 
    12 Modificar Account/login para que tome la informacion de UserManager 
    13 Agregar los servicios que faltan. 
        a.- 

### Migrations

* Install in the root of the repository (if it is not installed)
  * `dotnet new tool-manifest`
  * `dotnet tool install dotnet-ef`
* `dotnet ef migrations add InitialIdentityApplicationDbMigration -c ApplicationDbContext -o Infrastructure/Persistence/Migrations/Identity/ApplicationDb` (for more info add -v)
* `dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext  -o Infrastructure/Persistence/Migrations/IdentityServer/PersistedGrantDb`
* `dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Infrastructure/Persistence/Migrations/IdentityServer/ConfigurationDb`
* `dotnet ef migrations remove -p Infrastructure/ -s WebUI/`


 ### Referencias

 [Getting Started with Identity Server Part 1](https://www.youtube.com/watch?v=DUujxWdnl3M&ab_channel=IdentityServer)

 [Getting Started with Identity Server Part 2](https://www.youtube.com/watch?v=qyedQ6RzOHw&t=1500s&ab_channel=IdentityServer)
