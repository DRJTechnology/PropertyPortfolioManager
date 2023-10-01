using PropertyPortfolioManager.Models.Dto.Property;

namespace PropertyPortfolioManager.WebAPI.Repositories.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<PortfolioDto>> GetAll(int userId);
        Task<PortfolioDto> GetById(int id, int userId);
        Task<int> Create(int userId, PortfolioDto newUnitType);
        Task<bool> Update(int userId, PortfolioDto existingUnitType);
        Task<PortfolioDto> GetByUserObjectIdentifier(Guid userObjectIdentifier);
    }
}
