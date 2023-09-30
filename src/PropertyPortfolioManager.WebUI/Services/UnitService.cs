using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Helpers;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.Services
{
    public class UnitService : IUnitService
    {
        private readonly ILogger<UnitService> logger;
        private readonly IPpmApiFacade ppmApiFacade;

        public UnitService(ILogger<UnitService> logger, IPpmApiFacade ppmApiFacade)
        {
            this.logger = logger;
            this.ppmApiFacade = ppmApiFacade;
        }

        public async Task<int> Create(UnitEditModel unit)
        {
            try
            {
                return await this.ppmApiFacade.PostAsync<UnitEditModel>(unit, "Unit/Create");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UnitBasicResponseModel>> GetAll()
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<List<UnitBasicResponseModel>>("Unit/GetAll");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UnitResponseModel> GetById(int unitId)
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<UnitResponseModel>($"Unit/GetById/{unitId}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(UnitEditModel unit)
        {
            try
            {
                var unitId = await this.ppmApiFacade.PostAsync<UnitEditModel>(unit, "Unit/Update");
                return unitId > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
