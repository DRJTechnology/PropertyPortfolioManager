using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebAPI.Services.Interfaces
{
    public interface IUnitTypeService
    {
        Task<List<UnitTypeModel>> GetAll(int portfolioId);
        Task<UnitTypeModel> GetById(int unitId, int portfolioId);
        Task<int> Create(int currentUserId, int portfolioId, UnitTypeModel unit);
        Task<bool> Update(int currentUserId, int portfolioId, UnitTypeModel unit);
    }
}
