using Dapper;
using PropertyPortfolioManager.Models.Dto.Finance;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
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

        public async Task<UploadResultDto> AddBankStatementRecords(int currentUserId, int portfolioId, int bankAccountId, DataTable recordList)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BankAccountId", bankAccountId);
            parameters.Add("@Statement", recordList.AsTableValuedParameter("[finance].[StatementTableType]"));
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@CurrentUserId", currentUserId);

            var result = await this.dbConnection.QueryAsync<UploadResultDto>("finance.BankStatement_Upload", parameters, commandType: CommandType.StoredProcedure);
            return result.SingleOrDefault()!;
        }
    }
}
