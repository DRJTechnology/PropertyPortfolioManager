using PropertyPortfolioManager.Models.Dto.Finance;

namespace PropertyPortfolioManager.Server.Repositories.Interfaces
{
    public interface IAccountTypeRepository
    {
        Task<List<AccountTypeDto>> GetAll();
    }
}
