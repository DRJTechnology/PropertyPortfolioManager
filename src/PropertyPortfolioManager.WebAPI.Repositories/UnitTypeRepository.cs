using Dapper;
using Microsoft.Identity.Client;
using PropertyPortfolioManager.Models.Dto.General;
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

        public async Task<int> Create(int userId, UnitTypeDto newUnitType)
        {
            if (newUnitType == null)
            {
                throw new ArgumentNullException("newUnitType");
            }

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Type", newUnitType.Type);
                parameters.Add("@CurrentUserId", userId);

                await this.dbConnection.ExecuteAsync("property.UnitType_Create", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@Id");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UnitTypeDto>> GetAll()
        {
            var unitTypes = await this.dbConnection.QueryAsync<UnitTypeDto>("property.UnitType_GetAll", commandType: CommandType.StoredProcedure);

            return unitTypes.ToList(); ;
        }

        public async Task<UnitTypeDto> GetById(int id)
        {
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", id);

                    var unitType = await this.dbConnection.QuerySingleAsync<UnitTypeDto>("property.UnitType_GetById", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<bool> Update(int userId, UnitTypeDto existingUnitType)
        {
            if (existingUnitType == null)
            {
                throw new ArgumentNullException("existingUnit");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", existingUnitType.Id);
            parameters.Add("@Type", existingUnitType.Type);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.UnitType_Update", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
