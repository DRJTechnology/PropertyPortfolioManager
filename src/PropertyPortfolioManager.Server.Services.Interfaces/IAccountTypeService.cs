using PropertyPortfolioManager.Models.Model.Finance;

namespace PropertyPortfolioManager.Server.Services.Interfaces
{
    public interface IAccountTypeService
    {
        Task<List<AccountTypeResponseModel>> GetAll();
    }
}
