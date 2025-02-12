using Emite.Data.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Emite.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection EmiteData(this IServiceCollection services)
        {

            services.AddDbContext<EmiteDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb"));

            services.AddScoped<DbContext, EmiteDbContext>();

            return services;
        }
    }
}
