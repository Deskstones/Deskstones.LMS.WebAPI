namespace Deskstones.LMS.Domain.DI
{
    using Deskstones.LMS.Domain.Interface;
    using Deskstones.LMS.Domain.Services;
    using FileManager.Client;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddSingleton<ITokenService, TokenService>();

            services.AddSingleton<IFileUploader>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                string endpoint = configuration["Minio:Endpoint"]!;
                string accessKey = configuration["Minio:AccessKey"]!;
                string secretKey = configuration["Minio:SecretKey"]!;

                return new FileUploader(endpoint, accessKey, secretKey);
            });

        }
    }
}
