using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Mapping
{
    public static class DtoToDomainMapper
    {
        public static User ToUser(this UserDto userDto)
        {
            return new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = userDto.UserType,
                Money = decimal.Parse(userDto.Money),
            };
        }
    }
}
