using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("angularVSUrl", policy =>
    {
        policy.WithOrigins("https://localhost:5002")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});



// Step 1 Agregar los servicios de Jwt Authentication 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5000";
        options.Audience = "identityApi"; // Si el audience es incorrecto no acepta el token aun y cuando el token tenga el scope correcto.
                                         // una cosa es que el token que se solicita este correcto y otra que sea valido para esta api. 

        // Con este ultimo punto ya tenemos configurado la autorización (Error 401) pero aun no tenemos el Token 
        // cuando la petición ya con el token entonces si obtengo los datos
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" }; // aun sin este dato obtengo los datos

    });
// Add services to the container..

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdentityScope", policy =>
    {
        // socpeName valida mayusculas y minusculas Genera error 403 
        policy.RequireClaim("scope", "identity.api", "aveOrders.Purchases");
        policy.RequireAuthenticatedUser();
        //policy.Requirements.Add(new MinimumAgeRequirement());
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("angularVSUrl");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization("IdentityScope");

app.Run();
