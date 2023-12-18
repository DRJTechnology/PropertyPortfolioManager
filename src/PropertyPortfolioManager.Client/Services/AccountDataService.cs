using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Enums;
using PropertyPortfolioManager.Models.Model.Finance;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Services
{
    public class AccountDataService : GenericDataService, IAccountDataService
    {
        public AccountDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "Account";
        }

        public async Task<IEnumerable<AccountResponseModel>> GetByTypeAsync(AccountType accountType, bool ActiveOnly = true)
        {
            try
            {
                var returnVal = await httpClient.GetFromJsonAsync<IEnumerable<AccountResponseModel>>($"api/Account/GetByType/{accountType}/{ActiveOnly}");
                return returnVal;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
