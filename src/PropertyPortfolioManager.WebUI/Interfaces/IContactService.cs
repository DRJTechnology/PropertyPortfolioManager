using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.WebUI.Interfaces
{
    public interface IContactService
    {
        Task<List<ContactBasicResponseModel>> GetAll(bool activeOnly);
        Task<ContactResponseModel> GetById(int contactId);
        Task<int> Create(ContactEditModel contact);
        Task<bool> Update(ContactEditModel contact);
    }
}
