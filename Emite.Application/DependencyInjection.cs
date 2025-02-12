using Emite.Application.Repository.V1;
using Microsoft.Extensions.DependencyInjection;

namespace Emite.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection EmiteApplication(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
