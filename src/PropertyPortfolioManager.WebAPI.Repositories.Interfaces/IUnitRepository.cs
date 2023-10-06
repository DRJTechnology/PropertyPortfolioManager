using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebAPI.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        Task<List<UnitBasicDto>> GetAll(int portfolioId, bool activeOnly);
        Task<UnitDto> GetById(int id, int portfolioId);
        Task<int> Create(int userId, int portfolioId, UnitDto newUnit);
        Task<bool> Update(int userId, int portfolioId, UnitDto existingUnit);
    }
}
