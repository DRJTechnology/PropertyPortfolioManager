using Dapper;
using PropertyPortfolioManager.Models.Dto.General;
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

        public async Task<List<TenancyDto>> GetAll(int portfolioId, bool activeOnly)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@ActiveOnly", activeOnly);

            var tenancys = await this.dbConnection.QueryAsync<TenancyDto>("property.Tenancy_GetAll", parameters, commandType: CommandType.StoredProcedure);

            return tenancys.ToList();
        }

        public async Task<TenancyDto> GetById(int id, int portfolioId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@PortfolioId", portfolioId);

            using (var multipleResults = await this.dbConnection.QueryMultipleAsync("property.Tenancy_GetById", parameters, commandType: CommandType.StoredProcedure))
            {
                var tenancy = multipleResults.Read<TenancyDto>().SingleOrDefault();

                if (tenancy != null)
                {
                    tenancy.Contacts = multipleResults.Read<ContactBasicDto>().ToList();
                    return tenancy;
                }
                else
                {
                    throw new Exception($"Error: Tenancy (Id {id}) not found!");
                }
            }
        }

        public async Task<bool> Update(int userId, int portfolioId, TenancyDto existingTenancy)
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

        public async Task<bool> Delete(int userId, int portfolioId, int tenancyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", tenancyId);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.Tenancy_Delete", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

        public async Task<bool> RemoveContact(int userId, int portfolioId, TenancyContactDto tenancyContact)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TenancyId", tenancyContact.TenancyId);
            parameters.Add("@ContactId", tenancyContact.ContactId);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.Tenancy_RemoveContact", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

        public async Task<int> AddContact(int userId, int portfolioId, TenancyContactDto newTenancyContact)
        {
            if (newTenancyContact == null)
            {
                throw new ArgumentNullException("newTenancyContact");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TenancyId", newTenancyContact.TenancyId);
            parameters.Add("@ContactId", newTenancyContact.ContactId);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("property.Tenancy_AddContact", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@Id");
        }
    }
}
