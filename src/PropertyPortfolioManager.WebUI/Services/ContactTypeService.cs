using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.WebUI.Helpers;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.Services
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly ILogger<ContactTypeService> logger;
        private readonly IPpmApiFacade ppmApiFacade;

        public ContactTypeService(ILogger<ContactTypeService> logger, IPpmApiFacade ppmApiFacade)
        {
            this.logger = logger;
            this.ppmApiFacade = ppmApiFacade;
        }

        public async Task<int> Create(ContactTypeModel contactType)
        {
            try
            {
                return await this.ppmApiFacade.PostAsync<ContactTypeModel>(contactType, "ContactType/Create");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ContactTypeModel>> GetAll(bool activeOnly)
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<List<ContactTypeModel>>($"ContactType/GetAll/{activeOnly}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ContactTypeModel> GetById(int contactTypeId)
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<ContactTypeModel>($"ContactType/GetById/{contactTypeId}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(ContactTypeModel contactType)
        {
            try
            {
                var contactTypeId = await this.ppmApiFacade.PostAsync<ContactTypeModel>(contactType, "ContactType/Update");
                return contactTypeId > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
