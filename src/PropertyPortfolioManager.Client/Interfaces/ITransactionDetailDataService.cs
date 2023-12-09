using PropertyPortfolioManager.Models.Model.Finance;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface ITransactionDetailDataService
    {
        Task<IEnumerable<TransactionDetailResponseModel>> GetAsync(DateTime? fromDate, DateTime? toDate, int accountId, int transactionTypeId);
    }
}
