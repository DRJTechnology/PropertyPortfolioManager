using Dapper;
using PropertyPortfolioManager.Models.Dto.Finance;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.Server.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnection dbConnection;

        public AccountRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<int> Create(int userId, int portfolioId, AccountDto newAccount)
        {
            if (newAccount == null)
            {
                throw new ArgumentNullException("newAccount");
            }

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@PortfolioId", portfolioId);
                parameters.Add("@AccountTypeId", newAccount.AccountTypeId);
                parameters.Add("@Name", newAccount.Name);
                parameters.Add("@Notes", newAccount.Notes);
                parameters.Add("@Active", newAccount.Active);
                parameters.Add("@CurrentUserId", userId);

                await this.dbConnection.ExecuteAsync("finance.Account_Create", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@Id");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<AccountDto>> GetAll(int portfolioId, bool activeOnly)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@ActiveOnly", activeOnly);

            var accounts = await this.dbConnection.QueryAsync<AccountDto>("finance.Account_GetAll", parameters, commandType: CommandType.StoredProcedure);

            return accounts.ToList(); ;
        }

        public async Task<AccountDto> GetById(int id, int portfolioId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@PortfolioId", portfolioId);

                var account = await this.dbConnection.QueryAsync<AccountDto>("finance.Account_GetById", parameters, commandType: CommandType.StoredProcedure);
                return account.SingleOrDefault()!;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(int userId, int portfolioId, AccountDto existingAccount)
        {
            if (existingAccount == null)
            {
                throw new ArgumentNullException("existingAccount");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", existingAccount.Id);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@AccountTypeId", existingAccount.AccountTypeId);
            parameters.Add("@Name", existingAccount.Name);
            parameters.Add("@Notes", existingAccount.Notes);
            parameters.Add("@Active", existingAccount.Active);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("finance.Account_Update", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

        public async Task<bool> Delete(int userId, int portfolioId, int accountId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", accountId);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("finance.Account_Delete", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

    }
}
