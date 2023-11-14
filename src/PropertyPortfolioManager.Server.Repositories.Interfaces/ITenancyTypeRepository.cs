using PropertyPortfolioManager.Models.Dto.Property;

namespace PropertyPortfolioManager.Server.Repositories.Interfaces
{
    public interface ITenancyTypeRepository
    {
        Task<List<EntityTypeDto>> GetAll(int portfolioId, bool activeOnly);
        Task<EntityTypeDto> GetById(int id, int portfolioId);
        Task<int> Create(int userId, int portfolioId, EntityTypeDto newTenancyType);
        Task<bool> Update(int userId, int portfolioId, EntityTypeDto existingTenancyType);
        Task<bool> Delete(int userId, int portfolioId, int tenancyTypeId);
    }
}
