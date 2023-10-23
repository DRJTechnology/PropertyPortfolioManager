using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.InternalObjects;
using PropertyPortfolioManager.Models.Model.Property;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace PropertyPortfolioManager.Client.Services
{
    public class PortfolioDataService : GenericDataService<PortfolioModel>,  IPortfolioDataService
    {
        //private readonly HttpClient httpClient;

        public PortfolioDataService(HttpClient httpClient)
            : base(httpClient)
        {
        }

        //public async Task<int> Create(PortfolioModel portfolio)
        //{
        //    var response = await httpClient.PostAsJsonAsync<PortfolioModel>("api/Portfolio/Create", portfolio);
        //    if (response == null || !response.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Failed to create portfolio.");
        //    }

        //    var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

        //    return returnValue.CreatedId;
        //}

        //public async Task<bool> DeleteAsync(int portfolioId)
        //{
        //    var response = await httpClient.DeleteAsync($"api/Portfolio/Delete/{portfolioId}");
        //    if (response == null || !response.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Failed to delete portfolio.");
        //    }

        //    var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

        //    return returnValue.Success;
        //}

        //public async Task<IEnumerable<PortfolioModel>> GetAllPortfoliosAsync(bool ActiveOnly = true)
        //{
        //    var returnVal = await httpClient.GetFromJsonAsync<IEnumerable<PortfolioModel>>($"api/Portfolio/GetAll/{ActiveOnly}");
        //    return returnVal;
        //}

        //public async Task<PortfolioModel> GetByIdAsync(int portfolioId)
        //{
        //    return await httpClient.GetFromJsonAsync<PortfolioModel>($"api/Portfolio/GetById/{portfolioId}");
        //}

        public async Task SelectForCurrentUserAsync(int portfolioId)
        {
            await httpClient.GetFromJsonAsync<PortfolioModel>($"api/Portfolio/SelectForCurrentUser/{portfolioId}");
        }

        //public async Task<bool> Update(PortfolioModel portfolio)
        //{
        //    var response = await httpClient.PostAsJsonAsync<PortfolioModel>("api/Portfolio/Update", portfolio);
        //    if (response == null || !response.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Failed to create portfolio.");
        //    }

        //    var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

        //    return returnValue.CreatedId > 0;
        //}
    }
}
