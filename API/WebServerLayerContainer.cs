using API.Services;
using Application.Abstractions;
using System.Reflection;

namespace API
{
    internal static class WebServerLayerContainer
    {
        public static IServiceCollection AddWebAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(TimeProvider.System);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           
            return services;
        }
    }
}
