using PropertyPortfolioManager.Models.Dto.Finance;

namespace PropertyPortfolioManager.Server.Repositories.Interfaces
{
    public interface ITransactionTypeRepository
    {
        Task<List<TransactionTypeDto>> GetAll();
    }
}
