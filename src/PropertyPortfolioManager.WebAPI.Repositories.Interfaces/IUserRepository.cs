using PropertyPortfolioManager.Models.Dto.Profile;

namespace PropertyPortfolioManager.WebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> GetByObjectIdentifier(string userObjectIdentifier);
        Task<UserDto> GetByObjectIdentifier(Guid userObjectIdentifier);
        Task<int> Create(UserDto user);
    }
}
