using Dapper;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Property;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.WebAPI.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly IDbConnection dbConnection;

        public UnitRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<int> Create(int userId, UnitDto newUnit)
        {
            if (newUnit == null)
            {
                throw new ArgumentNullException("newUnit");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Code", newUnit.Code);
            parameters.Add("@StreetAddress", newUnit.Address.StreetAddress);
            parameters.Add("@TownCity", newUnit.Address.TownCity);
            parameters.Add("@CountyRegion", newUnit.Address.CountyRegion);
            parameters.Add("@PostCode", newUnit.Address.PostCode);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("profile.Unit_Create", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@Id");
        }

        public Task<List<UnitDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UnitDto> GetById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            using (var multipleResults = await this.dbConnection.QueryMultipleAsync("property.Unit_GetById", parameters, commandType: CommandType.StoredProcedure))
            {
                var unit = multipleResults.Read<UnitDto>().SingleOrDefault();

                if (unit != null)
                {
                    unit.Address = multipleResults.Read<AddressDto>().SingleOrDefault();
                }

                return unit;
            }
        }

        public async Task<bool> Update(int userId, UnitDto existingUnit)
        {
            if (existingUnit == null)
            {
                throw new ArgumentNullException("existingUnit");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Code", existingUnit.Code);
            parameters.Add("@StreetAddress", existingUnit.Address.StreetAddress);
            parameters.Add("@TownCity", existingUnit.Address.TownCity);
            parameters.Add("@CountyRegion", existingUnit.Address.CountyRegion);
            parameters.Add("@PostCode", existingUnit.Address.PostCode);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("profile.Unit_Update", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
