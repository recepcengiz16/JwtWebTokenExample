using JwtWebTokenExample.Manager.Services;

namespace JwtWebTokenExample.Manager.AppExtensions.DependencyResolves
{
    public static class DependencyExtensions
    {
        public static void AddDependencies(this IServiceCollection service, IConfiguration configuration)
        {            
            service.AddScoped<UserManager>();
        }      

    }
}
