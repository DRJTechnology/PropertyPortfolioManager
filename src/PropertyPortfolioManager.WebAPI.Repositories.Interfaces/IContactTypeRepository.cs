using PropertyPortfolioManager.Models.Dto.General;

namespace PropertyPortfolioManager.WebAPI.Repositories.Interfaces
{
    public interface IContactTypeRepository
    {
        Task<List<ContactTypeDto>> GetAll(int portfolioId, bool activeOnly);
        Task<ContactTypeDto> GetById(int id, int portfolioId);
        Task<int> Create(int userId, int portfolioId, ContactTypeDto newContactType);
        Task<bool> Update(int userId, int portfolioId, ContactTypeDto existingContactType);
        Task<bool> Delete(int userId, int portfolioId, int contactTypeId);
    }
}
