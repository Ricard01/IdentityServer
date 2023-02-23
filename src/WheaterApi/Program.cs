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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Step 1 Agregar los servicios de Jwt Authentication 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5000";
        options.Audience = "weatherapi"; // Si el audience es incorrecto no acepta el token aun y cuando el token tenga el scope correcto.
                                         // una cosa es que el token que se solicita este correcto y otra que sea valido para esta api. 

        // Con este ultimo punto ya tenemos configurado la autorización (Error 401) pero aun no tenemos el Token 
        // cuando la petición ya con el token entonces si obtengo los datos
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" }; // aun sin este dato obtengo los datos

    });

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

app.MapControllers();

app.Run();
