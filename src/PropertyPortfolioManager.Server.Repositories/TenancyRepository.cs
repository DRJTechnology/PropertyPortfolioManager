using Dapper;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.Server.Repositories
{
    public class TenancyRepository : ITenancyRepository
    {
        private readonly IDbConnection dbConnection;

        public TenancyRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<int> Create(int userId, int portfolioId, TenancyDto newTenancy)
        {
            if (newTenancy == null)
            {
                throw new ArgumentNullException("newTenancy");
            }

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@TenancyTypeId", newTenancy.TenancyTypeId);
                parameters.Add("@UnitId", newTenancy.UnitId);
                parameters.Add("@StartDate", newTenancy.StartDate);
                parameters.Add("@EndDate", newTenancy.EndDate);
                parameters.Add("@DurationQuantity", newTenancy.DurationQuantity);
                parameters.Add("@DurationUnitId", newTenancy.DurationUnitId);
                parameters.Add("@ExpireAfterEndDate", newTenancy.ExpireAfterEndDate);
                parameters.Add("@Active", newTenancy.Active);
                parameters.Add("@CurrentUserId", userId);

                await this.dbConnection.ExecuteAsync("property.Tenancy_Create", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@Id");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<TenancyDto>> GetAll(int portfolioId, bool activeOnly)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@ActiveOnly", activeOnly);

            var tenancys = await this.dbConnection.QueryAsync<TenancyDto>("property.Tenancy_GetAll", parameters, commandType: CommandType.StoredProcedure);

            return tenancys.ToList(); ;
        }

        public async Task<TenancyDto> GetById(int id, int portfolioId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@PortfolioId", portfolioId);

                var tenancy = await this.dbConnection.QueryFirstOrDefaultAsync<TenancyDto>("property.Tenancy_GetById", parameters, commandType: CommandType.StoredProcedure);

                if (tenancy != null)
                {
                    return tenancy!;
                }
                else
                {
                    throw new Exception($"Error: Tenancy (Id {id}) not found!");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(int userId, int portfolioId, TenancyDto existingTenancy)
        {
            try
            {
                if (existingTenancy == null)
                {
                    throw new ArgumentNullException("existingTenancy");
                }

                var parameters = new DynamicParameters();
                parameters.Add("@Id", existingTenancy.Id);
                parameters.Add("@PortfolioId", portfolioId);
                parameters.Add("@TenancyTypeId", existingTenancy.TenancyTypeId);
                parameters.Add("@UnitId", existingTenancy.UnitId);
                parameters.Add("@StartDate", existingTenancy.StartDate);
                parameters.Add("@EndDate", existingTenancy.EndDate);
                parameters.Add("@DurationQuantity", existingTenancy.DurationQuantity);
                parameters.Add("@DurationUnitId", existingTenancy.DurationUnitId);
                parameters.Add("@ExpireAfterEndDate", existingTenancy.ExpireAfterEndDate);
                parameters.Add("@Active", existingTenancy.Active);
                parameters.Add("@CurrentUserId", userId);

                await this.dbConnection.ExecuteAsync("property.Tenancy_Update", parameters, commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int userId, int portfolioId, int tenancyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", tenancyId);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.Tenancy_Delete", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

    }
}
