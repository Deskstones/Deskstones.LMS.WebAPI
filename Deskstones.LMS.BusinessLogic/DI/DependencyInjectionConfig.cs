namespace Deskstones.LMS.BusinessLogic.DI
{
    using Deskstones.LMS.BusinessLogic.Interface;
    using Microsoft.Extensions.DependencyInjection;
    public static class DependencyInjectionConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Register the depency for ochestrators here
            services.AddScoped<IAuthenticationOrchestrator, AuthenticationOrchestrator>();
            services.AddScoped<IUserProfileOrchestrator, UserProfileOrchestrator>();

        }
    }
}
