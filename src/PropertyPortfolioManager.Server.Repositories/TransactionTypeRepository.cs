using Dapper;
using PropertyPortfolioManager.Models.Dto.Finance;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.Server.Repositories
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly IDbConnection dbConnection;

        public TransactionTypeRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<List<TransactionTypeDto>> GetAll()
        {
            var transactionTypes = await this.dbConnection.QueryAsync<TransactionTypeDto>("finance.TransactionType_GetAll", commandType: CommandType.StoredProcedure);

            return transactionTypes.ToList(); ;
        }
    }
}
