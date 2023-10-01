using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModernRestApi.Application.Repositories;
using ModernRestApi.Persistence.Context;
using ModernRestApi.Persistence.Repositories;

namespace ModernRestApi.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("UserDb"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
