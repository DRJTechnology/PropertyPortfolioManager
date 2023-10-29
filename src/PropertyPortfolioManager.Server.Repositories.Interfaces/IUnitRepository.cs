using PropertyPortfolioManager.Models.Dto.Property;

namespace PropertyPortfolioManager.Server.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        Task<List<UnitBasicDto>> GetAll(int portfolioId, bool activeOnly);
        Task<UnitDto> GetById(int id, int portfolioId);
        Task<int> Create(int userId, int portfolioId, UnitDto newUnit);
        Task<bool> Update(int userId, int portfolioId, UnitDto existingUnit);
        Task<bool> Delete(int currentUserId, int portfolioId, int unitId);
    }
}
