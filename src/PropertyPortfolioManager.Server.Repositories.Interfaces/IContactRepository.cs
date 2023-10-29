using PropertyPortfolioManager.Models.Dto.General;

namespace PropertyPortfolioManager.Server.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<List<ContactBasicDto>> GetAll(int portfolioId, bool activeOnly);
        Task<ContactDto> GetById(int id, int portfolioId);
        Task<int> Create(int userId, int portfolioId, ContactDto newContact);
        Task<bool> Update(int userId, int portfolioId, ContactDto existingContact);
    }
}
