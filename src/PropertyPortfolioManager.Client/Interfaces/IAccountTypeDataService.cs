namespace PropertyPortfolioManager.Client.Interfaces
{
    public interface IAccountTypeDataService
    {
        Task<IEnumerable<AccountTypeResponseModel>> GetAllAsync<AccountTypeResponseModel>();
    }
}
