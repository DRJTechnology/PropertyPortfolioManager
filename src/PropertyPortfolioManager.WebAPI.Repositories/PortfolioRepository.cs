using Dapper;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.WebAPI.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly IDbConnection dbConnection;

        public PortfolioRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<int> Create(int userId, PortfolioDto newPortfolio)
        {
            if (newPortfolio == null)
            {
                throw new ArgumentNullException("newPortfolio");
            }

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Name", newPortfolio.Name);
                parameters.Add("@Active", newPortfolio.Active);
                parameters.Add("@CurrentUserId", userId);

                await this.dbConnection.ExecuteAsync("property.Portfolio_Create", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@Id");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<PortfolioDto>> GetAll(int userId, bool activeOnly)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@ActiveOnly", activeOnly);

            var portfolios = await this.dbConnection.QueryAsync<PortfolioDto>("property.Portfolio_GetAll", parameters, commandType: CommandType.StoredProcedure);

            return portfolios.ToList(); ;
        }

        public async Task<PortfolioDto> GetById(int id, int userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@UserId", userId);

                var portfolio = await this.dbConnection.QueryFirstOrDefaultAsync<PortfolioDto>("property.Portfolio_GetById", parameters, commandType: CommandType.StoredProcedure);

                if (portfolio != null)
                {
                    return portfolio!;
                }
                else
                {
                    throw new Exception($"Error: Portfolio (Id {id}) not found!");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PortfolioDto> GetByUserObjectIdentifier(Guid userObjectIdentifier)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ObjectIdentifier", userObjectIdentifier);

                var portfolio = await this.dbConnection.QueryFirstOrDefaultAsync<PortfolioDto>("property.Portfolio_GetByUserObjectIdentifier", parameters, commandType: CommandType.StoredProcedure);

                return portfolio;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> SelectForUser(int portfolioId, int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@UserId", userId);

            await this.dbConnection.ExecuteAsync("property.Portfolio_SelectForUser", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

        public async Task<bool> Update(int userId, PortfolioDto existingPortfolio)
        {
            if (existingPortfolio == null)
            {
                throw new ArgumentNullException("existingPortfolio");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", existingPortfolio.Id);
            parameters.Add("@Name", existingPortfolio.Name);
            parameters.Add("@Active", existingPortfolio.Active);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.Portfolio_Update", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

        public async Task<bool> Delete(int userId, int portfolioId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", portfolioId);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.Portfolio_Delete", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
