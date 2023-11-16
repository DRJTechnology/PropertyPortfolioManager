using PropertyPortfolioManager.Client.Interfaces;

namespace PropertyPortfolioManager.Client.Services
{
    public class TenancyDataService : GenericDataService, ITenancyDataService
    {
        public TenancyDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "Tenancy";
        }
    }
}
