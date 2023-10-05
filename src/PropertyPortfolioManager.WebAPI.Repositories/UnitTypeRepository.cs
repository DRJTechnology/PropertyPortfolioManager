using Dapper;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.WebAPI.Repositories
{
    public class UnitTypeRepository : IUnitTypeRepository
    {
        private readonly IDbConnection dbConnection;

        public UnitTypeRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<int> Create(int userId, int portfolioId, UnitTypeDto newUnitType)
        {
            if (newUnitType == null)
            {
                throw new ArgumentNullException("newUnitType");
            }

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@PortfolioId", portfolioId);
                parameters.Add("@Type", newUnitType.Type);
                parameters.Add("@Active", newUnitType.Active);
                parameters.Add("@CurrentUserId", userId);

                await this.dbConnection.ExecuteAsync("property.UnitType_Create", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@Id");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UnitTypeDto>> GetAll(int portfolioId, bool activeOnly)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@ActiveOnly", activeOnly);

            var unitTypes = await this.dbConnection.QueryAsync<UnitTypeDto>("property.UnitType_GetAll", parameters, commandType: CommandType.StoredProcedure);

            return unitTypes.ToList(); ;
        }

        public async Task<UnitTypeDto> GetById(int id, int portfolioId)
        {
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    parameters.Add("@PortfolioId", portfolioId);

                    var unitType = await this.dbConnection.QueryFirstOrDefaultAsync<UnitTypeDto>("property.UnitType_GetById", parameters, commandType: CommandType.StoredProcedure);

                    if (unitType != null)
                    {
                        return unitType!;
                    }
                    else
                    {
                        throw new Exception($"Error: UnitType (Id {id}) not found!");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<bool> Update(int userId, int portfolioId, UnitTypeDto existingUnitType)
        {
            if (existingUnitType == null)
            {
                throw new ArgumentNullException("existingUnitType");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", existingUnitType.Id);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@Type", existingUnitType.Type);
            parameters.Add("@Active", existingUnitType.Active);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.UnitType_Update", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
