using Sat.Recruitment.Api.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> AddUser(User user);
    }
}
