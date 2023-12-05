using PropertyPortfolioManager.Client.Interfaces;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Services
{
    public class AccountTypeDataService : IAccountTypeDataService
    {
        protected HttpClient httpClient { get; }

        public AccountTypeDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<AccountTypeResponseModel>> GetAllAsync<AccountTypeResponseModel>()
        {
            try
            {
                var returnVal = await httpClient.GetFromJsonAsync<IEnumerable<AccountTypeResponseModel>>($"api/AccountType/GetAll");
                return returnVal;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
