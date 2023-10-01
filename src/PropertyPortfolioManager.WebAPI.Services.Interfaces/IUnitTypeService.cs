using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebAPI.Services.Interfaces
{
    public interface IUnitTypeService
    {
        Task<List<UnitTypeModel>> GetAll();
        Task<UnitTypeModel> GetById(int unitId);
        Task<int> Create(int currentUserId, UnitTypeModel unit);
        Task<bool> Update(int currentUserId, UnitTypeModel unit);
    }
}
