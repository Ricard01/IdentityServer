using IdentityServer6;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("angularClientUrl", policy =>
    {
        policy.WithOrigins("https://localhost:5003")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Step 6
builder.Services.AddRazorPages();

// Step 3 
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
app.UseCors("angularClientUrl");
//app.UseHttpsRedirection();
app.UseIdentityServer();

// Step 6
// app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();

app.Run();
 