using PropertyPortfolioManager.Client.Interfaces;
using PropertyPortfolioManager.Models.Model.Finance;
using System.Net.Http.Json;

namespace PropertyPortfolioManager.Client.Services
{
    public class TransactionDetailDataService : ITransactionDetailDataService
    {
        protected HttpClient httpClient { get; }

        public TransactionDetailDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<TransactionDetailResponseModel>> GetAsync(DateTime? fromDate, DateTime? toDate, int accountId, int transactionTypeId)
        {
            try
            {
                if (fromDate == null) { fromDate = DateTime.MinValue; }
                if (toDate == null) { toDate = DateTime.MaxValue; }           
                var url = $"api/TransactionDetail/GetList/{fromDate.Value.ToString("yyyyMMdd")}/{toDate.Value.ToString("yyyyMMdd")}/{accountId}/{transactionTypeId}";

                var returnVal = await httpClient.GetFromJsonAsync<IEnumerable<TransactionDetailResponseModel>>(url);
                return returnVal;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
