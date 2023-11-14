using PropertyPortfolioManager.Client.Interfaces;

namespace PropertyPortfolioManager.Client.Services
{
    public class TenancyTypeDataService : GenericDataService,  ITenancyTypeDataService
    {
        public TenancyTypeDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "TenancyType";
        }
    }
}
