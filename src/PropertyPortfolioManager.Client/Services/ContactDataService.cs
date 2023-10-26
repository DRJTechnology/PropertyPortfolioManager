using PropertyPortfolioManager.Client.Interfaces;

namespace PropertyPortfolioManager.Client.Services
{
    public class ContactDataService : GenericDataService,  IContactDataService
    {
        public ContactDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "Contact";
        }
    }
}
