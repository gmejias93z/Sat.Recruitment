using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Api.Database;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repositories;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> CreateUser(User user)
        {
            var result = new Result();

            user.Money = CalculateBonus(user.Money, user.UserType);
            user.Email = NormalizeEmail(user.Email);

            var isUserAdded = await _userRepository.AddUser(user);

            if (!isUserAdded)
            {
                result.IsSuccess = false;
                result.Errors = "The user is duplicated";
                return result;
            }

            
            result.IsSuccess = true;
            result.Errors = "User Created";
            return result;
        }

        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            email = string.Join("@", new string[] { aux[0], aux[1] });
            return email;
        }

        private decimal CalculateBonus(decimal money, string userType)
        {
            if (userType == UserType.Normal)
            {
                if (money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    var gif = money * percentage;
                    money += gif;
                }
                else if (money > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = money * percentage;
                    money += gif;
                }
            }
            else if (userType == UserType.SuperUser)
            {
                if (money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = money * percentage;
                    money += gif;
                }
            }
            else if (userType == UserType.Premium)
            {
                if (money > 100)
                {
                    var gif = money * 2;
                    money += gif;
                }
            }

            return money;
        }
    }
}
