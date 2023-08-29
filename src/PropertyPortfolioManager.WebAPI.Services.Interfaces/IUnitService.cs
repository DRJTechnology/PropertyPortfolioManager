using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.WebAPI.Services.Interfaces
{
    public interface IUnitService
    {
        Task<List<UnitResponseModel>> GetAll();
        Task<UnitResponseModel> GetById(int unitId);
        Task<int> Create(int currentUserId, UnitRequestModel unit);
        Task<int> Update(int currentUserId, UnitRequestModel unit);
        Task<bool> Delete(int currentUserId, int unitId);
    }
}
