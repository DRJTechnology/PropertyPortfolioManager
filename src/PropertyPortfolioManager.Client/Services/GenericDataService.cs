using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.InternalObjects;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace PropertyPortfolioManager.Client.Services
{
    public class GenericDataService<TEntity> : IGenericDataService<TEntity> where TEntity : class
    {
        protected HttpClient httpClient { get; }

        public GenericDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> Create(TEntity portfolio)
        {
            var response = await httpClient.PostAsJsonAsync<TEntity>($"api/{GetEntityName()}/Create", portfolio);
            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create {GetEntityName()}.");
            }

            var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

            return returnValue.CreatedId;
        }

        public async Task<bool> DeleteAsync(int portfolioId)
        {
            var response = await httpClient.DeleteAsync($"api/{GetEntityName()}/Delete/{portfolioId}");
            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete {GetEntityName()}.");
            }

            var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

            return returnValue.Success;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool ActiveOnly = true)
        {
            var returnVal = await httpClient.GetFromJsonAsync<IEnumerable<TEntity>>($"api/{GetEntityName()}/GetAll/{ActiveOnly}");
            return returnVal;
        }

        public async Task<TEntity> GetByIdAsync(int portfolioId)
        {
            return await httpClient.GetFromJsonAsync<TEntity>($"api/{GetEntityName()}/GetById/{portfolioId}");
        }

        public async Task<bool> Update(TEntity portfolio)
        {
            var response = await httpClient.PostAsJsonAsync<TEntity>($"api/{GetEntityName()}/Update", portfolio);
            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create {GetEntityName()}.");
            }

            var returnValue = await response.Content.ReadFromJsonAsync<PpmApiResponse>();

            return returnValue.CreatedId > 0;
        }

        private string GetEntityName()
        {
            var fullName = typeof(TEntity).FullName;
            var name = fullName.Substring(fullName.LastIndexOf('.') + 1);
            string[] split = Regex.Split(name, @"(?<!^)(?=[A-Z])");
            var entityName = split[0];
            return entityName;
        }
    }
}
