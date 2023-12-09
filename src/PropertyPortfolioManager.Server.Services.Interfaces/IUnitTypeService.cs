using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IUnitTypeService
    {
        Task<List<EntityTypeModel>> GetAll(int portfolioId, bool activeOnly);
        Task<EntityTypeModel> GetById(int unitId, int portfolioId);
        Task<int> Create(int currentUserId, int portfolioId, EntityTypeModel unit);
        Task<bool> Update(int currentUserId, int portfolioId, EntityTypeModel unit);
        Task<bool> Delete(int currentUserId, int portfolioId, int unitTypeId);
    }
}
