using Dapper;
using PropertyPortfolioManager.Models.Dto.Finance;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.Server.Repositories
{
    public class TransactionDetailRepository : ITransactionDetailRepository
    {
        private readonly IDbConnection dbConnection;

        public TransactionDetailRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<List<TransactionDetailDto>> Get(int portfolioId, DateTime? fromDate, DateTime? toDate, int accountId, int transactionTypeId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PortfolioId", portfolioId);
                if (fromDate != DateTime.MinValue)
                {
                    parameters.Add("@FromDate", fromDate);
                }
                if (toDate < DateTime.MaxValue.AddDays(-1))
                {
                    parameters.Add("@ToDate", toDate);
                }
                parameters.Add("@AccountId", accountId);
                parameters.Add("@TransactionTypeId", transactionTypeId);

                var transactionDetails = await this.dbConnection.QueryAsync<TransactionDetailDto>("finance.TransactionDetail_Get", parameters, commandType: CommandType.StoredProcedure);

                return transactionDetails.ToList(); ;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
