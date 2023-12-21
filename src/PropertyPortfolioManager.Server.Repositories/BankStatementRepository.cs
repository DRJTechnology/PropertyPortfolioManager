using Dapper;
using Microsoft.Graph.Models;
using PropertyPortfolioManager.Models.Model.Finance;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using System.ComponentModel;
using System.Data;

namespace PropertyPortfolioManager.Server.Repositories
{
    public class BankStatementRepository : IBankStatementRepository
    {
        private readonly IDbConnection dbConnection;

        public BankStatementRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task AddBankStatementRecords(int currentUserId, int accountId, DataTable recordList)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AccountId", accountId);
            parameters.Add("@Statement", recordList.AsTableValuedParameter("[finance].[StatementTableType]"));
            parameters.Add("@CurrentUserId", currentUserId);

            await this.dbConnection.ExecuteAsync("finance.BankStatement_Upload", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
