using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
//0 se agrego authoriza al controller.
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1.0 Se Agregan los servicios de Jwt Authentiaciton 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5000";
        options.Audience = "weatherapi";

        // Con este ultimo punto ya tenemos configurado la autorizacion (Error 401) pero aun no tenemos el Token 
        // cuando la peticion ya con el token entonces si obtengo los datos
       // options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" }; // aun sin este dato obtengo los datos

    });

var app = builder.Build();

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
