using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Database;
using Sat.Recruitment.Api.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(RecruitmentDbContext context, ILogger<UserRepository> logger): base(context)
        {
            _logger = logger;
        }

        public async Task<bool> AddUser(User user)
        {
            var isDuplicate = await AnyAsync(x =>
                (x.Email == user.Email || x.Phone == user.Phone) || (x.Name == user.Name && x.Address == user.Address));

            if (isDuplicate)
                return false;

            await AddAsync(user);
            await SaveChangesAsync();
            _logger.LogInformation($"Added new User, Id: {user.Id}");
            return true;
        }
    }
}
