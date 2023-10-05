using Dapper;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Dto.Property;
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

        public async Task<int> Create(int userId, int portfolioId, UnitDto newUnit)
        {
            if (newUnit == null)
            {
                throw new ArgumentNullException("newUnit");
            }

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@PortfolioId", portfolioId);
                parameters.Add("@Code", newUnit.Code);
                parameters.Add("@UnitTypeId", newUnit.UnitTypeId);
                parameters.Add("@StreetAddress", newUnit.Address.StreetAddress);
                parameters.Add("@TownCity", newUnit.Address.TownCity);
                parameters.Add("@CountyRegion", newUnit.Address.CountyRegion);
                parameters.Add("@PostCode", newUnit.Address.PostCode);
                parameters.Add("@PurchasePrice", newUnit.PurchasePrice);
                parameters.Add("@PurchaseDate", newUnit.PurchaseDate);
                parameters.Add("@SalePrice", newUnit.SalePrice);
                parameters.Add("@SaleDate", newUnit.SaleDate);
                parameters.Add("@Active", newUnit.Active);
                parameters.Add("@CurrentUserId", userId);

                await this.dbConnection.ExecuteAsync("property.Unit_Create", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@Id");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UnitBasicDto>> GetAll(int portfolioId, bool activeOnly)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@ActiveOnly", activeOnly);

            var units = await this.dbConnection.QueryAsync<UnitBasicDto>("property.Unit_GetAll", parameters, commandType: CommandType.StoredProcedure);

            return units.ToList(); ;
        }

        public async Task<UnitDto> GetById(int id, int portfolioId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@PortfolioId", portfolioId);

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
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(int userId, int portfolioId, UnitDto existingUnit)
        {
            if (existingUnit == null)
            {
                throw new ArgumentNullException("existingUnit");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", existingUnit.Id);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@Code", existingUnit.Code);
            parameters.Add("@UnitTypeId", existingUnit.UnitTypeId);
            parameters.Add("@StreetAddress", existingUnit.Address.StreetAddress);
            parameters.Add("@TownCity", existingUnit.Address.TownCity);
            parameters.Add("@CountyRegion", existingUnit.Address.CountyRegion);
            parameters.Add("@PostCode", existingUnit.Address.PostCode);
            parameters.Add("@PurchasePrice", existingUnit.PurchasePrice);
            parameters.Add("@PurchaseDate", existingUnit.PurchaseDate);
            parameters.Add("@SalePrice", existingUnit.SalePrice);
            parameters.Add("@SaleDate", existingUnit.SaleDate);
            parameters.Add("@Active", existingUnit.Active);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.Unit_Update", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
