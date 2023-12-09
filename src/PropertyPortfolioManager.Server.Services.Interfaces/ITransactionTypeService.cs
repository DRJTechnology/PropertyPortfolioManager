using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface ITransactionTypeService
    {
        Task<List<EntityTypeBasicModel>> GetAll();
    }
}
