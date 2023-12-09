using PropertyPortfolioManager.Models.Dto.Finance;

namespace PropertyPortfolioManager.Server.Repositories.Interfaces
{
    public interface ITransactionDetailRepository
    {
        Task<List<TransactionDetailDto>> Get(int portfolioId, DateTime? fromDate, DateTime? toDate, int accountId, int transactionTypeId);
    }
}
