using Sat.Recruitment.Api.Models;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Controllers;

namespace Sat.Recruitment.Api.Services
{
    public interface IUserService
    {
        public Task<Result> CreateUser(User user);
    }
}
