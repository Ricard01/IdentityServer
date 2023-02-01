# Orden de Creación y Modificación desde Ceros 

1. En Properties/LaunchSettings.json modificar el puerto 
	>> "profiles>IdentityServer6>applicationUrl: "https://localhost:5000"
2. Agregar la libreria IdentityServer.
	>> "Duende.IdentityServer"

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
4. Create Config File