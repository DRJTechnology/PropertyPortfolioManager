using PropertyPortfolioManager.Models.Dto.Finance;
using PropertyPortfolioManager.Models.Enums;

namespace PropertyPortfolioManager.Server.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<AccountDto>> GetAll(int portfolioId, bool activeOnly);
        Task<List<AccountDto>> GetByType(int portfolioId, AccountType accountType, bool activeOnly);
        Task<AccountDto> GetById(int id, int portfolioId);
        Task<int> Create(int userId, int portfolioId, AccountDto newAccount);
        Task<bool> Update(int userId, int portfolioId, AccountDto existingAccount);
        Task<bool> Delete(int userId, int portfolioId, int accountId);
    }
}
