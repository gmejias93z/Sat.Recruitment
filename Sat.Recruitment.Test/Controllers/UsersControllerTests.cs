using FluentValidation;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;
using Moq;
using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Validations;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Controllers
{
    public class UsersControllerTests
    {
        private readonly IValidator<UserDto> _userValidator = new UserValidator();
        private readonly Mock<IUserService> _userServiceMock = new ();

        [Fact]
        [UseCulture("en-US")]
        public async Task CreateUser_ValidRequest_ResultWithSuccess()
        {
            var serviceResult = new Result()
            {
                IsSuccess = true,
                Errors = "User Created",

            };

            _userServiceMock.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(serviceResult);

            var sut = new UsersController(_userValidator, _userServiceMock.Object);
            var result = await sut.CreateUser("Test Name", "user@mail.com", "Test Address", "+1 55555", UserType.Normal, "100");
            result.IsSuccess.ShouldBe(true);
            result.Errors.ShouldBe("User Created");
        }

        [Fact]
        [UseCulture("en-US")]
        public async Task CreateUser_RequestWithNullOrEmptyFields_ResultWithErrors()
        {
            var sut = new UsersController(_userValidator, _userServiceMock.Object);

            // empty values
            var result = await sut.CreateUser(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            result.IsSuccess.ShouldBe(false);
            result.Errors.ShouldBe("'Name' must not be empty." +
                                   "|'Email' must not be empty." +
                                   "|'Address' must not be empty." +
                                   "|'Phone' must not be empty." +
                                   "|'User Type' must not be empty." +
                                   "|'Money' must not be empty.");

            // null values
            result = await sut.CreateUser(null, null, null, null, null, null);
            result.IsSuccess.ShouldBe(false);
            result.Errors.ShouldBe("'Name' must not be empty." +
                                   "|'Email' must not be empty." +
                                   "|'Address' must not be empty." +
                                   "|'Phone' must not be empty." +
                                   "|'User Type' must not be empty." +
                                   "|'Money' must not be empty.");
        }

        [Fact]
        [UseCulture("en-US")]
        public async Task CreateUser_RequestInvalidMail_ResultWithErrors()
        {
            var sut = new UsersController(_userValidator, _userServiceMock.Object);
            var result = await sut.CreateUser("Test Name", "invalid_mail", "Test Address", "+1 55555", UserType.Normal, "100");
            result.IsSuccess.ShouldBe(false);
            result.Errors.ShouldBe("'Email' is not a valid email address.");
        }

        [Fact]
        [UseCulture("en-US")]
        public async Task CreateUser_RequestInvalidPhone_ResultWithErrors()
        {
            var sut = new UsersController(_userValidator, _userServiceMock.Object);
            var result = await sut.CreateUser("Test Name", "user@mail.com", "Test Address", "invalid_phone", UserType.Normal, "100");
            result.IsSuccess.ShouldBe(false);
            result.Errors.ShouldBe("'Phone' is not in the correct format.");
        }

        [Fact]
        [UseCulture("en-US")]
        public async Task CreateUser_RequestInvalidUserType_ResultWithErrors()
        {
            var sut = new UsersController(_userValidator, _userServiceMock.Object);
            var result = await sut.CreateUser("Test Name", "user@mail.com", "Test Address", "+1 55555", "invalid_user_type", "100");
            result.IsSuccess.ShouldBe(false);
            result.Errors.ShouldBe("Invalid UserType.");
        }

        [Fact]
        [UseCulture("en-US")]
        public async Task CreateUser_RequestInvalidMoneyFormat_ResultWithErrors()
        {
            var sut = new UsersController(_userValidator, _userServiceMock.Object);
            var result = await sut.CreateUser("Test Name", "user@mail.com", "Test Address", "+1 55555", UserType.Normal, "invalid_money_format");
            result.IsSuccess.ShouldBe(false);
            result.Errors.ShouldBe("Value for 'Money' is not a valid amount format or is less than 0");
        }

        [Fact]
        [UseCulture("en-US")]
        public async Task CreateUser_RequestMoneyLessThanZero_ResultWithErrors()
        {
            var sut = new UsersController(_userValidator, _userServiceMock.Object);
            var result = await sut.CreateUser("Test Name", "user@mail.com", "Test Address", "+1 55555", UserType.Normal, "-1");
            result.IsSuccess.ShouldBe(false);
            result.Errors.ShouldBe("Value for 'Money' is not a valid amount format or is less than 0");
        }
    }
}
