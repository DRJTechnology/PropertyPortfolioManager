using PropertyPortfolioManager.Client.Interfaces;

namespace PropertyPortfolioManager.Client.Services
{
    public class UnitTypeDataService : GenericDataService,  IUnitTypeDataService
    {
        public UnitTypeDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "UnitType";
        }
    }
}
