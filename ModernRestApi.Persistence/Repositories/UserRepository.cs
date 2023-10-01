using Microsoft.EntityFrameworkCore;
using ModernRestApi.Application.Repositories;
using ModernRestApi.Domain.Entities;
using ModernRestApi.Persistence.Context;

namespace ModernRestApi.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApiContext context) : base(context)
        {

        }

        public Task<User> GetByName(string name, CancellationToken cancellationToken)
        {
            return Context.Users.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
        }
    }
}
