using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebUI.Interfaces
{
    public interface IUnitService
    {
        Task<List<UnitBasicResponseModel>> GetAll(bool activeOnly);
        Task<UnitResponseModel> GetById(int unitId);
        Task<int> Create(UnitEditModel unit);
        Task<bool> Update(UnitEditModel unit);
    }
}
