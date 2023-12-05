using PropertyPortfolioManager.Client.Interfaces;

namespace PropertyPortfolioManager.Client.Services
{
    public class AccountDataService : GenericDataService, IAccountDataService
    {
        public AccountDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "Account";
        }
    }
}
