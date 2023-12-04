using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface ITenancyDataService : IGenericDataService
    {
        Task<ContactResponseModel> AddContact(TenancyContactModel tenancyContact);
        Task<bool> RemoveContact(TenancyContactModel tenancyContact);
    }
}
