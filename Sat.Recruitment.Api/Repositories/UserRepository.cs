using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Database;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RecruitmentDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(RecruitmentDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddUser(User user)
        {
            var isDuplicate = await _context.Users.AnyAsync(x =>
                (x.Email == user.Email || x.Phone == user.Phone) || (x.Name == user.Name && x.Address == user.Address));

            if (isDuplicate)
                return false;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added new User, Id: {user.Id}");
            return true;
        }
    }
}
