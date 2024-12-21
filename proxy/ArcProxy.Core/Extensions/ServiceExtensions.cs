using ArcProxy.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArcProxy.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IGeoService, GeoService>();

            //services.Configure<JwtOptions>(config.GetSection(JwtOptions.JwtSettings));

            return services;
        }
    }
}
