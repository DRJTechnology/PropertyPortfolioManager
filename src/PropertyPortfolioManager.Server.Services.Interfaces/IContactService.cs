using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<ContactBasicResponseModel>> GetAll(int portfolioId, bool activeOnly);
        Task<ContactResponseModel> GetById(int contactId, int portfolioId);
        Task<int> Create(int currentUserId, int portfolioId, ContactEditModel contact);
        Task<bool> Update(int currentUserId, int portfolioId, ContactEditModel contact);
    }
}
