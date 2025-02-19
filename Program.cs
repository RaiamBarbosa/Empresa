using Empresa.Configuration;
using Empresa.Data;
using Empresa.Services;
using Hangfire.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<TokenGenerator>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtConfig"));

builder
    .IdentityConfig()
    .JwtConfig()
    .SwaggerConfig();
    

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
   