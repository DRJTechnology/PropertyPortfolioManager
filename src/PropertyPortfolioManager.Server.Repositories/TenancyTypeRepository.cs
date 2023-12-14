using Dapper;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.Server.Repositories
{
    public class TenancyTypeRepository : ITenancyTypeRepository
    {
        private readonly IDbConnection dbConnection;

        public TenancyTypeRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<int> Create(int userId, int portfolioId, EntityTypeDto newTenancyType)
        {
            if (newTenancyType == null)
            {
                throw new ArgumentNullException("newTenancyType");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@Type", newTenancyType.Type);
            parameters.Add("@Active", newTenancyType.Active);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.TenancyType_Create", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@Id");
        }

        public async Task<List<EntityTypeDto>> GetAll(int portfolioId, bool activeOnly)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@ActiveOnly", activeOnly);

            var tenancyTypes = await this.dbConnection.QueryAsync<EntityTypeDto>("property.TenancyType_GetAll", parameters, commandType: CommandType.StoredProcedure);

            return tenancyTypes.ToList(); ;
        }

        public async Task<EntityTypeDto> GetById(int id, int portfolioId)
        {
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@PortfolioId", portfolioId);

                var tenancyType = await this.dbConnection.QueryFirstOrDefaultAsync<EntityTypeDto>("property.TenancyType_GetById", parameters, commandType: CommandType.StoredProcedure);

                if (tenancyType != null)
                {
                    return tenancyType!;
                }
                else
                {
                    throw new Exception($"Error: TenancyType (Id {id}) not found!");
                }
            }
        }

        public async Task<bool> Update(int userId, int portfolioId, EntityTypeDto existingTenancyType)
        {
            if (existingTenancyType == null)
            {
                throw new ArgumentNullException("existingTenancyType");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", existingTenancyType.Id);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@Type", existingTenancyType.Type);
            parameters.Add("@Active", existingTenancyType.Active);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.TenancyType_Update", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

        public async Task<bool> Delete(int userId, int portfolioId, int tenancyTypeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", tenancyTypeId);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.TenancyType_Delete", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

    }
}
