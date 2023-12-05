using Dapper;
using PropertyPortfolioManager.Models.Dto.Finance;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.Server.Repositories
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly IDbConnection dbConnection;

        public AccountTypeRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<List<AccountTypeDto>> GetAll()
        {
            var accounts = await this.dbConnection.QueryAsync<AccountTypeDto>("finance.AccountType_GetAll", commandType: CommandType.StoredProcedure);

            return accounts.ToList(); ;
        }
    }
}
