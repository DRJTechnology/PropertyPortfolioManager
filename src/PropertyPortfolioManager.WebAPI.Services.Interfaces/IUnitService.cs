using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebAPI.Services.Interfaces
{
    public interface IUnitService
    {
        Task<List<UnitResponseModel>> GetAll();
        Task<UnitResponseModel> GetById(int unitId);
        Task<int> Create(int currentUserId, UnitEditModel unit);
        Task<bool> Update(int currentUserId, UnitEditModel unit);
        Task<bool> Delete(int currentUserId, int unitId);
    }
}
