using PropertyPortfolioManager.Models.Model.Property;
using System.Security.Claims;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IUnitService
    {
        Task<List<UnitBasicResponseModel>> GetAll(int portfolioId, bool activeOnly);
        Task<UnitResponseModel> GetById(int unitId, int portfolioId);
        Task<int> Create(int currentUserId, int portfolioId, UnitEditModel unit);
        Task<bool> Update(int currentUserId, int portfolioId, UnitEditModel unit);
        Task<bool> Delete(int currentUserId, int portfolioId, int unitId);
    }
}
