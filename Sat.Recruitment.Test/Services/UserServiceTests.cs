using System.Threading.Tasks;
using Moq;
using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repositories;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new ();

        public UserServiceTests()
        {
            _userRepositoryMock.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(true);
        }

        //valid user success
        //duplicated user scenario

        [Fact]
        public async Task CreateUser_NormalUser_BonusCheck()
        {
            var sut = new UserService(_userRepositoryMock.Object);
            var result1 = await sut.CreateUser(NormalUserAbove100Money());
            _userRepositoryMock.Verify(x => x.AddUser(It.Is<User>( y => y.Money == 168m)));

            var result2 = await sut.CreateUser(NormalUserBelow100Money());
            _userRepositoryMock.Verify(x => x.AddUser(It.Is<User>( y => y.Money == 162m)));
        }

        [Fact]
        public async Task CreateUser_SuperUser_BonusCheck()
        {
            var sut = new UserService(_userRepositoryMock.Object);
            var result1 = await sut.CreateUser(SuperUserUserAbove100Money());
            _userRepositoryMock.Verify(x => x.AddUser(It.Is<User>(y => y.Money == 180m)));

            var result2 = await sut.CreateUser(SuperUserlUserBelow100Money());
            _userRepositoryMock.Verify(x => x.AddUser(It.Is<User>(y => y.Money == 90m)));
        }

        [Fact]
        public async Task CreateUser_PremiumUser_BonusCheck()
        {
            var sut = new UserService(_userRepositoryMock.Object);
            var result1 = await sut.CreateUser(PremiumUserUserAbove100Money());
            _userRepositoryMock.Verify(x => x.AddUser(It.Is<User>(y => y.Money == 450)));

            var result2 = await sut.CreateUser(PremiumUserlUserBelow100Money());
            _userRepositoryMock.Verify(x => x.AddUser(It.Is<User>(y => y.Money == 90m)));
        }

        [Fact]
        public async Task CreateUser_EmailNormalization_Check()
        {
            var sut = new UserService(_userRepositoryMock.Object);
            var result = await sut.CreateUser(NotNormalizedEmailUser());
            _userRepositoryMock.Verify(x => x.AddUser(It.Is<User>(y => y.Email.Equals("user@mail.com"))));
        }

        private User NormalUserAbove100Money()
        {
            return new User()
            {
                Name = "Test Name",
                Email = "user@mail.com",
                UserType = UserType.Normal,
                Money = 150m,
                Phone = "+1 5555",
                Address = "Test Address",
            };
        }

        private User NormalUserBelow100Money()
        {
            return new User()
            {
                Name = "Test Name",
                Email = "user@mail.com",
                UserType = UserType.Normal,
                Money = 90m,
                Phone = "+1 5555",
                Address = "Test Address",
            };
        }

        private User SuperUserUserAbove100Money()
        {
            return new User()
            {
                Name = "Test Name",
                Email = "user@mail.com",
                UserType = UserType.SuperUser,
                Money = 150m,
                Phone = "+1 5555",
                Address = "Test Address",
            };
        }

        private User SuperUserlUserBelow100Money()
        {
            return new User()
            {
                Name = "Test Name",
                Email = "user@mail.com",
                UserType = UserType.SuperUser,
                Money = 90m,
                Phone = "+1 5555",
                Address = "Test Address",
            };
        }

        private User PremiumUserUserAbove100Money()
        {
            return new User()
            {
                Name = "Test Name",
                Email = "user@mail.com",
                UserType = UserType.Premium,
                Money = 150m,
                Phone = "+1 5555",
                Address = "Test Address",
            };
        }

        private User PremiumUserlUserBelow100Money()
        {
            return new User()
            {
                Name = "Test Name",
                Email = "user@mail.com",
                UserType = UserType.Premium,
                Money = 90m,
                Phone = "+1 5555",
                Address = "Test Address",
            };
        }

        private User NotNormalizedEmailUser()
        {
            return new User()
            {
                Name = "Test Name",
                Email = "user+some_text@mail.com",
                UserType = UserType.Premium,
                Money = 90m,
                Phone = "+1 5555",
                Address = "Test Address",
            };
        }
    }
}
