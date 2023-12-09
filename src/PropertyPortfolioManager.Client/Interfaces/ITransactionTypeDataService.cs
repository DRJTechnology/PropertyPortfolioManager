using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface ITransactionTypeDataService
    {
        Task<IEnumerable<EntityTypeBasicModel>> GetAllAsync();
    }
}
