using PropertyPortfolioManager.Models.Dto.Property;

namespace PropertyPortfolioManager.WebAPI.Repositories.Interfaces
{
    public interface IUnitTypeRepository
    {
        Task<List<UnitTypeDto>> GetAll(int portfolioId, bool activeOnly);
        Task<UnitTypeDto> GetById(int id, int portfolioId);
        Task<int> Create(int userId, int portfolioId, UnitTypeDto newUnitType);
        Task<bool> Update(int userId, int portfolioId, UnitTypeDto existingUnitType);
    }
}
