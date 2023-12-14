using Dapper;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using System.Data;

namespace PropertyPortfolioManager.Server.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDbConnection dbConnection;

        public ContactRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<int> Create(int userId, int portfolioId, ContactDto newContact)
        {
            if (newContact == null)
            {
                throw new ArgumentNullException("newContact");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@Name", newContact.Name);
            parameters.Add("@StreetAddress", newContact.Address.StreetAddress);
            parameters.Add("@TownCity", newContact.Address.TownCity);
            parameters.Add("@CountyRegion", newContact.Address.CountyRegion);
            parameters.Add("@PostCode", newContact.Address.PostCode);
            parameters.Add("@Notes", newContact.Notes);
            parameters.Add("@Active", newContact.Active);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("general.Contact_Create", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@Id");
        }

        public async Task<List<ContactBasicDto>> GetAll(int portfolioId, bool activeOnly)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@ActiveOnly", activeOnly);

            var contacts = await this.dbConnection.QueryAsync<ContactBasicDto>("general.Contact_GetAll", parameters, commandType: CommandType.StoredProcedure);

            return contacts.ToList(); ;
        }

        public async Task<ContactDto> GetById(int id, int portfolioId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@PortfolioId", portfolioId);

            using (var multipleResults = await this.dbConnection.QueryMultipleAsync("general.Contact_GetById", parameters, commandType: CommandType.StoredProcedure))
            {
                var contact = multipleResults.Read<ContactDto>().SingleOrDefault();

                if (contact != null)
                {
                    contact.Address = multipleResults.Read<AddressDto>().SingleOrDefault();
                }

                return contact;
            }
        }

        public async Task<bool> Update(int userId, int portfolioId, ContactDto existingContact)
        {
            if (existingContact == null)
            {
                throw new ArgumentNullException("existingContact");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", existingContact.Id);
            parameters.Add("@PortfolioId", portfolioId);
            parameters.Add("@Name", existingContact.Name);
            parameters.Add("@StreetAddress", existingContact.Address.StreetAddress);
            parameters.Add("@TownCity", existingContact.Address.TownCity);
            parameters.Add("@CountyRegion", existingContact.Address.CountyRegion);
            parameters.Add("@PostCode", existingContact.Address.PostCode);
            parameters.Add("@Notes", existingContact.Notes);
            parameters.Add("@Active", existingContact.Active);
            parameters.Add("@CurrentUserId", userId);

            await this.dbConnection.ExecuteAsync("general.Contact_Update", parameters, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
