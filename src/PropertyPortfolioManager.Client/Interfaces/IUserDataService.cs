using PropertyPortfolioManager.Models.Dto.Profile;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IUserDataService : IGenericDataService
    {
        Task<UserDto> GetCurrentAsync();
    }
}
