using PropertyPortfolioManager.Models.Enums;
using PropertyPortfolioManager.Models.Model.Finance;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IAccountDataService : IGenericDataService
    {
        Task<IEnumerable<AccountResponseModel>> GetByTypeAsync(AccountType accountType, bool ActiveOnly = true);
    }
}
