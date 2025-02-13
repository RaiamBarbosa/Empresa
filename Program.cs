using Empresa.Configuration;
using Empresa.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.JwtConfig().SwaggerConfig();

//Injector//
builder.Services.AddScoped<TokenGenerator>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtConfig"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => 
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Empresa");
        x.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
