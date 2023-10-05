using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Helpers;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.Services
{
    public class UnitTypeService : IUnitTypeService
    {
        private readonly ILogger<UnitTypeService> logger;
        private readonly IPpmApiFacade ppmApiFacade;

        public UnitTypeService(ILogger<UnitTypeService> logger, IPpmApiFacade ppmApiFacade)
        {
            this.logger = logger;
            this.ppmApiFacade = ppmApiFacade;
        }

        public async Task<int> Create(UnitTypeModel unitType)
        {
            try
            {
                return await this.ppmApiFacade.PostAsync<UnitTypeModel>(unitType, "UnitType/Create");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UnitTypeModel>> GetAll(bool activeOnly)
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<List<UnitTypeModel>>($"UnitType/GetAll/{activeOnly}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UnitTypeModel> GetById(int unitTypeId)
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<UnitTypeModel>($"UnitType/GetById/{unitTypeId}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update( UnitTypeModel unitType)
        {
            try
            {
                var unitTypeId = await this.ppmApiFacade.PostAsync<UnitTypeModel>(unitType, "UnitType/Update");
                return unitTypeId > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
