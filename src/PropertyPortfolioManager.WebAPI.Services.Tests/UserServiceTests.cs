using Moq;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;

namespace PropertyPortfolioManager.WebAPI.Services.Tests
{
    public class UserServiceTests
    {
        private List<UserDto> userList;

        public UserServiceTests()
        {
            this.userList = this.PopulateUserList();
        }

        [Fact]
        public async void Get_User_By_ObjectIdentifier()
        {
            var objectIdentifier = new Guid("00000001-0001-0001-0001-000000000009");
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(r => r.GetByObjectIdentifier(It.IsAny<Guid>()))
                                        .Returns(Task.FromResult(this.GetUser(objectIdentifier)));
            var userService = new UserService(userRepositoryMock.Object, null);

            var user = await userService.GetByObjectIdentifier(objectIdentifier);

            Assert.IsType<UserDto>(user);
            Assert.Equal(9, user.Id);
            Assert.Equal("Test User 9", user.Name);
            Assert.Equal(objectIdentifier, user.ObjectIdentifier);
        }

        private List<UserDto> PopulateUserList()
        {
            var userList = new List<UserDto>();

            for (int i = 1; i < 10; i++)
            {
                userList.Add(
                    new UserDto()
                    {
                        Id = i,
                        Name = $"Test User {i}",
                        ObjectIdentifier = new Guid($"00000001-0001-0001-0001-00000000000{i}"),
                    }
                );
            }
            return userList;
        }


        private UserDto? GetUser(Guid objectIdentifier)
        {
            return this.userList.Where(u => u.ObjectIdentifier == objectIdentifier).FirstOrDefault();
        }

        private UserDto? GetUser(string objectIdentifier)
        {
            var objectIdentifierGuid = new Guid(objectIdentifier);
            return this.userList.Where(u => u.ObjectIdentifier == objectIdentifierGuid).FirstOrDefault();
        }
    }
}