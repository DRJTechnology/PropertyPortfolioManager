using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.General;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Services
{
    public class TransactionTypeDataService : ITransactionTypeDataService
    {
        protected HttpClient httpClient { get; }

        public TransactionTypeDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<EntityTypeBasicModel>> GetAllAsync()
        {
            try
            {
                var returnVal = await httpClient.GetFromJsonAsync<IEnumerable<EntityTypeBasicModel>>($"api/TransactionType/GetAll");
                return returnVal;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
