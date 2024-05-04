using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MultiContactManagement.Ioc
{
    public static class DependencyInjectionSwagger
    {
        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Project Multi Contact Management Web application",
                    Version = "v1",
                    Description = "System developed for the company Alfasoft",
                    Contact = new OpenApiContact
                    {
                        Name = "Rodrigo José de Góis",
                        Email = "rodrigojosedegois@gmail.com.br",
                        Url = new Uri("https://www.linkedin.com/in/rodrigo-jos%C3%A9-de-g%C3%B3is/")
                    }
                });

                var xmlFile = "api-doc.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Simply enter your JWT Bearer token without the double quotes in the box below.",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
    }
}
