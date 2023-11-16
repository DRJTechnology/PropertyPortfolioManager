using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface ITenancyService
    {
        Task<List<TenancyResponseModel>> GetAll(int portfolioId, bool activeOnly);
        Task<TenancyResponseModel> GetById(int unitId, int portfolioId);
        Task<int> Create(int currentUserId, int portfolioId, TenancyEditModel tenancy);
        Task<bool> Update(int currentUserId, int portfolioId, TenancyEditModel tenancy);
        Task<bool> Delete(int currentUserId, int portfolioId, int unitTypeId);
    }
}
