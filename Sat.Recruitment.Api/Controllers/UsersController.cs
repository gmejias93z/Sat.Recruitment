using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly IValidator<UserDto> _userValidator;
        private readonly IUserService _userService;

        public UsersController(IValidator<UserDto> userValidator, IUserService userService)
        {
            _userValidator = userValidator;
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {

            var userDto = new UserDto()
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = money,
            };

            ValidationResult validationResult = await _userValidator.ValidateAsync(userDto);
            
            if (!validationResult.IsValid)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = string.Join('|', validationResult.Errors.Select(x => x.ErrorMessage).ToArray())
                };

            }

            var newUser = userDto.Adapt<User>();
            var result = await _userService.CreateUser(newUser);
            return result;
        }
    }
}
