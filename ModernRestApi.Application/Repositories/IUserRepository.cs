using ModernRestApi.Domain.Entities;

namespace ModernRestApi.Application.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByName(string name, CancellationToken cancellationToken);
    }
}
