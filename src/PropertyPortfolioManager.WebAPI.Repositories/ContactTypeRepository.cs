using Dapper;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.WebAPI.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.WebAPI.Repositories
{
    public class ContactTypeRepository : IContactTypeRepository
    {
        private readonly IDbConnection dbConnection;

        public ContactTypeRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<int> Create(int userId, int portfolioId, ContactTypeDto newContactType)
        {
            if (newContactType == null)
            {
                throw new ArgumentNullException("newContactType");
            }

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@PortfolioId", portfolioId);
                parameters.Add("@Type", newContactType.Type);
                parameters.Add("@CurrentUserId", userId);

                await this.dbConnection.ExecuteAsync("general.ContactType_Create", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@Id");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ContactTypeDto>> GetAll(int portfolioId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PortfolioId", portfolioId);

            var contactTypes = await this.dbConnection.QueryAsync<ContactTypeDto>("general.ContactType_GetAll", parameters, commandType: CommandType.StoredProcedure);

            return contactTypes.ToList(); ;
        }

        public async Task<ContactTypeDto> GetById(int id, int portfolioId)
        {
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", id);
                    parameters.Add("@PortfolioId", portfolioId);

                    var contactType = await this.dbConnection.QueryFirstOrDefaultAsync<ContactTypeDto>("general.ContactType_GetById", parameters, commandType: CommandType.StoredProcedure);

                    if (contactType != null)
                    {
                        return contactType!;
                    }
                    else
                    {
                        throw new Exception($"Error: ContactType (Id {id}) not found!");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<bool> Update(int userId, int portfolioId, ContactTypeDto existingContactType)
        {
            if (existingContactType == null)
            {
                throw new ArgumentNullException("existingContactType");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", existingContactType.Id);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@Type", existingContactType.Type);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("general.ContactType_Update", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
