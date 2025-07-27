using Deskstones.LMS.WebAPI.Behaviours;
using Deskstones.LMS.WebAPI.Interface;

namespace Deskstones.LMS.WebAPI.DI
{
    public static class DependencyInjectionConfig
    {
        // Register the Account repository
        public static void AddServices(this IServiceCollection services)
        {
            // Register the dependency used in api
            services.AddScoped<IAuthenticationController, AuthenticationControllerBehaviour>();
            services.AddScoped<IUserProfileController, UserProfileControllerBehaviour>();
            services.AddScoped<ISubjectController, SubjectControllerBehaviour>();


        }
    }
}
