using IdentityServerWebApp;

var builder = WebApplication.CreateBuilder(args);


// 3
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


var app = builder.Build();

app.UseIdentityServer();

app.MapGet("/", () => "Hello World!");

app.Run();

// 1er Modificar puerto LaunchSettings.json
// 2do Agregar la libreria IdentityServer. 