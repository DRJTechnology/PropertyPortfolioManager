using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebUI.Helpers;
using PropertyPortfolioManager.WebUI.Interfaces;

namespace PropertyPortfolioManager.WebUI.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly ILogger<UnitService> logger;
        private readonly IPpmApiFacade ppmApiFacade;

        public PortfolioService(ILogger<UnitService> logger, IPpmApiFacade ppmApiFacade)
        {
            this.logger = logger;
            this.ppmApiFacade = ppmApiFacade;
        }

        public async Task<int> Create(PortfolioModel portfolio)
        {
            try
            {
                return await this.ppmApiFacade.PostAsync<PortfolioModel>(portfolio, "Portfolio/Create");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<PortfolioModel>> GetAll(bool activeOnly)
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<List<PortfolioModel>>($"Portfolio/GetAll/{activeOnly}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PortfolioModel> GetById(int portfolioId)
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<PortfolioModel>($"Portfolio/GetById/{portfolioId}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PortfolioModel> GetCurrent()
        {
            try
            {
                return await this.ppmApiFacade.GetAsync<PortfolioModel>($"Portfolio/GetCurrent");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> SelectForCurrentUser(int portfolioId)
        {
            try
            {
                var response = await this.ppmApiFacade.GetAsync<PpmApiResponse>($"Portfolio/SelectForCurrentUser/{portfolioId}");
                return response.Success;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(PortfolioModel portfolio)
        {
            try
            {
                var portfolioId = await this.ppmApiFacade.PostAsync<PortfolioModel>(portfolio, "Portfolio/Update");
                return portfolioId > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
