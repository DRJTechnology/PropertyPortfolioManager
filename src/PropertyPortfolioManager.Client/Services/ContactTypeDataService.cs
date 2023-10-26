using PropertyPortfolioManager.Client.Interfaces;

namespace PropertyPortfolioManager.Client.Services
{
    public class ContactTypeDataService : GenericDataService,  IContactTypeDataService
    {
        public ContactTypeDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "ContactType";
        }
    }
}
