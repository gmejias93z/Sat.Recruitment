using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        private readonly IValidator<User> _userValidator;
        private readonly IUserService _userService;
        public UsersController(IValidator<User> userValidator, IUserService userService)
        {
            _userValidator = userValidator;
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var newUser = new User
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = decimal.Parse(money)
            };

            ValidationResult validationResult = await _userValidator.ValidateAsync(newUser);
            
            if (!validationResult.IsValid)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = string.Join('|', validationResult.Errors.Select(x => x.ErrorMessage).ToArray())
                };

            }

            var result = await _userService.CreateUser(newUser);
            return result;
        }
    }
}
