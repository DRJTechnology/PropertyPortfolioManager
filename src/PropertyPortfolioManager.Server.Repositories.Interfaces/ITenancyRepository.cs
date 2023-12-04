using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Server.Repositories.Interfaces
{
    public interface ITenancyRepository
    {
        Task<List<TenancyDto>> GetAll(int portfolioId, bool activeOnly);
        Task<TenancyDto> GetById(int id, int portfolioId);
        Task<int> Create(int userId, int portfolioId, TenancyDto newTenancy);
        Task<bool> Update(int userId, int portfolioId, TenancyDto existingTenancy);
        Task<bool> Delete(int userId, int portfolioId, int tenancyId);
        Task<bool> RemoveContact(int userId, int portfolioId, TenancyContactDto tenancyContact);
        Task<int> AddContact(int userId, int portfolioId, TenancyContactDto tenancyContact);
    }
}
