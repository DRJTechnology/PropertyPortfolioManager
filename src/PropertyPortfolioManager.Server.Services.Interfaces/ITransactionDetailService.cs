using PropertyPortfolioManager.Models.Model.Finance;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface ITransactionDetailService
    {
        Task<List<TransactionDetailResponseModel>> GetAsync(int portfolioId, DateTime? fromDate, DateTime? toDate, int accountId, int transactionTypeId);
    }
}
