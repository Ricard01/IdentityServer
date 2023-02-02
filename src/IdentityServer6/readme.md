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

 8. Configurar CORS

 ### Referencias

 [Getting Started with Identity Server Part 1](https://www.youtube.com/watch?v=DUujxWdnl3M&ab_channel=IdentityServer)

 [Getting Started with Identity Server Part 2](https://www.youtube.com/watch?v=qyedQ6RzOHw&t=1500s&ab_channel=IdentityServer)
