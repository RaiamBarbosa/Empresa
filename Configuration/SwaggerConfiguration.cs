using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Empresa.Configuration
{
    public static class SwaggerConfiguration
    {
        public static WebApplicationBuilder SwaggerConfig (this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(x => 
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);

                x.SwaggerDoc("v1", new OpenApiInfo() 
                {
                    Description = "Api Generator Token",
                    Title = "Generator Token",  
                    Version = "v1"
                });
                x.AddServer(new OpenApiServer
                {
                    Description = "Https",
                    Url = "https://localhost:7059"
                });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                         Array.Empty<string>()
                    }
                });
            });
            return builder;
        }
    }
}
