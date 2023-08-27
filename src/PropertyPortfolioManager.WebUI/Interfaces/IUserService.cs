using PropertyPortfolioManager.Models.Dto.Profile;

namespace PropertyPortfolioManager.WebUI.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByObjectIdentifier(Guid objectIdentifier);
        Task<UserDto> GetCurrent();
    }
}
