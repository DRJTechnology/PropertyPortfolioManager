using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.InternalObjects;
using System.Net;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Services
{
    public abstract class GenericDataService : IGenericDataService
    {
        protected HttpClient httpClient { get; }
        protected string ApiControllerName { get; set; }

        public GenericDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> Create<TEntity>(TEntity portfolio)
        {
            var response = await httpClient.PostAsJsonAsync<TEntity>($"api/{ApiControllerName}/Create", portfolio);
            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create {ApiControllerName}.");
            }

            var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

            return returnValue.CreatedId;
        }

        public async Task<bool> DeleteAsync(int portfolioId)
        {
            var response = await httpClient.DeleteAsync($"api/{ApiControllerName}/Delete/{portfolioId}");
            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete {ApiControllerName}.");
            }

            var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

            return returnValue.Success;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(bool ActiveOnly = true)
        {
            try
            {
                var returnVal = await httpClient.GetFromJsonAsync<IEnumerable<TEntity>>($"api/{ApiControllerName}/GetAll/{ActiveOnly}");
                return returnVal;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(int portfolioId)
        {
            return await httpClient.GetFromJsonAsync<TEntity>($"api/{ApiControllerName}/GetById/{portfolioId}");
        }

        public async Task<bool> Update<TEntity>(TEntity portfolio)
        {
            var response = await httpClient.PostAsJsonAsync<TEntity>($"api/{ApiControllerName}/Update", portfolio);
            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create {ApiControllerName}.");
            }

            var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

            return returnValue.CreatedId > 0;
        }
    }
}
