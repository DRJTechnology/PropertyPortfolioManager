using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebUI.Interfaces
{
    public interface IUnitTypeService
    {
        Task<List<UnitTypeModel>> GetAll(bool activeOnly);
        Task<UnitTypeModel> GetById(int unitTypeId);
        Task<int> Create(UnitTypeModel unitType);
        Task<bool> Update(UnitTypeModel unitType);
    }
}
