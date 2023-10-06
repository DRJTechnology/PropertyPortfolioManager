using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.WebUI.Helpers;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.Services
{
    public class ContactService : IContactService
    {
        private readonly ILogger<ContactService> logger;
        private readonly IPpmApiFacade ppmApiFacade;

        public ContactService(ILogger<ContactService> logger, IPpmApiFacade ppmApiFacade)
        {
            this.logger = logger;
            this.ppmApiFacade = ppmApiFacade;
        }

        public async Task<int> Create(ContactEditModel contact)
        {
            try
            {
                return await this.ppmApiFacade.PostAsync<ContactEditModel>(contact, "Contact/Create");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ContactBasicResponseModel>> GetAll(bool activeOnly)
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<List<ContactBasicResponseModel>>($"Contact/GetAll/{activeOnly}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ContactResponseModel> GetById(int contactId)
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<ContactResponseModel>($"Contact/GetById/{contactId}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(ContactEditModel contact)
        {
            try
            {
                var contactId = await this.ppmApiFacade.PostAsync<ContactEditModel>(contact, "Contact/Update");
                return contactId > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
