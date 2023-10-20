using PropertyPortfolioManager.Models.Dto.Property;

namespace PropertyPortfolioManager.WebAPI.Repositories.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<PortfolioDto>> GetAll(int userId, bool activeOnly);
        Task<PortfolioDto> GetById(int id, int userId);
        Task<int> Create(int userId, PortfolioDto newUnitType);
        Task<bool> Update(int userId, PortfolioDto existingUnitType);
        Task<PortfolioDto> GetByUserObjectIdentifier(Guid userObjectIdentifier);
        Task<bool> SelectForUser(int portfolioId, int userId);
        Task<bool> Delete(int userId, int portfolioId);
    }
}
