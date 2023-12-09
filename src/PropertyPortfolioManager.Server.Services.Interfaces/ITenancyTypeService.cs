using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface ITenancyTypeService
    {
        Task<List<EntityTypeModel>> GetAll(int portfolioId, bool activeOnly);
        Task<EntityTypeModel> GetById(int unitId, int portfolioId);
        Task<int> Create(int currentUserId, int portfolioId, EntityTypeModel tenancyType);
        Task<bool> Update(int currentUserId, int portfolioId, EntityTypeModel tenancyType);
        Task<bool> Delete(int currentUserId, int portfolioId, int unitTypeId);
    }
}
