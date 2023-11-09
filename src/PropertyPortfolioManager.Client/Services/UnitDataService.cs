using PropertyPortfolioManager.Client.Interfaces;

namespace PropertyPortfolioManager.Client.Services
{
    public class UnitDataService : GenericDataService,  IUnitDataService
    {
        public UnitDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "Unit";
        }
    }
}
