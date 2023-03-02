using IdentityServer6.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("angularClientUrl", policy =>
//    {
//        policy.WithOrigins("https://localhost:5002")
//        .AllowAnyHeader()
//        .AllowAnyMethod();
//    });
//});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    //TODO: Checar si esta PRO (correcto el uso de await / async ) 

    // AspNet Identity
    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await initializer.InitialiseAsync();
    await initializer.SeedAsync();


    //Identity Server
    var identityServer = scope.ServiceProvider.GetRequiredService<IdentityServerDbContextInitialiser>();
    await identityServer.InitialiseConfigurationDb();
    await identityServer.InitialisePersistedGrantDb();
   
    await identityServer.SeedAsync();

    //await scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.MigrateAsync();


}



//app.UseCors("angularClientUrl");
app.UseHttpsRedirection();
app.UseIdentityServer();

// Step 6
// app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();

app.Run();
 