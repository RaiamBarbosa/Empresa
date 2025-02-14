using Empresa.Configuration;
using Empresa.Data;
using Empresa.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.JwtConfig().SwaggerConfig();

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
