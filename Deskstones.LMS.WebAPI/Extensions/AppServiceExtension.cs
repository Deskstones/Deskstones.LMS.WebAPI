using Deskstones.LMS.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Software.Helper.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Deskstones.LMS.WebAPI.Extensions
{
    public static class AppServiceExtension
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {

            //Enable production
            SetupProduction(builder);

            // Add services
            ConfigureAppSettings(builder);

            //add html page
            EnableLandingPage(builder);

            ConfigureDatabase(builder);
            ConfigureAuth(builder);
            // Add standard services to the container.
            builder.Services.AddEndpointsApiExplorer();  // Adds support for exploring endpoints
            AddSwaggerGenWithAuthentication(builder);  // Adds Swagger/OpenAPI documentation
            builder.Services.AddControllers();  // Adds MVC controllers to the services container
            builder.Services.RegisterAllDI();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

        }

        private static void ConfigureDatabase(WebApplicationBuilder builder)
        {
            // Fetch connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Database connection string is missing.");
            }

            builder.Services.AddDbContext<RailwayContext>(options =>
            options.UseNpgsql(connectionString));
        }

        private static void ConfigureAuth(WebApplicationBuilder builder)
        {
            var jwtSecret = builder.Configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret cannot be null or empty."); ;
            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                };
            });

            // Add Authorization
            builder.Services.AddAuthorization();

        }

        private static void ConfigureAppSettings(WebApplicationBuilder builder)
        {
            // List of environment variables to override appsettings
            var environmentVariables = new[]
            {
                "CONNECTIONSTRINGS_DEFAULTCONNECTION",
                "JWT_SECRET",
                "JWT_ISSUER",
                "JWT_AUDIENCE"
            };

            foreach (var envVar in environmentVariables)
            {
                // Get the environment variable value
                var envValue = Environment.GetEnvironmentVariable(envVar);
                if (!string.IsNullOrEmpty(envValue))
                {
                    // Transform the environment variable key to match the appsettings key format
                    // Replace "_" with ":" and convert to lowercase (for case-insensitive matching)
                    var configKey = envVar.Replace('_', ':').ToLowerInvariant();

                    // Check if the key exists in the configuration
                    if (builder.Configuration.GetSection(configKey).Exists())
                    {
                        // Override the corresponding configuration value
                        builder.Configuration[configKey] = envValue;
                    }
                    else
                    {
                        // Optionally handle the case where the key doesn't exist
                        Console.WriteLine($"Key {configKey} does not exist in configuration.");
                    }
                }
            }
        }

        private static void SetupProduction(WebApplicationBuilder builder)
        {
            // Get the port from the environment variable or default to 5000
            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            //builder.WebHost.UseUrls($"http://*:{port}");
            var environment = builder.Environment.EnvironmentName; // Get environment name
            if (environment == "Development")
            {
                // Use localhost for development
                builder.WebHost.UseUrls($"http://localhost:{port}");
            }
            else
            {
                // Bind to all interfaces in production
                builder.WebHost.UseUrls($"http://*:{port}");
            }
            builder.Services.AddHealthChecks();

        }

        private static void EnableLandingPage(WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
        }

        public static void AddSwaggerGenWithAuthentication(WebApplicationBuilder builder)
        {

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Deskstones LMS API",
                    Version = "v1",
                    Description = "This API powers the Deskstones Learning Management System. It supports user authentication, course management, and admin features.",
                    Contact = new OpenApiContact
                    {
                        Name = "Deskstones Support",
                        Email = "support@deskstones.com",
                        Url = new Uri("https://deskstones.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token like this: **Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...**"
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

        }
    }
}
