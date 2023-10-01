using PropertyPortfolioManager.Models.Dto.Property;

namespace PropertyPortfolioManager.WebAPI.Repositories.Interfaces
{
    public interface IUnitTypeRepository
    {
        Task<List<UnitTypeDto>> GetAll();
        Task<UnitTypeDto> GetById(int id);
        Task<int> Create(int userId, UnitTypeDto newUnitType);
        Task<bool> Update(int userId, UnitTypeDto existingUnitType);
    }
}
