using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MultiContactManagement.Application.Interfaces;
using MultiContactManagement.Application.InterfacesExternal;
using MultiContactManagement.Application.Mappings;
using MultiContactManagement.Application.Services;
using MultiContactManagement.Application.ServicesExternal;
using MultiContactManagement.Domain.Account;
using MultiContactManagement.Domain.Interfaces;
using MultiContactManagement.Domain.InterfacesExternal;
using MultiContactManagement.Infra.Context;
using MultiContactManagement.Infra.Identity;
using MultiContactManagement.Infra.Repositories;
using MultiContactManagement.Infra.RepositoriesExternal;
using System.Text;

namespace MultiContactManagement.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 26)));
            });

            TestDatabaseConnection(configuration);


            // JWT
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,           // Validate who is generating the token
                    ValidateAudience = true,         // Validate the recipient
                    ValidateLifetime = true,         // Token validation time
                    ValidateIssuerSigningKey = true, // Validate Login

                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["jwt:secretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // AutoMapper
            services.AddAutoMapper(typeof(DomainToDTOMappingsProfile));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            return services;
        }

        private static void TestDatabaseConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using var dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)))
                .Options);

            try
            {
                dbContext.Database.OpenConnection();
                dbContext.Database.CloseConnection();
                Console.WriteLine("Database connection established successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
        }
    }
}
