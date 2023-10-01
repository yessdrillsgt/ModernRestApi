using Microsoft.EntityFrameworkCore;
using ModernRestApi.Domain.Entities;

namespace ModernRestApi.Persistence.Context
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
