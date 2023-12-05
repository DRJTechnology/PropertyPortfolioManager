using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.InternalObjects;
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

        public async Task<int> Create<TEntity>(TEntity entity)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<TEntity>($"api/{ApiControllerName}/Create", entity);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to create {ApiControllerName}.");
                }

                var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

                return returnValue.CreatedId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int entityId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/{ApiControllerName}/Delete/{entityId}");
                if (response == null || !response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to delete {ApiControllerName}.");
                }

                var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

                return returnValue.Success;
            }
            catch (Exception ex)
            {
                throw;
            }
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

        public async Task<TEntity> GetByIdAsync<TEntity>(int entityId)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<TEntity>($"api/{ApiControllerName}/GetById/{entityId}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update<TEntity>(TEntity entity)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<TEntity>($"api/{ApiControllerName}/Update", entity);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to create {ApiControllerName}.");
                }

                var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

                return returnValue.CreatedId > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
