using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Property;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Services
{
    public class PortfolioDataService : GenericDataService,  IPortfolioDataService
    {
        public PortfolioDataService(HttpClient httpClient)
            : base(httpClient)
        {
            ApiControllerName = "Portfolio";
        }

        public async Task SelectForCurrentUserAsync(int portfolioId)
        {
            await httpClient.GetFromJsonAsync<PortfolioModel>($"api/Portfolio/SelectForCurrentUser/{portfolioId}");
        }

        public async Task<PortfolioModel> GetCurrentAsync()
        {
            return await httpClient.GetFromJsonAsync<PortfolioModel>($"api/Portfolio/GetCurrent");
        }
    }
}
