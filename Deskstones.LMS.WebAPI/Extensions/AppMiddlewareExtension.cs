namespace Deskstones.LMS.WebAPI.Extensions
{
    using Microsoft.AspNetCore.Diagnostics;
    using Software.DataContracts;
    using Software.DataContracts.Shared;
    using System.Text.Json;

    public static class AppMiddlewareExtension
    {
        public static void ConfigureMiddleware(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();  // Enables Swagger for development environment
                app.UseSwaggerUI();  // Displays Swagger UI for API documentation
            }


            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();  // Redirects HTTP requests to HTTPS


            app.UseRouting();  // Enables routing middleware (this is actually already included by default)

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();  // Maps controller routes to their respective endpoints
            SetupProduction(app);
            EnableLandingPage(app);
            EnableCustomException(app);
        }

        private static void SetupProduction(WebApplication app)
        {
            // Get the port from the environment variable or default to 5000
            app.UseHealthChecks("/health");

        }

        private static void EnableLandingPage(WebApplication app)
        {
            // Serve static files from wwwroot folder
            app.UseStaticFiles();

            // Serve default files like index.html
            app.UseDefaultFiles();

            app.MapGet("/", () => Results.Redirect("/index.html"));
        }

        private static void EnableCustomException(WebApplication app)
        {
            // Global error handling middleware
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (exception != null)
                    {

                        var statusCode = 500;
                        var message = "An unexpected error occurred.";
                        var details = exception?.StackTrace;

                        if (exception is ArgumentException || exception is InvalidOperationException || exception is CustomApiException)
                        {
                            statusCode = 400;
                            message = exception.Message;
                        }
                        else if (exception is UnauthorizedAccessException)
                        {
                            statusCode = 401;
                            message = "Unauthorized access.";
                        }
                        else if (exception is NotImplementedException)
                        {
                            statusCode = 501;
                            message = "This functionality is not implemented.";
                        }
                        else if (exception != null)
                        {
                            message = exception.Message;
                        }

                        context.Response.StatusCode = statusCode;

                        var errorResponse = new ErrorResponse
                        {
                            Message = message,
                            StatusCode = statusCode,
                            //Details = app.Environment.IsDevelopment() ? details : null // Hide details in production
                        };

                        var jsonResponse = JsonSerializer.Serialize(errorResponse);
                        await context.Response.WriteAsync(jsonResponse);
                    }
                });
            });

        }
    }
}
