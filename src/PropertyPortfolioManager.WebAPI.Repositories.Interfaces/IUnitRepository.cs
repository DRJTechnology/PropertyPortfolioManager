using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebAPI.Repositories.Interfaces
{
    public interface IUnitRepository
    {
        Task<List<UnitDto>> GetAll();
        Task<UnitDto> GetById(int id);
        Task<int> Create(int userId, UnitDto newUnit);
        Task<bool> Update(int userId, UnitDto existingUnit);
    }
}
