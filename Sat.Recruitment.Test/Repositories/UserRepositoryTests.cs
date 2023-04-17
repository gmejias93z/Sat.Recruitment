using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Database;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Sat.Recruitment.Test.Repositories
{
    public class UserRepositoryTests
    {
        private readonly Mock<RecruitmentDbContext> _contextMock = new ();
        private readonly Mock<ILogger<UserRepository>> _loggerMock = new ();
        private readonly Mock<UserRepository> _userRepositoryMock;

        public UserRepositoryTests()
        {
            _userRepositoryMock = new Mock<UserRepository>(_contextMock.Object, _loggerMock.Object);
            _userRepositoryMock.CallBase = true;
        }

        [Fact]
        public async Task AddUser_Valid_TrueResult()
        {
            var user = new User
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Phone = "1234567890",
                Address = "123 Main St"
            };

            _userRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(false);
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            var result = await _userRepositoryMock.Object.AddUser(user);

            result.ShouldBe(true);

            _userRepositoryMock.Verify(x => x.AddAsync(user), Times.Once);
            _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            
            _loggerMock.Verify(logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                    It.Is<EventId>(eventId => eventId.Id == 0),
                    It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == $"Added new User, Id: {user.Id}"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task AddUser_Duplicated_FalseResult()
        {
            var user = new User
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Phone = "1234567890",
                Address = "123 Main St"
            };

            _userRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);

            var result = await _userRepositoryMock.Object.AddUser(user);

            result.ShouldBe(false);
        }
    }
}
