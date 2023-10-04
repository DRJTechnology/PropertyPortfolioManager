using PropertyPortfolioManager.Models.Model.General;

namespace PropertyPortfolioManager.WebUI.Interfaces
{
    public interface IContactTypeService
    {
        Task<List<ContactTypeModel>> GetAll();
        Task<ContactTypeModel> GetById(int contactTypeId);
        Task<int> Create(ContactTypeModel contactType);
        Task<bool> Update(ContactTypeModel contactType);
    }
}
