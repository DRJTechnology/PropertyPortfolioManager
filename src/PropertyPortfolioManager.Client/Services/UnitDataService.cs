using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Property;

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
