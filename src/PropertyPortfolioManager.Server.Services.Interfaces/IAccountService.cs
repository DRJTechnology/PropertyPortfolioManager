using PropertyPortfolioManager.Models.Model.Finance;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountResponseModel>> GetAll(int portfolioId, bool activeOnly);
        Task<AccountResponseModel> GetById(int accountId, int portfolioId);
        Task<int> Create(int currentUserId, int portfolioId, AccountEditModel account);
        Task<bool> Update(int currentUserId, int portfolioId, AccountEditModel account);
        Task<bool> Delete(int currentUserId, int portfolioId, int accountId);
    }
}
